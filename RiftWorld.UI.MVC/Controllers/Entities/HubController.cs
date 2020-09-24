using System;
using System.Collections.Generic;
using System.Data;
using System.Data.Entity;
using System.Linq;
using System.Net;
using System.Web;
using System.Web.Mvc;
using Microsoft.AspNet.Identity;
using Microsoft.AspNet.Identity.EntityFramework;
using RiftWorld.DATA.EF;
using RiftWorld.UI.MVC.Models;

namespace RiftWorld.UI.MVC.Controllers.Entities
{
    [Authorize(Roles = "Character")]
    public class HubController : Controller
    {
        private RiftWorldEntities db = new RiftWorldEntities();

        // GET: Hub
        public ActionResult Index()
        {
            var userId = User.Identity.GetUserId();

            UserDetail user = db.UserDetails.Where(u => u.UserId == userId).First();

            Character character = db.Characters.Where(c => c.CharacterId == user.CurrentCharacterId).First();
            return View(character);
        }
        //todo - thought: maybe I can change the hub into ajax. 

        public PartialViewResult _Secrets()
        {
            var userId = User.Identity.GetUserId();

            UserDetail user = db.UserDetails.Where(u => u.UserId == userId).First();

            Character character = db.Characters.Where(c => c.CharacterId == user.CurrentCharacterId).First();

            var selfTags = from c in db.CharSecrets
                           where c.CharId == character.CharacterId
                           select c.SecretId
                          ;
            var known = from t in db.SecretSecretTags
                        where selfTags.Contains(t.SecretTagId)
                        select t.Secret
                        ;
            List<SecretComplete_PlayerVM> list = new List<SecretComplete_PlayerVM>() { };
            foreach (Secret secret in known)
            {
                SecretComplete_PlayerVM toAdd = new SecretComplete_PlayerVM
                {
                    Secret = secret,
                    Tags = (from s in db.SecretSecretTags
                            join st in db.SecretTags on s.SecretTagId equals st.SecretTagId
                            where s.SecretId == secret.SecretId && selfTags.Contains(s.SecretTagId)
                            select st)
                             .ToList()
                };
                list.Add(toAdd);
            }
            //GetSecretHubVM known = new GetSecretHubVM(id);
            return PartialView(list);
        }

        public PartialViewResult _UnderApproval()
        {
            var userId = User.Identity.GetUserId();

            UserDetail user = db.UserDetails.Where(u => u.UserId == userId).First();

            Character character = db.Characters.Where(c => c.CharacterId == user.CurrentCharacterId).First();

            var rumors = db.Rumors.Where(r => !r.IsApproved && r.AuthorId == character.CharacterId).ToList();
            var journals = db.Journals.Where(r => !r.IsApproved && r.CharacterId == character.CharacterId).ToList();
            var orgs = db.CharOrgs.Where(r => !r.KatherineApproved && r.CharId == character.CharacterId).ToList();

            UnderApprovalVM model = new UnderApprovalVM { Rumors = rumors, Journals = journals, Orgs = orgs };
            return PartialView(model);
        }

        public PartialViewResult _JournalLog()
        {
            var userId = User.Identity.GetUserId();

            UserDetail user = db.UserDetails.Where(u => u.UserId == userId).First();

            Character character = db.Characters.Where(c => c.CharacterId == user.CurrentCharacterId).First();

            var journals = db.Journals.Where(j => j.CharacterId == character.CharacterId && j.IsApproved).OrderByDescending(x => x.OocDateWritten).ThenByDescending(x => x.JournalId).ToList();
            List<MiniJournalVM> journalsMini = new List<MiniJournalVM>();
            foreach (Journal item in journals)
            {
                MiniJournalVM toAdd = new MiniJournalVM(item, 300);
                journalsMini.Add(toAdd);
            }

            return PartialView(journalsMini);
        }

