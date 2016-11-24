using System;
using System.Collections.Generic;
using System.Linq;
using System.Linq.Expressions;
using System.Threading.Tasks;
using System.Data.Entity;

using System.Data.Entity.Migrations;


namespace DALSinger
{
    public class Repository : IRepository
    {
        private DbContext dbcontext;

        public Repository(DbContext dbcontext)
        {
            this.dbcontext = dbcontext;
        }

        public async Task CreateAsync<T>(T entity) where T : class
        {
            if (entity == null) throw new ArgumentNullException(nameof(entity));

            dbcontext.Set<T>().Add(entity);
            await dbcontext.SaveChangesAsync();
        }

        public async Task DeleteAsync<T>(T entity) where T : class
        {
            if (entity == null) throw new ArgumentNullException(nameof(entity));

            dbcontext.Set<T>().Remove(entity);
            await dbcontext.SaveChangesAsync();
        }

        public async Task UpdateAsync<T>(T entity) where T : class
        {
            if (entity == null) throw new ArgumentNullException(nameof(entity));

            dbcontext.Set<T>().AddOrUpdate(entity);
            await dbcontext.SaveChangesAsync();
        }

        public async Task<IEnumerable<T>> Get<T>(Expression<Func<T, bool>> predicate) where T : class
        {
            if (predicate == null) throw new ArgumentNullException(nameof(predicate));

            return await dbcontext.Set<T>().Where(predicate).ToListAsync();
        }

        public async Task<T> Get<T>(int id) where T : class
        {
            if (id <= 0) throw new ArgumentOutOfRangeException(nameof(id));

            var result = dbcontext.Set<T>().Find(id);

            return result == null ? null : result;
        }

        public async Task<IEnumerable<T>> GetAllAsync<T>() where T : class
        {
            return await dbcontext.Set<T>().ToListAsync();
        }        
    }
}
