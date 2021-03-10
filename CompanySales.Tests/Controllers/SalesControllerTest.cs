using System;
using System.Collections.Generic;
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
        private Sales[] _emptyEntities;
        private List<List<string>> _invertedList;

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

            _emptyEntities = new[] { new Sales() };

            _invertedList = new List<List<string>>();
            _invertedList.Add(new List<string> { "120", "230" });
            _invertedList.Add(new List<string> { "567", "129" });

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
            _salesServiceMock.Setup(m => m.GetInvertedSales(_initialEntities)).Returns(_invertedList);
            _salesServiceMock.Setup(m => m.GetColumnHeaders(_initialEntities)).Returns(new List<string> { "567", "129" });
            _salesServiceMock.Setup(m => m.GetMedian(_invertedList)).Returns(new List<string> { "567", "129" });
            _salesServiceMock.Setup(m => m.GetAverage(_invertedList)).Returns(new List<string> { "567", "129" });
            _salesServiceMock.Setup(m => m.GetTotal(_invertedList)).Returns(new List<string> { "567", "129" });


            // Act
            var result = (ViewResult)controller.Index();

            // Assert
            Assert.IsNotNull(result);
        }

        public void CompanySales_Index_InvalidDataConnection()
        {
            // Arrange
            SalesController controller = new SalesController(_salesServiceMock.Object);
            SalesViewModel smv = new SalesViewModel();
            try
            {
                smv.NewSale = _emptyEntities[0];
                _salesServiceMock.Setup(m => m.GetSales()).Returns(_emptyEntities);
                _salesServiceMock.Setup(m => m.GetInvertedSales(_emptyEntities)).Returns(new List<List<string>>());
                _salesServiceMock.Setup(m => m.GetColumnHeaders(_emptyEntities)).Returns(new List<string>());
                _salesServiceMock.Setup(m => m.GetMedian(_invertedList)).Returns(new List<string>());
                _salesServiceMock.Setup(m => m.GetAverage(_invertedList)).Returns(new List<string>());
                _salesServiceMock.Setup(m => m.GetTotal(_invertedList)).Returns(new List<string>());
            }
            catch (Exception)
            {

            }

            // Act
            var result = (ViewResult)controller.Index();

            // Assert
            //Assert
            //do nothing

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
