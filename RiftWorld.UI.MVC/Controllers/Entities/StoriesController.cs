using System;
using System.Collections.Generic;
using System.Data;
using System.Data.Entity;
using System.Linq;
using System.Net;
using System.Web;
using System.Web.Mvc;
using RiftWorld.DATA.EF;
using RiftWorld.UI.MVC.Models;

namespace RiftWorld.UI.MVC.Controllers.Entities
{
    public class StoriesController : Controller
    {
        private RiftWorldEntities db = new RiftWorldEntities();

        //get stories
        public PartialViewResult _Stories(short id)
        {
            var stories = db.Stories.Where(r => r.IsAboutId == id).ToList();

            return PartialView(stories);
        }

        // GET: Stories
        public ActionResult Index()
        {
            var stories = db.Stories.Include(s => s.Info);
            return View(stories.ToList());
        }

        // GET: Stories/Details/5
        public ActionResult Details(short? id)
        {
            if (id == null)
            {
                return new HttpStatusCodeResult(HttpStatusCode.BadRequest);
            }
            Story story = db.Stories.Find(id);
            if (story == null)
            {
                return HttpNotFound();
            }
            return View(story);
        }

        // GET: Stories/Create
        public ActionResult Create()
        {
            ViewBag.IsAboutId = new SelectList(db.Infos, "InfoId", "Name");
            ViewBag.Tags = new MultiSelectList(db.Tags, "TagId", "TagName");
            return View();
        }

        // POST: Stories/Create
        // To protect from overposting attacks, please enable the specific properties you want to bind to, for 
        // more details see https://go.microsoft.com/fwlink/?LinkId=317598.
        [HttpPost]
        [ValidateAntiForgeryToken]
        public ActionResult Create([Bind(Include = "IsAboutId,DateTold,CommissionedBy,IsCannon,TheContent,Title")] StoryCreateVM story,
            List<short> tags)
        {
            story.DateTold = System.DateTime.Now.Date;
            if (ModelState.IsValid)
            {
                Story daStory = new Story
                {
                    DateTold = story.DateTold,
                    CommissionedBy = story.CommissionedBy,
                    IsAboutId = story.IsAboutId,
                    IsCannon = story.IsCannon,
                    TheContent = story.TheContent,
                    Title = story.Title
                };
                db.Stories.Add(daStory);
                db.SaveChanges();

                short storyId = db.Stories.Max(s => s.StoryId);
                #region Adding Tags
                if (tags != null)
                {
                    foreach (short t in tags)
                    {
                        StoryTag storyTag = new StoryTag { StoryId = storyId, TagId = t };
                        db.StoryTags.Add(storyTag);
                    }
                }
                #endregion
                db.SaveChanges();
                return RedirectToAction("Index");
            }

            ViewBag.IsAboutId = new SelectList(db.Infos, "InfoId", "Name", story.IsAboutId);
            ViewBag.Tags = new MultiSelectList(db.Tags, "TagId", "TagName", tags);
            return View(story);
        }

        // GET: Stories/Edit/5
        public ActionResult Edit(short? id)
        {
            if (id == null)
            {
                return new HttpStatusCodeResult(HttpStatusCode.BadRequest);
            }
            Story story = db.Stories.Find(id);
            if (story == null)
            {
                return HttpNotFound();
            }
            ViewBag.IsAboutId = new SelectList(db.Infos, "InfoId", "Name", story.IsAboutId);
            return View(story);
        }

        // POST: Stories/Edit/5
        // To protect from overposting attacks, please enable the specific properties you want to bind to, for 
        // more details see https://go.microsoft.com/fwlink/?LinkId=317598.
        [HttpPost]
        [ValidateAntiForgeryToken]
        public ActionResult Edit([Bind(Include = "StoryId,IsAboutId,DateTold,CommissionedBy,IsCannon,TheContent,Title")] Story story)
        {
            if (ModelState.IsValid)
            {
                db.Entry(story).State = EntityState.Modified;
                db.SaveChanges();
                return RedirectToAction("Index");
            }
            ViewBag.IsAboutId = new SelectList(db.Infos, "InfoId", "Name", story.IsAboutId);
            return View(story);
        }

        // GET: Stories/Delete/5
        public ActionResult Delete(short? id)
        {
            if (id == null)
            {
                return new HttpStatusCodeResult(HttpStatusCode.BadRequest);
            }
            Story story = db.Stories.Find(id);
            if (story == null)
            {
                return HttpNotFound();
            }
            return View(story);
        }

        // POST: Stories/Delete/5
        [HttpPost, ActionName("Delete")]
        [ValidateAntiForgeryToken]
        public ActionResult DeleteConfirmed(short id)
        {
            Story story = db.Stories.Find(id);
            db.Stories.Remove(story);
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
