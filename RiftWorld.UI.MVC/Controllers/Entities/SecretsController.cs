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

namespace RiftWorld.UI.MVC.Controllers.Entities
{
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
        public PartialViewResult  _Details(short id)
        {
            var secrets = (from s in db.Secrets
                          where s.IsAboutId == id
                          select s)
                          .ToList()
                          ;


            ////if the user isn't an Admin, dynamically change the content 
            //if (!User.IsInRole("Admin"))
            //{
            //    var userId = User.Identity.GetUserId();
            //    UserDetail user = db.UserDetails.Where(u => u.UserId == userId).FirstOrDefault();
            //    var curChar = user.CurrentCharacterId;
            //    if (curChar != null)
            //    {
            //        var tags = 
            //            (
            //                from st in db.SecretTags
            //            )
            //        //intersect with secrets
            //    }

            //}
            //logic to check if the user matches the tags

            return PartialView(secrets);
        }

        public PartialViewResult _UserSecrets()
        {
            var user = User.Identity.GetUserId();
            var curChar = db.Characters.Where(c => c.PlayerId == user && c.IsRetired == false).FirstOrDefault();
            if (curChar != null)
            {
                List<short> secretList = db.CharSecrets.Where(cs => cs.CharId == curChar.CharacterId).Select(cs => cs.SecretId).ToList();
                ViewBag.SecretList = secretList;
            }

            return PartialView();
        }


        //// GET: Secrets
        //public async Task<ActionResult> Index()
        //{
        //    var secrets = db.Secrets.Include(s => s.Info);
        //    return View(await secrets.ToListAsync());
        //}

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
        //public ActionResult Create()
        //{
        //    ViewBag.IsAboutId = new SelectList(db.Infos, "InfoId", "Blurb");
        //    return View();
        //}

        //// POST: Secrets/Create
        //// To protect from overposting attacks, please enable the specific properties you want to bind to, for 
        //// more details see https://go.microsoft.com/fwlink/?LinkId=317598.
        //[HttpPost]
        //[ValidateAntiForgeryToken]
        //public async Task<ActionResult> Create([Bind(Include = "SecretId,IsAboutId,TheContent")] Secret secret)
        //{
        //    if (ModelState.IsValid)
        //    {
        //        db.Secrets.Add(secret);
        //        await db.SaveChangesAsync();
        //        return RedirectToAction("Index");
        //    }

        //    ViewBag.IsAboutId = new SelectList(db.Infos, "InfoId", "Blurb", secret.IsAboutId);
        //    return View(secret);
        //}

        //// GET: Secrets/Edit/5
        //public async Task<ActionResult> Edit(short? id)
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
        //    ViewBag.IsAboutId = new SelectList(db.Infos, "InfoId", "Blurb", secret.IsAboutId);
        //    return View(secret);
        //}

        //// POST: Secrets/Edit/5
        //// To protect from overposting attacks, please enable the specific properties you want to bind to, for 
        //// more details see https://go.microsoft.com/fwlink/?LinkId=317598.
        //[HttpPost]
        //[ValidateAntiForgeryToken]
        //public async Task<ActionResult> Edit([Bind(Include = "SecretId,IsAboutId,TheContent")] Secret secret)
        //{
        //    if (ModelState.IsValid)
        //    {
        //        db.Entry(secret).State = EntityState.Modified;
        //        await db.SaveChangesAsync();
        //        return RedirectToAction("Index");
        //    }
        //    ViewBag.IsAboutId = new SelectList(db.Infos, "InfoId", "Blurb", secret.IsAboutId);
        //    return View(secret);
        //}

        //// GET: Secrets/Delete/5
        //public async Task<ActionResult> Delete(short? id)
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

        //// POST: Secrets/Delete/5
        //[HttpPost, ActionName("Delete")]
        //[ValidateAntiForgeryToken]
        //public async Task<ActionResult> DeleteConfirmed(short id)
        //{
        //    Secret secret = await db.Secrets.FindAsync(id);
        //    db.Secrets.Remove(secret);
        //    await db.SaveChangesAsync();
        //    return RedirectToAction("Index");
        //}

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
