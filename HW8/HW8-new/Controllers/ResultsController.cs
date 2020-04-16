using System;
using System.Collections.Generic;
using System.Data;
using System.Data.Entity;
using System.Linq;
using System.Net;
using System.Web;
using System.Web.Mvc;
using HW8_new.DAL;
using HW8_new.Models;

namespace HW8_new.Controllers
{
    public class ResultsController : Controller
    {
        private DatabaseContext db = new DatabaseContext();


        public ActionResult AthleteResults(int? Athlete)
        {
            IEnumerable<Result> results = db.Results.Where(x => x.AthleteID == Athlete).OrderBy(x => x.Meet.MeetDate);

            var athletes = new List<KeyValuePair<string, int>>();
            foreach (var athlete in db.Athletes)
            {
                athletes.Add(new KeyValuePair<string, int>(athlete.AthleteName, athlete.ID));
            }

            SelectList athletelist = new SelectList(
                athletes.Select(x => new { Text = x.Key, Value = x.Value }),
                "Value",
                "Text"
             );

            ViewBag.athletelist = athletelist;

            return View(results);
        }

        public ActionResult RaceResults()
        {
            var teams = new List<KeyValuePair<string, int>>();
            foreach (var team in db.Teams)
            {
                teams.Add(new KeyValuePair<string, int>(team.TeamName, team.ID));
            }

            SelectList teamlist = new SelectList(
                teams.Select(x => new { Text = x.Key, Value = x.Value }),
                "Value",
                "Text"
             );

            ViewBag.teamlist = teamlist;

            //if (Team != null)
            //{
            //var athletes = new List<KeyValuePair<string, int>>();
            //foreach (var athlete in db.Athletes.Where(x => x.TeamID == Team))
            //{
            //athletes.Add(new KeyValuePair<string, int>(athlete.AthleteName, athlete.ID));
            //}

            //SelectList athletelist = new SelectList(
            //athletes.Select(x => new { Text = x.Key, Value = x.Value }),
            //"Value",
            //"Text"
            //);

            //ViewBag.athletelist = athletelist;
            //}



            var races = new List<KeyValuePair<string, int>>();
            foreach (var race in db.Races)
            {
                races.Add(new KeyValuePair<string, int>(race.RaceName, race.ID));
            }

            SelectList racelist = new SelectList(
                races.Select(x => new { Text = x.Key, Value = x.Value }),
                "Value",
                "Text"
             );

            ViewBag.racelist = racelist;


            return View();
            //return Json(results, JsonRequestBehavior.AllowGet);
        }

        public ActionResult getAthleteByTeamID(int Team)
        {
            var athletes = new List<KeyValuePair<string, int>>();
            foreach (var athlete in db.Athletes.Where(x => x.TeamID == Team))
            {
                athletes.Add(new KeyValuePair<string, int>(athlete.AthleteName, athlete.ID));
            }

            SelectList athletelist = new SelectList(
            athletes.Select(x => new { Text = x.Key, Value = x.Value }),
            "Value",
            "Text"
            );

            return Json(athletelist, JsonRequestBehavior.AllowGet);
        }

        struct resultItem
        {
            public string meetDate;
            public int num;
            public double raceTime;
            public string meetLocation;
        }

        public ActionResult GetResultsByAthlete(int Athlete, int Race)
        {
            var results = new List<resultItem>();
            int i = 0;
            foreach (var result in db.Results.Where(x => x.RaceID == Race && x.AthleteID == Athlete).OrderBy(x => x.Meet.MeetDate))
            {
                results.Add(new resultItem { meetDate = result.Meet.MeetDate.ToString(), num = i, raceTime = Math.Round(result.RaceTime, 2), meetLocation = result.Meet.MeetLocation });
                i++;
            }
            return Json(results, JsonRequestBehavior.AllowGet);
        }

