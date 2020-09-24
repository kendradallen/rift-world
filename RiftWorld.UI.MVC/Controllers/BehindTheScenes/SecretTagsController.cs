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

namespace RiftWorld.UI.MVC.Controllers.BehindTheScenes
{
    //todo increase name limit to 50 characters and decription to 300 characters
    [Authorize(Roles = "Admin")]
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

            List<Character> characters = db.CharSecrets.Where(x => x.SecretId == id).Select(x => x.Character).ToList();
            List<Secret> secrets = db.SecretSecretTags.Where(x => x.SecretTagId == id).Select(x => x.Secret).ToList();
            SecretTagVM model = new SecretTagVM { Tag = secretTag, CharactersKnow = characters, KnowledgeGiven = secrets };
            return View(model);
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
            //trim down stuff
            if (secretTag.Name != null)
            {
                secretTag.Name = secretTag.Name.Trim();
            }
            if (secretTag.Description != null)
            {
                secretTag.Description = secretTag.Description.Trim();
            }
            //check if unique
            bool uniqueCheck = db.SecretTags.Any(x => x.Name == secretTag.Name);
            if (uniqueCheck)
            {
                ModelState.AddModelError("Name", "");
            }

            //actually run if valid
            if (ModelState.IsValid)
            {
                db.SecretTags.Add(secretTag);
                db.SaveChanges();
                return RedirectToAction("CreateWhat", "Infos");
            }

            //if invalid, return the trimmed strings allong with the errors they should have
            ModelState.Clear();
            if (secretTag.Name == null)
            {
                ModelState.AddModelError("Name", "Ya need a name to identify this with");
            }
            else if (uniqueCheck)
            {
                ModelState.AddModelError("Name", "To make your life easier, I'm not letting you have identical Secret Tag names. Changing the capitalization will not count.");
            }
            if (secretTag.Description == null)
            {
                ModelState.AddModelError("Description", "To make future you's life easier, I am not allowing a secret tag with no description. Just write something that might help you remember what all secrets that have this tag relate to.");
            }
            ModelState.AddModelError("", "Something went wrong. If you don't see any red trying submiting again; you maybe just had a bunch of spaces in something");
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
            //trim down stuff
            if (secretTag.Name != null)
            {
                secretTag.Name = secretTag.Name.Trim();
            }
            if (secretTag.Description != null)
            {
                secretTag.Description = secretTag.Description.Trim();
            }
            //check if unique
            bool uniqueCheck = db.SecretTags.Any(x => x.Name == secretTag.Name && x.SecretTagId != secretTag.SecretTagId);
            if (uniqueCheck)
            {
                ModelState.AddModelError("Name", "");
            }

            //actually run if valid
            if (ModelState.IsValid)
            {
                db.Entry(secretTag).State = EntityState.Modified;
                db.SaveChanges();
                return RedirectToAction("Index");
            }

            //if invalid, return the trimmed strings allong with the errors they should have
            ModelState.Clear();
            if (secretTag.Name == null)
            {
                ModelState.AddModelError("Name", "How did the name disapper?");
            }
            else if (uniqueCheck)
            {
                ModelState.AddModelError("Name", "To make your life easier, I'm not letting you have identical Secret Tag names. Changing the capitalization will not count.");
            }
            if (secretTag.Description == null)
            {
                ModelState.AddModelError("Description", "To make future you's life easier, I am not allowing a secret tag with no description. Just write something that might help you remember what all secrets that have this tag relate to.");
            }
            ModelState.AddModelError("", "Something went wrong. If you don't see any red trying submiting again; you maybe just had a bunch of spaces in something");
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

            int characters = db.CharSecrets.Where(x => x.SecretId == id && !x.Character.IsRetired).Select(x => x.CharId).ToList().Count;
            int secrets = db.SecretSecretTags.Where(x => x.SecretTagId == id).Select(x => x.SecretId).ToList().Count;
            //todo - v2 cascade a removal instead of preventing deletion
            if (characters != 0 || secrets != 0)
            {
                ViewBag.Message = "Something in the database is using this secret tag currently. There are "+secrets+"secrets with this tag and " + characters +" active characters who know this. You can't delete a secret tag unless nothing is using it. You'll have find the entries using the secret tag and change them first.";
                return View("Error");

            }
            return View(secretTag);
        }

        // POST: SecretTags/Delete/5
        [HttpPost, ActionName("Delete")]
        [ValidateAntiForgeryToken]
        public ActionResult DeleteConfirmed(short id)
        {
            SecretTag secretTag = db.SecretTags.Find(id);

            //removing the retired characters who know this secret tag
            List<CharSecret> chars = db.CharSecrets.Where(x => x.SecretId == id).ToList();
            foreach (CharSecret gone in chars)
            {
                db.CharSecrets.Remove(gone);
            }
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
