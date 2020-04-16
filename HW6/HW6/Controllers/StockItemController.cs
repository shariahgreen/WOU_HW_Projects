using System;
using System.Collections.Generic;
using System.Data;
using System.Data.Entity;
using System.Linq;
using System.Net;
using System.Web;
using System.Web.Mvc;
using HW6.DAL;
using HW6.Models;
using HW6.Models.ViewModels;

namespace HW6.Controllers
{
    public class StockItemController : Controller
    {
        private WorldWideImportersContext db = new WorldWideImportersContext();

        // GET: StockItems
        public ActionResult Index(string search)
        {
            var searchResults = db.StockItems.Where(x => x.StockItemName.Contains(search)).ToList();
            return View(searchResults);
        }

        // GET: WorldWideImporters/Details/5
        public ActionResult Details(int? id)
        {
            if (id == null)
            {
                return new HttpStatusCodeResult(HttpStatusCode.BadRequest);
            }
            StockItem stockItem = db.StockItems.Find(id);

            if (stockItem == null)
            {
                return HttpNotFound();
            }
            StockItemDetailsViewModel viewModel = new StockItemDetailsViewModel(stockItem);
            return View(viewModel);
        }

        protected override void Dispose(bool disposing)
        {
            if (disposing)
            {
                db.Dispose();
            }
            base.Dispose(disposing);
        }
    }
}
