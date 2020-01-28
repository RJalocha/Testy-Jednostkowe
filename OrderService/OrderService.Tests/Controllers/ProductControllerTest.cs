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
    public class ProductControllerTest
    {
        List<Product> products = new List<Product>()
        {
            new Product() { Id=1, Name="Delivery1", Description="Description1", Price = 1.00m },
            new Product() { Id=2, Name="Delivery2", Description="Description2", Price = 2.00m },
        };

        [TestMethod]
        public void Index_ReturnsCorrectViewResult() {
            // Arrange
            Mock<IProductProvider> productProviderMock = new Mock<IProductProvider>();
            productProviderMock.Setup(x => x.GetAll())
                .Returns(products);
            Mock<IProductHandler> productHandlerMock = new Mock<IProductHandler>();
            Mock<IProductValidator> productValidatorMock = new Mock<IProductValidator>();

            ProductController controller = new ProductController(productProviderMock.Object, productHandlerMock.Object, productValidatorMock.Object);
            // Act
            ViewResult result = controller.Index() as ViewResult;
            // Assert
            Assert.IsNotNull( result );
            Assert.IsTrue(((List<Product>)result.Model).Count == 2);
            Assert.AreEqual("GetProductList", result.ViewName);
        }

        [TestMethod]
        public void GetProductList_ReturnsCorrectViewResult()
        {
            // Arrange
            Mock<IProductProvider> productProviderMock = new Mock<IProductProvider>();
            productProviderMock.Setup(x => x.GetAll())
                .Returns(products);
            Mock<IProductHandler> productHandlerMock = new Mock<IProductHandler>();
            Mock<IProductValidator> productValidatorMock = new Mock<IProductValidator>();

            ProductController controller = new ProductController(productProviderMock.Object, productHandlerMock.Object, productValidatorMock.Object);
            // Act
            ViewResult result = controller.GetProductList() as ViewResult;
            // Assert
            Assert.IsNotNull(result);
            Assert.IsTrue(((List<Product>)result.Model).Count == 2);
            Assert.AreEqual("GetProductList", result.ViewName);
        }

        [TestMethod]
        public void GetProduct_ReturnsCorrectViewResult()
        {
            // Arrange
            Mock<IProductProvider> productProviderMock = new Mock<IProductProvider>();
            productProviderMock.Setup(x => x.GetById(1))
                .Returns(products[0]);
            Mock<IProductHandler> productHandlerMock = new Mock<IProductHandler>();
            Mock<IProductValidator> productValidatorMock = new Mock<IProductValidator>();

            ProductController controller = new ProductController(productProviderMock.Object, productHandlerMock.Object, productValidatorMock.Object);
            // Act
            ViewResult result = controller.GetProduct(1) as ViewResult;
            // Assert
            Assert.IsNotNull(result);
            Assert.IsTrue(((Product)result.Model).Id != default(int));
            Assert.AreEqual("GetProduct", result.ViewName);
        }

        [TestMethod]
        public void AddProduct_ReturnsCorrectViewResult()
        {
            // Arrange
            Mock<IProductProvider> productProviderMock = new Mock<IProductProvider>();
            Mock<IProductHandler> productHandlerMock = new Mock<IProductHandler>();
            Mock<IProductValidator> productValidatorMock = new Mock<IProductValidator>();

            ProductController controller = new ProductController(productProviderMock.Object, productHandlerMock.Object, productValidatorMock.Object);
            // Act
            ViewResult result = controller.AddProduct(null) as ViewResult;
            // Assert
            Assert.IsNotNull(result);
            Assert.AreEqual("AddProduct", result.ViewName);
        }

        [TestMethod]
        public void UpdateProduct_ReturnsCorrectViewResult()
        {
            // Arrange
            Mock<IProductProvider> productProviderMock = new Mock<IProductProvider>();
            productProviderMock.Setup(x => x.GetById(1))
                .Returns(products[0]);
            Mock<IProductHandler> productHandlerMock = new Mock<IProductHandler>();
            Mock<IProductValidator> productValidatorMock = new Mock<IProductValidator>();

            ProductController controller = new ProductController(productProviderMock.Object, productHandlerMock.Object, productValidatorMock.Object);
            // Act
            ViewResult result = controller.UpdateProduct(1) as ViewResult;
            // Assert
            Assert.IsNotNull(result);
            Assert.IsTrue(((Product)result.Model).Id != default(int));
            Assert.AreEqual("UpdateProduct", result.ViewName);
        }


        [TestMethod]
        public void AddProductPost_NullProdut_ReturnsCorrectViewResult()
        {
            // Arrange
            Mock<IProductProvider> productProviderMock = new Mock<IProductProvider>();
            Mock<IProductHandler> productHandlerMock = new Mock<IProductHandler>();
            Mock<IProductValidator> productValidatorMock = new Mock<IProductValidator>();
            productValidatorMock.Setup(x => x.CanAddProduct(null))
                .Throws<ArgumentNullException>();

            ProductController controller = new ProductController(productProviderMock.Object, productHandlerMock.Object, productValidatorMock.Object);
            // Act
            ViewResult result = controller.AddProductPost(null) as ViewResult;
            // Assert
            Assert.IsNotNull(result);
            Assert.IsFalse(controller.ModelState.IsValid);
            Assert.AreEqual(1, controller.ModelState.Keys.Count);
            Assert.AreEqual("", controller.ModelState.Keys.FirstOrDefault());
            Assert.AreEqual("AddProduct", result.ViewName);
        }

        [TestMethod]
        public void AddProductPost_CorrectProduct_ReturnsCorrectRedirectToRouteResult()
        {
            // Arrange
            Mock<IProductProvider> productProviderMock = new Mock<IProductProvider>();
            Mock<IProductHandler> productHandlerMock = new Mock<IProductHandler>();
            productHandlerMock.Setup(x => x.Add(It.IsAny<Product>()))
                .Returns(1);
            Mock<IProductValidator> productValidatorMock = new Mock<IProductValidator>();
            productValidatorMock.Setup(x => x.CanAddProduct(It.IsAny<Product>()))
                .Returns(new List<KeyValuePair<string, string>>());

            ProductController controller = new ProductController(productProviderMock.Object, productHandlerMock.Object, productValidatorMock.Object);
            // Act
            RedirectToRouteResult result = controller.AddProductPost(this.products[0]) as RedirectToRouteResult;
            // Assert
            Assert.IsNotNull(result);
            Assert.AreEqual("GetProductList", result.RouteValues.FirstOrDefault().Value);
        }


        [TestMethod]
        public void UpdateProductPost_NullProdut_ReturnsCorrectViewResult()
        {
            // Arrange
            Mock<IProductProvider> productProviderMock = new Mock<IProductProvider>();
            Mock<IProductHandler> productHandlerMock = new Mock<IProductHandler>();
            Mock<IProductValidator> productValidatorMock = new Mock<IProductValidator>();
            productValidatorMock.Setup(x => x.CanAddProduct(null))
                .Throws<ArgumentNullException>();

            ProductController controller = new ProductController(productProviderMock.Object, productHandlerMock.Object, productValidatorMock.Object);
            // Act
            ViewResult result = controller.UpdateProductPost(null) as ViewResult;
            // Assert
            Assert.IsNotNull(result);
            Assert.IsFalse(controller.ModelState.IsValid);
            Assert.AreEqual(1, controller.ModelState.Keys.Count);
            Assert.AreEqual("", controller.ModelState.Keys.FirstOrDefault());
            Assert.AreEqual("UpdateProduct", result.ViewName);
        }

        [TestMethod]
        public void UpdateProductPost_CorrectProduct_ReturnsCorrectViewRedirectToRouteResult()
        {
            // Arrange
            Mock<IProductProvider> productProviderMock = new Mock<IProductProvider>();
            Mock<IProductHandler> productHandlerMock = new Mock<IProductHandler>();
            productHandlerMock.Setup(x => x.Update(It.IsAny<Product>()))
                .Returns(true);
            Mock<IProductValidator> productValidatorMock = new Mock<IProductValidator>();
            productValidatorMock.Setup(x => x.CanUpdateProduct(It.IsAny<Product>()))
                .Returns(new List<KeyValuePair<string, string>>());

            ProductController controller = new ProductController(productProviderMock.Object, productHandlerMock.Object, productValidatorMock.Object);
            // Act
            RedirectToRouteResult result = controller.UpdateProductPost(this.products[0]) as RedirectToRouteResult;
            // Assert
            Assert.IsNotNull(result);
            Assert.AreEqual("GetProductList", result.RouteValues.FirstOrDefault().Value);
        }


        [TestMethod]
        public void DeleteProductPost_CorrectProduct_ReturnsCorrectViewResult()
        {
            // Arrange
            Mock<IProductProvider> productProviderMock = new Mock<IProductProvider>();
            Mock<IProductHandler> productHandlerMock = new Mock<IProductHandler>();
            productHandlerMock.Setup(x => x.Delete(1))
                .Returns(true);
            Mock<IProductValidator> productValidatorMock = new Mock<IProductValidator>();

            ProductController controller = new ProductController(productProviderMock.Object, productHandlerMock.Object, productValidatorMock.Object);
            // Act
            RedirectToRouteResult result = controller.DeleteProductPost(1) as RedirectToRouteResult;
            // Assert
            Assert.IsNotNull(result);
            Assert.AreEqual("GetProductList", result.RouteValues.FirstOrDefault().Value);
        }
    }
}
