using System;
using System.Collections.Generic;
using InternetMagazine.DAL.Entities;
using InternetMagazine.DAL.Repositories;

namespace InternetMagazine.DAL.Interfaces
{
    public interface IUnitOfWork:IDisposable
    {
        GenericRepository<Category> Categories { get; }
        GenericRepository<Product> Products { get; }
    }
}
