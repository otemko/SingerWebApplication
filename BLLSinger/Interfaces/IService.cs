using System.Collections.Generic;
using System.Threading.Tasks;

namespace BLLSinger.Interfaces
{
    public interface IService<T>
    { 
        Task CreateAsync(T entity);
        Task UpdateAsync(T entity);
        Task DeleteAsync(T entity);
        Task<IEnumerable<T>> GetAllAsync();
        Task<T> Get(int id);
    }
}
