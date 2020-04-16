using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;
using System.Web.Mvc;

namespace HW8.Controllers
{
    public class HomeController : Controller
    {
        public ActionResult Index()
        {
            return View();
        }

        public ActionResult Results()
        {
            ViewBag.Message = "Your application \"Display Results\" page.";
            return View();
        }

        public ActionResult AddNew()
        {
            ViewBag.Message = "Your application \"Add New\" page.";
            return View();
        }
    }
}