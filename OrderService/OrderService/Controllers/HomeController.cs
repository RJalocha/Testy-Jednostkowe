using OrderService.Models;
using System.Collections.Generic;
using System.Linq;
using System.Web.Mvc;

namespace OrderService.Controllers
{
    public class HomeController : Controller
    {
        public ActionResult Index() {
            return View( "Index" );
        }
    }
}