        // GET: Results
        public ActionResult Index()
        {
            var results = db.Results.Include(r => r.Athlete).Include(r => r.Meet).Include(r => r.Race);
            return View(results.ToList());
        }

        // GET: Results/Details/5
        public ActionResult Details(int? id)
        {
            if (id == null)
            {
                return new HttpStatusCodeResult(HttpStatusCode.BadRequest);
            }
            Result result = db.Results.Find(id);
            if (result == null)
            {
                return HttpNotFound();
            }
            return View(result);
        }

        // GET: Results/Create
        public ActionResult Create()
        {
            ViewBag.AthleteID = new SelectList(db.Athletes, "ID", "AthleteName");
            ViewBag.MeetID = new SelectList(db.Meets, "ID", "MeetLocation");
            ViewBag.RaceID = new SelectList(db.Races, "ID", "RaceName");
            return View();
        }

        // POST: Results/Create
        // To protect from overposting attacks, please enable the specific properties you want to bind to, for 
        // more details see https://go.microsoft.com/fwlink/?LinkId=317598.
        [HttpPost]
        [ValidateAntiForgeryToken]
        public ActionResult Create([Bind(Include = "ID,RaceID,MeetID,RaceTime,AthleteID")] Result result)
        {
            if (ModelState.IsValid)
            {
                db.Results.Add(result);
                db.SaveChanges();
                return RedirectToAction("Index");
            }

            ViewBag.AthleteID = new SelectList(db.Athletes, "ID", "AthleteName", result.AthleteID);
            ViewBag.MeetID = new SelectList(db.Meets, "ID", "MeetLocation", result.MeetID);
            ViewBag.RaceID = new SelectList(db.Races, "ID", "RaceName", result.RaceID);
            return View(result);
        }

        // GET: Results/Edit/5
        public ActionResult Edit(int? id)
        {
            if (id == null)
            {
                return new HttpStatusCodeResult(HttpStatusCode.BadRequest);
            }
            Result result = db.Results.Find(id);
            if (result == null)
            {
                return HttpNotFound();
            }
            ViewBag.AthleteID = new SelectList(db.Athletes, "ID", "AthleteName", result.AthleteID);
            ViewBag.MeetID = new SelectList(db.Meets, "ID", "MeetLocation", result.MeetID);
            ViewBag.RaceID = new SelectList(db.Races, "ID", "RaceName", result.RaceID);
            return View(result);
        }

        // POST: Results/Edit/5
        // To protect from overposting attacks, please enable the specific properties you want to bind to, for 
        // more details see https://go.microsoft.com/fwlink/?LinkId=317598.
        [HttpPost]
        [ValidateAntiForgeryToken]
        public ActionResult Edit([Bind(Include = "ID,RaceID,MeetID,RaceTime,AthleteID")] Result result)
        {
            if (ModelState.IsValid)
            {
                db.Entry(result).State = EntityState.Modified;
                db.SaveChanges();
                return RedirectToAction("Index");
            }
            ViewBag.AthleteID = new SelectList(db.Athletes, "ID", "AthleteName", result.AthleteID);
            ViewBag.MeetID = new SelectList(db.Meets, "ID", "MeetLocation", result.MeetID);
            ViewBag.RaceID = new SelectList(db.Races, "ID", "RaceName", result.RaceID);
            return View(result);
        }

        // GET: Results/Delete/5
        public ActionResult Delete(int? id)
        {
            if (id == null)
            {
                return new HttpStatusCodeResult(HttpStatusCode.BadRequest);
            }
            Result result = db.Results.Find(id);
            if (result == null)
            {
                return HttpNotFound();
            }
            return View(result);
        }

        // POST: Results/Delete/5
        [HttpPost, ActionName("Delete")]
        [ValidateAntiForgeryToken]
        public ActionResult DeleteConfirmed(int id)
        {
            Result result = db.Results.Find(id);
            db.Results.Remove(result);
            db.SaveChanges();
            return RedirectToAction("Index");
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
