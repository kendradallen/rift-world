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
    //todo increase name limit to 35 characters
    [Authorize(Roles = "Admin")]
    public class TagsController : Controller
    {
        private RiftWorldEntities db = new RiftWorldEntities();

        // GET: Tags
        [OverrideAuthorization]
        public ActionResult Index()
        {
            return View(db.Tags.ToList());
        }

        // GET: Tags/Details/5
        public ActionResult Details(short? id)
        {
            if (id == null)
            {
                return new HttpStatusCodeResult(HttpStatusCode.BadRequest);
            }
            Tag tag = db.Tags.Find(id);
            if (tag == null)
            {
                return HttpNotFound();
            }
            return View(tag);
        }

        // GET: Tags/Create
        public ActionResult Create()
        {
            return View();
        }

        // POST: Tags/Create
        // To protect from overposting attacks, please enable the specific properties you want to bind to, for 
        // more details see https://go.microsoft.com/fwlink/?LinkId=317598.
        [HttpPost]
        [ValidateAntiForgeryToken]
        public ActionResult Create([Bind(Include = "TagId,TagName,Description")] Tag tag)
        {
            //trim down stuff
            if (tag.TagName != null)
            {
                tag.TagName = tag.TagName.Trim();
            }
            if (tag.Description != null)
            {
                tag.Description = tag.Description.Trim();
            }
            //check if unique
            bool uniqueCheck = db.Tags.Any(x => x.TagName == tag.TagName);
            if (uniqueCheck)
            {
                ModelState.AddModelError("TagName", "");
            }

            //actually run if valid
            if (ModelState.IsValid)
            {
                db.Tags.Add(tag);
                db.SaveChanges();
                return RedirectToAction("CreateWhat", "Infos");
            }

            //if invalid, return the trimmed strings allong with the errors they should have
            ModelState.Clear();
            if (tag.TagName == null)
            {
                ModelState.AddModelError("TagName", "Ya need a name");
            }
            else if (uniqueCheck)
            {
                ModelState.AddModelError("TagName", "That exact tag name already exists. It'd be too confusing to allow the same tag again.");
            }
            ModelState.AddModelError("", "Something went wrong. If you don't see any red trying submiting again; you maybe just had a bunch of spaces in something");

            return View(tag);
        }

        // GET: Tags/Edit/5
        public ActionResult Edit(short? id)
        {
            if (id == null)
            {
                return new HttpStatusCodeResult(HttpStatusCode.BadRequest);
            }
            Tag tag = db.Tags.Find(id);
            if (tag == null)
            {
                return HttpNotFound();
            }
            return View(tag);
        }

        // POST: Tags/Edit/5
        // To protect from overposting attacks, please enable the specific properties you want to bind to, for 
        // more details see https://go.microsoft.com/fwlink/?LinkId=317598.
        [HttpPost]
        [ValidateAntiForgeryToken]
        public ActionResult Edit([Bind(Include = "TagId,TagName,Description")] Tag tag)
        {
            if (tag.TagName != null)
            {
                tag.TagName = tag.TagName.Trim();
            }
            if (tag.Description != null)
            {
                tag.Description = tag.Description.Trim();
            }
            bool uniqueCheck = db.Tags.Any(x => x.TagName == tag.TagName && x.TagId != tag.TagId);
            if (uniqueCheck)
            {
                ModelState.AddModelError("TagName", "");
            }

            if (ModelState.IsValid)
            {
                db.Entry(tag).State = EntityState.Modified;
                db.SaveChanges();
                return RedirectToAction("Index");
            }

            //if invalid, return the trimmed strings allong with the errors they should have
            ModelState.Clear();
            if (tag.TagName == null)
            {
                ModelState.AddModelError("TagName", "Okay, wait, where did the name go?");
            }
            else if (uniqueCheck)
            {
                ModelState.AddModelError("TagName", "That exact tag name already exists. It'd be too confusing to allow the same tag again.");
            }
            ModelState.AddModelError("", "Something went wrong. If you don't see any red trying submiting again; you maybe just had a bunch of spaces in something");

            return View(tag);
        }

        // GET: Tags/Delete/5
        public ActionResult Delete(short? id)
        {
            if (id == null)
            {
                return new HttpStatusCodeResult(HttpStatusCode.BadRequest);
            }
            Tag tag = db.Tags.Find(id);
            if (tag == null)
            {
                return HttpNotFound();
            }

            int infos = db.InfoTags.Where(x => x.TagId == id).Select(x => x.InfoId).ToList().Count;
            int stories = db.StoryTags.Where(x => x.TagId == id).Select(x => x.StoryId).ToList().Count;
            //todo - v2 cascade a removal instead of preventing deletion
            if (infos != 0 || stories != 0)
            {
                ViewBag.Message = "Something in the database is using this tag currently. You can't delete a tag unless nothing is using it. You'll have find the entries using the tag and change them first.";
                return View("Error");
            }
            return View(tag);
        }

        // POST: Tags/Delete/5
        [HttpPost, ActionName("Delete")]
        [ValidateAntiForgeryToken]
        public ActionResult DeleteConfirmed(short id)
        {
            Tag tag = db.Tags.Find(id);
            db.Tags.Remove(tag);
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
