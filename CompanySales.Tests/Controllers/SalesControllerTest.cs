using System;
using System.Data.Entity;
using System.Web.Mvc;
using CompanySales.Controllers;
using CompanySales.DAL;
using CompanySales.Models;
using CompanySales.Service;
using CompanySales.Tests.TestHelper;
using CompanySales.ViewModel;
using Microsoft.VisualStudio.TestTools.UnitTesting;
using Moq;

namespace CompanySales.Tests.Controllers
{
    [TestClass]
    public class SalesControllerTest
    {
        private Mock<ISalesService> _salesServiceMock;
        private Sales[] _initialEntities;

        [TestInitialize]
        public void Init()
        {
            _initialEntities = new[]{
                new Sales()
            {
                Month = "January",
                State = "Illinois",
                Sale = 123
            }, new Sales()
            {
                Month = "February",
                State = "California",
                Sale = 560
            }};

            _salesServiceMock = new Mock<ISalesService>(MockBehavior.Strict);
        }

        [TestMethod]
        public void CompanySales_Index_DataConnection()
        {
            // Arrange
            SalesController controller = new SalesController(_salesServiceMock.Object);
            SalesViewModel smv = new SalesViewModel();
            smv.NewSale = _initialEntities[0];
            _salesServiceMock.Setup(m => m.GetSales()).Returns(_initialEntities);

            // Act
            var result = (ViewResult)controller.Index();

            // Assert
            Assert.IsNotNull(result);
        }

        [TestMethod]
        public void CompanySales_Create_AddNewSales()
        {
            // Arrange
            SalesController controller = new SalesController(_salesServiceMock.Object);
            SalesViewModel smv = new SalesViewModel();
            smv.NewSale = _initialEntities[0];
            _salesServiceMock.Setup(m => m.AddSale(It.IsAny<Sales>())).Returns(true);

            // Act
            var result = (RedirectToRouteResult)controller.Create(smv);

            // Assert
            _salesServiceMock.Verify(m => m.AddSale(smv.NewSale), Times.Once);
            Assert.IsNotNull(result);
            Assert.AreEqual("Index", result.RouteValues["action"]); 
            

        }
    }
}
