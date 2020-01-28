using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Web.Mvc;
using Microsoft.VisualStudio.TestTools.UnitTesting;
using OrderService;
using OrderService.Controllers;
using OrderService.DataAccess.Providers;
using OrderService.DataAccess.Handlers;
using OrderService.DataAccess.Validators;
using OrderService.Models;
using Moq;

namespace OrderService.Tests.Controllers
{
    [TestClass]
    public class DeliveryControllerTest
    {
        List<Delivery> deliveries = new List<Delivery>()
        {
            new Delivery() { Id=1, Name="Delivery1", Price = 1.00m },
            new Delivery() { Id=2, Name="Delivery2", Price = 2.00m },
        };

        [TestMethod]
        public void Index_ReturnsCorrectViewResult() {
            // Arrange
            Mock<IDeliveryProvider> deliveryProviderMock = new Mock<IDeliveryProvider>();
            deliveryProviderMock.Setup(x => x.GetAll())
                .Returns(deliveries);
            Mock<IDeliveryHandler> deliveryHandlerMock = new Mock<IDeliveryHandler>();
            Mock<IDeliveryValidator> deliveryValidatorMock = new Mock<IDeliveryValidator>();

            DeliveryController controller = new DeliveryController(deliveryProviderMock.Object, deliveryHandlerMock.Object, deliveryValidatorMock.Object);
            // Act
            ViewResult result = controller.Index() as ViewResult;
            // Assert
            Assert.IsNotNull( result );
            Assert.IsTrue(((List<Delivery>)result.Model).Count == 2);
            Assert.AreEqual("GetDeliveriesList", result.ViewName);
        }

        [TestMethod]
        public void GetDeliveriesList_ReturnsCorrectViewResult()
        {
            // Arrange
            Mock<IDeliveryProvider> deliveryProviderMock = new Mock<IDeliveryProvider>();
            deliveryProviderMock.Setup(x => x.GetAll())
                .Returns(deliveries);
            Mock<IDeliveryHandler> deliveryHandlerMock = new Mock<IDeliveryHandler>();
            Mock<IDeliveryValidator> deliveryValidatorMock = new Mock<IDeliveryValidator>();

            DeliveryController controller = new DeliveryController(deliveryProviderMock.Object, deliveryHandlerMock.Object, deliveryValidatorMock.Object);
            // Act
            ViewResult result = controller.GetDeliveriesList() as ViewResult;
            // Assert
            Assert.IsNotNull(result);
            Assert.IsTrue(((List<Delivery>)result.Model).Count == 2);
            Assert.AreEqual("GetDeliveriesList", result.ViewName);
        }

        [TestMethod]
        public void GetDelivery_ReturnsCorrectViewResult()
        {
            // Arrange
            Mock<IDeliveryProvider> deliveryProviderMock = new Mock<IDeliveryProvider>();
            deliveryProviderMock.Setup(x => x.GetById(1))
                .Returns(deliveries[0]);
            Mock<IDeliveryHandler> deliveryHandlerMock = new Mock<IDeliveryHandler>();
            Mock<IDeliveryValidator> deliveryValidatorMock = new Mock<IDeliveryValidator>();

            DeliveryController controller = new DeliveryController(deliveryProviderMock.Object, deliveryHandlerMock.Object, deliveryValidatorMock.Object);
            // Act
            ViewResult result = controller.GetDelivery(1) as ViewResult;
            // Assert
            Assert.IsNotNull(result);
            Assert.IsTrue(((Delivery)result.Model).Id != default(int));
            Assert.AreEqual("GetDelivery", result.ViewName);
        }

        [TestMethod]
        public void AddDelivery_ReturnsCorrectViewResult()
        {
            // Arrange
            Mock<IDeliveryProvider> deliveryProviderMock = new Mock<IDeliveryProvider>();
            Mock<IDeliveryHandler> deliveryHandlerMock = new Mock<IDeliveryHandler>();
            Mock<IDeliveryValidator> deliveryValidatorMock = new Mock<IDeliveryValidator>();

            DeliveryController controller = new DeliveryController(deliveryProviderMock.Object, deliveryHandlerMock.Object, deliveryValidatorMock.Object);
            // Act
            ViewResult result = controller.AddDelivery(null) as ViewResult;
            // Assert
            Assert.IsNotNull(result);
            Assert.AreEqual("AddDelivery", result.ViewName);
        }

        [TestMethod]
        public void UpdateDelivery_ReturnsCorrectViewResult()
        {
            // Arrange
            Mock<IDeliveryProvider> deliveryProviderMock = new Mock<IDeliveryProvider>();
            deliveryProviderMock.Setup(x => x.GetById(1))
                .Returns(deliveries[0]);
            Mock<IDeliveryHandler> deliveryHandlerMock = new Mock<IDeliveryHandler>();
            Mock<IDeliveryValidator> deliveryValidatorMock = new Mock<IDeliveryValidator>();

            DeliveryController controller = new DeliveryController(deliveryProviderMock.Object, deliveryHandlerMock.Object, deliveryValidatorMock.Object);
            // Act
            ViewResult result = controller.UpdateDelivery(1) as ViewResult;
            // Assert
            Assert.IsNotNull(result);
            Assert.IsTrue(((Delivery)result.Model).Id != default(int));
            Assert.AreEqual("UpdateDelivery", result.ViewName);
        }


