using System;
using System.Collections.Generic;
using System.Linq;

namespace InternetMagazine.DAL.Interfaces
{
    public interface IGenericRepository<TEntity> where TEntity:class
    {
      
        int Create(TEntity item);
        TEntity FindById(int id);
        IEnumerable<TEntity> Get();
        IEnumerable<TEntity> Get(Func<TEntity,bool> predicate);
        void Remove(TEntity item);
        void Remove(int id);
        void Update(TEntity item);
    }
}
