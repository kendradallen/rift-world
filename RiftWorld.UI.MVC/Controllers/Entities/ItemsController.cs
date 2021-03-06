﻿using System;
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
    public class ItemsController : Controller
    {
        private RiftWorldEntities db = new RiftWorldEntities();

        // GET: Items
        [OverrideAuthorization]
        public ActionResult Index()
        {
            //v1 - for testing so I don't have to constantly switch accounts
            var items = db.Items.Include(i => i.Info);

            //v2 - prevent non-admin from seeing unpublished work
            //todo - uncomment below to prevent users from seeing un-published work
            //var items = db.Items.Include(i => i.Info).Where(i => i.IsPublished);
            return View(items.ToList());
        }

        // GET: Items/Details/5
        [OverrideAuthorization]
        public ActionResult Details(short? id)
        {
            if (id == null)
            {
                return new HttpStatusCodeResult(HttpStatusCode.BadRequest);
            }
            Item item = db.Items.Find(id);
            if (item == null)
            {
                return HttpNotFound();
            }

            //prevent users from seeing un-published work
            if (!item.IsPublished && !User.IsInRole("Admin"))
            {
                return View("Error");
                //todo change redirect to a error 404 page
            }

            return View(item);
        }

        // GET: Items/Create
        public ActionResult Create()
        {
            //ViewBag.Tags = db.Tags.ToList();
            ViewBag.Tags = new MultiSelectList(db.Tags, "TagId", "TagName");
            return View();
        }

        // POST: Items/Create
        // To protect from overposting attacks, please enable the specific properties you want to bind to, for 
        // more details see https://go.microsoft.com/fwlink/?LinkId=317598.
        [HttpPost]
        [ValidateAntiForgeryToken]
        public ActionResult Create([Bind(Include = "Name,Blurb,PictureFileName,DescriptionText,PropertyText,HistoryText, Artist, IsSecret")] ItemCreateVM item,
            HttpPostedFileBase picture,
            List<short> tags,
            string submit)
        {
            //so it seems that - at least for none complicated associations like tags - I am actually able to just use the base submit 

            //extra logic here for when I allow the empty placeholders during save actions. Check if the action is save and then stop her if it isn't
            #region Save or Publish?
            switch (submit)
            {
                case "Save Progress":
                case "Un-Publish":
                    item.IsPublished = false;
                    break;
                case "Publish":
                case "Save":
                    item.IsPublished = true;
                    break;
            }
            #endregion

            #region Pre-model picture check
            if (picture != null)
            {
                string[] goodExts = { ".jpg", ".jpeg", ".gif", ".png" };
                string imgName = picture.FileName;

                var length = picture.ContentLength;
                string ext = imgName.Substring(imgName.LastIndexOf('.'));

                if (!goodExts.Contains(ext.ToLower()))
                {
                    ModelState.AddModelError("PictureFileName", "You have submitted a incorrect file type for your portrait. Please use either: .jpg, .jpeg, .gif, or .png");
                }

                if (item.Artist == null)
                {
                    ModelState.AddModelError("Artist", "Katherine, you're trying to submit something with a picture without an artist. That's a no-no! But seriously, if something came up that means you need to change this rule, you know who to call.");
                }
            }
            #endregion

            if (ModelState.IsValid)
            {

                #region Info

                Info info = new Info
                {
                    InfoTypeId = 7,  //<-------------------Info Type ID. CHANGE UPON COPY
                    IdWithinType = null,
                    Blurb = item.Blurb,
                    Name = item.Name,
                    IsPublished = item.IsPublished,
                    IsSecret = item.IsSecret
                };
                db.Infos.Add(info);
                db.SaveChanges(); //this has to go here in order to ensure that the infoId short below is accurate. Also at this point I am doing no further gets on validity so there is no point to not saving 
                #endregion

                short infoId = db.Infos.Max(i => i.InfoId);

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

                #region Image uploads
                string[] goodExts = { ".jpg", ".jpeg", ".gif", ".png" };
                item.PictureFileName = "default.jpg";
                if (picture != null)
                {
                    string imgName = picture.FileName;

                    string ext = imgName.Substring(imgName.LastIndexOf('.'));

                    if (goodExts.Contains(ext.ToLower()))
                    {
                        imgName = "item-" + infoId.ToString() + ext;

                        picture.SaveAs(Server.MapPath("~/Content/img/item/" + imgName));
                    }
                    item.PictureFileName = imgName;

                }
                #endregion

                #region Item
                Item daItem = new Item
                {
                    InfoId = infoId,
                    Name = item.Name,
                    PictureFileName = item.PictureFileName,
                    DescriptionText = item.DescriptionText,
                    PropertyText = item.PropertyText,
                    HistoryText = item.HistoryText,
                    IsPublished = item.IsPublished,
                    Artist = item.Artist
                };
                db.Items.Add(daItem);
                db.SaveChanges();
                #endregion

                //now give the info the idWithinType
                #region give info the IdWithinType
                short maxi = db.Items.Max(i => i.ItemId);
                info.IdWithinType = maxi;
                db.Entry(info).State = EntityState.Modified;
                db.SaveChanges();
                #endregion

                return RedirectToAction("Details", new { id = maxi });
            }

            //if model is not valid
            ViewBag.Tags = new MultiSelectList(db.Tags, "TagId", "TagName", tags);

            if (picture != null)
            {
                ModelState.AddModelError("PictureFileName", "Hey, there was some error, so you have to re-upload the picture");
            }
            ModelState.AddModelError("", "Something has gone wrong. Look for red text to see where is went wrong");
            return View(item);
        }

        // GET: Items/Edit/5
        public ActionResult Edit(short? id)
        {
            if (id == null)
            {
                return new HttpStatusCodeResult(HttpStatusCode.BadRequest);
            }
            Item item = db.Items.Find(id);
            if (item == null)
            {
                return HttpNotFound();
            }

            short infoid = item.InfoId;
            Info info = db.Infos.Find(infoid);
            //make editVM
            ItemEditVM model = new ItemEditVM(item, info);

            List<short> selectedTags = db.InfoTags.Where(t => t.InfoId == infoid).Select(t => t.TagId).ToList();
            ViewBag.Selected = selectedTags;
            ViewBag.Tags = db.Tags.ToList();

            return View(model);
        }

        // POST: Items/Edit/5
        // To protect from overposting attacks, please enable the specific properties you want to bind to, for 
        // more details see https://go.microsoft.com/fwlink/?LinkId=317598.
        [HttpPost]
        [ValidateAntiForgeryToken]
        public ActionResult Edit([Bind(Include = "ItemId,InfoId,Name,Blurb,PictureFileName,DescriptionText,PropertyText,HistoryText, Artist, IsPublished, IsSecret")] ItemEditPostVM item,
            HttpPostedFileBase picture,
            List<short> tags, string submit)
        {

            #region Pre-model picture check
            if (picture != null)
            {
                string[] goodExts = { ".jpg", ".jpeg", ".gif", ".png" };
                string imgName = picture.FileName;

                var length = picture.ContentLength;
                string ext = imgName.Substring(imgName.LastIndexOf('.'));

                if (!goodExts.Contains(ext.ToLower()))
                {
                    ModelState.AddModelError("PictureFileName", "You have submitted a incorrect file type for your portrait. Please use either: .jpg, .jpeg, .gif, or .png");
                }

                if (item.Artist == null)
                {
                    ModelState.AddModelError("Artist", "Katherine, you're trying to submit something with a picture without an artist. That's a no-no! But seriously, if something came up that means you need to change this rule, you know who to call.");
                }
            }
            else if (item.PictureFileName != "default.jpg" && item.Artist == null)
            {
                ModelState.AddModelError("Artist", "Yo bud, you tired? Seems you deleted the artist by accident. Why don't ya fix that?");

            }
            #endregion

            if (ModelState.IsValid)
            {
                #region Save or Publish?
                switch (submit)
                {
                    case "Save Progress":
                    case "Un-Publish":
                        item.IsPublished = false;
                        break;
                    case "Publish":
                    case "Save":
                        item.IsPublished = true;
                        break;
                }
                #endregion

                var infoid = item.InfoId;
                #region Info Update
                //Info info = db.Infos.Find(infoid);
                Info info = db.Infos.Where(i => i.InfoId == infoid).FirstOrDefault();
                info.Name = item.Name;
                info.Blurb = item.Blurb;
                info.IsPublished = item.IsPublished;
                info.IsSecret = item.IsSecret;
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


                #region Image update/upload
                string[] goodExts = { ".jpg", ".jpeg", ".gif", ".png" };

                #region Image Upload - picture
                if (picture != null)
                {
                    string imgName = picture.FileName;

                    string ext = imgName.Substring(imgName.LastIndexOf('.'));

                    if (goodExts.Contains(ext.ToLower()))
                    {
                        imgName = "item-" + infoid + ext;

                        picture.SaveAs(Server.MapPath("~/Content/img/item/" + imgName));
                    }
                    //remove old picture if it had a different extension (and thus would not be overridden)
                    string oldName = item.PictureFileName;
                    string oldExt = oldName.Substring(oldName.LastIndexOf('.'));
                    if (oldName != "default.jpg" && oldExt != ext)
                    {
                        string fullPath = Request.MapPath("~/Content/img/item/" + oldName);
                        if (System.IO.File.Exists(fullPath))
                        {
                            System.IO.File.Delete(fullPath);
                        }
                    }
                    //assign new PictureFileName
                    item.PictureFileName = imgName;
                }
                #endregion
                #endregion

                #region Update Item
                Item daItem = new Item
                {
                    ItemId = item.ItemId,
                    InfoId = item.InfoId,
                    Name = item.Name,
                    PictureFileName = item.PictureFileName,
                    DescriptionText = item.DescriptionText,
                    PropertyText = item.PropertyText,
                    HistoryText = item.HistoryText,
                    IsPublished = item.IsPublished,
                    Artist = item.Artist
                };
                db.Entry(daItem).State = EntityState.Modified;
                db.Entry(info).State = EntityState.Modified;
                db.SaveChanges();
                #endregion

                return RedirectToAction("Details", new { id = item.ItemId });
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
            if (picture != null)
            {
                ModelState.AddModelError("PictureFileName", "Hey, there was some error, so you have to re-upload the picture");
            }
            ModelState.AddModelError("", "Something has gone wrong. Look for red text to see where is went wrong");
            ItemEditVM aItem = new ItemEditVM(item);
            return View(aItem);
        }

        // GET: Items/Delete/5
        public ActionResult Delete(short? id)
        {
            if (id == null)
            {
                return new HttpStatusCodeResult(HttpStatusCode.BadRequest);
            }
            Item item = db.Items.Find(id);
            if (item == null)
            {
                return HttpNotFound();
            }
            return View(item);
        }

        // POST: Items/Delete/5
        [HttpPost, ActionName("Delete")]
        [ValidateAntiForgeryToken]
        public ActionResult DeleteConfirmed(short id)
        {
            Item item = db.Items.Find(id);
            short infoId = item.InfoId;
            string picture = item.PictureFileName;
            db.Items.Remove(item);

            #region Remove Picture
            if (picture != "default.jpg")
            {
                string fullPath = Request.MapPath("~/Content/img/item/" + picture);
                if (System.IO.File.Exists(fullPath))
                {
                    System.IO.File.Delete(fullPath);
                }
            }
            #endregion

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
