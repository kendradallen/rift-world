using System;
using System.Collections.Generic;
using System.Data;
using System.Data.Entity;
using System.Linq;
using System.Threading.Tasks;
using System.Net;
using System.Web;
using System.Web.Mvc;
using RiftWorld.DATA.EF;
using Microsoft.AspNet.Identity;
using RiftWorld.UI.MVC.Models;

namespace RiftWorld.UI.MVC.Controllers.Entities
{
    //[Authorize(Roles = "Admin")]
    public class SecretsController : Controller
    {
        private RiftWorldEntities db = new RiftWorldEntities();

        //TODO accidentally made this an ajax action controller.... I have no idea what to do but this should be looked into later

        // GET: Secrets/Details/5
        //public PartialViewResult _Details(short? id)
        //{
        //    if (id == null)
        //    {
        //        return new HttpStatusCodeResult(HttpStatusCode.BadRequest);
        //    }
        //    Secret secret = db.Secrets.Find(id);
        //    if (secret == null)
        //    {
        //        return HttpNotFound();
        //    }
        //    return PartialView(secret);
        //}

        //Get: the secrets
        [OverrideAuthorization]
        public PartialViewResult  _Details(short id)
        {
            List<Secret> secrets;
            ////if the user isn't an Admin, dynamically change the content 
            if (!User.IsInRole("Admin"))
            {
                string userId = User.Identity.GetUserId();

                short[] known = (from c in db.CharSecrets
                                 where c.CharId ==
                                    (
                                        from u in db.UserDetails
                                        where u.UserId == userId
                                        select u.CurrentCharacterId
                                    ).FirstOrDefault()
                                 select c.SecretId).ToArray();
                short[] knownB = (from sst in db.SecretSecretTags
                                  select sst.SecretTagId).ToArray()
                             ;
                secrets = (from t in db.SecretTags
                                 join sst in db.SecretSecretTags on t.SecretTagId equals sst.SecretTagId
                                 join s in db.Secrets on sst.SecretId equals s.SecretId
                                 where known.Intersect(knownB).Contains(t.SecretTagId) && s.IsAboutId == id
                                 select s)
                                .ToList()
                                ;
            }
            else
            {
                secrets = (from s in db.Secrets
                               where s.IsAboutId == id
                               select s)
                              .ToList()
                              ;

            }
            //logic to check if the user matches the tags

            return PartialView(secrets);
        }


        //// GET: Secrets
        public ActionResult Index()
        {
            var secrets = db.Secrets.Include(s => s.Info);
            ViewBag.Tags = db.SecretSecretTags.ToList();
            return View(secrets);
        }

        //// GET: Secrets/Details/5
        //public async Task<ActionResult> _Details(short? id)
        //{
        //    if (id == null)
        //    {
        //        return new HttpStatusCodeResult(HttpStatusCode.BadRequest);
        //    }
        //    Secret secret = await db.Secrets.FindAsync(id);
        //    if (secret == null)
        //    {
        //        return HttpNotFound();
        //    }
        //    return View(secret);
        //}

        //// GET: Secrets/Create
        public ActionResult Create()
        {
            ViewBag.IsAboutId = new SelectList(db.Infos, "InfoId", "Name");
            ViewBag.Tags = new MultiSelectList(db.SecretTags, "SecretTagId", "Name");
            return View();
        }

        //// POST: Secrets/Create
        //// To protect from overposting attacks, please enable the specific properties you want to bind to, for 
        //// more details see https://go.microsoft.com/fwlink/?LinkId=317598.
        [HttpPost]
        [ValidateAntiForgeryToken]
        public ActionResult Create([Bind(Include = "IsAboutId,TheContent")] SecretCreate secret,
            List<short> tags)
        {
            if (ModelState.IsValid)
            {
                #region make secret
                Secret daSecret = new Secret
                {
                    IsAboutId = secret.IsAboutId,
                    TheContent = secret.TheContent
                };
                db.Secrets.Add(daSecret);
                db.SaveChanges();
                #endregion

                #region Add the tags
                if (tags != null)
                {
                    short id = db.Secrets.Max(s => s.SecretId);
                    foreach (short t in tags)
                    {
                        SecretSecretTag tag = new SecretSecretTag { SecretId = id, SecretTagId = t};
                        db.SecretSecretTags.Add(tag);
                    }
                }
                db.SaveChanges();
                #endregion


                return RedirectToAction("Index");
            }

            ViewBag.IsAboutId = new SelectList(db.Infos, "InfoId", "Name");
            ViewBag.Tags = new MultiSelectList(db.SecretTags, "SecretTagId", "Name", tags);
            return View(secret);
        }

