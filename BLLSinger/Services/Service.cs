using System.Collections.Generic;
using System.Threading.Tasks;
using ModelSinger;
using DALSinger;

namespace BLLSinger.Services
{
    public abstract class Service<T> where T: class, IEntity
    {
        IRepository repository;

        public Service(IRepository repository)
        {
            this.repository = repository;
        }

        public virtual async Task CreateAsync(T entity)
        {
            await repository.CreateAsync(entity);
        }
        
        public virtual async Task UpdateAsync(T entity)
        {
            await repository.UpdateAsync<T>(entity);
        }
        
        public virtual async Task DeleteAsync(T entity)
        {
            await repository.DeleteAsync<T>(entity);
        }
        
        public virtual Task<IEnumerable<T>> GetAllAsync()
        {
            var kk = repository.GetAllAsync<T>();
            return kk;
        }

        public async Task<T> Get(int id)
        {
            var result = await repository.Get<T>(id);
            return result;
        }
    }
}
