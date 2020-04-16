using System;
using System.Collections.Generic;
using System.Drawing;
using System.Linq;
using System.Web;
using System.Web.Mvc;

namespace HW4.Controllers
{
    public class HomeController : Controller
    {
        public ActionResult Index()
        {
            return View();
        }

        [HttpGet]
        public ActionResult RGBColor(int? red, int? green, int? blue)
        {            
            ViewBag.Message = "Your RGB color application page";

            if (red != null && green != null && blue != null)
            {
                int r = red.GetValueOrDefault();
                int g = green.GetValueOrDefault();
                int b = blue.GetValueOrDefault();
                Color newColor = Color.FromArgb(r,g,b);
                string htmlcolor = ColorTranslator.ToHtml(newColor);
                ViewBag.Color = htmlcolor;
                ViewBag.Success = true;
            }
            else{}
            return View();
        }

    }
}