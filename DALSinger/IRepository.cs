using System;
using System.Collections.Generic;
using System.Threading.Tasks;
using System.Linq.Expressions;

namespace DALSinger
{
    public interface IRepository
    {
        Task CreateAsync<T>(T entity) where T : class;
        Task DeleteAsync<T>(T entity) where T : class;
        Task UpdateAsync<T>(T entity) where T : class;

        Task<IEnumerable<T>> GetAllAsync<T>() where T : class;
        Task<T> Get<T>(int id) where T : class;
        Task<IEnumerable<T>> Get<T>(Expression<Func<T, bool>> predicate) where T : class;
    }
}
