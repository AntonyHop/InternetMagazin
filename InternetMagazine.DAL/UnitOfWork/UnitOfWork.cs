using InternetMagazine.DAL.Entities;
using InternetMagazine.DAL.Interfaces;
using InternetMagazine.DAL.Repositories;
using InternetMagazine.DAL.EF;
using System;

namespace InternetMagazine.DAL.UnitOfWork
{
    public class UnitOfWork : IUnitOfWork
    {
        private EFContext ctx;
        private IGenericRepository<Category> _categories;
        private IGenericRepository<Product> _products;
        private IGenericRepository<User> _users;
        private IGenericRepository<Order> _orders;
      

        public UnitOfWork(string Connection)
        {
            ctx = new EFContext(Connection);
        }

        public IGenericRepository<Category> Categories {
            get {
                if (_categories == null)
                    _categories = new GenericRepository<Category>(ctx);
                return _categories;
            }
        }

        public IGenericRepository<Product> Products
        {
            get
            {
                if (_products == null)
                    _products = new GenericRepository<Product>(ctx);
                return _products;
            }
        }

        public IGenericRepository<User> Users
        {
            get
            {
                if (_users == null)
                    _users = new GenericRepository<User>(ctx);
                return _users;
            }
        }

        public IGenericRepository<Order> Orders
        {
            get
            {
                if (_orders == null)
                    _orders = new GenericRepository<Order>(ctx);
                return _orders;
            }
        }

       

        private bool disposed = false;

        public virtual void Dispose(bool disposing)
        {
            if (!this.disposed)
            {
                if (disposing)
                {
                    ctx.Dispose();
                }
                this.disposed = true;
            }
        }

        public void Dispose()
        {
            Dispose(true);
            GC.SuppressFinalize(this);
        }


    }
}
