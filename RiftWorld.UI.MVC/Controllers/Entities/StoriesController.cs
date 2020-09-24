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

            List<StoryVM> model = new List<StoryVM>();

            foreach (Story story in stories)
            {
                List<Tag> tags = (from st in db.StoryTags
                                    join t in db.Tags on st.TagId equals t.TagId
                                  where st.StoryId == story.StoryId
                                  select t
                                )
                                .ToList()
                                ;
                StoryVM toAdd = new StoryVM { Story = story, Tags = tags };
                model.Add(toAdd);
            }

            return PartialView(model);
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
        public ActionResult Create([Bind(Include = "IsAboutId,CommissionedBy,IsCannon,TheContent,Title")] StoryCreateVM story,
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
                //if the story is labled as non-canon auto add the non-cannon tag as the first tag
                if (!story.IsCannon)
                {
                    short noncanTagID = db.Tags.Where(t => t.TagName == "Non-Canon").Select(t => t.TagId).First();
                    if (tags.Contains(noncanTagID))
                    {
                        tags.Remove(noncanTagID);
                    }
                    tags.Insert(0, noncanTagID);
                }
                //add story tags 
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
                return RedirectToAction("Details", "Infos", new { id = story.IsAboutId });
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
            StoryEditVM model = new StoryEditVM(story);

            List<short> selectedTags = db.StoryTags.Where(t => t.StoryId == id).Select(t => t.TagId).ToList();
            ViewBag.Selected = selectedTags;
            ViewBag.Tags = db.Tags.ToList();

            return View(model);
        }

        // POST: Stories/Edit/5
        // To protect from overposting attacks, please enable the specific properties you want to bind to, for 
        // more details see https://go.microsoft.com/fwlink/?LinkId=317598.
        [HttpPost]
        [ValidateAntiForgeryToken]
        public ActionResult Edit([Bind(Include = "StoryId,IsAboutId,DateTold,CommissionedBy,IsCannon,TheContent,Title")] StoryEditVM story,
            List<short> tags)
        {
            if (ModelState.IsValid)
            {


                #region Update Tags
                List<short> currentTagIds = db.StoryTags.Where(x => x.StoryId == story.StoryId).Select(x => x.TagId).ToList();
                if (tags != null)
                {
                    foreach (short t in tags)
                    {
                        //if this is an already existing tag 
                        if (currentTagIds.Contains(t))
                        {
                            currentTagIds.Remove(t);
                        }
                        else
                        {
                            StoryTag newTag = new StoryTag { StoryId = story.StoryId, TagId = t };
                            db.StoryTags.Add(newTag);
                        }
                    }
                }

                if (currentTagIds.Count != 0)
                {
                    foreach (short id in currentTagIds)
                    {
                        StoryTag gone = db.StoryTags.Where(x => x.StoryId == story.StoryId & x.TagId == id).FirstOrDefault();
                        db.StoryTags.Remove(gone);
                    }
                }
                #endregion

                #region Update Story
                Story daStory = new Story
                {
                    StoryId = story.StoryId,
                    DateTold = story.DateTold,
                    CommissionedBy = story.CommissionedBy,
                    IsAboutId = story.IsAboutId,
                    IsCannon = story.IsCannon,
                    TheContent = story.TheContent,
                    Title = story.Title
                };
                db.Entry(daStory).State = EntityState.Modified;
                db.SaveChanges();
                #endregion
                return RedirectToAction("Details", "Infos", new { id = story.IsAboutId });
            }
            ViewBag.IsAboutId = new SelectList(db.Infos, "InfoId", "Name", story.IsAboutId);
            ViewBag.Tags = db.Tags.ToList();
            if (tags != null)
            {
                ViewBag.Selected = tags;
            }
            else
            {
                ViewBag.Selected = new List<short>();
            }

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
            List<Tag> tags = (from st in db.StoryTags
                              join t in db.Tags on st.TagId equals t.TagId
                              where st.StoryId == story.StoryId
                              select t
                            )
                            .ToList()
                            ;
            StoryVM daStory = new StoryVM { Story = story, Tags = tags };
            return View(daStory);
        }

        // POST: Stories/Delete/5
        [HttpPost, ActionName("Delete")]
        [ValidateAntiForgeryToken]
        public ActionResult DeleteConfirmed(short id)
        {
            Story story = db.Stories.Find(id);

            List<StoryTag> st = db.StoryTags.Where(s => s.StoryId == id).ToList();
            //remove story tags
            foreach (StoryTag storyTag in st)
            {
                db.StoryTags.Remove(storyTag);
            }

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
