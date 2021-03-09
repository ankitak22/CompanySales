using System;
using System.Web.Mvc;
using CompanySales.Controllers;
using CompanySales.ViewModel;
using Microsoft.VisualStudio.TestTools.UnitTesting;

namespace CompanySales.Tests.Controllers
{
    [TestClass]
    public class SalesControllerTest
    {
        [TestMethod]
        public void Index()
        {
            // Arrange
            SalesController controller = new SalesController();

            // Act
            ViewResult result = controller.Index() as ViewResult;

            // Assert
            Assert.IsNotNull(result);
        }

        [TestMethod]
        public void Create(SalesViewModel salesViewModel)
        {
            // Arrange
            SalesController controller = new SalesController();

            // Act
            ViewResult result = controller.Create(salesViewModel) as ViewResult;

            // Assert
            Assert.IsNotNull(result);
        }
    }
}
