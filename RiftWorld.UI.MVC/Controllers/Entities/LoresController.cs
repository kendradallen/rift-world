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
    [Authorize(Roles = "Admin")]
    public class LoresController : Controller
    {
        private RiftWorldEntities db = new RiftWorldEntities();

        // GET: Lores
        [OverrideAuthorization]
        public ActionResult Index()
        {
            //admin sees all (client request)
            List<Lore> lores = new List<Lore> { };
            if (User.IsInRole("Admin"))
            {
                lores = db.Lores.Include(l => l.Info).OrderBy(l=>l.Info.Name).ToList();
            }
            else
            {
                lores = db.Lores.Include(l => l.Info).Where(l => l.Info.IsPublished).OrderBy(l=> l.Info.Name).ToList();
            }
            return View(lores);
        }

        // GET: Lores/Details/5
        [OverrideAuthorization]
        public ActionResult Details(short? id, short? story)
        {
            if (id == null)
            {
                return new HttpStatusCodeResult(HttpStatusCode.BadRequest);
            }
            Lore lore = db.Lores.Find(id);
            if (lore == null)
            {
                return HttpNotFound();
            }


            //prevent users from seeing un-published work
            if (!lore.Info.IsPublished && !User.IsInRole("Admin"))
            {
                ViewBag.Message = "Whatever you think exists, doesn't yet.";
                return View("TheForbiddenZone");
            }
            ViewBag.OpenStory = story;
            return View(lore);
        }

        // GET: Lores/Create
        public ActionResult Create()
        {
            ViewBag.Tags = new MultiSelectList(db.Tags, "TagId", "TagName");
            return View();
        }

        // POST: Lores/Create
        // To protect from overposting attacks, please enable the specific properties you want to bind to, for 
        // more details see https://go.microsoft.com/fwlink/?LinkId=317598.
        [HttpPost]
        [ValidateAntiForgeryToken]
        public ActionResult Create([Bind(Include = "Name,TheContent, Blurb, IsSecret")] LoreCreateVM lore,
            List<short> tags,
            string submit)
        {
            #region Save or Publish?
            switch (submit)
            {
                case "Save Progress":
                case "Un-Publish":
                    lore.IsPublished = false;
                    break;
                case "Publish":
                case "Save":
                    lore.IsPublished = true;
                    break;
            }
            #endregion

            if (ModelState.IsValid)
            {
                #region Info
                Info info = new Info
                {
                    InfoTypeId = 1,  //<-------------------Info Type ID. CHANGE UPON COPY
                    IdWithinType = null,
                    Blurb = lore.Blurb,
                    Name = lore.Name,
                    IsPublished = lore.IsPublished,
                    IsSecret = lore.IsSecret
                };
                db.Infos.Add(info);
                db.SaveChanges(); //this has to go here in order to ensure that the infoId short below is accurate. Also at this point I am doing no further gets on validity so there is no point to not saving 

                short infoId = db.Infos.Max(i => i.InfoId);

                #endregion

                #region Adding Tags
                if (tags != null)
                {
                    foreach (short t in tags)
                    {
                        InfoTag infoTag = new InfoTag { InfoId = infoId, TagId = t };
                        db.InfoTags.Add(infoTag);
                    }
                }
                #endregion

                #region Lore
                Lore daLore = new Lore
                {
                    InfoId = infoId,
                    TheContent = lore.TheContent
                };
                db.Lores.Add(daLore);
                db.SaveChanges();
                #endregion

                #region give info the IdWithinType
                short maxi = db.Lores.Max(i => i.LoreId); // <------------ CHANGE THIS UPON COPY PASTE!!!!!!!!
                info.IdWithinType = maxi; 
                db.Entry(info).State = EntityState.Modified;
                db.SaveChanges();
                #endregion

                return RedirectToAction("Details", new { id = maxi });
            }

            //if model is not valid
            ViewBag.Tags = new MultiSelectList(db.Tags, "TagId", "TagName", tags);
            ModelState.AddModelError("", "Something has gone wrong. Look for red text to see where is went wrong");
            return View(lore);
        }

        // GET: Lores/Edit/5
        public ActionResult Edit(short? id)
        {
            if (id == null)
            {
                return new HttpStatusCodeResult(HttpStatusCode.BadRequest);
            }
            Lore lore = db.Lores.Find(id);
            if (lore == null)
            {
                return HttpNotFound();
            }
            short infoid = lore.InfoId;
            Info info = db.Infos.Find(infoid);
            LoreEditVM model = new LoreEditVM(lore, info);

            List<short> selectedTags = db.InfoTags.Where(t => t.InfoId == infoid).Select(t => t.TagId).ToList();
            ViewBag.Selected = selectedTags;
            ViewBag.Tags = db.Tags.ToList();

            return View(model);
        }

        // POST: Lores/Edit/5
        // To protect from overposting attacks, please enable the specific properties you want to bind to, for 
        // more details see https://go.microsoft.com/fwlink/?LinkId=317598.
        [HttpPost]
        [ValidateAntiForgeryToken]
        public ActionResult Edit([Bind(Include = "LoreId,InfoId,Name,TheContent,Blurb,IsPublished, IsSecret")] LoreEditVM lore,
            List<short> tags, string submit)
        {

            if (ModelState.IsValid)
            {
                #region Save or Publish?
                switch (submit)
                {
                    case "Save Progress":
                    case "Un-Publish":
                        lore.IsPublished = false;
                        break;
                    case "Publish":
                    case "Save":
                        lore.IsPublished = true;
                        break;
                }
                #endregion

                var infoid = lore.InfoId;
                #region Info Update
                //Info info = db.Infos.Find(infoid);
                Info info = db.Infos.Where(i => i.InfoId == infoid).FirstOrDefault();
                info.Name = lore.Name;
                info.Blurb = lore.Blurb;
                info.IsPublished = lore.IsPublished;
                info.IsSecret = lore.IsSecret;
                #endregion

                #region Update tags
                List<short> currentTagIds = db.InfoTags.Where(x => x.InfoId == infoid).Select(x => x.TagId).ToList();

                if (tags != null)
                {
                    foreach (short tag in tags)
                    {
                        //if this is an already existing tag 
                        if (currentTagIds.Contains(tag))
                        {
                            currentTagIds.Remove(tag);
                        }
                        //if this is a newly added tag
                        else
                        {
                            InfoTag newTag = new InfoTag { InfoId = infoid, TagId = tag };
                            db.InfoTags.Add(newTag);
                        }
                    }
                }

                if (currentTagIds.Count != 0)
                {
                    foreach (short id in currentTagIds)
                    {
                        InfoTag gone = db.InfoTags.Where(x => x.InfoId == infoid & x.TagId == id).FirstOrDefault();
                        db.InfoTags.Remove(gone);
                    }
                }

                #endregion

                #region Update Lore
                Lore daLore = new Lore
                {
                    LoreId = lore.LoreId,
                    InfoId = lore.InfoId,
                    TheContent = lore.TheContent
                };
                db.Entry(daLore).State = EntityState.Modified;
                db.SaveChanges();
                #endregion

                return RedirectToAction("Details", new { id = lore.LoreId });
            }

            ViewBag.Tags = db.Tags.ToList();
            if (tags != null)
            {
                ViewBag.Selected = tags;
            }
            else
            {
                ViewBag.Selected = new List<short>();
            }
            ModelState.AddModelError("", "Something has gone wrong. Look for red text to see where is went wrong");
            return View(lore);
        }

        // GET: Lores/Delete/5
        public ActionResult Delete(short? id)
        {
            if (id == null)
            {
                return new HttpStatusCodeResult(HttpStatusCode.BadRequest);
            }
            Lore lore = db.Lores.Find(id);
            if (lore == null)
            {
                return HttpNotFound();
            }
            return View(lore);
        }

        // POST: Lores/Delete/5
        [HttpPost, ActionName("Delete")]
        [ValidateAntiForgeryToken]
        public ActionResult DeleteConfirmed(short id)
        {
            Lore lore = db.Lores.Find(id);
            short infoId = lore.InfoId;
            db.Lores.Remove(lore);

            #region Remove Rumors
            var rumors = db.Rumors.Where(r => r.RumorOfId == infoId);
            foreach (Rumor r in rumors)
            {
                db.Rumors.Remove(r);
            }
            #endregion

            #region Remove Secrets
            var secrets = db.Secrets.Where(s => s.IsAboutId == infoId);
            List<short> secretIds = db.Secrets.Where(s => s.IsAboutId == infoId).Select(s => s.SecretId).ToList();
            List<SecretSecretTag> ssts = db.SecretSecretTags.Where(s => secretIds.Contains(s.SecretId)).ToList();

            //remove sst
            foreach (SecretSecretTag secretSecretTag in ssts)
            {
                db.SecretSecretTags.Remove(secretSecretTag);
            }

            //remove secrets
            foreach (Secret secret in secrets)
            {
                db.Secrets.Remove(secret);
            }

            #endregion

            #region remove stories
            var stories = db.Stories.Where(s => s.IsAboutId == infoId);
            List<short> storyIds = db.Stories.Where(s => s.IsAboutId == infoId).Select(s => s.StoryId).ToList();
            List<StoryTag> st = db.StoryTags.Where(s => storyIds.Contains(s.StoryId)).ToList();

            //remove story tags
            foreach (StoryTag storyTag in st)
            {
                db.StoryTags.Remove(storyTag);
            }

            //remove stories
            foreach (Story story in stories)
            {
                db.Stories.Remove(story);
            }
            #endregion

            #region Remove info
            Info info = db.Infos.Where(i => i.InfoId == infoId).First();
            var infoTags = db.InfoTags.Where(it => it.InfoId == infoId).ToList();
            foreach (InfoTag infoTag in infoTags)
            {
                db.InfoTags.Remove(infoTag);
            }

            db.Infos.Remove(info);
            #endregion

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
