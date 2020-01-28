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
    public class ClientOrderValidatorTest
    {
        [TestMethod]
        public void CanAddClientOrder_NullClientOrder_ThrowsException() {
            // Arrange
            ClientOrderValidator clientOrderValidator = new ClientOrderValidator();
            ClientOrder clientOrder = null;
            // Act
            // Assert
            Assert.ThrowsException<ArgumentNullException>(() => clientOrderValidator.CanAddClientOrder(clientOrder));
        }

        [TestMethod]
        public void CanAddClientOrder_ClientOrderHasEmptyClientId_ReturnErrorMessage()
        {
            // Arrange
            ClientOrderValidator clientOrderValidator = new ClientOrderValidator();
            ClientOrder clientOrder = new ClientOrder();
            clientOrder.DeliveryId = 123;
            clientOrder.Orders = new List<Order>();
            clientOrder.Orders.Add(new Order() { ProductId = 11, Quantity = 123 });
            clientOrder.Orders.Add(new Order() { ProductId = 22, Quantity = 321 });
            // Act
            List<KeyValuePair<string, string>> result = clientOrderValidator.CanAddClientOrder(clientOrder);

            // Assert
            Assert.IsNotNull(result);
            Assert.AreEqual(1, result.Count);
            Assert.AreEqual("ClientId", result[0].Key);
        }

        [TestMethod]
        public void CanAddClientOrder_ClientOrderHasEmptyDeliveryId_ReturnErrorMessage()
        {
            // Arrange
            ClientOrderValidator clientOrderValidator = new ClientOrderValidator();
            ClientOrder clientOrder = new ClientOrder();
            clientOrder.ClientId = "GUIDGUID";
            clientOrder.Orders = new List<Order>();
            clientOrder.Orders.Add(new Order() { ProductId = 11, Quantity = 123 });
            clientOrder.Orders.Add(new Order() { ProductId = 22, Quantity = 321 });
            // Act
            List<KeyValuePair<string, string>> result = clientOrderValidator.CanAddClientOrder(clientOrder);

            // Assert
            Assert.IsNotNull(result);
            Assert.AreEqual(1, result.Count);
            Assert.AreEqual("DeliveryId", result[0].Key);
        }

        [TestMethod]
        public void CanAddClientOrder_ClientOrderHasNoOrders_ReturnErrorMessage()
        {
            // Arrange
            ClientOrderValidator clientOrderValidator = new ClientOrderValidator();
            ClientOrder clientOrder = new ClientOrder();
            clientOrder.ClientId = "GUIDGUID";
            clientOrder.DeliveryId = 123;
            clientOrder.Orders = new List<Order>();
            // Act
            List<KeyValuePair<string, string>> result = clientOrderValidator.CanAddClientOrder(clientOrder);

            // Assert
            Assert.IsNotNull(result);
            Assert.AreEqual(1, result.Count);
            Assert.AreEqual("ProductId", result[0].Key);
        }

        [TestMethod]
        public void CanAddClientOrder_ClientOrderHasEmptyProductId_ReturnErrorMessage()
        {
            // Arrange
            ClientOrderValidator clientOrderValidator = new ClientOrderValidator();
            ClientOrder clientOrder = new ClientOrder();
            clientOrder.ClientId = "GUIDGUID";
            clientOrder.DeliveryId = 123;
            clientOrder.Orders = new List<Order>();
            clientOrder.Orders.Add(new Order() { ProductId = 0, Quantity = 123 });
            // Act
            List<KeyValuePair<string, string>> result = clientOrderValidator.CanAddClientOrder(clientOrder);

            // Assert
            Assert.IsNotNull(result);
            Assert.AreEqual(1, result.Count);
            Assert.AreEqual("ProductId", result[0].Key);
        }

        [TestMethod]
        public void CanAddClientOrder_ClientOrderHasEmptyQuantity_ReturnErrorMessage()
        {
            // Arrange
            ClientOrderValidator clientOrderValidator = new ClientOrderValidator();
            ClientOrder clientOrder = new ClientOrder();
            clientOrder.ClientId = "GUIDGUID";
            clientOrder.DeliveryId = 123;
            clientOrder.Orders = new List<Order>();
            clientOrder.Orders.Add(new Order() { ProductId = 11, Quantity = 0 });
            // Act
            List<KeyValuePair<string, string>> result = clientOrderValidator.CanAddClientOrder(clientOrder);

            // Assert
            Assert.IsNotNull(result);
            Assert.AreEqual(1, result.Count);
            Assert.AreEqual("Quantity", result[0].Key);
        }



        [TestMethod]
        public void CanUpdateClientOrder_NullClientOrder_ThrowsException()
        {
            // Arrange
            ClientOrderValidator clientOrderValidator = new ClientOrderValidator();
            ClientOrder clientOrder = null;
            // Act
            // Assert
            Assert.ThrowsException<ArgumentNullException>(() => clientOrderValidator.CanUpdateClientOrder(clientOrder));
        }

        [TestMethod]
        public void CanUpdateClientOrder_ClientOrderHasEmptyClientId_ReturnErrorMessage()
        {
            // Arrange
            ClientOrderValidator clientOrderValidator = new ClientOrderValidator();
            ClientOrder clientOrder = new ClientOrder();
            clientOrder.DeliveryId = 123;
            clientOrder.Orders = new List<Order>();
            clientOrder.Orders.Add(new Order() { ProductId = 11, Quantity = 123 });
            clientOrder.Orders.Add(new Order() { ProductId = 22, Quantity = 321 });
            // Act
            List<KeyValuePair<string, string>> result = clientOrderValidator.CanUpdateClientOrder(clientOrder);

            // Assert
            Assert.IsNotNull(result);
            Assert.AreEqual(1, result.Count);
            Assert.AreEqual("ClientId", result[0].Key);
        }

        [TestMethod]
        public void CanUpdateClientOrder_ClientOrderHasEmptyDeliveryId_ReturnErrorMessage()
        {
            // Arrange
            ClientOrderValidator clientOrderValidator = new ClientOrderValidator();
            ClientOrder clientOrder = new ClientOrder();
            clientOrder.ClientId = "GUIDGUID";
            clientOrder.Orders = new List<Order>();
            clientOrder.Orders.Add(new Order() { ProductId = 11, Quantity = 123 });
            clientOrder.Orders.Add(new Order() { ProductId = 22, Quantity = 321 });
            // Act
            List<KeyValuePair<string, string>> result = clientOrderValidator.CanUpdateClientOrder(clientOrder);

            // Assert
            Assert.IsNotNull(result);
            Assert.AreEqual(1, result.Count);
            Assert.AreEqual("DeliveryId", result[0].Key);
        }

        [TestMethod]
        public void CanUpdateClientOrder_ClientOrderHasNoOrders_ReturnErrorMessage()
        {
            // Arrange
            ClientOrderValidator clientOrderValidator = new ClientOrderValidator();
            ClientOrder clientOrder = new ClientOrder();
            clientOrder.ClientId = "GUIDGUID";
            clientOrder.DeliveryId = 123;
            clientOrder.Orders = new List<Order>();
            // Act
            List<KeyValuePair<string, string>> result = clientOrderValidator.CanUpdateClientOrder(clientOrder);

            // Assert
            Assert.IsNotNull(result);
            Assert.AreEqual(1, result.Count);
            Assert.AreEqual("ProductId", result[0].Key);
        }

        [TestMethod]
        public void CanUpdateClientOrder_ClientOrderHasEmptyProductId_ReturnErrorMessage()
        {
            // Arrange
            ClientOrderValidator clientOrderValidator = new ClientOrderValidator();
            ClientOrder clientOrder = new ClientOrder();
            clientOrder.ClientId = "GUIDGUID";
            clientOrder.DeliveryId = 123;
            clientOrder.Orders = new List<Order>();
            clientOrder.Orders.Add(new Order() { ProductId = 0, Quantity = 123 });
            // Act
            List<KeyValuePair<string, string>> result = clientOrderValidator.CanUpdateClientOrder(clientOrder);

            // Assert
            Assert.IsNotNull(result);
            Assert.AreEqual(1, result.Count);
            Assert.AreEqual("ProductId", result[0].Key);
        }

        [TestMethod]
        public void CanUpdateClientOrder_ClientOrderHasEmptyQuantity_ReturnErrorMessage()
        {
            // Arrange
            ClientOrderValidator clientOrderValidator = new ClientOrderValidator();
            ClientOrder clientOrder = new ClientOrder();
            clientOrder.ClientId = "GUIDGUID";
            clientOrder.DeliveryId = 123;
            clientOrder.Orders = new List<Order>();
            clientOrder.Orders.Add(new Order() { ProductId = 11, Quantity = 0 });
            // Act
            List<KeyValuePair<string, string>> result = clientOrderValidator.CanUpdateClientOrder(clientOrder);

            // Assert
            Assert.IsNotNull(result);
            Assert.AreEqual(1, result.Count);
            Assert.AreEqual("Quantity", result[0].Key);
        }

    }
}
