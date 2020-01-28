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
    public class ClientOrderDeliveryDateCalculatorTest
    {
        [TestMethod]
        public void Calculate_NullClientOrder_ThrowsException()
        {
            // Arrange
            ClientOrderDeliveryDateCalculator clientOrderDeliveryDateCalculator = new ClientOrderDeliveryDateCalculator();
            ClientOrder clientOrder = null;
            // Act
            // Assert
            Assert.ThrowsException<ArgumentNullException>(() => clientOrderDeliveryDateCalculator.Calculate(clientOrder));
        }

        [TestMethod]
        public void Calculate_ClientOrderNotNullNoWeekendDays_ReturnCorrectResult()
        {
            // Arrange
            ClientOrderDeliveryDateCalculator clientOrderDeliveryDateCalculator = new ClientOrderDeliveryDateCalculator();
            ClientOrder clientOrder = new ClientOrder()
            {
                CreationDate = new DateTime(2020, 1, 1), // Wednsday
                Delivery = new Delivery()
                {
                     DeliveryDays = 2
                }
            };
            // Act
            DateTime result = clientOrderDeliveryDateCalculator.Calculate(clientOrder);
            // Assert
            Assert.AreEqual(new DateTime(2020, 1, 1).AddDays(2).Day, result.Day);
        }

        [TestMethod]
        public void Calculate_ClientOrderNotNullWithWeekendDays_ReturnCorrectResult()
        {
            // Arrange
            ClientOrderDeliveryDateCalculator clientOrderDeliveryDateCalculator = new ClientOrderDeliveryDateCalculator();
            ClientOrder clientOrder = new ClientOrder()
            {
                CreationDate = new DateTime(2020, 1, 1), // Wednsday
                Delivery = new Delivery()
                {
                    DeliveryDays = 5
                }
            };
            // Act
            DateTime result = clientOrderDeliveryDateCalculator.Calculate(clientOrder);
            // Assert
            Assert.AreEqual(new DateTime(2020, 1, 1).AddDays(7).Day, result.Day);
        }
    }
}
