using System;
using System.Collections.Generic;
using System.Drawing;
using System.Linq;
using System.Web;
using System.Web.Mvc;

namespace HW4.Controllers
{
    public class ColorInterpolatorController : Controller
    {
        // GET: ColorInterpolator
        [HttpGet]
        public ActionResult Create()
        {
            return View();
        }

        [HttpPost]
        public ActionResult Create(string col1, string col2, int? num)
        {
            ViewBag.Message = "Your color interpolation application page"; 
            if(ModelState.IsValid)
            {
                int number = num.GetValueOrDefault();
                if (number == 0)
                {
                    return RedirectToAction("Create", "ColorInterpolator");
                }
                Color color1 = ColorTranslator.FromHtml(col1);
                Color color2 = ColorTranslator.FromHtml(col2);

                double h1, h2, s1, s2, v1, v2;

                ColorToHSV(color1, out h1, out s1, out v1);
                ColorToHSV(color2, out h2, out s2, out v2);

                double hStep = (h2 - h1) / number;
                double sStep = (s2 - s1) / number;
                double vStep = (v2 - v1) / number;

                IList<string> colorList = new List<string>();
                for (int i = 0; i < number; i ++)
                {
                    Color newColor = ColorFromHSV(h1 + i * hStep, s1 + i * sStep, v1 + i * vStep);
                    string htmlcolor = ColorTranslator.ToHtml(newColor);

                    colorList.Add(htmlcolor);
                }
                ViewBag.ColorList = colorList;
                ViewBag.Success = true;
            }
            else
            {
                return RedirectToAction("Create", "ColorInterpolator");
            }
            return View();
        }

        public static void ColorToHSV(Color color, out double hue, out double saturation, out double value)
        {
            int max = Math.Max(color.R, Math.Max(color.G, color.B));
            int min = Math.Min(color.R, Math.Min(color.G, color.B));

            hue = color.GetHue();
            saturation = (max == 0) ? 0 : 1d - (1d * min / max);
            value = max / 255d;
        }

        public static Color ColorFromHSV(double hue, double saturation, double value)
        {
            int hi = Convert.ToInt32(Math.Floor(hue / 60)) % 6;
            double f = hue / 60 - Math.Floor(hue / 60);

            value = value * 255;
            int v = Convert.ToInt32(value);
            int p = Convert.ToInt32(value * (1 - saturation));
            int q = Convert.ToInt32(value * (1 - f * saturation));
            int t = Convert.ToInt32(value * (1 - (1 - f) * saturation));

            if (hi == 0)
                return Color.FromArgb(255, v, t, p);
            else if (hi == 1)
                return Color.FromArgb(255, q, v, p);
            else if (hi == 2)
                return Color.FromArgb(255, p, v, t);
            else if (hi == 3)
                return Color.FromArgb(255, p, q, v);
            else if (hi == 4)
                return Color.FromArgb(255, t, p, v);
            else
                return Color.FromArgb(255, v, p, q);
        }
    }
}