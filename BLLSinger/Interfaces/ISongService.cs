using ModelSinger;
using System.Collections.Generic;
using System.Threading.Tasks;

namespace BLLSinger.Interfaces
{
    public interface ISongService: IService<Song>
    {
        Task<IEnumerable<Song>> GetBySingerId(int id);
    }
}
