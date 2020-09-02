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
    [Authorize(Roles = "Admin")]
    public class GendersController : Controller
    {
        private RiftWorldEntities db = new RiftWorldEntities();

        // GET: Genders
        public ActionResult Index()
        {
            return View(db.Genders.ToList());
        }

        // GET: Genders/Details/5
        //public ActionResult Details(byte? id)
        //{
        //    if (id == null)
        //    {
        //        return new HttpStatusCodeResult(HttpStatusCode.BadRequest);
        //    }
        //    Gender gender = db.Genders.Find(id);
        //    if (gender == null)
        //    {
        //        return HttpNotFound();
        //    }
        //    return View(gender);
        //}

        // GET: Genders/Create
        public ActionResult Create()
        {
            return View();
        }

        // POST: Genders/Create
        // To protect from overposting attacks, please enable the specific properties you want to bind to, for 
        // more details see https://go.microsoft.com/fwlink/?LinkId=317598.
        [HttpPost]
        [ValidateAntiForgeryToken]
        public ActionResult Create([Bind(Include = "GenderId,GenderName")] Gender gender)
        {
            if (ModelState.IsValid)
            {
                db.Genders.Add(gender);
                db.SaveChanges();
                return RedirectToAction("CreateWhat", "Infos");
            }
            ModelState.AddModelError("", "There is preciesly ONE place that something could have gone wrong.");

            return View(gender);
        }

        // GET: Genders/Edit/5
        public ActionResult Edit(byte? id)
        {
            if (id == null)
            {
                return new HttpStatusCodeResult(HttpStatusCode.BadRequest);
            }
            Gender gender = db.Genders.Find(id);
            if (gender == null)
            {
                return HttpNotFound();
            }
            return View(gender);
        }

        // POST: Genders/Edit/5
        // To protect from overposting attacks, please enable the specific properties you want to bind to, for 
        // more details see https://go.microsoft.com/fwlink/?LinkId=317598.
        [HttpPost]
        [ValidateAntiForgeryToken]
        public ActionResult Edit([Bind(Include = "GenderId,GenderName")] Gender gender)
        {
            if (ModelState.IsValid)
            {
                db.Entry(gender).State = EntityState.Modified;
                db.SaveChanges();
                return RedirectToAction("Index");
            }
            return View(gender);
        }

        // GET: Genders/Delete/5
        public ActionResult Delete(byte? id)
        {
            if (id == null)
            {
                return new HttpStatusCodeResult(HttpStatusCode.BadRequest);
            }
            Gender gender = db.Genders.Find(id);
            if (gender == null)
            {
                return HttpNotFound();
            }
            int characters = db.Characters.Where(x => x.GenderId == id).Select(x => x.CharacterId).ToList().Count;
            int npcs = db.NPCs.Where(x => x.GenderId == id).Select(x => x.NpcId).ToList().Count;
            if (characters != 0 || npcs != 0)
            {
                ViewBag.Message = "Something in the database is using this gender currently. You can't delete a gender unless nothing is using it. You'll have find the entries using the gender and change them first.";
                return View("Error");
            }
            return View(gender);
        }

        // POST: Genders/Delete/5
        [HttpPost, ActionName("Delete")]
        [ValidateAntiForgeryToken]
        public ActionResult DeleteConfirmed(byte id)
        {
            Gender gender = db.Genders.Find(id);
            db.Genders.Remove(gender);
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
