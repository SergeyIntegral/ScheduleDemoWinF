using System;
using System.Collections.Generic;
using System.Data.Entity;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Schedule.DAL
{
    public class EntityRepository<T> : IRepository<T> where T : class, IDomainObject
    {
        private readonly DbContext _dbContext;

        public EntityRepository(DbContext db)
        {
            _dbContext = db;
        }

        public void Add(T item)
        {
            var dbSet = _dbContext.Set<T>();
            dbSet.Add(item);
        }

        public void Remove(T item)
        {
            var dbSet = _dbContext.Set<T>();
            dbSet.Remove(item);
        }

        public void RemoveById(int id)
        {
            var dbSet = _dbContext.Set<T>();
            var item = dbSet.Find(id);
            if (item != null)
                dbSet.Remove(item);
        }

        public T Find(int id)
        {
            var dbSet = _dbContext.Set<T>();
            var item = dbSet.Find(id);
            return item;
        }

        public IQueryable<T> GetAll()
        {
            var dbSet = _dbContext.Set<T>();
            return dbSet;
        }

        public void Update(T entity)
        {
            _dbContext.Entry(entity).State = EntityState.Modified;
            _dbContext.SaveChanges();
        }

        public void Detach(T entity)
        {
            _dbContext.Entry(entity).State = EntityState.Detached;
        }
    }
}
