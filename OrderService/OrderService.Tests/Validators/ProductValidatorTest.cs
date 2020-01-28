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
    public class ProductValidatorTest
    {
        [TestMethod]
        public void CanAddProduct_NullProduct_ThrowsException() {
            // Arrange
            ProductValidator productValidator = new ProductValidator();
            Product product = null;
            // Act
            // Assert
            Assert.ThrowsException<ArgumentNullException>(() => productValidator.CanAddProduct(product));
        }

        [TestMethod]
        public void CanAddProduct_ProductHasEmptyName_ReturnErrorMessage()
        {
            // Arrange
            ProductValidator productValidator = new ProductValidator();
            Product product = new Product();
            product.Description = "Description";
            product.Price = 123.45m;
            // Act
            List<KeyValuePair<string, string>> result = productValidator.CanAddProduct(product);

            // Assert
            Assert.IsNotNull(result);
            Assert.AreEqual(1, result.Count);
            Assert.AreEqual("Name", result[0].Key);
        }

        [TestMethod]
        public void CanAddProduct_ProductHasEmptyDescription_ReturnErrorMessage()
        {
            // Arrange
            ProductValidator productValidator = new ProductValidator();
            Product product = new Product();
            product.Name = "Name";
            product.Price = 123.45m;
            // Act
            List<KeyValuePair<string, string>> result = productValidator.CanAddProduct(product);

            // Assert
            Assert.IsNotNull(result);
            Assert.AreEqual(1, result.Count);
            Assert.AreEqual("Description", result[0].Key);
        }

        [TestMethod]
        public void CanAddProduct_ProductHasEmptyPrice_ReturnErrorMessage()
        {
            // Arrange
            ProductValidator productValidator = new ProductValidator();
            Product product = new Product();
            product.Name = "Name";
            product.Description = "Description";
            // Act
            List<KeyValuePair<string, string>> result = productValidator.CanAddProduct(product);

            // Assert
            Assert.IsNotNull(result);
            Assert.AreEqual(1, result.Count);
            Assert.AreEqual("Price", result[0].Key);
        }

        [TestMethod]
        public void CanAddProduct_RateNotNull_ReturnErrorMessage()
        {
            // Arrange
            ProductValidator productValidator = new ProductValidator();
            Product product = new Product();
            product.Name = "Name";
            product.Description = "Description";
            product.Price = 123.45m;
            product.Rates = new List<Rate>();
            // Act
            List<KeyValuePair<string, string>> result = productValidator.CanAddProduct(product);

            // Assert
            Assert.IsNotNull(result);
            Assert.AreEqual(1, result.Count);
            Assert.AreEqual("", result[0].Key);
        }


        [TestMethod]
        public void CanUpdateProduct_NullProduct_ThrowsException()
        {
            // Arrange
            ProductValidator productValidator = new ProductValidator();
            Product product = null;
            // Act
            // Assert
            Assert.ThrowsException<ArgumentNullException>(() => productValidator.CanUpdateProduct(product));
        }

        [TestMethod]
        public void CanUpdateProduct_ProductHasEmptyName_ReturnErrorMessage()
        {
            // Arrange
            ProductValidator productValidator = new ProductValidator();
            Product product = new Product();
            product.Description = "Description";
            product.Price = 123.45m;
            // Act
            List<KeyValuePair<string, string>> result = productValidator.CanUpdateProduct(product);

            // Assert
            Assert.IsNotNull(result);
            Assert.AreEqual(1, result.Count);
            Assert.AreEqual("Name", result[0].Key);
        }

        [TestMethod]
        public void CanUpdateProduct_ProductHasEmptyDescription_ReturnErrorMessage()
        {
            // Arrange
            ProductValidator productValidator = new ProductValidator();
            Product product = new Product();
            product.Name = "Name";
            product.Price = 123.45m;
            // Act
            List<KeyValuePair<string, string>> result = productValidator.CanUpdateProduct(product);

            // Assert
            Assert.IsNotNull(result);
            Assert.AreEqual(1, result.Count);
            Assert.AreEqual("Description", result[0].Key);
        }

        [TestMethod]
        public void CanUpdateProduct_ProductHasEmptyPrice_ReturnErrorMessage()
        {
            // Arrange
            ProductValidator productValidator = new ProductValidator();
            Product product = new Product();
            product.Name = "Name";
            product.Description = "Description";
            // Act
            List<KeyValuePair<string, string>> result = productValidator.CanUpdateProduct(product);

            // Assert
            Assert.IsNotNull(result);
            Assert.AreEqual(1, result.Count);
            Assert.AreEqual("Price", result[0].Key);
        }

    }
}
