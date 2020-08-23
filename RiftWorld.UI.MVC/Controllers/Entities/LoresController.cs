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
    //[Authorize(Roles = "Admin")]
    public class LoresController : Controller
    {
        private RiftWorldEntities db = new RiftWorldEntities();

        // GET: Lores
        [OverrideAuthorization]
        public ActionResult Index()
        {
            var lores = db.Lores.Include(l => l.Info);
            return View(lores.ToList());
        }

        // GET: Lores/Details/5
        [OverrideAuthorization]
        public ActionResult Details(short? id)
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

        // GET: Lores/Create
        public ActionResult Create()
        {
            ViewBag.Tags = new MultiSelectList(db.Tags, "TagId", "TagName");
            ViewBag.startName = 50;
            ViewBag.startBlurb = 100;

            return View();
        }

        // POST: Lores/Create
        // To protect from overposting attacks, please enable the specific properties you want to bind to, for 
        // more details see https://go.microsoft.com/fwlink/?LinkId=317598.
        [HttpPost]
        [ValidateAntiForgeryToken]
        public ActionResult Create([Bind(Include = "Name,TheContent, Blurb")] LoreCreateVM lore,
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
                    IsPublished = lore.IsPublished
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
                    Name = lore.Name,
                    TheContent = lore.TheContent,
                    IsPublished = lore.IsPublished
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
            ViewBag.startName = 50;
            ViewBag.startBlurb = 100;

            if (lore.Name != null)
            {
                ViewBag.startName = 50 - lore.Name.Length;
            }
            if (lore.Blurb != null)
            {
                ViewBag.startBlurb = 100 - lore.Blurb.Length;
            }

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
            string blurb = db.Infos.Where(i => i.InfoId == infoid).Select(i => i.Blurb).FirstOrDefault();
            LoreEditVM model = new LoreEditVM(lore, blurb);

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
        public ActionResult Edit([Bind(Include = "LoreId,InfoId,Name,TheContent,Blurb")] LoreEditVM lore,
            List<short> tags, string submit)
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
                var infoid = lore.InfoId;
                #region Info Update
                //Info info = db.Infos.Find(infoid);
                Info info = db.Infos.Where(i => i.InfoId == infoid).FirstOrDefault();
                info.Name = lore.Name;
                info.Blurb = lore.Blurb;
                info.IsPublished = lore.IsPublished;
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
                    Name = lore.Name,
                    TheContent = lore.TheContent,
                    IsPublished = lore.IsPublished
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
            db.Lores.Remove(lore);
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