        [TestMethod]
        public void AddDeliveryPost_NullDelivery_ThrowsException()
        {
            // Arrange
            Mock<IDeliveryProvider> deliveryProviderMock = new Mock<IDeliveryProvider>();
            Mock<IDeliveryHandler> deliveryHandlerMock = new Mock<IDeliveryHandler>();
            Mock<IDeliveryValidator> deliveryValidatorMock = new Mock<IDeliveryValidator>();
            deliveryValidatorMock.Setup(x => x.CanAddDelivery(null))
                .Throws<ArgumentNullException>();

            DeliveryController controller = new DeliveryController(deliveryProviderMock.Object, deliveryHandlerMock.Object, deliveryValidatorMock.Object);
            // Act
            ViewResult result = controller.AddDeliveryPost(null) as ViewResult;
            // Assert
            Assert.IsNotNull(result);
            Assert.IsFalse(controller.ModelState.IsValid);
            Assert.AreEqual(1, controller.ModelState.Keys.Count);
            Assert.AreEqual("", controller.ModelState.Keys.FirstOrDefault());
            Assert.AreEqual("AddDelivery", result.ViewName);
        }

        [TestMethod]
        public void AddDeliveryPost_CorrectDelivery_ReturnsCorrectRedirectToRouteResult()
        {
            // Arrange
            Mock<IDeliveryProvider> deliveryProviderMock = new Mock<IDeliveryProvider>();
            Mock<IDeliveryHandler> deliveryHandlerMock = new Mock<IDeliveryHandler>();
            deliveryHandlerMock.Setup(x => x.Add(It.IsAny<Delivery>()))
                .Returns(1);
            Mock<IDeliveryValidator> deliveryValidatorMock = new Mock<IDeliveryValidator>();
            deliveryValidatorMock.Setup(x => x.CanAddDelivery(It.IsAny<Delivery>()))
                .Returns(new List<KeyValuePair<string, string>>());

            DeliveryController controller = new DeliveryController(deliveryProviderMock.Object, deliveryHandlerMock.Object, deliveryValidatorMock.Object);
            // Act
            RedirectToRouteResult result = controller.AddDeliveryPost(this.deliveries[0]) as RedirectToRouteResult;
            // Assert
            Assert.IsNotNull(result);
            Assert.AreEqual("GetDeliveriesList", result.RouteValues.FirstOrDefault().Value);
        }

        [TestMethod]
        public void UpdateDeliveryPost_NullDelivery_ThrowsException()
        {
            // Arrange
            Mock<IDeliveryProvider> deliveryProviderMock = new Mock<IDeliveryProvider>();
            Mock<IDeliveryHandler> deliveryHandlerMock = new Mock<IDeliveryHandler>();
            Mock<IDeliveryValidator> deliveryValidatorMock = new Mock<IDeliveryValidator>();
            deliveryValidatorMock.Setup(x => x.CanAddDelivery(null))
                .Throws<ArgumentNullException>();

            DeliveryController controller = new DeliveryController(deliveryProviderMock.Object, deliveryHandlerMock.Object, deliveryValidatorMock.Object);
            // Act
            ViewResult result = controller.UpdateDeliveryPost(null) as ViewResult;
            // Assert
            Assert.IsNotNull(result);
            Assert.IsFalse(controller.ModelState.IsValid);
            Assert.AreEqual(1, controller.ModelState.Keys.Count);
            Assert.AreEqual("", controller.ModelState.Keys.FirstOrDefault());
            Assert.AreEqual("UpdateDelivery", result.ViewName);
        }
        
        [TestMethod]
        public void UpdateDeliveryPost_CorrectDelivery_ReturnsCorrectRedirectToRouteResult()
        {
            // Arrange
            Mock<IDeliveryProvider> deliveryProviderMock = new Mock<IDeliveryProvider>();
            Mock<IDeliveryHandler> deliveryHandlerMock = new Mock<IDeliveryHandler>();
            deliveryHandlerMock.Setup(x => x.Update(It.IsAny<Delivery>()))
                .Returns(true);
            Mock<IDeliveryValidator> deliveryValidatorMock = new Mock<IDeliveryValidator>();
            deliveryValidatorMock.Setup(x => x.CanUpdateDelivery(It.IsAny<Delivery>()))
                .Returns(new List<KeyValuePair<string, string>>());

            DeliveryController controller = new DeliveryController(deliveryProviderMock.Object, deliveryHandlerMock.Object, deliveryValidatorMock.Object);
            // Act
            RedirectToRouteResult result = controller.UpdateDeliveryPost(this.deliveries[0]) as RedirectToRouteResult;
            // Assert
            Assert.IsNotNull(result);
            Assert.AreEqual("GetDeliveriesList", result.RouteValues.FirstOrDefault().Value);
        }


        [TestMethod]
        public void DeleteDeliveryPost_CorrectDelivery_ReturnsCorrectViewResult()
        {
            // Arrange
            Mock<IDeliveryProvider> deliveryProviderMock = new Mock<IDeliveryProvider>();
            Mock<IDeliveryHandler> deliveryHandlerMock = new Mock<IDeliveryHandler>();
            deliveryHandlerMock.Setup(x => x.Delete(1))
                .Returns(true);
            Mock<IDeliveryValidator> deliveryValidatorMock = new Mock<IDeliveryValidator>();

            DeliveryController controller = new DeliveryController(deliveryProviderMock.Object, deliveryHandlerMock.Object, deliveryValidatorMock.Object);
            // Act
            RedirectToRouteResult result = controller.DeleteDeliveryPost(1) as RedirectToRouteResult;
            // Assert
            Assert.IsNotNull(result);
            Assert.AreEqual("GetDeliveriesList", result.RouteValues.FirstOrDefault().Value);
        }
    }
}
