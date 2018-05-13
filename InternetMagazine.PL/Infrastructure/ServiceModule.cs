using System;
using InternetMagazine.DAL.Interfaces;
using InternetMagazine.DAL.UnitOfWork;
using InternetMagazine.DAL.Entities;
using Ninject.Modules;

namespace InternetMagazine.PL.Infrastructure
{
    public class ServiceModule:NinjectModule
    {
        private string connectionString;
       

        public ServiceModule(string connection)
        {
            connectionString = connection;
           
        }
        
        public override void Load()
        {
            Bind<IUnitOfWork>().To<UnitOfWork>().WithConstructorArgument(connectionString);
        }
    }
}
