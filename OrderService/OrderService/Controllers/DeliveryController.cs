using OrderService.DataAccess.Handlers;
using OrderService.DataAccess.Providers;
using OrderService.DataAccess.Validators;
using OrderService.Models;
using System;
using System.Collections.Generic;
using System.Web.Mvc;

namespace OrderService.Controllers
{
    public class DeliveryController : Controller
    {
        private readonly IDeliveryProvider deliveryProvider;
        private readonly IDeliveryHandler deliveryHandler;
        private readonly IDeliveryValidator deliveryValidator;
        public DeliveryController( IDeliveryProvider deliveryProvider, IDeliveryHandler deliveryHandler, IDeliveryValidator deliveryValidator ) {
            this.deliveryProvider = deliveryProvider;
            this.deliveryHandler = deliveryHandler;
            this.deliveryValidator = deliveryValidator;
        }

        public ActionResult Index() {
            return GetDeliveriesList();
        }

        public ActionResult GetDeliveriesList() {
            var deliveries = this.deliveryProvider.GetAll();
            return View( "GetDeliveriesList", deliveries );
        }

        public ActionResult GetDelivery( int id ) {
            var delivery = this.deliveryProvider.GetById( id );
            return View( "GetDelivery", delivery );
        }


        public ActionResult AddDelivery( Delivery delivery ) {
            return View( "AddDelivery", delivery );
        }

        [HttpPost]
        public ActionResult AddDeliveryPost( Delivery delivery ) {
            try {
                List<KeyValuePair<string, string>> errors = deliveryValidator.CanAddDelivery( delivery );
                if( errors.Count == 0 ) {
                    int id = deliveryHandler.Add( delivery );
                    if( id > 0 ) {
                        return RedirectToAction( "GetDeliveriesList" );
                    } else {
                        this.ModelState.AddModelError( "", "Something got wrong when adding delivery. Delivery not added!" );
                        return AddDelivery( delivery );
                    }
                } else {
                    foreach( KeyValuePair<string, string> error in errors ) {
                        this.ModelState.AddModelError( error.Key, error.Value );
                    }
                    return AddDelivery( delivery );
                }
            }
            catch( Exception ex ) {
                this.ModelState.AddModelError( "", ex.Message );
                if( ex.InnerException != null ) {
                    this.ModelState.AddModelError( "", ex.InnerException.Message );
                }
                return AddDelivery( delivery );
            }
        }


        public ActionResult UpdateDelivery( int id ) {
            var delivery = this.deliveryProvider.GetById( id );
            return View( "UpdateDelivery", delivery );
        }

        [HttpPost]
        public ActionResult UpdateDeliveryPost( Delivery delivery ) {
            try {
                List<KeyValuePair<string, string>> errors = deliveryValidator.CanUpdateDelivery( delivery );
                if( errors.Count == 0 ) {
                    bool result = deliveryHandler.Update( delivery );
                    if( result ) {
                        return RedirectToAction( "GetDeliveriesList" );
                    } else {
                        this.ModelState.AddModelError( "", "Something got wrong when update delivery. Delivery not updated!" );
                        return View( "UpdateDelivery", delivery );
                        ;
                    }
                } else {
                    foreach( KeyValuePair<string, string> error in errors ) {
                        this.ModelState.AddModelError( error.Key, error.Value );
                    }
                    return View("UpdateDelivery", delivery );
                }
            }
            catch( Exception ex ) {
                this.ModelState.AddModelError( "", ex.Message );
                if( ex.InnerException != null ) {
                    this.ModelState.AddModelError( "", ex.InnerException.Message );
                }
                return View( "UpdateDelivery", delivery );
            }
        }

        [HttpPost]
        public ActionResult DeleteDeliveryPost( int id ) {
            try {
                bool result = deliveryHandler.Delete( id );
                if( result ) {
                    return RedirectToAction( "GetDeliveriesList" );
                } else {
                    this.ModelState.AddModelError( "", "Something got wrong when delete delivery. Delivery not deleted!" );
                    return GetDeliveriesList();
                }
            }
            catch( Exception ex ) {
                this.ModelState.AddModelError( "", ex.Message );
                if( ex.InnerException != null ) {
                    this.ModelState.AddModelError( "", ex.InnerException.Message );
                }
                return GetDeliveriesList();
            }
        }

    }
}