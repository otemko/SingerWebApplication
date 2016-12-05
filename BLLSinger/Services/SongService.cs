using BLLSinger.Interfaces;
using ModelSinger;
using System.Collections.Generic;
using System.Threading.Tasks;
using DALSinger;

namespace BLLSinger.Services
{
    public class SongService : Service<Song>, ISongService
    {
        IRepository repository;

        public SongService(IRepository repository) : base(repository)
        {
            this.repository = repository;
        }

        public int GetCountBySungerId(int id)
        {
            return repository.GetCount<Song>(s => s.SingerId == id);
        }

        public async Task<IEnumerable<Song>> GetBySingerId(int id)
        {
            var result = await repository.Get<Song>(s => s.SingerId == id);
            return result == null ? null : result; 
        }

        public async Task<IEnumerable<Song>> GetBySingerIdPartOrderBy(int id, int take, int skip, bool isDesc, string propertyName)
        {
            return await repository.GetPartOrderByWhere<Song>(take, skip, isDesc, propertyName, s => s.SingerId == id);
        }
    }
}
