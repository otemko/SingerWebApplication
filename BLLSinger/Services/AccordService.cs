using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;
using BLLSinger.Interfaces;
using DALSinger;
using ModelSinger;

namespace BLLSinger.Services
{
    public class AccordService : Service<Accord>, IAccordService
    {
        IRepository repository;

        public AccordService(IRepository repository) : base(repository)
        {
            this.repository = repository;
        }

        public async Task<IEnumerable<Accord>> GetAccordsByAccordNames(string[] accordNames)
        {
            return await repository.Get<Accord>(a => accordNames.Contains(a.Name));
        }
    }
}
