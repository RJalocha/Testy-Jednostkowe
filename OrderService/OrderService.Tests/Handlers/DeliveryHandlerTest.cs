using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Web.Mvc;
using Microsoft.VisualStudio.TestTools.UnitTesting;
using OrderService;
using OrderService.Controllers;
using OrderService.DataAccess.Handlers;
using OrderService.Models;
using OrderService.DataAccess.Providers;
using System.Reflection;

namespace OrderService.Tests.Controllers
{
    [TestClass]
    public class DeliveryHandlerTest
    {
        // DB operation are done on different db file - it's loacted in bin/debug directory

        [TestMethod]
        public void Add_DeliveryNull_ThrowsException()
        {
            // Arrange
            DeliveryHandler deliveryHandler = new DeliveryHandler();
            Delivery delivery = null;
            // Act
            // Assert
            Assert.ThrowsException<ArgumentNullException>(() => deliveryHandler.Add(delivery));
        }

        [TestMethod]
        public void Add_DeliveryCorrectEntity_ReturnId()
        {
            // Arrange
            DeliveryProvider deliveryProvider = new DeliveryProvider();
            var allDeliverys = deliveryProvider.GetAll().OrderByDescending(p => p.Id);
            Delivery lastDelivery = allDeliverys.FirstOrDefault() ?? new Delivery();

            DeliveryHandler deliveryHandler = new DeliveryHandler();
            Delivery delivery = new Delivery();
            delivery.Name = "Name";
            delivery.Price = 123.45m;
            // Act
            int id = deliveryHandler.Add(delivery);
            // Assert
            Assert.IsTrue(lastDelivery.Id < id);
        }


        [TestMethod]
        public void Update_DeliveryNull_ThrowsException()
        {
            // Arrange
            DeliveryHandler deliveryHandler = new DeliveryHandler();
            Delivery delivery = null;
            // Act
            // Assert
            Assert.ThrowsException<TargetException>(() => deliveryHandler.Update(delivery));
        }

        [TestMethod]
        public void Update_DeliveryCorrectEntity_ReturnTrueAndNewEntityHasCorrectSetFields()
        {
            // Arrange
            DeliveryProvider deliveryProvider = new DeliveryProvider();
            var oldDeliverys = deliveryProvider.GetById(1);

            DeliveryHandler deliveryHandler = new DeliveryHandler();
            Delivery delivery = new Delivery();
            delivery.Id = 1;
            delivery.Name = "NEW_Name";
            delivery.Price = 999.99m;
            // Act
            bool result = deliveryHandler.Update(delivery);
            var newDeliverys = deliveryProvider.GetById(1);
            // Assert
            Assert.IsTrue(result);
            Assert.AreEqual("NEW_Name", newDeliverys.Name);
            Assert.AreEqual(999.99m, newDeliverys.Price);
        }


        [TestMethod]
        public void Delete_DeliveryExists_ReturnTrue()
        {
            // Arrange
            DeliveryProvider deliveryProvider = new DeliveryProvider();
            var allDeliverys = deliveryProvider.GetAll().OrderByDescending(p => p.Id);
            Delivery lastDelivery = allDeliverys.FirstOrDefault() ?? new Delivery();
            DeliveryHandler deliveryHandler = new DeliveryHandler();

            // Act
            bool result = deliveryHandler.Delete(lastDelivery.Id);
            var deletedDelivery = deliveryProvider.GetById(lastDelivery.Id);
            // Assert
            Assert.IsTrue(result);
            Assert.IsNull(deletedDelivery);
        }
    }
}
