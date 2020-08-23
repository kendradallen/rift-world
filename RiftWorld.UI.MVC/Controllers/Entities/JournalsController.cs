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

namespace RiftWorld.UI.MVC.Controllers.Entities
{
    public class JournalsController : Controller
    {
        private RiftWorldEntities db = new RiftWorldEntities();

        // GET: Journals
        public ActionResult Index()
        {
            var journals = db.Journals.Include(j => j.Character);
            return View(journals.ToList());
        }

        // GET: Journals/Details/5
        public ActionResult Details(int? id)
        {
            if (id == null)
            {
                return new HttpStatusCodeResult(HttpStatusCode.BadRequest);
            }
            Journal journal = db.Journals.Find(id);
            if (journal == null)
            {
                return HttpNotFound();
            }
            return View(journal);
        }

        public ActionResult NoCharacter()
        {
            return View();
        }

        // GET: Journals/Create
        [Authorize(Roles = "Player, Mod")]
        public ActionResult Create()
        {
            string userId = User.Identity.GetUserId();
            UserDetail currentUser = db.UserDetails.Where(u => u.UserId == userId).FirstOrDefault();
            if (currentUser.CurrentCharacterId == null)
            {
                ViewBag.Message = "You can't journal without an active character.";
                return View("NoCharacter");
            }

            return View();
        }

        // POST: Journals/Create
        // To protect from overposting attacks, please enable the specific properties you want to bind to, for 
        // more details see https://go.microsoft.com/fwlink/?LinkId=317598.
        [HttpPost]
        [ValidateAntiForgeryToken]
        [Authorize(Roles = "Player, Mod")]
        public ActionResult Create([Bind(Include = "JournalId,TheContent")] Journal journal)
        {
            string userId = User.Identity.GetUserId();
            UserDetail currentUser = db.UserDetails.Where(u => u.UserId == userId).FirstOrDefault();
            if (currentUser.CurrentCharacterId == null)
            {
                ViewBag.Message = "I'm not sure how you got this far, but you can't journal without an active character.";
                return View("Error");
            }

            journal.IsApproved = false;
            journal.OocDateWritten = DateTime.Now.Date;
            journal.HasUnseenEdit = false;
            journal.CharacterId = (short)currentUser.CurrentCharacterId;
            if (ModelState.IsValid)
            {
                db.Journals.Add(journal);
                db.SaveChanges();
                return RedirectToAction("Hub", "Characters", new { id = currentUser.CurrentCharacterId });
            }

            return View(journal);
        }

        // GET: Journals/Edit/5
        [Authorize(Roles = "Player, Mod")]
        public ActionResult Edit(int? id)
        {
            if (id == null)
            {
                return new HttpStatusCodeResult(HttpStatusCode.BadRequest);
            }
            Journal journal = db.Journals.Find(id);
            if (journal == null)
            {
                return HttpNotFound();
            }
            UserDetail player = journal.Character.UserDetail;
            
            //todo - add actual redirects to prevent the anyone but current character player from editing journal
            //error cases
            if (User.Identity.GetUserId() != player.UserId)
            {
                //this journal is not yours. You can't edit it. 
            }
            if (!journal.Character.IsApproved)
            {
                //this character is under approval and cannot use the journal function till it is approved.
            }
            if (player.CurrentCharacterId != journal.CharacterId && journal.Character.IsApproved)
            {
                //this character is retired and can no longer be journaled upon by player
            }

            return View(journal);
        }

        // POST: Journals/Edit/5
        // To protect from overposting attacks, please enable the specific properties you want to bind to, for 
        // more details see https://go.microsoft.com/fwlink/?LinkId=317598.
        [HttpPost]
        [ValidateAntiForgeryToken]
        [Authorize(Roles = "Player, Mod")]
        public ActionResult Edit([Bind(Include = "JournalId,CharacterId,OocDateWritten,TheContent,IsApproved")] Journal journal)
        {
            journal.HasUnseenEdit = true;
            UserDetail player = journal.Character.UserDetail;

            //todo - add actual redirects to prevent the anyone but current character player from editing journal
            //error cases
            if (User.Identity.GetUserId() != player.UserId)
            {
                //this journal is not yours. You can't edit it. 
            }
            if (!journal.Character.IsApproved)
            {
                //this character is under approval and cannot use the journal function till it is approved.
            }
            if (player.CurrentCharacterId != journal.CharacterId && journal.Character.IsApproved)
            {
                //this character is retired and can no longer be journaled upon by player
            }

            if (ModelState.IsValid)
            {
                db.Entry(journal).State = EntityState.Modified;
                db.SaveChanges();
                return RedirectToAction("Hub", "Characters", new { id = journal.CharacterId });
            }
            return View(journal);
        }

        // GET: Journals/Approve/5
        [Authorize(Roles = "Mod, Admin")]
        public ActionResult Approve(int? id)
        {
            if (id == null)
            {
                return new HttpStatusCodeResult(HttpStatusCode.BadRequest);
            }
            Journal journal = db.Journals.Find(id);
            if (journal == null)
            {
                return HttpNotFound();
            }
            return View(journal);
        }

        // POST: Journals/Approve/5
        // To protect from overposting attacks, please enable the specific properties you want to bind to, for 
        // more details see https://go.microsoft.com/fwlink/?LinkId=317598.
        [HttpPost]
        [ValidateAntiForgeryToken]
        [Authorize(Roles = "Mod, Admin")]
        public ActionResult Approve([Bind(Include = "JournalId,CharacterId,OocDateWritten,TheContent")] Journal journal)
        {
            journal.IsApproved = true;
            journal.HasUnseenEdit = false;
            if (ModelState.IsValid)
            {
                db.Entry(journal).State = EntityState.Modified;
                db.SaveChanges();
                return RedirectToAction("Index");
            }
            return View(journal);
        }

        // GET: Journals/Delete/5
        public ActionResult Delete(int? id)
        {
            if (id == null)
            {
                return new HttpStatusCodeResult(HttpStatusCode.BadRequest);
            }
            Journal journal = db.Journals.Find(id);
            if (journal == null)
            {
                return HttpNotFound();
            }
            return View(journal);
        }

        // POST: Journals/Delete/5
        [HttpPost, ActionName("Delete")]
        [ValidateAntiForgeryToken]
        public ActionResult DeleteConfirmed(int id)
        {
            Journal journal = db.Journals.Find(id);
            db.Journals.Remove(journal);
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
