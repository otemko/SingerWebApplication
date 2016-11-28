using System.Collections.Generic;
using System.Threading.Tasks;

namespace BLLSinger.Interfaces
{
    public interface IService<T>
    {
        void Create(T entity);
        void CreateRange(T[] entities);
        void Update(T entity);
        void UpdateRange(T[] entities);
        void Delete(T entity);
        void DeleteRange(T[] entities);

        Task<IEnumerable<T>> GetAllAsync();
        Task<T> Get(int id);
    }
}
