using System.Collections.Generic;
using System.Threading.Tasks;
using ModelSinger;

namespace BLLSinger.Interfaces
{
    public interface IAccordService: IService<Accord>
    {
        Task<IEnumerable<Accord>> GetAccordsByAccordNames(string[] accordNames);
    }
}
