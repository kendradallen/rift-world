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
    public class RumorsController : Controller
    {
        private RiftWorldEntities db = new RiftWorldEntities();


        //get Rumors
        public PartialViewResult _Rumors(short id)
        {
            var rumors = db.Rumors.Where(r => r.RumorOfId == id).ToList() ;

            return PartialView(rumors);
        }


        // GET: Rumors
        public ActionResult Index()
        {
            var rumors = db.Rumors.Include(r => r.Character).Include(r => r.Info);
            return View(rumors.ToList());
        }

        // GET: Rumors/Details/5
        public ActionResult Details(int? id)
        {
            if (id == null)
            {
                return new HttpStatusCodeResult(HttpStatusCode.BadRequest);
            }
            Rumor rumor = db.Rumors.Find(id);
            if (rumor == null)
            {
                return HttpNotFound();
            }
            return View(rumor);
        }

        // GET: Rumors/Create
        public ActionResult Create()
        {
            ViewBag.AuthorId = new SelectList(db.Characters, "CharacterId", "PlayerId");
            ViewBag.RumorOfId = new SelectList(db.Infos, "InfoId", "Blurb");
            return View();
        }

        // POST: Rumors/Create
        // To protect from overposting attacks, please enable the specific properties you want to bind to, for 
        // more details see https://go.microsoft.com/fwlink/?LinkId=317598.
        [HttpPost]
        [ValidateAntiForgeryToken]
        public ActionResult Create([Bind(Include = "RumorsId,RumorOfId,AuthorId,IsApproved,TheContent")] Rumor rumor)
        {
            if (ModelState.IsValid)
            {
                db.Rumors.Add(rumor);
                db.SaveChanges();
                return RedirectToAction("Index");
            }

            ViewBag.AuthorId = new SelectList(db.Characters, "CharacterId", "PlayerId", rumor.AuthorId);
            ViewBag.RumorOfId = new SelectList(db.Infos, "InfoId", "Blurb", rumor.RumorOfId);
            return View(rumor);
        }

        // GET: Rumors/Edit/5
        public ActionResult Edit(int? id)
        {
            if (id == null)
            {
                return new HttpStatusCodeResult(HttpStatusCode.BadRequest);
            }
            Rumor rumor = db.Rumors.Find(id);
            if (rumor == null)
            {
                return HttpNotFound();
            }
            ViewBag.AuthorId = new SelectList(db.Characters, "CharacterId", "PlayerId", rumor.AuthorId);
            ViewBag.RumorOfId = new SelectList(db.Infos, "InfoId", "Blurb", rumor.RumorOfId);
            return View(rumor);
        }

        // POST: Rumors/Edit/5
        // To protect from overposting attacks, please enable the specific properties you want to bind to, for 
        // more details see https://go.microsoft.com/fwlink/?LinkId=317598.
        [HttpPost]
        [ValidateAntiForgeryToken]
        public ActionResult Edit([Bind(Include = "RumorsId,RumorOfId,AuthorId,IsApproved,TheContent")] Rumor rumor)
        {
            if (ModelState.IsValid)
            {
                db.Entry(rumor).State = EntityState.Modified;
                db.SaveChanges();
                return RedirectToAction("Index");
            }
            ViewBag.AuthorId = new SelectList(db.Characters, "CharacterId", "PlayerId", rumor.AuthorId);
            ViewBag.RumorOfId = new SelectList(db.Infos, "InfoId", "Blurb", rumor.RumorOfId);
            return View(rumor);
        }

        // GET: Rumors/Delete/5
        public ActionResult Delete(int? id)
        {
            if (id == null)
            {
                return new HttpStatusCodeResult(HttpStatusCode.BadRequest);
            }
            Rumor rumor = db.Rumors.Find(id);
            if (rumor == null)
            {
                return HttpNotFound();
            }
            return View(rumor);
        }

        // POST: Rumors/Delete/5
        [HttpPost, ActionName("Delete")]
        [ValidateAntiForgeryToken]
        public ActionResult DeleteConfirmed(int id)
        {
            Rumor rumor = db.Rumors.Find(id);
            db.Rumors.Remove(rumor);
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
