using System.Collections.Generic;
using System.Linq;
using Microsoft.VisualStudio.TestTools.UnitTesting;
using InternetMagazine.PL.BuisnessModels;
using InternetMagazine.PL.DTO;

namespace InternetMagazine.Tests.Logic
{
    [TestClass]
    public class ChartTest
    {
        [TestMethod]
        public void AddToChartTest()
        {
            ProductDTO p1 = new ProductDTO() { Name = "Test1", Author = "Test", Price = 250, Desc = "test" };
            ProductDTO p2 = new ProductDTO() { Name = "Test2", Author = "Test", Price = 250, Desc = "test" };
            ProductDTO p3 = new ProductDTO() { Name = "Test3", Author = "Test", Price = 250, Desc = "test" };
            Order o = new Order();

            o.AddItem(p1,1);
            o.AddItem(p2, 1);
            o.AddItem(p3, 1);
            List<OrderItem> results = o.Lines.ToList();

            Assert.AreEqual(results.Count, 3);
            Assert.AreEqual(p1, results[0]);
            Assert.AreEqual(p1, results[1]);
            Assert.AreEqual(p1, results[3]);

        }
    }
}
