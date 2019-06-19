using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;
using System.Web.Mvc;

namespace azurecosmosdb.Controllers
{
    public class HomeController : Controller
    {
        public ActionResult Index()
        {
            return View();
        }

        public ActionResult About()
        {
            ViewBag.Message = "edited Your application description page.";

            return View();
        }

        public ActionResult Contact()
        {
            ViewBag.Message = " change from kalyan branch Your contact page.";

            return View();
        }
    }
}