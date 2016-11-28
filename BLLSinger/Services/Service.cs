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

        public virtual void Create(T entity)
        {
            repository.Create(entity);
        }

        public virtual void CreateRange(T[] entities)
        {
            repository.CreateRange(entities);
        }

        public virtual void Update(T entity)
        {
            repository.Update<T>(entity);
        }

        public virtual void UpdateRange(T[] entities)
        {
            repository.UpdateRange<T>(entities);
        }

        public virtual void Delete(T entity)
        {
            repository.Delete<T>(entity);
        }

        public virtual void DeleteRange(T[] entities)
        {
            repository.DeleteRange<T>(entities);
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
