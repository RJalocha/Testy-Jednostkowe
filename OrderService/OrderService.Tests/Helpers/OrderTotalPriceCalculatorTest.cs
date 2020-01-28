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
    public class OrderTotalPriceCalculatorTest
    {
        [TestMethod]
        public void Calculate_NullOrder_ThrowsException() {
            // Arrange
            OrderTotalPriceCalculator orderTotalPriceCalculator = new OrderTotalPriceCalculator();
            Order order = null;
            // Act
            // Assert
            Assert.ThrowsException<ArgumentNullException>(() => orderTotalPriceCalculator.Calculate(order));
        }

        [TestMethod]
        public void Calculate_OrderNotNull_ReturnCorrectResult()
        {
            // Arrange
            OrderTotalPriceCalculator orderTotalPriceCalculator = new OrderTotalPriceCalculator();
            Order order = new Order() {
                Product = new Product() { Price = 2.5m },
                Quantity = 3
            };
            // Act
            decimal result = orderTotalPriceCalculator.Calculate(order);
            // Assert
            Assert.AreEqual(7.5m, result);
        }

    }
}
