using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Web.Mvc;
using Microsoft.VisualStudio.TestTools.UnitTesting;
using OrderService;
using OrderService.Controllers;
using OrderService.DataAccess.Validators;
using OrderService.Models;

namespace OrderService.Tests.Controllers
{
    [TestClass]
    public class DeliveryValidatorTest
    {
        [TestMethod]
        public void CanAddDelivery_NullDelivery_ThrowsException() {
            // Arrange
            DeliveryValidator deliveryValidator = new DeliveryValidator();
            Delivery delivery = null;
            // Act
            // Assert
            Assert.ThrowsException<ArgumentNullException>(() => deliveryValidator.CanAddDelivery(delivery));
        }

        [TestMethod]
        public void CanAddDelivery_DeliveryHasEmptyName_ReturnErrorMessage()
        {
            // Arrange
            DeliveryValidator deliveryValidator = new DeliveryValidator();
            Delivery delivery = new Delivery();
            delivery.Price = 123.45m;
            // Act
            List<KeyValuePair<string, string>> result = deliveryValidator.CanAddDelivery(delivery);

            // Assert
            Assert.IsNotNull(result);
            Assert.AreEqual(1, result.Count);
            Assert.AreEqual("Name", result[0].Key);
        }       

        [TestMethod]
        public void CanAddDelivery_DeliveryHasEmptyPrice_ReturnErrorMessage()
        {
            // Arrange
            DeliveryValidator deliveryValidator = new DeliveryValidator();
            Delivery delivery = new Delivery();
            delivery.Name = "Name";
            // Act
            List<KeyValuePair<string, string>> result = deliveryValidator.CanAddDelivery(delivery);

            // Assert
            Assert.IsNotNull(result);
            Assert.AreEqual(1, result.Count);
            Assert.AreEqual("Price", result[0].Key);
        }

        [TestMethod]
        public void CanUpdateDelivery_NullDelivery_ThrowsException()
        {
            // Arrange
            DeliveryValidator deliveryValidator = new DeliveryValidator();
            Delivery delivery = null;
            // Act
            // Assert
            Assert.ThrowsException<ArgumentNullException>(() => deliveryValidator.CanUpdateDelivery(delivery));
        }

        [TestMethod]
        public void CanUpdateDelivery_DeliveryHasEmptyName_ReturnErrorMessage()
        {
            // Arrange
            DeliveryValidator deliveryValidator = new DeliveryValidator();
            Delivery delivery = new Delivery();
            delivery.Price = 123.45m;
            // Act
            List<KeyValuePair<string, string>> result = deliveryValidator.CanUpdateDelivery(delivery);

            // Assert
            Assert.IsNotNull(result);
            Assert.AreEqual(1, result.Count);
            Assert.AreEqual("Name", result[0].Key);
        }

        [TestMethod]
        public void CanUpdateDelivery_DeliveryHasEmptyPrice_ReturnErrorMessage()
        {
            // Arrange
            DeliveryValidator deliveryValidator = new DeliveryValidator();
            Delivery delivery = new Delivery();
            delivery.Name = "Name";
            // Act
            List<KeyValuePair<string, string>> result = deliveryValidator.CanUpdateDelivery(delivery);

            // Assert
            Assert.IsNotNull(result);
            Assert.AreEqual(1, result.Count);
            Assert.AreEqual("Price", result[0].Key);
        }

    }
}
