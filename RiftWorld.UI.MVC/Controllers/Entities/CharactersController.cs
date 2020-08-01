using System;
using System.Collections.Generic;
using System.Data;
using System.Data.Entity;
using System.Linq;
using System.Net;
using System.Web;
using System.Web.Mvc;
using RiftWorld.DATA.EF;

namespace RiftWorld.UI.MVC.Controllers
{
    public class CharactersController : Controller
    {
        private RiftWorldEntities db = new RiftWorldEntities();

        // GET: Characters
        public ActionResult Index()
        {
            var characters = db.Characters.Include(c => c.Gender).Include(c => c.Locale).Include(c => c.Race).Include(c => c.Tier).Include(c => c.UserDetails);
            return View(characters.ToList());
        }

        // GET: Characters/Details/5
        public ActionResult Details(short? id)
        {
            if (id == null)
            {
                return new HttpStatusCodeResult(HttpStatusCode.BadRequest);
            }
            Character character = db.Characters.Find(id);
            if (character == null)
            {
                return HttpNotFound();
            }
            return View(character);
        }

        // GET: Characters/Create
        public ActionResult Create()
        {
            ViewBag.GenderId = new SelectList(db.Genders, "GenderId", "GenderName");
            ViewBag.CurrentLocationId = new SelectList(db.Locales, "LocaleId", "Name");
            ViewBag.RaceId = new SelectList(db.Races, "RaceId", "RaceName");
            ViewBag.TierId = new SelectList(db.Tiers, "TierId", "TierName");
            ViewBag.PlayerId = new SelectList(db.UserDetails, "UserId", "DiscordName");
            return View();
        }

        // POST: Characters/Create
        // To protect from overposting attacks, please enable the specific properties you want to bind to, for 
        // more details see https://go.microsoft.com/fwlink/?LinkId=317598.
        [HttpPost]
        [ValidateAntiForgeryToken]
        public ActionResult Create([Bind(Include = "CharacterId,PlayerId,CharacterName,RaceId,GenderId,PortraitFileName,Description,About,CurrentLocationId,TierId,IsRetired,IsApproved")] Character character)
        {
            if (ModelState.IsValid)
            {
                db.Characters.Add(character);
                db.SaveChanges();
                return RedirectToAction("Index");
            }

            ViewBag.GenderId = new SelectList(db.Genders, "GenderId", "GenderName", character.GenderId);
            ViewBag.CurrentLocationId = new SelectList(db.Locales, "LocaleId", "Name", character.CurrentLocationId);
            ViewBag.RaceId = new SelectList(db.Races, "RaceId", "RaceName", character.RaceId);
            ViewBag.TierId = new SelectList(db.Tiers, "TierId", "TierName", character.TierId);
            ViewBag.PlayerId = new SelectList(db.UserDetails, "UserId", "DiscordName", character.PlayerId);
            return View(character);
        }

        // GET: Characters/Edit/5
        public ActionResult Edit(short? id)
        {
            if (id == null)
            {
                return new HttpStatusCodeResult(HttpStatusCode.BadRequest);
            }
            Character character = db.Characters.Find(id);
            if (character == null)
            {
                return HttpNotFound();
            }
            ViewBag.GenderId = new SelectList(db.Genders, "GenderId", "GenderName", character.GenderId);
            ViewBag.CurrentLocationId = new SelectList(db.Locales, "LocaleId", "Name", character.CurrentLocationId);
            ViewBag.RaceId = new SelectList(db.Races, "RaceId", "RaceName", character.RaceId);
            ViewBag.TierId = new SelectList(db.Tiers, "TierId", "TierName", character.TierId);
            ViewBag.PlayerId = new SelectList(db.UserDetails, "UserId", "DiscordName", character.PlayerId);
            return View(character);
        }

        // POST: Characters/Edit/5
        // To protect from overposting attacks, please enable the specific properties you want to bind to, for 
        // more details see https://go.microsoft.com/fwlink/?LinkId=317598.
        [HttpPost]
        [ValidateAntiForgeryToken]
        public ActionResult Edit([Bind(Include = "CharacterId,PlayerId,CharacterName,RaceId,GenderId,PortraitFileName,Description,About,CurrentLocationId,TierId,IsRetired,IsApproved")] Character character)
        {
            if (ModelState.IsValid)
            {
                db.Entry(character).State = EntityState.Modified;
                db.SaveChanges();
                return RedirectToAction("Index");
            }
            ViewBag.GenderId = new SelectList(db.Genders, "GenderId", "GenderName", character.GenderId);
            ViewBag.CurrentLocationId = new SelectList(db.Locales, "LocaleId", "Name", character.CurrentLocationId);
            ViewBag.RaceId = new SelectList(db.Races, "RaceId", "RaceName", character.RaceId);
            ViewBag.TierId = new SelectList(db.Tiers, "TierId", "TierName", character.TierId);
            ViewBag.PlayerId = new SelectList(db.UserDetails, "UserId", "DiscordName", character.PlayerId);
            return View(character);
        }

        // GET: Characters/Delete/5
        public ActionResult Delete(short? id)
        {
            if (id == null)
            {
                return new HttpStatusCodeResult(HttpStatusCode.BadRequest);
            }
            Character character = db.Characters.Find(id);
            if (character == null)
            {
                return HttpNotFound();
            }
            return View(character);
        }

        // POST: Characters/Delete/5
        [HttpPost, ActionName("Delete")]
        [ValidateAntiForgeryToken]
        public ActionResult DeleteConfirmed(short id)
        {
            Character character = db.Characters.Find(id);
            db.Characters.Remove(character);
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
