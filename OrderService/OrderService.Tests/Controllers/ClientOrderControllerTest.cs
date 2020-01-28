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
    public class ClientOrderControllerTest
    {
        List<ClientOrder> clientOrders = new List<ClientOrder>()
        {
            new ClientOrder() { Id=1, ClientId = "c983fea1-a550-4ea6-b45a-db66d9defcc1", DeliveryId = 1, CreationDate = DateTime.Now, DeliveryDate = DateTime.Now, Orders = new List<Order>() },
            new ClientOrder() { Id=2, ClientId = "c983fea1-a550-4ea6-b45a-db66d9defcc1", DeliveryId = 2, CreationDate = DateTime.Now, DeliveryDate = DateTime.Now, Orders = new List<Order>() },
        };

        [TestMethod]
        public void Index_ReturnsCorrectViewResult() {
            // Arrange
            Mock<IClientOrderProvider> clientOrderProviderMock = new Mock<IClientOrderProvider>();
            clientOrderProviderMock.Setup(x => x.GetAll())
                .Returns(clientOrders);
            Mock<IClientOrderHandler> clientOrderHandlerMock = new Mock<IClientOrderHandler>();
            Mock<IClientOrderValidator> clientOrderValidatorMock = new Mock<IClientOrderValidator>();
            Mock<IUserProvider> userProviderMock = new Mock<IUserProvider>();
            Mock<IDeliveryProvider> deliveryProviderMock = new Mock<IDeliveryProvider>();
            Mock<IProductProvider> productProviderMock = new Mock<IProductProvider>();

            ClientOrderController controller = new ClientOrderController(clientOrderProviderMock.Object, 
                clientOrderHandlerMock.Object, 
                clientOrderValidatorMock.Object,
                userProviderMock.Object,
                deliveryProviderMock.Object,
                productProviderMock.Object
                );
            // Act
            ViewResult result = controller.Index() as ViewResult;
            // Assert
            Assert.IsNotNull( result );
            Assert.IsTrue(((List<ClientOrder>)result.Model).Count == 2);
            Assert.AreEqual("GetClientOrderList", result.ViewName);
        }

        [TestMethod]
        public void GetClientOrdersList_ReturnsCorrectViewResult()
        {
            // Arrange
            Mock<IClientOrderProvider> clientOrderProviderMock = new Mock<IClientOrderProvider>();
            clientOrderProviderMock.Setup(x => x.GetAll())
                .Returns(clientOrders);
            Mock<IClientOrderHandler> clientOrderHandlerMock = new Mock<IClientOrderHandler>();
            Mock<IClientOrderValidator> clientOrderValidatorMock = new Mock<IClientOrderValidator>();
            Mock<IUserProvider> userProviderMock = new Mock<IUserProvider>();
            Mock<IDeliveryProvider> deliveryProviderMock = new Mock<IDeliveryProvider>();
            Mock<IProductProvider> productProviderMock = new Mock<IProductProvider>();

            ClientOrderController controller = new ClientOrderController(clientOrderProviderMock.Object,
                clientOrderHandlerMock.Object,
                clientOrderValidatorMock.Object,
                userProviderMock.Object,
                deliveryProviderMock.Object,
                productProviderMock.Object
                );
            // Act
            ViewResult result = controller.GetClientOrderList() as ViewResult;
            // Assert
            Assert.IsNotNull(result);
            Assert.IsTrue(((List<ClientOrder>)result.Model).Count == 2);
            Assert.AreEqual("GetClientOrderList", result.ViewName);
        }

        [TestMethod]
        public void GetClientOrder_ReturnsCorrectViewResult()
        {
            // Arrange
            Mock<IClientOrderProvider> clientOrderProviderMock = new Mock<IClientOrderProvider>();
            clientOrderProviderMock.Setup(x => x.GetById(1))
                .Returns(clientOrders[0]);
            Mock<IClientOrderHandler> clientOrderHandlerMock = new Mock<IClientOrderHandler>();
            Mock<IClientOrderValidator> clientOrderValidatorMock = new Mock<IClientOrderValidator>();
            Mock<IUserProvider> userProviderMock = new Mock<IUserProvider>();
            Mock<IDeliveryProvider> deliveryProviderMock = new Mock<IDeliveryProvider>();
            Mock<IProductProvider> productProviderMock = new Mock<IProductProvider>();

            ClientOrderController controller = new ClientOrderController(clientOrderProviderMock.Object,
                clientOrderHandlerMock.Object,
                clientOrderValidatorMock.Object,
                userProviderMock.Object,
                deliveryProviderMock.Object,
                productProviderMock.Object
                );
            // Act
            ViewResult result = controller.GetClientOrder(1) as ViewResult;
            // Assert
            Assert.IsNotNull(result);
            Assert.IsTrue(((ClientOrder)result.Model).Id != default(int));
            Assert.AreEqual("GetClientOrder", result.ViewName);
        }

        [TestMethod]
        public void AddProduct_ReturnsCorrectViewResult()
        {
            Mock<IClientOrderProvider> clientOrderProviderMock = new Mock<IClientOrderProvider>();
            Mock<IClientOrderHandler> clientOrderHandlerMock = new Mock<IClientOrderHandler>();
            Mock<IClientOrderValidator> clientOrderValidatorMock = new Mock<IClientOrderValidator>();
            Mock<IUserProvider> userProviderMock = new Mock<IUserProvider>();
            userProviderMock.Setup(x => x.GetAll()).Returns(new List<ApplicationUser>());
            Mock<IDeliveryProvider> deliveryProviderMock = new Mock<IDeliveryProvider>();
            deliveryProviderMock.Setup(x => x.GetAll()).Returns(new List<Delivery>());
            Mock<IProductProvider> productProviderMock = new Mock<IProductProvider>();
            productProviderMock.Setup(x => x.GetAll()).Returns(new List<Product>());

            ClientOrderController controller = new ClientOrderController(clientOrderProviderMock.Object,
                clientOrderHandlerMock.Object,
                clientOrderValidatorMock.Object,
                userProviderMock.Object,
                deliveryProviderMock.Object,
                productProviderMock.Object
                );
            // Act
            ViewResult result = controller.AddClientOrder(null) as ViewResult;
            // Assert
            Assert.IsNotNull(result);
            Assert.AreEqual("AddClientOrder", result.ViewName);
        }


        [TestMethod]
        public void AddOrder_ReturnsCorrectPartialViewResult()
        {
            // Arrange
            Mock<IClientOrderProvider> clientOrderProviderMock = new Mock<IClientOrderProvider>();
            Mock<IClientOrderHandler> clientOrderHandlerMock = new Mock<IClientOrderHandler>();
            Mock<IClientOrderValidator> clientOrderValidatorMock = new Mock<IClientOrderValidator>();
            Mock<IUserProvider> userProviderMock = new Mock<IUserProvider>();
            Mock<IDeliveryProvider> deliveryProviderMock = new Mock<IDeliveryProvider>();
            Mock<IProductProvider> productProviderMock = new Mock<IProductProvider>();

            ClientOrderController controller = new ClientOrderController(clientOrderProviderMock.Object,
                clientOrderHandlerMock.Object,
                clientOrderValidatorMock.Object,
                userProviderMock.Object,
                deliveryProviderMock.Object,
                productProviderMock.Object
                );
            // Act
            PartialViewResult result = controller.AddOrder(null) as PartialViewResult;
            // Assert
            Assert.IsNotNull(result);
            Assert.AreEqual("_AddOrderPartial", result.ViewName);
        }

        [TestMethod]
        public void UpdateClientOrder_ReturnsCorrectViewResult()
        {
            // Arrange
            Mock<IClientOrderProvider> clientOrderProviderMock = new Mock<IClientOrderProvider>();
            clientOrderProviderMock.Setup(x => x.GetById(1))
                .Returns(clientOrders[0]);
            Mock<IClientOrderHandler> clientOrderHandlerMock = new Mock<IClientOrderHandler>();
            Mock<IClientOrderValidator> clientOrderValidatorMock = new Mock<IClientOrderValidator>();
            Mock<IUserProvider> userProviderMock = new Mock<IUserProvider>();
            userProviderMock.Setup(x => x.GetAll()).Returns(new List<ApplicationUser>());
            Mock<IDeliveryProvider> deliveryProviderMock = new Mock<IDeliveryProvider>();
            deliveryProviderMock.Setup(x => x.GetAll()).Returns(new List<Delivery>());
            Mock<IProductProvider> productProviderMock = new Mock<IProductProvider>();
            productProviderMock.Setup(x => x.GetAll()).Returns(new List<Product>());

            ClientOrderController controller = new ClientOrderController(clientOrderProviderMock.Object,
                clientOrderHandlerMock.Object,
                clientOrderValidatorMock.Object,
                userProviderMock.Object,
                deliveryProviderMock.Object,
                productProviderMock.Object
                );
            // Act
            ViewResult result = controller.UpdateClientOrder(1) as ViewResult;
            // Assert
            Assert.IsNotNull(result);
            Assert.IsTrue(((ClientOrder)result.Model).Id != default(int));
            Assert.AreEqual("UpdateClientOrder", result.ViewName);
        }


        [TestMethod]
        public void AddClientOrderPost_NullClientOrder_ReturnsCorrectViewResult()
        {
            // Arrange
            Mock<IClientOrderProvider> clientOrderProviderMock = new Mock<IClientOrderProvider>();
            Mock<IClientOrderHandler> clientOrderHandlerMock = new Mock<IClientOrderHandler>();
            Mock<IClientOrderValidator> clientOrderValidatorMock = new Mock<IClientOrderValidator>();
            clientOrderValidatorMock.Setup(x => x.CanAddClientOrder(null))
                .Throws<ArgumentNullException>();
            Mock<IUserProvider> userProviderMock = new Mock<IUserProvider>();
            userProviderMock.Setup(x => x.GetAll()).Returns(new List<ApplicationUser>());
            Mock<IDeliveryProvider> deliveryProviderMock = new Mock<IDeliveryProvider>();
            deliveryProviderMock.Setup(x => x.GetAll()).Returns(new List<Delivery>());
            Mock<IProductProvider> productProviderMock = new Mock<IProductProvider>();
            productProviderMock.Setup(x => x.GetAll()).Returns(new List<Product>());

            ClientOrderController controller = new ClientOrderController(clientOrderProviderMock.Object,
                clientOrderHandlerMock.Object,
                clientOrderValidatorMock.Object,
                userProviderMock.Object,
                deliveryProviderMock.Object,
                productProviderMock.Object
                );

            // Act
            ViewResult result = controller.AddClientOrderPost(null) as ViewResult;
            // Assert
            Assert.IsNotNull(result);
            Assert.IsFalse(controller.ModelState.IsValid);
            Assert.AreEqual(1, controller.ModelState.Keys.Count);
            Assert.AreEqual("", controller.ModelState.Keys.FirstOrDefault());
            Assert.AreEqual("AddClientOrder", result.ViewName);
        }

        [TestMethod]
        public void AddClientOrderPost_CorrectProduct_ReturnsCorrectRedirectToRouteResult()
        {
            // Arrange
            Mock<IClientOrderProvider> clientOrderProviderMock = new Mock<IClientOrderProvider>();
            Mock<IClientOrderHandler> clientOrderHandlerMock = new Mock<IClientOrderHandler>();
            clientOrderHandlerMock.Setup(x => x.Add(It.IsAny<ClientOrder>()))
                .Returns(1);
            Mock<IClientOrderValidator> clientOrderValidatorMock = new Mock<IClientOrderValidator>();
            clientOrderValidatorMock.Setup(x => x.CanAddClientOrder(It.IsAny<ClientOrder>()))
                .Returns(new List<KeyValuePair<string, string>>());
            Mock<IUserProvider> userProviderMock = new Mock<IUserProvider>();
            userProviderMock.Setup(x => x.GetAll()).Returns(new List<ApplicationUser>());
            Mock<IDeliveryProvider> deliveryProviderMock = new Mock<IDeliveryProvider>();
            deliveryProviderMock.Setup(x => x.GetAll()).Returns(new List<Delivery>());
            Mock<IProductProvider> productProviderMock = new Mock<IProductProvider>();
            productProviderMock.Setup(x => x.GetAll()).Returns(new List<Product>());

            ClientOrderController controller = new ClientOrderController(clientOrderProviderMock.Object,
                clientOrderHandlerMock.Object,
                clientOrderValidatorMock.Object,
                userProviderMock.Object,
                deliveryProviderMock.Object,
                productProviderMock.Object
                );
            // Act
            JsonResult result = controller.AddClientOrderPost(this.clientOrders[0]) as JsonResult;
            // Assert
            Assert.IsNotNull(result);
        }


        [TestMethod]
        public void UpdateProductPost_NullProdut_ReturnsCorrectViewResult()
        {
            // Arrange
            Mock<IClientOrderProvider> clientOrderProviderMock = new Mock<IClientOrderProvider>();
            Mock<IClientOrderHandler> clientOrderHandlerMock = new Mock<IClientOrderHandler>();
            Mock<IClientOrderValidator> clientOrderValidatorMock = new Mock<IClientOrderValidator>();
            clientOrderValidatorMock.Setup(x => x.CanAddClientOrder(null))
                .Throws<ArgumentNullException>();
            Mock<IUserProvider> userProviderMock = new Mock<IUserProvider>();
            userProviderMock.Setup(x => x.GetAll()).Returns(new List<ApplicationUser>());
            Mock<IDeliveryProvider> deliveryProviderMock = new Mock<IDeliveryProvider>();
            deliveryProviderMock.Setup(x => x.GetAll()).Returns(new List<Delivery>());
            Mock<IProductProvider> productProviderMock = new Mock<IProductProvider>();
            productProviderMock.Setup(x => x.GetAll()).Returns(new List<Product>());

            ClientOrderController controller = new ClientOrderController(clientOrderProviderMock.Object,
                clientOrderHandlerMock.Object,
                clientOrderValidatorMock.Object,
                userProviderMock.Object,
                deliveryProviderMock.Object,
                productProviderMock.Object
                );
            // Act

            ViewResult result = controller.UpdateClientOrderPost(null) as ViewResult;
            // Assert
            Assert.IsNotNull(result);
            Assert.IsFalse(controller.ModelState.IsValid);
            Assert.AreEqual(1, controller.ModelState.Keys.Count);
            Assert.AreEqual("", controller.ModelState.Keys.FirstOrDefault());
            Assert.AreEqual("UpdateClientOrder", result.ViewName);
        }

        [TestMethod]
        public void UpdateProductPost_CorrectProduct_ReturnsCorrectViewRedirectToRouteResult()
        {
            // Arrange
            Mock<IClientOrderProvider> clientOrderProviderMock = new Mock<IClientOrderProvider>();
            Mock<IClientOrderHandler> clientOrderHandlerMock = new Mock<IClientOrderHandler>();
            clientOrderHandlerMock.Setup(x => x.Update(It.IsAny<ClientOrder>()))
                .Returns(true);
            Mock<IClientOrderValidator> clientOrderValidatorMock = new Mock<IClientOrderValidator>();
            clientOrderValidatorMock.Setup(x => x.CanUpdateClientOrder(It.IsAny<ClientOrder>()))
                .Returns(new List<KeyValuePair<string, string>>());
            Mock<IUserProvider> userProviderMock = new Mock<IUserProvider>();
            userProviderMock.Setup(x => x.GetAll()).Returns(new List<ApplicationUser>());
            Mock<IDeliveryProvider> deliveryProviderMock = new Mock<IDeliveryProvider>();
            deliveryProviderMock.Setup(x => x.GetAll()).Returns(new List<Delivery>());
            Mock<IProductProvider> productProviderMock = new Mock<IProductProvider>();
            productProviderMock.Setup(x => x.GetAll()).Returns(new List<Product>());

            ClientOrderController controller = new ClientOrderController(clientOrderProviderMock.Object,
                clientOrderHandlerMock.Object,
                clientOrderValidatorMock.Object,
                userProviderMock.Object,
                deliveryProviderMock.Object,
                productProviderMock.Object
                );
            // Act
            JsonResult result = controller.UpdateClientOrderPost(this.clientOrders[0]) as JsonResult;
            // Assert
            Assert.IsNotNull(result);
        }


        [TestMethod]
        public void DeleteClientOrderPost_CorrectClientOrder_ReturnsCorrectViewResult()
        {
            // Arrange
            Mock<IClientOrderProvider> clientOrderProviderMock = new Mock<IClientOrderProvider>();
            Mock<IClientOrderHandler> clientOrderHandlerMock = new Mock<IClientOrderHandler>();
            clientOrderHandlerMock.Setup(x => x.Delete(It.IsAny<int>()))
                .Returns(true);
            Mock<IClientOrderValidator> clientOrderValidatorMock = new Mock<IClientOrderValidator>();
            Mock<IUserProvider> userProviderMock = new Mock<IUserProvider>();
            userProviderMock.Setup(x => x.GetAll()).Returns(new List<ApplicationUser>());
            Mock<IDeliveryProvider> deliveryProviderMock = new Mock<IDeliveryProvider>();
            deliveryProviderMock.Setup(x => x.GetAll()).Returns(new List<Delivery>());
            Mock<IProductProvider> productProviderMock = new Mock<IProductProvider>();
            productProviderMock.Setup(x => x.GetAll()).Returns(new List<Product>());

            ClientOrderController controller = new ClientOrderController(clientOrderProviderMock.Object,
                clientOrderHandlerMock.Object,
                clientOrderValidatorMock.Object,
                userProviderMock.Object,
                deliveryProviderMock.Object,
                productProviderMock.Object
                );

            // Act
            RedirectToRouteResult result = controller.DeleteClientOrderPost(1) as RedirectToRouteResult;
            // Assert
            Assert.IsNotNull(result);
            Assert.AreEqual("GetClientOrderList", result.RouteValues.FirstOrDefault().Value);
        }

    }
}
