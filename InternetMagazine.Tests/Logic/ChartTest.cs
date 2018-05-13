using System.Collections.Generic;
using System.Linq;
using Microsoft.VisualStudio.TestTools.UnitTesting;
using InternetMagazine.PL.BuisnessModels;
using InternetMagazine.PL.DTO;
using InternetMagazine.PL.Services;
using InternetMagazine.PL.Interfaces;

namespace InternetMagazine.Tests.Logic
{
    [TestClass]
    public class ChartTest
    {
        [TestMethod]
        public void AddToChartTest()
        {
            ProductDTO p1 = new ProductDTO() { Id = 1, Name = "Test1", Author = "Test", Price = 250, Desc = "test" };
            ProductDTO p2 = new ProductDTO() { Id = 2, Name = "Test2", Author = "Test", Price = 250, Desc = "test" };
            ProductDTO p3 = new ProductDTO() { Id = 3, Name = "Test3", Author = "Test", Price = 250, Desc = "test" };
            OrderLogic cart = new OrderLogic();

            cart.AddItem(p1, 1);
            cart.AddItem(p2, 1);
            cart.AddItem(p3, 1);
            List<OrderItemDTO> results = cart.Lines.ToList();

            Assert.AreEqual(3, results.Count());
            Assert.AreEqual(results[0].Product, p1);
            Assert.AreEqual(results[1].Product, p2);
            Assert.AreEqual(results[2].Product, p3);

        }

        [TestMethod]
        public void CanAddCountForExistingLines()
        {
            // Организация - создание нескольких тестовых игр
            ProductDTO game1 = new ProductDTO { Id = 1, Name = "Test1" };
            ProductDTO game2 = new ProductDTO { Id = 2, Name = "Test2" };

            // Организация - создание корзины
            OrderLogic cart = new OrderLogic();

            // Действие
            cart.AddItem(game1, 1);
            cart.AddItem(game2, 1);
            cart.AddItem(game1, 5);
            List<OrderItemDTO> results = cart.Lines.OrderBy(c => c.Product.Id).ToList();

            // Утверждение
            Assert.AreEqual(results.Count(), 2);
            Assert.AreEqual(results[0].Count, 6);    // 6 экземпляров добавлено в корзину
            Assert.AreEqual(results[1].Count, 1);
        }

        [TestMethod]
        public void CanRemoveLine()
        {
            // Организация - создание нескольких тестовых игр
            ProductDTO game1 = new ProductDTO { Id = 1, Name = "Игра1" };
            ProductDTO game2 = new ProductDTO { Id = 2, Name = "Игра2" };
            ProductDTO game3 = new ProductDTO { Id = 3, Name = "Игра3" };

            // Организация - создание корзины
            OrderLogic cart = new OrderLogic();

            // Организация - добавление нескольких игр в корзину
            cart.AddItem(game1, 1);
            cart.AddItem(game2, 4);
            cart.AddItem(game3, 2);
            cart.AddItem(game2, 1);

            // Действие
            cart.RemoveLine(game2);

            // Утверждение
            Assert.AreEqual(cart.Lines.Where(c => c.Product == game2).Count(), 0);
            Assert.AreEqual(cart.Lines.Count(), 2);
        }

        [TestMethod]
        public void CanRemoveLineByID()
        {
            // Организация - создание нескольких тестовых игр
            ProductDTO game1 = new ProductDTO { Id = 1, Name = "Игра1" };
            ProductDTO game2 = new ProductDTO { Id = 2, Name = "Игра2" };
            ProductDTO game3 = new ProductDTO { Id = 3, Name = "Игра3" };

            // Организация - создание корзины
            OrderLogic cart = new OrderLogic();

            // Организация - добавление нескольких игр в корзину
            cart.AddItem(game1, 1);
            cart.AddItem(game2, 4);
            cart.AddItem(game3, 2);
            cart.AddItem(game2, 1);

            // Действие
            cart.RemoveLine(2);

            // Утверждение
            Assert.AreEqual(cart.Lines.Where(c => c.Product == game2).Count(), 0);
            Assert.AreEqual(cart.Lines.Count(), 2);
        }

        // ...	
        [TestMethod]
        public void CalculateCartTotal()
        {
            // Организация - создание нескольких тестовых игр
            ProductDTO game1 = new ProductDTO { Id = 1, Name = "Игра1", Price = 100 };
            ProductDTO game2 = new ProductDTO { Id = 2, Name = "Игра2", Price = 55 };

            // Организация - создание корзины
            OrderLogic cart = new OrderLogic();

            // Действие
            cart.AddItem(game1, 1);
            cart.AddItem(game2, 1);
            cart.AddItem(game1, 5);
            decimal result = cart.ComputeTotalValue();

            // Утверждение
            Assert.AreEqual(result, 655);
        }

        [TestMethod]
        public void CanClearContents()
        {
            // Организация - создание нескольких тестовых игр
            ProductDTO game1 = new ProductDTO { Id = 1, Name = "Игра1", Price = 100 };
            ProductDTO game2 = new ProductDTO { Id = 2, Name = "Игра2", Price = 55 };

            // Организация - создание корзины
            OrderLogic cart = new OrderLogic();

            // Действие
            cart.AddItem(game1, 1);
            cart.AddItem(game2, 1);
            cart.AddItem(game1, 5);
            cart.Clear();

            // Утверждение
            Assert.AreEqual(cart.Lines.Count(), 0);
        }
    }
}
