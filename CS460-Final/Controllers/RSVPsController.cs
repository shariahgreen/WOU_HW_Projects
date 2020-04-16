using System;
using System.Collections.Generic;
using System.Data;
using System.Data.Entity;
using System.Linq;
using System.Net;
using System.Web;
using System.Web.Mvc;
using CS460_Final.DAL;
using CS460_Final.Models;

namespace CS460_Final.Controllers
{
    public class RSVPsController : Controller
    {
        private ManagementContext db = new ManagementContext();

        // GET: RSVPs
        public ActionResult Index()
        {
            var rSVPs = db.RSVPs.Include(r => r.Event1).Include(r => r.Person1);
            return View(rSVPs.ToList());
        }

        // GET: RSVPs/Details/5
        public ActionResult Details(int? id)
        {
            if (id == null)
            {
                return new HttpStatusCodeResult(HttpStatusCode.BadRequest);
            }
            RSVP rSVP = db.RSVPs.Find(id);
            if (rSVP == null)
            {
                return HttpNotFound();
            }
            return View(rSVP);
        }

        // GET: RSVPs/Create
        public ActionResult Create()
        {
            ViewBag.Event = new SelectList(db.Events, "ID", "Title");
            var people = new List<KeyValuePair<string, int>>();
            var pp = db.Persons.OrderBy(x => x.Last);
            foreach (var person in pp)
            {
                var name = person.First + " " + person.Last;
                people.Add(new KeyValuePair<string, int>(name, person.ID));
            }

            ViewBag.Person = new SelectList(
                people.Select(x => new { Text = x.Key, Value = x.Value }),
                "Value",
                "Text"
                );
            return View();
        }

        // POST: RSVPs/Create
        // To protect from overposting attacks, please enable the specific properties you want to bind to, for 
        // more details see https://go.microsoft.com/fwlink/?LinkId=317598.
        [HttpPost]
        [ValidateAntiForgeryToken]
        public ActionResult Create([Bind(Include = "ID,Event,Person,Timestamp")] RSVP rSVP)
        {
            if (ModelState.IsValid)
            {
                db.RSVPs.Add(rSVP);
                db.SaveChanges();
                return RedirectToAction("Index");
            }

            ViewBag.Event = new SelectList(db.Events, "ID", "Title", rSVP.Event);
            ViewBag.Person = new SelectList(db.Persons, "ID", "First", rSVP.Person);
            return View(rSVP);
        }

        // GET: RSVPs/Edit/5
        public ActionResult Edit(int? id)
        {
            if (id == null)
            {
                return new HttpStatusCodeResult(HttpStatusCode.BadRequest);
            }
            RSVP rSVP = db.RSVPs.Find(id);
            if (rSVP == null)
            {
                return HttpNotFound();
            }
            ViewBag.Event = new SelectList(db.Events, "ID", "Title", rSVP.Event);
            ViewBag.Person = new SelectList(db.Persons, "ID", "First", rSVP.Person);
            return View(rSVP);
        }

        // POST: RSVPs/Edit/5
        // To protect from overposting attacks, please enable the specific properties you want to bind to, for 
        // more details see https://go.microsoft.com/fwlink/?LinkId=317598.
        [HttpPost]
        [ValidateAntiForgeryToken]
        public ActionResult Edit([Bind(Include = "ID,Event,Person,Timestamp")] RSVP rSVP)
        {
            if (ModelState.IsValid)
            {
                db.Entry(rSVP).State = EntityState.Modified;
                db.SaveChanges();
                return RedirectToAction("Index");
            }
            ViewBag.Event = new SelectList(db.Events, "ID", "Title", rSVP.Event);
            ViewBag.Person = new SelectList(db.Persons, "ID", "First", rSVP.Person);
            return View(rSVP);
        }

        // GET: RSVPs/Delete/5
        public ActionResult Delete(int? id)
        {
            if (id == null)
            {
                return new HttpStatusCodeResult(HttpStatusCode.BadRequest);
            }
            RSVP rSVP = db.RSVPs.Find(id);
            if (rSVP == null)
            {
                return HttpNotFound();
            }
            return View(rSVP);
        }

        // POST: RSVPs/Delete/5
        [HttpPost, ActionName("Delete")]
        [ValidateAntiForgeryToken]
        public ActionResult DeleteConfirmed(int id)
        {
            RSVP rSVP = db.RSVPs.Find(id);
            db.RSVPs.Remove(rSVP);
            db.SaveChanges();
            return RedirectToAction("Index");
        }

        public ActionResult GetRSVP(int Event)
        {
            var rsvps = new List<int>();
            var r = db.RSVPs.Where(x => x.Event1.ID == Event);
            foreach (var x in r)
            {
                rsvps.Add(1);
            }
            return Json(rsvps, JsonRequestBehavior.AllowGet);
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
