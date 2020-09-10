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
    public class ClassesController : Controller
    {
        private RiftWorldEntities db = new RiftWorldEntities();

        // GET: Classes
        public ActionResult Index()
        {
            return View(db.Classes.ToList());
        }

        // GET: Classes/Details/5
        //public ActionResult Details(byte? id)
        //{
        //    if (id == null)
        //    {
        //        return new HttpStatusCodeResult(HttpStatusCode.BadRequest);
        //    }
        //    Class taclass = db.Classes.Find(id);
        //    if (taclass == null)
        //    {
        //        return HttpNotFound();
        //    }
        //    return View(taclass);
        //}

        // GET: Classes/Create
        public ActionResult Create()
        {
            return View();
        }

        // POST: Classes/Create
        // To protect from overposting attacks, please enable the specific properties you want to bind to, for 
        // more details see https://go.microsoft.com/fwlink/?LinkId=317598.
        [HttpPost]
        [ValidateAntiForgeryToken]
        public ActionResult Create([Bind(Include = "ClassId,ClassName,IsPlayerEnabled")] Class taclass)
        {
            //todo - on this and every other BTS controller, prevent the creation of a duplicate name
            if (ModelState.IsValid)
            {
                db.Classes.Add(taclass);
                db.SaveChanges();
                return RedirectToAction("CreateWhat", "Infos");
            }

            return View(taclass);
        }

        // GET: Classes/Edit/5
        public ActionResult Edit(byte? id)
        {
            if (id == null)
            {
                return new HttpStatusCodeResult(HttpStatusCode.BadRequest);
            }
            Class taclass = db.Classes.Find(id);
            if (taclass == null)
            {
                return HttpNotFound();
            }
            return View(taclass);
        }

        // POST: Classes/Edit/5
        // To protect from overposting attacks, please enable the specific properties you want to bind to, for 
        // more details see https://go.microsoft.com/fwlink/?LinkId=317598.
        [HttpPost]
        [ValidateAntiForgeryToken]
        public ActionResult Edit([Bind(Include = "ClassId,ClassName,IsPlayerEnabled")] Class taclass)
        {
            if (ModelState.IsValid)
            {
                db.Entry(taclass).State = EntityState.Modified;
                db.SaveChanges();
                return RedirectToAction("Index");
            }
            return View(taclass);
        }

        // GET: Classes/Delete/5
        public ActionResult Delete(byte? id)
        {
            if (id == null)
            {
                return new HttpStatusCodeResult(HttpStatusCode.BadRequest);
            }
            Class taclass = db.Classes.Find(id);
            if (taclass == null)
            {
                return HttpNotFound();
            }
            int npcs = db.ClassNPCs.Where(x => x.ClassId == id).Select(x => x.NpcId).ToList().Count;
            if (npcs != 0)
            {
                ViewBag.Message = "Something in the database is using this class currently. You can't delete a class unless nothing is using it. You'll have find the entries using the class and change them first.";
                return View("Error");
            }
            return View(taclass);
        }

        // POST: Classes/Delete/5
        [HttpPost, ActionName("Delete")]
        [ValidateAntiForgeryToken]
        public ActionResult DeleteConfirmed(byte id)
        {
            Class taclass = db.Classes.Find(id);
            db.Classes.Remove(taclass);
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
