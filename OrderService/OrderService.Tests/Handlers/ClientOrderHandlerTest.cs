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
    public class ClientOrderHandlerTest
    {
        // DB operation are done on different db file - it's loacted in bin/debug directory

        [TestMethod]
        public void Add_ClientOrderNull_ThrowsException()
        {
            // Arrange
            ClientOrderHandler clientOrderHandler = new ClientOrderHandler();
            ClientOrder clientOrder = null;
            // Act
            // Assert
            Assert.ThrowsException<NullReferenceException>(() => clientOrderHandler.Add(clientOrder));
        }

        [TestMethod]
        public void Add_ClientOrderCorrectEntity_ReturnId()
        {
            // Arrange
            ClientOrderProvider clientOrderProvider = new ClientOrderProvider();
            var allClientOrders = clientOrderProvider.GetAll().OrderByDescending(p => p.Id);
            ClientOrder lastClientOrder = allClientOrders.FirstOrDefault() ?? new ClientOrder();

            ClientOrderHandler clientOrderHandler = new ClientOrderHandler();
            ClientOrder clientOrder = new ClientOrder();
            clientOrder.ClientId = "c983fea1-a550-4ea6-b45a-db66d9defcc1";
            clientOrder.DeliveryId = 1;
            clientOrder.CreationDate = DateTime.Now;
            clientOrder.DeliveryDate = DateTime.Now;
            clientOrder.Orders = new List<Order>()
            {
                new Order() { ProductId = 1, Quantity = 1 }
            };

            // Act
            int id = clientOrderHandler.Add(clientOrder);
            // Assert
            Assert.IsTrue(lastClientOrder.Id < id);
        }


        [TestMethod]
        public void Update_ClientOrderNull_ThrowsException()
        {
            // Arrange
            ClientOrderHandler clientOrderHandler = new ClientOrderHandler();
            ClientOrder clientOrder = null;
            // Act
            // Assert
            Assert.ThrowsException<TargetException>(() => clientOrderHandler.Update(clientOrder));
        }

        [TestMethod]
        public void Update_ClientOrderCorrectEntity_ReturnTrueAndNewEntityHasCorrectSetFields()
        {
            // Arrange
            ClientOrderProvider clientOrderProvider = new ClientOrderProvider();
            var oldClientOrder= clientOrderProvider.GetById(1);

            ClientOrderHandler clientOrderHandler = new ClientOrderHandler();
            ClientOrder clientOrder = new ClientOrder();
            clientOrder.Id = 1;
            clientOrder.ClientId = "c983fea1-a550-4ea6-b45a-db66d9defcc1";
            clientOrder.DeliveryId = 1;
            clientOrder.CreationDate = DateTime.Now;
            clientOrder.DeliveryDate = DateTime.Now;
            clientOrder.Orders = new List<Order>();
            // Act
            bool result = clientOrderHandler.Update(clientOrder);
            var newClientOrder = clientOrderProvider.GetById(1);
            // Assert
            Assert.IsTrue(result);
            Assert.AreEqual("c983fea1-a550-4ea6-b45a-db66d9defcc1", newClientOrder.ClientId);
            Assert.AreEqual(1, newClientOrder.DeliveryId);
        }



        [TestMethod]
        public void Delete_ClientOrderExists_ReturnTrue()
        {
            // Arrange
            ClientOrderProvider clientOrderProvider = new ClientOrderProvider();
            var allClientOrders = clientOrderProvider.GetAll().OrderByDescending(p => p.Id);
            ClientOrder lastClientOrder = allClientOrders.FirstOrDefault() ?? new ClientOrder();
            ClientOrderHandler clientOrderHandler = new ClientOrderHandler();

            // Act
            bool result = clientOrderHandler.Delete(lastClientOrder.Id);
            var deletedClientOrder = clientOrderProvider.GetById(lastClientOrder.Id);
            // Assert
            Assert.IsTrue(result);
            Assert.IsNull(deletedClientOrder);
        }
    }
}
