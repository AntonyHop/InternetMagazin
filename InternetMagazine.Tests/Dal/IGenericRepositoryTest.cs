using System;
using Microsoft.VisualStudio.TestTools.UnitTesting;
using InternetMagazine.DAL.EF;
using InternetMagazine.DAL.Entities;
using InternetMagazine.DAL.Interfaces;
using InternetMagazine.DAL.Repositories;
using System.Data.Entity;

namespace InternetMagazine.Tests.Dal
{
    [TestClass]
    public class IGenericRepositoryTest
    {

        IGenericRepository<Event> rep;
        EFContext ctx = new EFContext(@"Data Source=(localdb)\MSSQLLocalDB;Initial Catalog=InternetMagazine;Integrated Security=True");
        Event tp = new Event() {Name = "Test1", Desc = "Test", CategoryId = 1, Price = 228.50M };

        [TestInitialize]
        public void Setup()
        {
            rep = new GenericRepository<Event>(ctx);
        }

        [TestMethod]
        public void CreateTest()
        {
            try
            {
                rep.Create(tp);
                tp = new Event() { Name = "Test2", Desc = "Test", CategoryId = 1, Price = 228.55M };
                rep.Create(tp);
                tp = new Event() { Name = "Test3", Desc = "Test", CategoryId = 1, Price = 228M };
                rep.Create(tp);

            }
            catch(Exception ex){
                Assert.Fail(ex.Message);
            }

        }

        [TestCleanup]
        public void Cleanup()
        {
            ctx.Dispose();
        }

    }
}
