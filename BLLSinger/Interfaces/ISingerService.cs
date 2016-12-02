using System.Collections.Generic;
using System.Threading.Tasks;
using ModelSinger;

namespace BLLSinger.Interfaces
{
    public interface ISingerService : IService<Singer>
    {
        int GetCount ();
        Task<IEnumerable<Singer>> GetPartOrderBy(int take, int skip, bool isDesc, string property);
    }
}
