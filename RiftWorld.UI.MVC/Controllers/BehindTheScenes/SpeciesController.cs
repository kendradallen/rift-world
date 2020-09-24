using System;
using System.Collections.Generic;
using System.Data;
using System.Data.Entity;
using System.Linq;
using System.Net;
using System.Web;
using System.Web.Mvc;
using RiftWorld.DATA.EF;

namespace RiftWorld.UI.MVC.Controllers.BehindTheScenes
{
    [Authorize(Roles ="Admin")]
    public class SpeciesController : Controller
    {
        private RiftWorldEntities db = new RiftWorldEntities();

        // GET: Species
        public ActionResult Index()
        {
            return View(db.Races.ToList());
        }

        // GET: Species/Details/5
        public ActionResult Details(byte? id)
        {
            if (id == null)
            {
                return new HttpStatusCodeResult(HttpStatusCode.BadRequest);
            }
            Race race = db.Races.Find(id);
            if (race == null)
            {
                return HttpNotFound();
            }
            return View(race);
        }

        // GET: Species/Create
        public ActionResult Create()
        {
            return View();
        }

        // POST: Species/Create
        // To protect from overposting attacks, please enable the specific properties you want to bind to, for 
        // more details see https://go.microsoft.com/fwlink/?LinkId=317598.
        [HttpPost]
        [ValidateAntiForgeryToken]
        public ActionResult Create([Bind(Include = "RaceId,RaceName,IsPlayerEnabled")] Race race)
        {
            if (race.RaceName != null)
            {
                race.RaceName = race.RaceName.Trim();
            }
            bool uniqueCheck = db.Races.Any(x => x.RaceName == race.RaceName);
            if (uniqueCheck)
            {
                ModelState.AddModelError("RaceName", "");
            }

            if (ModelState.IsValid)
            {
                db.Races.Add(race);
                db.SaveChanges();
                return RedirectToAction("CreateWhat", "Infos");
            }

            //if model invalid, return trimmed name and remake the errors 
            ModelState.Clear();
            if (race.RaceName == null)
            {
                ModelState.AddModelError("RaceName", "Making the un-knowable species is not a good plan. This is how we get the Dweller. We don't make the un-knowable species!");
            }
            else if (race.RaceName.Length > 15)
            {
                ModelState.AddModelError("RaceName", "Well... this is awkward. So, I figured species names would never be more than 15 characters... guess I'm wrong. So, either contact me for a fix or figure out how to shorten it.");
            }
            else if (uniqueCheck)
            {
                ModelState.AddModelError("RaceName", "That exact species already exists. It'd be wasteful and confusing to allow it to be made again.");
            }
            ModelState.AddModelError("", "Something went wrong. If you don't see any red trying submiting again; you maybe just had a bunch of spaces in something");
            return View(race);
        }

        // GET: Species/Edit/5
        public ActionResult Edit(byte? id)
        {
            if (id == null)
            {
                return new HttpStatusCodeResult(HttpStatusCode.BadRequest);
            }
            Race race = db.Races.Find(id);
            if (race == null)
            {
                return HttpNotFound();
            }
            return View(race);
        }

        // POST: Species/Edit/5
        // To protect from overposting attacks, please enable the specific properties you want to bind to, for 
        // more details see https://go.microsoft.com/fwlink/?LinkId=317598.
        [HttpPost]
        [ValidateAntiForgeryToken]
        public ActionResult Edit([Bind(Include = "RaceId,RaceName,IsPlayerEnabled")] Race race)
        {
            if (race.RaceName != null)
            {
                race.RaceName = race.RaceName.Trim();
            }
            bool uniqueCheck = db.Races.Any(x => x.RaceName == race.RaceName && x.RaceId != race.RaceId);
            if (uniqueCheck)
            {
                ModelState.AddModelError("RaceName", "");
            }

            if (ModelState.IsValid)
            {
                db.Entry(race).State = EntityState.Modified;
                db.SaveChanges();
                return RedirectToAction("Index");
            }

            //if model invalid, return trimmed name and remake the errors 
            ModelState.Clear();
            if (race.RaceName == null)
            {
                ModelState.AddModelError("RaceName", "Making the un-knowable species is not a good plan. This is how we get the Dweller. We don't make the un-knowable species!");
            }
            else if (race.RaceName.Length > 15)
            {
                ModelState.AddModelError("RaceName", "Well... this is awkward. So, I figured species names would never be more than 15 characters... guess I'm wrong. So, either contact me for a fix or figure out how to shorten it.");
            }
            else if (uniqueCheck)
            {
                ModelState.AddModelError("RaceName", "That exact species already exists. It'd be wasteful and confusing to allow it to be made again.");
            }
            ModelState.AddModelError("", "Something went wrong. If you don't see any red trying submiting again; you maybe just had a bunch of spaces in something");
            return View(race);
        }

        // GET: Species/Delete/5
        public ActionResult Delete(byte? id)
        {
            if (id == null)
            {
                return new HttpStatusCodeResult(HttpStatusCode.BadRequest);
            }
            Race race = db.Races.Find(id);
            if (race == null)
            {
                return HttpNotFound();
            }

            int characters = db.Characters.Where(x => x.RaceId == id).Select(x => x.CharacterId).ToList().Count;
            int npcs = db.NPCs.Where(x => x.RaceId == id).Select(x => x.NpcId).ToList().Count;
            if (characters != 0 || npcs != 0)
            {
                ViewBag.Message = "Something in the database is using this species currently. You can't delete a species unless nothing is using it. You'll have find the entries using the species and change them first.";
                return View("Error");
            }
            return View(race);
        }

        // POST: Species/Delete/5
        [HttpPost, ActionName("Delete")]
        [ValidateAntiForgeryToken]
        public ActionResult DeleteConfirmed(byte id)
        {
            Race race = db.Races.Find(id);
            db.Races.Remove(race);
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
