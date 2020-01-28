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
using OrderService.Helpers;

namespace OrderService.Tests.Helpers
{
    [TestClass]
    public class ClientOrderTotalPriceCalculatorTest
    {
        [TestMethod]
        public void Calculate_NullClientOrder_ThrowsException() {
            // Arrange
            ClientOrderTotalPriceCalculator clientOrderTotalPriceCalculator = new ClientOrderTotalPriceCalculator();
            ClientOrder clientOrder = null;
            // Act
            // Assert
            Assert.ThrowsException<ArgumentNullException>(() => clientOrderTotalPriceCalculator.Calculate(clientOrder));
        }

        [TestMethod]
        public void Calculate_NullOrder_ThrowsException()
        {
            // Arrange
            ClientOrderTotalPriceCalculator clientOrderTotalPriceCalculator = new ClientOrderTotalPriceCalculator();
            ClientOrder clientOrder = new ClientOrder();
            // Act
            // Assert
            Assert.ThrowsException<ArgumentNullException>(() => clientOrderTotalPriceCalculator.Calculate(clientOrder));
        }

        [TestMethod]
        public void Calculate_OrderNotNull_ReturnCorrectResult()
        {
            // Arrange
            ClientOrderTotalPriceCalculator clientOrderTotalPriceCalculator = new ClientOrderTotalPriceCalculator();
            ClientOrder clientOrder = new ClientOrder();
            clientOrder.Discount = 1m;
            clientOrder.Delivery = new Delivery() { Price = 0.5m };
            clientOrder.Orders = new List<Order>();
            Order order1 = new Order() {
                Product = new Product() { Price = 2.5m },
                Quantity = 3
            };
            Order order2 = new Order()
            {
                Product = new Product() { Price = 1.5m },
                Quantity = 2
            };
            clientOrder.Orders.Add(order1);
            clientOrder.Orders.Add(order2);
            // Act
            decimal result = clientOrderTotalPriceCalculator.Calculate(clientOrder);
            // Assert
            Assert.AreEqual(10m, result);
        }

    }
}
