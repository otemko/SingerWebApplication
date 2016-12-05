using ModelSinger;
using System.Collections.Generic;
using System.Threading.Tasks;

namespace BLLSinger.Interfaces
{
    public interface ISongService : IService<Song>
    {
        int GetCountBySungerId(int id);
        Task<IEnumerable<Song>> GetBySingerId(int id);
        Task<IEnumerable<Song>> GetBySingerIdPartOrderBy(int id, int take, int skip, bool isDesc, string propertyName);
    }
}
