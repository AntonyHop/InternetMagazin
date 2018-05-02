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

        IGenericRepository<Product> rep;
        EFContext ctx = new EFContext(@"Data Source=(localdb)\MSSQLLocalDB;Initial Catalog=InternetMagazine;Integrated Security=True");
        Product tp = new Product() {Name = "Test", Desc = "Test", CategoryId = 1, Price = 150 };

        [TestInitialize]
        public void Setup()
        {
            rep = new GenericRepository<Product>(ctx);
        }

        [TestMethod]
        public void CreateTest()
        {
            try
            {
                rep.Create(tp);

                
            }catch(Exception ex){
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
