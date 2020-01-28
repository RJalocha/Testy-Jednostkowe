using OrderService.DataAccess.Handlers;
using OrderService.DataAccess.Providers;
using OrderService.DataAccess.Validators;
using OrderService.Models;
using System;
using System.Collections.Generic;
using System.Web.Mvc;

namespace OrderService.Controllers
{
    public class ProductController : Controller
    {
        private readonly IProductProvider productProvider;
        private readonly IProductHandler productHandler;
        private readonly IProductValidator productValidator;
        public ProductController( IProductProvider productProvider, IProductHandler productHandler, IProductValidator productValidator ) {
            this.productProvider = productProvider;
            this.productHandler = productHandler;
            this.productValidator = productValidator;
        }

        public ActionResult Index() {
            return GetProductList();
        }

        public ActionResult GetProductList() {
            var products = this.productProvider.GetAll();
            return View( "GetProductList", products );
        }

        public ActionResult GetProduct( int id ) {
            var product = this.productProvider.GetById( id );
            return View( "GetProduct", product );
        }


        public ActionResult AddProduct( Product product ) {
            return View( "AddProduct", product );
        }

        [HttpPost]
        public ActionResult AddProductPost( Product product ) {
            try {
                List<KeyValuePair<string, string>> errors = productValidator.CanAddProduct( product );
                if( errors.Count == 0 ) {
                    int id = productHandler.Add( product );
                    if( id > 0 ) {
                        return RedirectToAction( "GetProductList" );
                    } else {
                        this.ModelState.AddModelError( "", "Something got wrong when adding product. Product not added!" );
                        return AddProduct( product );
                    }
                } else {
                    foreach( KeyValuePair<string, string> error in errors ) {
                        this.ModelState.AddModelError( error.Key, error.Value );
                    }
                    return AddProduct( product );
                }
            }
            catch( Exception ex ) {
                this.ModelState.AddModelError( "", ex.Message );
                if( ex.InnerException != null ) {
                    this.ModelState.AddModelError( "", ex.InnerException.Message );
                }
                return AddProduct( product );
            }
        }


        public ActionResult UpdateProduct( int id ) {
            var product = this.productProvider.GetById( id );
            return View( "UpdateProduct", product );
        }

        [HttpPost]
        public ActionResult UpdateProductPost( Product product ) {
            try {
                List<KeyValuePair<string, string>> errors = productValidator.CanUpdateProduct( product );
                if( errors.Count == 0 ) {
                    bool result = productHandler.Update( product );
                    if( result ) {
                        return RedirectToAction( "GetProductList" );
                    } else {
                        this.ModelState.AddModelError( "", "Something got wrong when update product. Product not updated!" );
                        return View( "UpdateProduct", product );
                        ;
                    }
                } else {
                    foreach( KeyValuePair<string, string> error in errors ) {
                        this.ModelState.AddModelError( error.Key, error.Value );
                    }
                    return View( "UpdateProduct", product );
                }
            }
            catch( Exception ex ) {
                this.ModelState.AddModelError( "", ex.Message );
                if( ex.InnerException != null ) {
                    this.ModelState.AddModelError( "", ex.InnerException.Message );
                }
                return View( "UpdateProduct", product );
            }
        }

        [HttpPost]
        public ActionResult DeleteProductPost( int id ) {
            try {
                bool result = productHandler.Delete( id );
                if( result ) {
                    return RedirectToAction( "GetProductList" );
                } else {
                    this.ModelState.AddModelError( "", "Something got wrong when delete product. Product not deleted!" );
                    return GetProductList();
                }
            }
            catch( Exception ex ) {
                this.ModelState.AddModelError( "", ex.Message );
                if( ex.InnerException != null ) {
                    this.ModelState.AddModelError( "", ex.InnerException.Message );
                }
                return GetProductList();
            }
        }
    }
}