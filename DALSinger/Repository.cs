using System;
using System.Collections.Generic;
using System.Linq;
using System.Linq.Expressions;
using System.Threading.Tasks;
using System.Data.Entity;
using System.Data.Entity.Migrations;
using ORMSinger;


namespace DALSinger
{
    public class Repository : IRepository
    {
        private SingerDbContext dbcontext;

        public Repository(SingerDbContext dbcontext)
        {
            this.dbcontext = dbcontext;
        }

        public void Create<T>(T entity) where T : class
        {
            if (entity == null) throw new ArgumentNullException(nameof(entity));

            dbcontext.Set<T>().Add(entity);
            dbcontext.SaveChanges();
        }

        public void CreateRange<T>(T[] entities) where T : class
        {
            if (entities == null) throw new ArgumentNullException(nameof(entities));

            dbcontext.Set<T>().AddRange(entities);
            dbcontext.SaveChanges();
        }

        public void Delete<T>(T entity) where T : class
        {
            if (entity == null) throw new ArgumentNullException(nameof(entity));

            dbcontext.Set<T>().Remove(entity);
            dbcontext.SaveChanges();
        }

        public void DeleteRange<T>(T[] entities) where T : class
        {
            if (entities == null) throw new ArgumentNullException(nameof(entities));

            dbcontext.Set<T>().RemoveRange(entities);
            dbcontext.SaveChanges();
        }

        public void Update<T>(T entity) where T : class
        {
            if (entity == null) throw new ArgumentNullException(nameof(entity));

            dbcontext.Set<T>().AddOrUpdate(entity);
            dbcontext.SaveChanges();
        }

        public void UpdateRange<T>(T[] entities) where T : class
        {
            if (entities == null) throw new ArgumentNullException(nameof(entities));

            dbcontext.Set<T>().AddOrUpdate(entities);
            dbcontext.SaveChanges();
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

        public async Task<IEnumerable<T>> GetPartOrderBy<T>(int take, int skip, bool isDesc, string propName) where T : class
        {
            return await dbcontext.Set<T>().OrderByProperty(propName, isDesc).Skip(skip).Take(take).ToListAsync();
        }

        public async Task<IEnumerable<T>> GetPartOrderByWhere<T>(int take, int skip, bool isDesc, string propName, Expression<Func<T, bool>> predicate) where T : class
        {
            var ss = dbcontext.Set<T>().Where(predicate).OrderByProperty(propName, isDesc).Skip(skip).Take(take);
            return await ss.ToListAsync();
        }


        public int GetCount<T>() where T : class
        {
            return dbcontext.Set<T>().Count();
        }

        public int GetCount<T>(Expression<Func<T, bool>> predicate) where T : class
        {
            return dbcontext.Set<T>().Count(predicate);
        }
    }
}
