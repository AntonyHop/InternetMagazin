using Microsoft.VisualStudio.TestTools.UnitTesting;
using InternetMagazine.DAL.EF;
using InternetMagazine.DAL.Entities;
using System;

namespace InternetMagazine.Tests.Dal
{
    [TestClass]
    public class EFContextTest
    {
        [TestMethod]
        public void EFContextInitTest()//Проверка Подключения к єнтити фреймворку
        {
            using (EFContext EFContext = new EFContext(@"Data Source=(localdb)\MSSQLLocalDB;Initial Catalog=InternetMagazine;Integrated Security=True")) {
                Category fc = EFContext.Categories.Find(1);

                Assert.IsNotNull(fc);
            }

        }
        
    }
}
