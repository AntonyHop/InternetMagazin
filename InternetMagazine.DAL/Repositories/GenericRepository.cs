using System;
using System.Collections.Generic;
using System.Linq;
using InternetMagazine.DAL.Interfaces;
using System.Data.Entity;
using System.Linq.Expressions;

namespace InternetMagazine.DAL.Repositories
{
    public class GenericRepository<TEntity> : IGenericRepository<TEntity> where TEntity:class
    {

        DbContext _ctx = null;
        DbSet<TEntity> _dbSet = null;

        public GenericRepository(DbContext ctx)
        {
            _ctx = ctx;
            _dbSet = _ctx.Set<TEntity>();
        }

        public int Create(TEntity item)
        {
            _dbSet.Add(item);
            _ctx.SaveChanges();

            var idProperty = item.GetType().GetProperty("Id").GetValue(item, null);
            return (int)idProperty;
        }

        public TEntity FindById(int id)
        {
            return _dbSet.Find(id);
        }

        public IEnumerable<TEntity> Get()
        {
            return _dbSet.AsNoTracking().ToList();
        }

        public IEnumerable<TEntity> Get(Func<TEntity, bool> predicate)
        {
            return _dbSet.AsNoTracking().Where(predicate).ToList();
        }

        public void Remove(TEntity item)
        {
            _dbSet.Remove(item);
            _ctx.SaveChanges();
        }

        public void Remove(int id)
        {
            TEntity Elem =FindById(id);
            if (Elem != null)
                Remove(Elem);
                
        }

        public void Update(TEntity item)
        {
            _ctx.Entry(item).State = EntityState.Modified;
            _ctx.SaveChanges();
        }

        public IEnumerable<TEntity> GetWithInclude(params Expression<Func<TEntity, object>>[] includeProperties)
        {
            return Include(includeProperties).ToList();
        }

        public IEnumerable<TEntity> GetWithInclude(Func<TEntity, bool> predicate,
            params Expression<Func<TEntity, object>>[] includeProperties)
        {
            var query = Include(includeProperties);
            return query.Where(predicate).ToList();
        }

        private IQueryable<TEntity> Include(params Expression<Func<TEntity, object>>[] includeProperties)
        {
            IQueryable<TEntity> query = _dbSet.AsNoTracking();
            return includeProperties
                .Aggregate(query, (current, includeProperty) => current.Include(includeProperty));
        }
    }
}