        // GET: Secrets/Edit/5
        public ActionResult Edit(short? id)
        {
            if (id == null)
            {
                return new HttpStatusCodeResult(HttpStatusCode.BadRequest);
            }
            Secret secret = db.Secrets.Find(id);
            if (secret == null)
            {
                return HttpNotFound();
            }
            ViewBag.IsAboutId = new SelectList(db.Infos, "InfoId", "Name", secret.IsAboutId);

            List<short> selectedTags = db.SecretSecretTags.Where(s => s.SecretId == id).Select(s => s.SecretTagId).ToList();
            SecretEdit model = new SecretEdit(secret);
            ViewBag.Tags = db.SecretTags.ToList();
            ViewBag.Selected = selectedTags;

            return View(model);
        }

        // POST: Secrets/Edit/5
        // To protect from overposting attacks, please enable the specific properties you want to bind to, for 
        // more details see https://go.microsoft.com/fwlink/?LinkId=317598.
        [HttpPost]
        [ValidateAntiForgeryToken]
        public ActionResult Edit([Bind(Include = "SecretId,IsAboutId,TheContent")] SecretEdit secret,
            List<short> tags)
        {
            if (ModelState.IsValid)
            {
                #region Update Tags
                short secretId = secret.SecretId;
                List<short> currentTagIds = db.SecretSecretTags.Where(x => x.SecretId == secretId).Select(x => x.SecretTagId).ToList();
                if (tags != null)
                {
                    foreach (short tag in tags)
                    {
                        if (true)
                        {
                            //if this is an already existing tag 
                            if (currentTagIds.Contains(tag))
                            {
                                currentTagIds.Remove(tag);
                            }
                            //if this is a newly added tag
                            else
                            {
                                SecretSecretTag newTag = new SecretSecretTag { SecretId = secretId, SecretTagId = tag };
                                db.SecretSecretTags.Add(newTag);
                            }
                        }
                    }
                }

                if (currentTagIds.Count != 0)
                {
                    foreach (short id in currentTagIds)
                    {
                        SecretSecretTag gone = db.SecretSecretTags.Where(x => x.SecretId == secretId & x.SecretTagId == id).FirstOrDefault();
                        db.SecretSecretTags.Remove(gone);
                    }
                }
                #endregion

                #region Update Secret
                Secret daSecret = new Secret
                {
                    SecretId = secret.SecretId,
                    IsAboutId = secret.IsAboutId,
                    TheContent = secret.TheContent
                };
                db.Entry(secret).State = EntityState.Modified;
                db.SaveChanges();
                #endregion

                return RedirectToAction("Index");
            }
            ViewBag.IsAboutId = new SelectList(db.Infos, "InfoId", "Name", secret.IsAboutId);
            ViewBag.Tags = db.SecretTags.ToList();
            ViewBag.Selected = tags;
            return View(secret);
        }

        //// GET: Secrets/Delete/5
        public ActionResult Delete(short? id)
        {
            if (id == null)
            {
                return new HttpStatusCodeResult(HttpStatusCode.BadRequest);
            }
            Secret secret = db.Secrets.Find(id);
            if (secret == null)
            {
                return HttpNotFound();
            }
            return View(secret);
        }

        //// POST: Secrets/Delete/5
        [HttpPost, ActionName("Delete")]
        [ValidateAntiForgeryToken]
        public ActionResult DeleteConfirmed(short id)
        {
            Secret secret = db.Secrets.Find(id);
            db.Secrets.Remove(secret);
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
