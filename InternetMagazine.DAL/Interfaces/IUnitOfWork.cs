using System;
using System.Collections.Generic;
using InternetMagazine.DAL.Entities;
using InternetMagazine.DAL.Repositories;

namespace InternetMagazine.DAL.Interfaces
{
    public interface IUnitOfWork:IDisposable
    {
        GenericRepository<Room> Categories { get; }
        GenericRepository<Event> Products { get; }
        GenericRepository<User> Users { get; }
        GenericRepository<Order> Orders { get; }
      
    }
}
