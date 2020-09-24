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
            //reason for beyond only including approved rumors is that, seeing unapproved rumors is not what this list is for. 
            var rumors = db.Rumors.Include(r => r.Character).Include(r => r.Info).Where(r=>r.IsApproved);
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
        [Authorize(Roles = "Character")]
        public ActionResult CreateRumor([Bind(Include = "RumorOfId,RumorText, AuthorId, IsApproved")] RumorCreateVM rumor)
        {
            Rumor daRumor = new Rumor { RumorOfId = rumor.RumorOfId, IsApproved = rumor.IsApproved, RumorText = rumor.RumorText, AuthorId = (short)rumor.AuthorId };
            db.Rumors.Add(daRumor);
            db.SaveChanges();
            return Json(new { Message = "YES!", JsonRequestBehavior.AllowGet });
        }

        // GET: Rumors/Delete/5
        [Authorize(Roles = "Admin")]
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
        [Authorize(Roles = "Admin")]
        public ActionResult DeleteConfirmed(int id)
        {
            Rumor rumor = db.Rumors.Find(id);
            db.Rumors.Remove(rumor);
            db.SaveChanges();
            return RedirectToAction("Index");
        }

        [Authorize(Roles = "Mod, Admin")]
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
        [Authorize(Roles = "Mod, Admin")]
        public ActionResult Approve(int rumorsId)
        {
            Rumor rumor = db.Rumors.Where(x => x.RumorsId == rumorsId).FirstOrDefault();
            if (rumor == null)
            {
                ViewBag.Message = "Looks like another mod or the admin denied this rumor's existance";
                return View("Error");
            }
            //check to see if other mod/admin has already approved this
            if (rumor.IsApproved == true)
            {
                ViewBag.Message = "Looks like another mod or the admin approved this";
                return View("Approvals", "Infos");
            }

            rumor.IsApproved = true;

            if (ModelState.IsValid)
            {
                db.Entry(rumor).State = EntityState.Modified;
                db.SaveChanges();
                return RedirectToAction("Approvals", "Infos");
            }
            return View(rumor);
        }

        [HttpPost]
        [ValidateAntiForgeryToken]
        [Authorize(Roles = "Mod, Admin")]
        public ActionResult Deny(int rumorsId)
        {
            Rumor rumor = db.Rumors.Where(x => x.RumorsId == rumorsId).FirstOrDefault();
            //check to see if other mod/admin has already denied this (thus it won't exist)
            if (rumor == null)
            {
                ViewBag.Message = "Looks like another mod or the admin denied this rumor's existance";
                return View("Approvals", "Infos");
            }
            //check to see if other mod/admin has already approved this
            if (rumor.IsApproved == true)
            {
                ViewBag.Message = "Looks like another mod or the admin approved this. If Katherine is reading this, you'll have to manually delete the rumor in order to undo this";
                return View("Error");
            }
            db.Rumors.Remove(rumor);
            db.SaveChanges();
            return RedirectToAction("Approvals", "Infos");
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
