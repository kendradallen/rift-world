using System;
using System.Collections.Generic;
using System.Data;
using System.Data.Entity;
using System.Linq;
using System.Net;
using System.Web;
using System.Web.Mvc;
using RiftWorld.DATA.EF;

namespace RiftWorld.UI.MVC.Controllers.Entities
{
    public class NPCsController : Controller
    {
        private RiftWorldEntities db = new RiftWorldEntities();

        // GET: NPCs
        public ActionResult Index()
        {
            var nPCs = db.NPCs.Include(n => n.Info).Include(n => n.Locale).Include(n => n.Race);
            return View(nPCs.ToList());
        }

        // GET: NPCs/Details/5
        public ActionResult Details(short? id)
        {
            if (id == null)
            {
                return new HttpStatusCodeResult(HttpStatusCode.BadRequest);
            }
            NPC nPC = db.NPCs.Find(id);
            if (nPC == null)
            {
                return HttpNotFound();
            }
            return View(nPC);
        }

        // GET: NPCs/Create
        public ActionResult Create()
        {
            ViewBag.InfoId = new SelectList(db.Infos, "InfoId", "Blurb");
            ViewBag.LastLocationId = new SelectList(db.Locales, "LocaleId", "Name");
            ViewBag.RaceId = new SelectList(db.Races, "RaceId", "RaceName");
            return View();
        }

        // POST: NPCs/Create
        // To protect from overposting attacks, please enable the specific properties you want to bind to, for 
        // more details see https://go.microsoft.com/fwlink/?LinkId=317598.
        [HttpPost]
        [ValidateAntiForgeryToken]
        public ActionResult Create([Bind(Include = "NpcId,InfoId,Name,Alias,Quote,PortraitFileName,RaceId,CrestFileName,ApperanceText,AboutText,LastLocationId,IsWorkInProgress")] NPC nPC)
        {
            if (ModelState.IsValid)
            {
                db.NPCs.Add(nPC);
                db.SaveChanges();
                return RedirectToAction("Index");
            }

            ViewBag.InfoId = new SelectList(db.Infos, "InfoId", "Blurb", nPC.InfoId);
            ViewBag.LastLocationId = new SelectList(db.Locales, "LocaleId", "Name", nPC.LastLocationId);
            ViewBag.RaceId = new SelectList(db.Races, "RaceId", "RaceName", nPC.RaceId);
            return View(nPC);
        }

        // GET: NPCs/Edit/5
        public ActionResult Edit(short? id)
        {
            if (id == null)
            {
                return new HttpStatusCodeResult(HttpStatusCode.BadRequest);
            }
            NPC nPC = db.NPCs.Find(id);
            if (nPC == null)
            {
                return HttpNotFound();
            }
            ViewBag.InfoId = new SelectList(db.Infos, "InfoId", "Blurb", nPC.InfoId);
            ViewBag.LastLocationId = new SelectList(db.Locales, "LocaleId", "Name", nPC.LastLocationId);
            ViewBag.RaceId = new SelectList(db.Races, "RaceId", "RaceName", nPC.RaceId);
            return View(nPC);
        }

        // POST: NPCs/Edit/5
        // To protect from overposting attacks, please enable the specific properties you want to bind to, for 
        // more details see https://go.microsoft.com/fwlink/?LinkId=317598.
        [HttpPost]
        [ValidateAntiForgeryToken]
        public ActionResult Edit([Bind(Include = "NpcId,InfoId,Name,Alias,Quote,PortraitFileName,RaceId,CrestFileName,ApperanceText,AboutText,LastLocationId,IsWorkInProgress")] NPC nPC)
        {
            if (ModelState.IsValid)
            {
                db.Entry(nPC).State = EntityState.Modified;
                db.SaveChanges();
                return RedirectToAction("Index");
            }
            ViewBag.InfoId = new SelectList(db.Infos, "InfoId", "Blurb", nPC.InfoId);
            ViewBag.LastLocationId = new SelectList(db.Locales, "LocaleId", "Name", nPC.LastLocationId);
            ViewBag.RaceId = new SelectList(db.Races, "RaceId", "RaceName", nPC.RaceId);
            return View(nPC);
        }

        // GET: NPCs/Delete/5
        public ActionResult Delete(short? id)
        {
            if (id == null)
            {
                return new HttpStatusCodeResult(HttpStatusCode.BadRequest);
            }
            NPC nPC = db.NPCs.Find(id);
            if (nPC == null)
            {
                return HttpNotFound();
            }
            return View(nPC);
        }

        // POST: NPCs/Delete/5
        [HttpPost, ActionName("Delete")]
        [ValidateAntiForgeryToken]
        public ActionResult DeleteConfirmed(short id)
        {
            NPC nPC = db.NPCs.Find(id);
            db.NPCs.Remove(nPC);
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
