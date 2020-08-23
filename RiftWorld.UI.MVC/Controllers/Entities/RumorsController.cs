using System;
using System.Collections.Generic;
using System.Data;
using System.Data.Entity;
using System.Linq;
using System.Net;
using System.Web;
using System.Web.Mvc;
using Microsoft.AspNet.Identity;
using RiftWorld.DATA.EF;
using RiftWorld.UI.MVC.Models;

namespace RiftWorld.UI.MVC.Controllers.Entities
{
    [Authorize]
    public class RumorsController : Controller
    {
        private RiftWorldEntities db = new RiftWorldEntities();

        public ActionResult Confirmed()
        {
            return View();
        }

        public ActionResult RumorFailed()
        {
            return View();
        }


        //get Rumors
        [OverrideAuthorization]
        public PartialViewResult _Rumors(short id)
        {
            var rumors = db.Rumors.Where(r => r.RumorOfId == id && r.IsApproved == true).Include(r => r.Character).ToList() ;

            return PartialView(rumors);
        }

        // GET: Rumors
        public ActionResult Index()
        {
            var rumors = db.Rumors.Include(r => r.Character).Include(r => r.Info);
            return View(rumors.ToList());
        }

        // GET: Rumors/Details/5
        public ActionResult Details(int? id)
        {
            if (id == null)
            {
                return new HttpStatusCodeResult(HttpStatusCode.BadRequest);
            }
            Rumor rumor = db.Rumors.Find(id);
            if (rumor == null)
            {
                return HttpNotFound();
            }
            return View(rumor);
        }

        // GET: Rumors/Create
        [OverrideAuthorization]
        public PartialViewResult CreateRumor(short id)
        {
            //ViewBag.HasChar = null;
            var userId = User.Identity.GetUserId();
            var character = db.UserDetails.Where(u => u.UserId == userId).Select(u => u.CurrentCharacterId).FirstOrDefault();
            RumorCreateVM model = new RumorCreateVM { RumorOfId = id, AuthorId = character, IsApproved = false };
            return PartialView(model);
        }

        // POST: Rumors/Create
        // To protect from overposting attacks, please enable the specific properties you want to bind to, for 
        // more details see https://go.microsoft.com/fwlink/?LinkId=317598.
        [HttpPost]
        [ValidateAntiForgeryToken]
        [OverrideAuthorization]
        [Authorize(Roles = "Player, Mod")]
        public ActionResult CreateRumor([Bind(Include = "RumorOfId,RumorText, AuthorId, IsApproved")] RumorCreateVM rumor)
        {
            //v2
            Rumor daRumor = new Rumor { RumorOfId = rumor.RumorOfId, IsApproved = rumor.IsApproved, RumorText = rumor.RumorText, AuthorId = (short)rumor.AuthorId };
            db.Rumors.Add(daRumor);
            db.SaveChanges();
            return Json(new { Message = "YES!", JsonRequestBehavior.AllowGet });
            //v1
            //if (ModelState.IsValid)
            //{
            //    Rumor daRumor = new Rumor { RumorOfId = rumor.RumorOfId, IsApproved = rumor.IsApproved, RumorText = rumor.RumorText, AuthorId = (short)rumor.AuthorId };
            //    db.Rumors.Add(daRumor);
            //    db.SaveChanges();
            //    return RedirectToAction("Confirmed");
            //}
            //else
            //{
            //    return RedirectToAction("RumorFailed");
            //}
        }

        //// GET: Rumors/Create
        //[OverrideAuthorization]
        //public ActionResult Create()
        //{
        //    return View();
        //}

        //// POST: Rumors/Create
        //// To protect from overposting attacks, please enable the specific properties you want to bind to, for 
        //// more details see https://go.microsoft.com/fwlink/?LinkId=317598.
        //[HttpPost]
        //[ValidateAntiForgeryToken]
        //public ActionResult Create([Bind(Include = "RumorsId,RumorOfId,RumorText, IsApproved, AuthorId")] Rumor rumor)
        //{
        //    if (ModelState.IsValid)
        //    {
        //        db.Rumors.Add(rumor);
        //        db.SaveChanges();
        //        return RedirectToAction("Confirmed", "Rumors");
        //    }
        //    return RedirectToAction("Failed");
        //}
        public ActionResult Failed()
        {
            return View();
        }
        //todo make sure every action of a post validates antiforgery token

        // GET: Rumors/Edit/5
        public ActionResult Edit(int? id)
        {
            if (id == null)
            {
                return new HttpStatusCodeResult(HttpStatusCode.BadRequest);
            }
            Rumor rumor = db.Rumors.Find(id);
            if (rumor == null)
            {
                return HttpNotFound();
            }
            ViewBag.AuthorId = new SelectList(db.Characters, "CharacterId", "PlayerId", rumor.AuthorId);
            ViewBag.RumorOfId = new SelectList(db.Infos, "InfoId", "Blurb", rumor.RumorOfId);
            return View(rumor);
        }

        // POST: Rumors/Edit/5
        // To protect from overposting attacks, please enable the specific properties you want to bind to, for 
        // more details see https://go.microsoft.com/fwlink/?LinkId=317598.
        [HttpPost]
        [ValidateAntiForgeryToken]
        public ActionResult Edit([Bind(Include = "RumorsId,RumorOfId,AuthorId,IsApproved,RumorText")] Rumor rumor)
        {
            if (ModelState.IsValid)
            {
                db.Entry(rumor).State = EntityState.Modified;
                db.SaveChanges();
                return RedirectToAction("Index");
            }
            ViewBag.AuthorId = new SelectList(db.Characters, "CharacterId", "PlayerId", rumor.AuthorId);
            ViewBag.RumorOfId = new SelectList(db.Infos, "InfoId", "Blurb", rumor.RumorOfId);
            return View(rumor);
        }

        // GET: Rumors/Delete/5
        public ActionResult Delete(int? id)
        {
            if (id == null)
            {
                return new HttpStatusCodeResult(HttpStatusCode.BadRequest);
            }
            Rumor rumor = db.Rumors.Find(id);
            if (rumor == null)
            {
                return HttpNotFound();
            }
            return View(rumor);
        }

        // POST: Rumors/Delete/5
        [HttpPost, ActionName("Delete")]
        [ValidateAntiForgeryToken]
        public ActionResult DeleteConfirmed(int id)
        {
            Rumor rumor = db.Rumors.Find(id);
            db.Rumors.Remove(rumor);
            db.SaveChanges();
            return RedirectToAction("Index");
        }

        public ActionResult Approve(int? id)
        {
            if (id == null)
            {
                return new HttpStatusCodeResult(HttpStatusCode.BadRequest);
            }
            Rumor rumor = db.Rumors.Find(id);
            if (rumor == null)
            {
                return HttpNotFound();
            }
            return View(rumor);
        }

        // POST: Rumors/Edit/5
        // To protect from overposting attacks, please enable the specific properties you want to bind to, for 
        // more details see https://go.microsoft.com/fwlink/?LinkId=317598.
        [HttpPost]
        [ValidateAntiForgeryToken]
        public ActionResult Approve(int rumorsId)
        {
            Rumor rumor = db.Rumors.Where(x => x.RumorsId == rumorsId).First();
            rumor.IsApproved = true;

            if (ModelState.IsValid)
            {
                db.Entry(rumor).State = EntityState.Modified;
                db.SaveChanges();
                return RedirectToAction("Approvals", "Infos");
            }
            return View(rumor);
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
