using OrderService.DataAccess.Handlers;
using OrderService.DataAccess.Providers;
using OrderService.DataAccess.Validators;
using OrderService.Helpers;
using OrderService.Models;
using OrderService.ViewModels;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Web.Mvc;

namespace OrderService.Controllers
{
    public class ClientOrderController : Controller
    {
        private readonly IClientOrderProvider clientOrderProvider;
        private readonly IClientOrderHandler clientOrderHandler;
        private readonly IClientOrderValidator clientOrderValidator;

        private readonly IUserProvider userProvider;
        private readonly IDeliveryProvider deliveryProvider;
        private readonly IProductProvider productProvider;
        public ClientOrderController( IClientOrderProvider clientOrderProvider,
            IClientOrderHandler clientOrderHandler,
            IClientOrderValidator clientOrderValidator,
            IUserProvider userProvider,
            IDeliveryProvider deliveryProvider,
            IProductProvider productProvider
            ) {
            this.clientOrderProvider = clientOrderProvider;
            this.clientOrderHandler = clientOrderHandler;
            this.clientOrderValidator = clientOrderValidator;
            this.userProvider = userProvider;
            this.deliveryProvider = deliveryProvider;
            this.productProvider = productProvider;
        }

        public ActionResult Index() {
            return GetClientOrderList();
        }

        public ActionResult GetClientOrderList() {
            var clientOrders = this.clientOrderProvider.GetAll();
            return View( "GetClientOrderList", clientOrders );
        }

        public ActionResult GetClientOrder( int id ) {
            var delivery = this.clientOrderProvider.GetById( id );
            return View( "GetClientOrder", delivery );
        }


        public ActionResult AddClientOrder( ClientOrder clientOrder ) {
            FillViewBags();
            return View( "AddClientOrder", clientOrder );
        }
        [HttpPost]
        public ActionResult AddOrder( AddOrderPartialViewModel addOrderPartialViewModel ) {
            return PartialView( "_AddOrderPartial", addOrderPartialViewModel );
        }

        [HttpPost]
        public ActionResult AddClientOrderPost( ClientOrder clientOrder ) {
            try {
                List<KeyValuePair<string, string>> errors = clientOrderValidator.CanAddClientOrder( clientOrder );
                if( errors.Count == 0 ) {
                    int id = clientOrderHandler.Add( clientOrder );
                    if( id > 0 ) {
                        return new JsonResult() { Data = new { success = true } };
                    } else {
                        this.ModelState.AddModelError( "", "Something got wrong when adding client order. ClientOrder not added!" );
                        return AddClientOrder( clientOrder );
                    }
                } else {
                    foreach( KeyValuePair<string, string> error in errors ) {
                        this.ModelState.AddModelError( error.Key, error.Value );
                    }
                    return AddClientOrder( clientOrder );
                }
            }
            catch( Exception ex ) {
                this.ModelState.AddModelError( "", ex.Message );
                if( ex.InnerException != null ) {
                    this.ModelState.AddModelError( "", ex.InnerException.Message );
                }
                return AddClientOrder( clientOrder );
            }
        }


        public ActionResult UpdateClientOrder( int id ) {
            var clientOrder = this.clientOrderProvider.GetById( id );
            FillViewBags();
            return View( "UpdateClientOrder", clientOrder );
        }

        [HttpPost]
        public ActionResult UpdateClientOrderPost( ClientOrder clientOrder ) {
            try {
                List<KeyValuePair<string, string>> errors = clientOrderValidator.CanUpdateClientOrder( clientOrder );
                if( errors.Count == 0 ) {
                    bool result = clientOrderHandler.Update( clientOrder );
                    if( result ) {
                        return new JsonResult() { Data = new { success = true } };
                    } else {
                        this.ModelState.AddModelError( "", "Something got wrong when update client order. ClientOrder not updated!" );
                        FillViewBags();
                        return View( "UpdateClientOrder", clientOrder );
                    }
                } else {
                    foreach( KeyValuePair<string, string> error in errors ) {
                        this.ModelState.AddModelError( error.Key, error.Value );
                    }
                    FillViewBags();
                    return View( "UpdateClientOrder", clientOrder );
                }
            }
            catch( Exception ex ) {
                this.ModelState.AddModelError( "", ex.Message );
                if( ex.InnerException != null ) {
                    this.ModelState.AddModelError( "", ex.InnerException.Message );
                }
                FillViewBags();
                return View( "UpdateClientOrder", clientOrder );
            }
        }


        [HttpPost]
        public ActionResult DeleteClientOrderPost( int id ) {
            try {
                bool result = clientOrderHandler.Delete( id );
                if( result ) {
                    return RedirectToAction( "GetClientOrderList" );
                } else {
                    this.ModelState.AddModelError( "", "Something got wrong when delete delivery. ClientOrder not deleted!" );
                    return GetClientOrderList();
                }
            }
            catch( Exception ex ) {
                this.ModelState.AddModelError( "", ex.Message );
                if( ex.InnerException != null ) {
                    this.ModelState.AddModelError( "", ex.InnerException.Message );
                }
                return GetClientOrderList();
            }
        }

        void FillViewBags() {
            ViewBag.Clients = this.userProvider.GetAll().Select( u => new SelectListItem() {
                Value = u.Id, Text = u.UserName
            } ).ToList();
            ( (List<SelectListItem>)ViewBag.Clients ).Insert( 0, new SelectListItem() { Value = "", Text = "Select Client" } );

            ViewBag.Deliveries = this.deliveryProvider.GetAll().Select( d => new SelectListItem() {
                Value = d.Id.ToString(),
                Text = d.Name
            } ).ToList();
            ( (List<SelectListItem>)ViewBag.Deliveries ).Insert( 0, new SelectListItem() { Value = "", Text = "Select Delivery" } );

            ViewBag.Products = this.productProvider.GetAll().Select( p => new SelectListItem() {
                Value = p.Id.ToString(), Text = p.Name
            } ).ToList();
            ( (List<SelectListItem>)ViewBag.Products ).Insert( 0, new SelectListItem() { Value = "", Text = "Select Product" } );
        }
    }
}