        public PartialViewResult _Orgs()
        {
            var userId = User.Identity.GetUserId();

            UserDetail user = db.UserDetails.Where(u => u.UserId == userId).First();

            Character character = db.Characters.Where(c => c.CharacterId == user.CurrentCharacterId).First();

            var orgs = db.CharOrgs.Where(x => x.CharId == character.CharacterId && x.KatherineApproved).OrderBy(x=>x.Org.Info.Name);
            return PartialView(orgs.ToList());
        }

        public ActionResult RequestRetire()
        {
            var userId = User.Identity.GetUserId();

            UserDetail user = db.UserDetails.Where(u => u.UserId == userId).First();

            Character character = db.Characters.Where(c => c.CharacterId == user.CurrentCharacterId).First();

            character.IsRequestingRetire = true;
            db.Entry(character).State = EntityState.Modified;
            db.SaveChanges();
            return RedirectToAction("Index");
        }

        public ActionResult CancelRetireRequest()
        {
            var userId = User.Identity.GetUserId();

            UserDetail user = db.UserDetails.Where(u => u.UserId == userId).First();

            Character character = db.Characters.Where(c => c.CharacterId == user.CurrentCharacterId).First();

            character.IsRequestingRetire = false;
            db.Entry(character).State = EntityState.Modified;
            db.SaveChanges();
            return RedirectToAction("Index");

        }

        public ActionResult ChangeLocation()
        {
            var userId = User.Identity.GetUserId();

            UserDetail user = db.UserDetails.Where(u => u.UserId == userId).First();

            Character character = db.Characters.Where(c => c.CharacterId == user.CurrentCharacterId).First();

            ViewBag.CurrentLocationId = new SelectList(db.Locales.Where(l=>l.Info.IsPublished).OrderBy(l => l.Info.Name), "LocaleId", "Name", character.CurrentLocationId);

            return View();
        }

        [HttpPost]
        [ValidateAntiForgeryToken]
        public ActionResult ChangeLocation(short currentLocationId)
        {
            var userId = User.Identity.GetUserId();

            UserDetail user = db.UserDetails.Where(u => u.UserId == userId).First();

            Character character = db.Characters.Where(c => c.CharacterId == user.CurrentCharacterId).First();

            character.CurrentLocationId = currentLocationId;
            character.HasUnseenEdit = true;

            db.Entry(character).State = EntityState.Modified;
            db.SaveChanges();
            return RedirectToAction("Index");
        }

        // GET: Journals/Create
        public ActionResult Journal()
        {
            string userId = User.Identity.GetUserId();
            UserDetail currentUser = db.UserDetails.Where(u => u.UserId == userId).FirstOrDefault();
            if (currentUser.CurrentCharacterId == null)
            {
                ViewBag.Message = "You can't journal without an active character.";
                return View("TheForbiddenZone");
            }

            return View();
        }

        // POST: Journals/Create
        // To protect from overposting attacks, please enable the specific properties you want to bind to, for 
        // more details see https://go.microsoft.com/fwlink/?LinkId=317598.
        [HttpPost]
        [ValidateAntiForgeryToken]
        public ActionResult Journal([Bind(Include = "JournalId,TheContent")] Journal journal)
        {
            string userId = User.Identity.GetUserId();
            UserDetail currentUser = db.UserDetails.Where(u => u.UserId == userId).FirstOrDefault();
            if (currentUser.CurrentCharacterId == null)
            {
                ViewBag.Message = "I'm not sure how you got this far, but you can't journal without an active character.";
                return View("TheForbiddenZone");
            }

            journal.IsApproved = false;
            journal.OocDateWritten = DateTime.Now.Date;
            journal.HasUnseenEdit = false;
            journal.CharacterId = (short)currentUser.CurrentCharacterId;
            if (ModelState.IsValid)
            {
                db.Journals.Add(journal);
                db.SaveChanges();
                return RedirectToAction("Index");
            }

            return View(journal);
        }

    }
}