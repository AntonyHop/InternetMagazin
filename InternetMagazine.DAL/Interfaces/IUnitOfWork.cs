using System;
using System.Collections.Generic;
using InternetMagazine.DAL.Entities;
using InternetMagazine.DAL.Repositories;

namespace InternetMagazine.DAL.Interfaces
{
    public interface IUnitOfWork:IDisposable
    {
        IGenericRepository<Category> Categories { get; }
        IGenericRepository<Product> Products { get; }
        IGenericRepository<User> Users { get; }
        IGenericRepository<Order> Orders { get; }
      
    }
}
