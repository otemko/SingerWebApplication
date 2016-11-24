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

        public async Task<IEnumerable<Song>> GetBySingerId(int id)
        {
            var result = await repository.Get<Song>(s => s.SingerId == id);
            return result == null ? null : result; 
        }
    }
}
