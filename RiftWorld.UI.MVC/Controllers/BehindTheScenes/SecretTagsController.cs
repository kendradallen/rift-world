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
    public class SecretTagsController : Controller
    {
        private RiftWorldEntities db = new RiftWorldEntities();

        // GET: SecretTags
        public ActionResult Index()
        {
            return View(db.SecretTags.ToList());
        }

        // GET: SecretTags/Details/5
        public ActionResult Details(short? id)
        {
            if (id == null)
            {
                return new HttpStatusCodeResult(HttpStatusCode.BadRequest);
            }
            SecretTag secretTag = db.SecretTags.Find(id);
            if (secretTag == null)
            {
                return HttpNotFound();
            }
            return View(secretTag);
        }

        // GET: SecretTags/Create
        public ActionResult Create()
        {
            return View();
        }

        // POST: SecretTags/Create
        // To protect from overposting attacks, please enable the specific properties you want to bind to, for 
        // more details see https://go.microsoft.com/fwlink/?LinkId=317598.
        [HttpPost]
        [ValidateAntiForgeryToken]
        public ActionResult Create([Bind(Include = "SecretTagId,Name,Description")] SecretTag secretTag)
        {
            if (ModelState.IsValid)
            {
                db.SecretTags.Add(secretTag);
                db.SaveChanges();
                return RedirectToAction("Index");
            }

            return View(secretTag);
        }

        // GET: SecretTags/Edit/5
        public ActionResult Edit(short? id)
        {
            if (id == null)
            {
                return new HttpStatusCodeResult(HttpStatusCode.BadRequest);
            }
            SecretTag secretTag = db.SecretTags.Find(id);
            if (secretTag == null)
            {
                return HttpNotFound();
            }
            return View(secretTag);
        }

        // POST: SecretTags/Edit/5
        // To protect from overposting attacks, please enable the specific properties you want to bind to, for 
        // more details see https://go.microsoft.com/fwlink/?LinkId=317598.
        [HttpPost]
        [ValidateAntiForgeryToken]
        public ActionResult Edit([Bind(Include = "SecretTagId,Name,Description")] SecretTag secretTag)
        {
            if (ModelState.IsValid)
            {
                db.Entry(secretTag).State = EntityState.Modified;
                db.SaveChanges();
                return RedirectToAction("Index");
            }
            return View(secretTag);
        }

        // GET: SecretTags/Delete/5
        public ActionResult Delete(short? id)
        {
            if (id == null)
            {
                return new HttpStatusCodeResult(HttpStatusCode.BadRequest);
            }
            SecretTag secretTag = db.SecretTags.Find(id);
            if (secretTag == null)
            {
                return HttpNotFound();
            }
            return View(secretTag);
        }

        // POST: SecretTags/Delete/5
        [HttpPost, ActionName("Delete")]
        [ValidateAntiForgeryToken]
        public ActionResult DeleteConfirmed(short id)
        {
            SecretTag secretTag = db.SecretTags.Find(id);
            db.SecretTags.Remove(secretTag);
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
