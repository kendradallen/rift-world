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
        public ActionResult Approve(int journalId)
        {
            //todo - add logic to check if the approved thing and what's on the db match
            Journal journal = db.Journals.Where(x => x.JournalId == journalId).FirstOrDefault();
            //check to see if other mod/admin has already denied this (thus it won't exist)
            if (journal == null)
            {
                ViewBag.Message = "Looks like another mod or the admin denied this journal's existance";
                return View("Error");
            }
            //check to see if other mod/admin has already approved this
            if (journal.IsApproved == true)
            {
                ViewBag.Message = "Looks like another mod or the admin approved this";
                return View("Approvals", "Infos");
            }

            journal.IsApproved = true;
            journal.HasUnseenEdit = false;
            if (ModelState.IsValid)
            {
                db.Entry(journal).State = EntityState.Modified;
                db.SaveChanges();
                return RedirectToAction("Approvals", "Infos");
            }
            return View(journal);
        }

        [HttpPost]
        [ValidateAntiForgeryToken]
        [Authorize(Roles = "Mod, Admin")]
        public ActionResult Deny(int journalId)
        {
            Journal journal = db.Journals.Where(x => x.JournalId == journalId).FirstOrDefault();
            //check to see if other mod/admin has already denied this (thus it won't exist)
            if (journal == null)
            {
                ViewBag.Message = "Looks like another mod or the admin denied this journals's existance";
                return View("Approvals", "Infos");
            }
            //check to see if other mod/admin has already approved this
            if (journal.IsApproved == true)
            {
                ViewBag.Message = "Looks like another mod or the admin approved this. If Katherine is reading this, you'll have to manually delete the journal in order to undo this";
                return View("Error");
            }
            db.Journals.Remove(journal);

            db.SaveChanges();
            return RedirectToAction("Approvals", "Infos");
        }

        // GET: Journals/Edit/5
        [Authorize(Roles = "Character, Mod, Admin")]
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
            var user = User.Identity.GetUserId();
            //doing this so it doesn't have to calculate is user is a player more than once
            bool isPlayer = User.IsInRole("Player");

            //error cases
            if (isPlayer && player.UserId != user)
            {
                ViewBag.Message = "Hello Mr.Rogue. This journal is not yours to edit.";
                return View("TheForbiddenZone");
            }
            else if (isPlayer && player.CurrentCharacterId != journal.CharacterId)
            {
                ViewBag.Message = "I know this was your character, but you can't edit a journal for a character you have since retired. If it's really that important, ask Katherine or a mod to do it.";
                return View("TheForbiddenZone");
            }

            if (User.IsInRole("Mod"))
            {
                ViewBag.ModChar = db.UserDetails.Where(u => u.UserId == user).Select(u => u.CurrentCharacterId).First();
            }
            return View(journal);
        }

        // POST: Journals/Edit/5
        // To protect from overposting attacks, please enable the specific properties you want to bind to, for 
        // more details see https://go.microsoft.com/fwlink/?LinkId=317598.
        [HttpPost]
        [ValidateAntiForgeryToken]
        [Authorize(Roles = "Character, Mod, Admin")]
        public ActionResult Edit([Bind(Include = "JournalId,CharacterId,OocDateWritten,TheContent,IsApproved")] Journal journal)
        {
            var user = User.Identity.GetUserId();
            Character character = db.Characters.Find(journal.CharacterId);
            UserDetail player = character.UserDetail;

            if (User.IsInRole("Player"))
            {

                if (player.UserId != user)
                {
                    ViewBag.Message = "Hello Mr.Rogue. I am sorry but my amazing security skills have once again foiled yours. The character you are trying to edit does not belong to you.";
                    return View("TheForbiddenZone");
                }
                else if (player.CurrentCharacterId != journal.CharacterId)
                {
                    ViewBag.Message = "I know this was your character, but you can't edit a journal for a character you have since retired. If it's really that important, ask Katherine or a mod to do it.";
                    return View("TheForbiddenZone");
                }

                //add modified bool while we are looking if user is a player
                journal.HasUnseenEdit = true;
            }
            else
            {
                journal.HasUnseenEdit = false;
            }

            if (ModelState.IsValid)
            {
                db.Entry(journal).State = EntityState.Modified;
                db.SaveChanges();
                #region Success Redirect based on user role
                if (journal.IsApproved)
                {
                    return RedirectToAction("Details", new { id=journal.JournalId});
                }
                //if user is a player OR is a mod editing their own current character's journal if it is unpublished
                if (User.IsInRole("Player") || (User.IsInRole("Mod") && user == player.UserId && player.CurrentCharacterId == journal.CharacterId))
                {
                    return RedirectToAction("Index", "Hub");
                }
                else
                {
                    return RedirectToAction("Approvals", "Infos");
                }
                #endregion
            }
            return View(journal);
        }

        [Authorize(Roles = "Mod, Admin")]
        public ActionResult RecognizeEdit(short id)
        {
            Journal journal = db.Journals.Where(x => x.JournalId == id).FirstOrDefault();
            journal.HasUnseenEdit = false;
            db.Entry(journal).State = EntityState.Modified;
            db.SaveChanges();
            return RedirectToAction("Approvals", "Infos");
        }


        // GET: Journals/Delete/5
        [Authorize(Roles = "Admin")]
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
        [Authorize(Roles = "Admin")]
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
