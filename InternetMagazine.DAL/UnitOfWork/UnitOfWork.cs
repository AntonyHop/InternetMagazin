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
        private GenericRepository<Room> _categories;
        private GenericRepository<Event> _products;
        private GenericRepository<User> _users;
        private GenericRepository<Order> _orders;
      

        public UnitOfWork(string Connection)
        {
            ctx = new EFContext(Connection);
        }

        public GenericRepository<Room> Categories {
            get {
                if (_categories == null)
                    _categories = new GenericRepository<Room>(ctx);
                return _categories;
            }
        }

        public GenericRepository<Event> Products
        {
            get
            {
                if (_products == null)
                    _products = new GenericRepository<Event>(ctx);
                return _products;
            }
        }

        public GenericRepository<User> Users
        {
            get
            {
                if (_users == null)
                    _users = new GenericRepository<User>(ctx);
                return _users;
            }
        }

        public GenericRepository<Order> Orders
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
