using System;
using System.Collections.Generic;
using System.Threading.Tasks;
using System.Linq.Expressions;

namespace DALSinger
{
    public interface IRepository
    {
        void Create<T>(T entity) where T : class;
        void CreateRange<T>(T[] entities) where T : class;
        void Delete<T>(T entity) where T : class;
        void DeleteRange<T>(T[] entities) where T : class;
        void Update<T>(T entity) where T : class;
        void UpdateRange<T>(T[] entities) where T : class;

        Task<IEnumerable<T>> GetAllAsync<T>() where T : class;
        Task<T> Get<T>(int id) where T : class;
        Task<IEnumerable<T>> Get<T>(Expression<Func<T, bool>> predicate) where T : class;
        int GetCount<T>() where T : class;
        int GetCount<T>(Expression<Func<T, bool>> predicate) where T : class;
        Task<IEnumerable<T>> GetPartOrderBy<T>(int take, int skip, bool isDesc, string propName) where T : class;
    }
}
