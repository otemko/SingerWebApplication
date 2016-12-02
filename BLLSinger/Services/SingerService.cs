using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;
using BLLSinger.Interfaces;
using ModelSinger;
using DALSinger;

namespace BLLSinger.Services
{
    public class SingerService : Service<Singer>, ISingerService
    {
        IRepository repository;

        public SingerService(IRepository repository) : base(repository)
        {
            this.repository = repository;
        }

        public async Task<IEnumerable<Singer>> GetPartOrderBy(int take, int skip, bool isDesc, string propertyName)
        {
            return await repository.GetPartOrderBy<Singer>(take, skip, isDesc, propertyName);
        }

        public int GetCount()
        {
           return repository.GetCount<Singer>();
        }
    }
}
