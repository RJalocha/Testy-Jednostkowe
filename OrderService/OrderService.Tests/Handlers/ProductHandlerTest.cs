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
    public class ProductHandlerTest
    {
        // DB operation are done on different db file - it's loacted in bin/debug directory

        [TestMethod]
        public void Add_ProductNull_ThrowsException()
        {
            // Arrange
            ProductHandler productHandler = new ProductHandler();
            Product product = null;
            // Act
            // Assert
            Assert.ThrowsException<ArgumentNullException>(() => productHandler.Add(product));
        }

        [TestMethod]
        public void Add_ProductCorrectEntity_ReturnId()
        {
            // Arrange
            ProductProvider productProvider = new ProductProvider();
            var allProducts = productProvider.GetAll().OrderByDescending(p => p.Id);
            Product lastProduct = allProducts.FirstOrDefault() ?? new Product();

            ProductHandler productHandler = new ProductHandler();
            Product product = new Product();
            product.Name = "Name";
            product.Description = "Description";
            product.Price = 123.45m;
            // Act
            int id = productHandler.Add(product);
            // Assert
            Assert.IsTrue(lastProduct.Id < id);
        }


        [TestMethod]
        public void Update_ProductNull_ThrowsException()
        {
            // Arrange
            ProductHandler productHandler = new ProductHandler();
            Product product = null;
            // Act
            // Assert
            Assert.ThrowsException<TargetException>(() => productHandler.Update(product));
        }

        [TestMethod]
        public void Update_ProductCorrectEntity_ReturnTrueAndNewEntityHasCorrectSetFields()
        {
            // Arrange
            ProductProvider productProvider = new ProductProvider();
            var oldProducts = productProvider.GetById(1);

            ProductHandler productHandler = new ProductHandler();
            Product product = new Product();
            product.Id = 1;
            product.Name = "NEW_Name";
            product.Description = "NEW_Description";
            product.Price = 999.99m;
            // Act
            bool result = productHandler.Update(product);
            var newProducts = productProvider.GetById(1);
            // Assert
            Assert.IsTrue(result);
            Assert.AreEqual("NEW_Name", newProducts.Name);
            Assert.AreEqual("NEW_Description", newProducts.Description);
            Assert.AreEqual(999.99m, newProducts.Price);
        }



        [TestMethod]
        public void Delete_ProductExists_ReturnTrue()
        {
            // Arrange
            ProductProvider productProvider = new ProductProvider();
            var allProducts = productProvider.GetAll().OrderByDescending(p => p.Id);
            Product lastProduct = allProducts.FirstOrDefault() ?? new Product();
            ProductHandler productHandler = new ProductHandler();

            // Act
            bool result = productHandler.Delete(lastProduct.Id);
            var deletedProduct = productProvider.GetById(lastProduct.Id);
            // Assert
            Assert.IsTrue(result);
            Assert.IsNull(deletedProduct);
        }
    }
}
