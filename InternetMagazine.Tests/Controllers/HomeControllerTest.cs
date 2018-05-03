using System.Web.Mvc;
using Microsoft.VisualStudio.TestTools.UnitTesting;
using InternetMagazine.Controllers;
using InternetMagazine.PL.Interfaces;
using InternetMagazine.PL.DTO;
using Moq;
using System.Collections.Generic;

namespace InternetMagazine.Tests.Controllers
{
    [TestClass]
    public class HomeControllerTest
    {
        [TestMethod]
        public void Index()
        {
            // Arrange
            var mock = new Mock<ICategoryService>();
            mock.Setup(a => a.Categories()).Returns(new List<CategoryDTO>());
            HomeController controller = new HomeController(mock.Object);

            // Act
            ViewResult result = controller.Index(0) as ViewResult;

            // Assert
            Assert.IsNotNull(result);
        }

        [TestMethod]
        public void About()
        {
            // Arrange
            var mock = new Mock<ICategoryService>();
            mock.Setup(a => a.Categories()).Returns(new List<CategoryDTO>());
            HomeController controller = new HomeController(mock.Object);

            // Act
            ViewResult result = controller.About() as ViewResult;

            // Assert
            Assert.AreEqual("Your application description page.", result.ViewBag.Message);
        }

        [TestMethod]
        public void Contact()
        {
            // Arrange
            var mock = new Mock<ICategoryService>();
            mock.Setup(a => a.Categories()).Returns(new List<CategoryDTO>());
            HomeController controller = new HomeController(mock.Object);

            // Act
            ViewResult result = controller.Contact() as ViewResult;

            // Assert
            Assert.IsNotNull(result);
        }
    }
}
