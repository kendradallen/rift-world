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
            //trim down stuff
            if (taclass.ClassName != null)
            {
                taclass.ClassName = taclass.ClassName.Trim();
            }
            //check if unique
            bool uniqueCheck = db.Classes.Any(x => x.ClassName == taclass.ClassName);
            if (uniqueCheck)
            {
                ModelState.AddModelError("ClassName", "");
            }

            //actually run if valid
            if (ModelState.IsValid)
            {
                db.Classes.Add(taclass);
                db.SaveChanges();
                return RedirectToAction("CreateWhat", "Infos");
            }

            //if invalid, return the trimmed strings allong with the errors they should have
            ModelState.Clear();
            if (taclass.ClassName == null)
            {
                ModelState.AddModelError("ClassName", "Katherine, seriously. If you're going to make a new class you need to actually name it.");
            }
            else if (taclass.ClassName.Length > 15)
            {
                ModelState.AddModelError("ClassName", "Too long. You have 15 characters; try again.");
            }
            else if (uniqueCheck)
            {
                ModelState.AddModelError("ClassName", "You don't need to have a class twice. If you want to make something player-enabled, just head to the BTS Lists, go to classes, and edit the class.");
            }
            ModelState.AddModelError("", "Something went wrong. If you don't see any red trying submiting again; you maybe just had a bunch of spaces in something");
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
            //trim down stuff
            if (taclass.ClassName != null)
            {
                taclass.ClassName = taclass.ClassName.Trim();
            }
            //check if unique
            bool uniqueCheck = db.Classes.Any(x => x.ClassName == taclass.ClassName && x.ClassId != taclass.ClassId);
            if (uniqueCheck)
            {
                ModelState.AddModelError("ClassName", "");
            }

            //actually run if valid
            if (ModelState.IsValid)
            {
                db.Entry(taclass).State = EntityState.Modified;
                db.SaveChanges();
                return RedirectToAction("Index");
            }

            //if invalid, return the trimmed strings allong with the errors they should have
            ModelState.Clear();
            if (taclass.ClassName == null)
            {
                ModelState.AddModelError("ClassName", "Katherine, seriously. If you're going to make a new class you need to actually name it.");
            }
            else if (taclass.ClassName.Length > 15)
            {
                ModelState.AddModelError("ClassName", "Too long. You have 15 characters; try again.");
            }
            else if (uniqueCheck)
            {
                ModelState.AddModelError("ClassName", "You don't need to have a class twice. I am actually kind of impressed you got this error here. Were you looking for it? You sly dog you. ");
            }
            ModelState.AddModelError("", "Something went wrong. If you don't see any red trying submiting again; you maybe just had a bunch of spaces in something");
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
