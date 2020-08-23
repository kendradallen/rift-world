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

namespace RiftWorld.UI.MVC.Controllers
{
    public class CharactersController : Controller
    {
        private RiftWorldEntities db = new RiftWorldEntities();

        // GET: Characters
        public ActionResult Index()
        {
            var characters = db.Characters.Include(c => c.Gender).Include(c => c.Locale).Include(c => c.Race).Include(c => c.Tier).Include(c => c.UserDetails);
            return View(characters.ToList());
        }

        // GET: Characters/Details/5
        public ActionResult Details(short? id)
        {
            if (id == null)
            {
                return new HttpStatusCodeResult(HttpStatusCode.BadRequest);
            }
            Character character = db.Characters.Find(id);
            if (character == null)
            {
                return HttpNotFound();
            }
            return View(character);
        }

        // GET: Characters/Create
        [Authorize(Roles = "Player, Mod")]
        public ActionResult Create()
        {
            string userId = User.Identity.GetUserId();
            UserDetail currentUser = db.UserDetails.Where(u => u.UserId == userId).FirstOrDefault();
            if (currentUser.CurrentCharacterId != null)
            {
                ViewBag.Message = "You already have a character. You can't make another one until you have decided to retire the other";
                return View("Error");
            }
            Character currentCharacter = db.Characters.Where(x => x.PlayerId == userId && !x.IsRetired).FirstOrDefault();
            if (currentCharacter != null)
            {
                ViewBag.Message = "You currently have one character already awaiting approval. Please wait paitently for the results to come back on it";
                return View("Error");
            }

            ViewBag.GenderId = new SelectList(db.Genders, "GenderId", "GenderName");
            ViewBag.CurrentLocationId = new SelectList(db.Locales, "LocaleId", "Name");
            ViewBag.RaceId = new SelectList(db.Races, "RaceId", "RaceName");
            ViewBag.TierId = new SelectList(db.Tiers, "TierId", "TierName");

            ViewBag.startCharacterName = 25;
            ViewBag.startDescription = 2000;
            ViewBag.startAbout = 8000;
            ViewBag.startClass = 40;

            return View();
        }

        // POST: Characters/Create
        // To protect from overposting attacks, please enable the specific properties you want to bind to, for 
        // more details see https://go.microsoft.com/fwlink/?LinkId=317598.
        [HttpPost]
        [ValidateAntiForgeryToken]
        [Authorize(Roles = "Player, Mod")]
        public ActionResult Create([Bind(Include = "CharacterId,CharacterName,RaceId,GenderId,PortraitFileName,Description,About,CurrentLocationId,TierId, Artist, ClassString")] Character character,
            HttpPostedFileBase picture)
        {
            string userId = User.Identity.GetUserId();
            UserDetail currentUser = db.UserDetails.Where(u => u.UserId == userId).FirstOrDefault();
            if (currentUser.CurrentCharacterId != null)
            {
                ViewBag.Message = "I'm not sure how you got this far, but you already have a character. You can't make another one until you have decided to retire the other";
                return View("Error");
            }
            Character currentCharacter = db.Characters.Where(x => x.PlayerId == userId && !x.IsRetired).FirstOrDefault();
            if (currentCharacter != null)
            {
                ViewBag.Message = "I'm not sure how you got this far, but you currently have one character already awaiting approval. Please wait paitently for the results to come back on it";
                return View("Error");
            }

            character.PlayerId = userId;
            character.IsApproved = false;
            character.IsRetired = false;
            character.IsDead = false;
            character.HasUnseenEdit = false;
            if (picture != null)
            {
                string[] goodExts = { ".jpg", ".jpeg", ".gif", ".png" };
                string imgName = picture.FileName;

                var length = picture.ContentLength;
                string ext = imgName.Substring(imgName.LastIndexOf('.'));

                if (!goodExts.Contains(ext.ToLower()))
                {
                    ModelState.AddModelError("PortraitFileName", "You have submitted a incorrect file type for your portrait. Please use either: .jpg, .jpeg, .gif, or .png");
                }
                if (length > 50000)
                {
                    ModelState.AddModelError("PortraitFileName", "The file you submitted is too big! There is an arbitrarily set limit of 50kb on the size. Check the interaction guide for advice.");
                }
                if (character.Artist == null)
                {
                    ModelState.AddModelError("Artist", "Hey, we want to really want to give credit where credit is due on this website. You haven't listed the artist that did that artwork you tried to submit. Re-upload that artwork and list who drew it so we can all gush about it!");
                }
            }
            if (ModelState.IsValid)
            {
                db.Characters.Add(character);
                db.SaveChanges();

                List<short> charaIds = db.Characters.Where(x => x.PlayerId == userId).Select(x => x.CharacterId).ToList();
                short charaId = charaIds.Max();
                #region Image uploads

                if (picture != null)
                {
                    string[] goodExts = { ".jpg", ".jpeg", ".gif", ".png" };

                    string imgName = picture.FileName;
                    string ext = imgName.Substring(imgName.LastIndexOf('.'));

                    imgName = "character-" + charaId.ToString() + ext;

                    picture.SaveAs(Server.MapPath("~/Content/img/character/" + imgName));
                    character.PortraitFileName = imgName;
                    db.Entry(character).State = EntityState.Modified;
                    db.SaveChanges();
                }
                #endregion

                return RedirectToAction("Index");
            }

            ViewBag.GenderId = new SelectList(db.Genders, "GenderId", "GenderName", character.GenderId);
            ViewBag.CurrentLocationId = new SelectList(db.Locales, "LocaleId", "Name", character.CurrentLocationId);
            ViewBag.RaceId = new SelectList(db.Races, "RaceId", "RaceName", character.RaceId);
            ViewBag.TierId = new SelectList(db.Tiers, "TierId", "TierName", character.TierId);

            ViewBag.startCharacterName = 25;
            ViewBag.startDescription = 2000;
            ViewBag.startAbout = 8000;
            ViewBag.startClass = 40;
            if (picture != null)
            {
                ModelState.AddModelError("PortraitFileName", "Hey, there was some error, so you have to re-upload the picture");
            }

            if (character.CharacterName != null)
            {
                ViewBag.startCharacterName = 25 - character.CharacterName.Length;
            }
            if (character.Description != null)
            {
                ViewBag.startDescription = 2000 - character.Description.Length;
            }
            if (character.About != null)
            {
                ViewBag.startAbout = 8000 - character.About.Length;
            }
            if (character.ClassString != null)
            {
                ViewBag.startClass = 40 - character.ClassString.Length;
            }
            return View(character);
        }

        // GET: Characters/Edit/5
        public ActionResult Edit(short? id)
        {
            if (id == null)
            {
                return new HttpStatusCodeResult(HttpStatusCode.BadRequest);
            }
            Character character = db.Characters.Find(id);
            if (character == null)
            {
                return HttpNotFound();
            }
            ViewBag.GenderId = new SelectList(db.Genders, "GenderId", "GenderName", character.GenderId);
            ViewBag.CurrentLocationId = new SelectList(db.Locales, "LocaleId", "Name", character.CurrentLocationId);
            ViewBag.RaceId = new SelectList(db.Races, "RaceId", "RaceName", character.RaceId);
            ViewBag.TierId = new SelectList(db.Tiers, "TierId", "TierName", character.TierId);

            ViewBag.startCharacterName = 25 - character.CharacterName.Length;
            ViewBag.startDescription = 2000 - character.Description.Length;
            ViewBag.startAbout = 8000 - character.About.Length;
            ViewBag.startClass = 40 - character.ClassString.Length;
            return View(character);
        }

        // POST: Characters/Edit/5
        // To protect from overposting attacks, please enable the specific properties you want to bind to, for 
        // more details see https://go.microsoft.com/fwlink/?LinkId=317598.
        [HttpPost]
        [ValidateAntiForgeryToken]
        public ActionResult Edit([Bind(Include = "CharacterId,PlayerId,CharacterName,RaceId,GenderId,PortraitFileName,Description,About,CurrentLocationId,TierId,IsRetired,IsApproved, Artist, ClassString, IsDead")] Character character,
            HttpPostedFileBase picture)
        {
            //check if picture uploaded is valid BEFORE checking  if model is valid
            //if picture does not meet my given extra parameters, make the model invalid
            if (picture != null)
            {
                string[] goodExts = { ".jpg", ".jpeg", ".gif", ".png" };
                string imgName = picture.FileName;

                var length = picture.ContentLength;
                string ext = imgName.Substring(imgName.LastIndexOf('.'));

                if (!goodExts.Contains(ext.ToLower()))
                {
                    ModelState.AddModelError("PortraitFileName", "You have submitted a incorrect file type for your portrait. Please use either: .jpg, .jpeg, .gif, or .png");
                }
                if (length > 50000)
                {
                    ModelState.AddModelError("PortraitFileName", "The file you submitted is too big! There is an arbitrarily set limit of 50kb on the size. Check the interaction guide for advice.");
                }
                if (character.Artist == null)
                {
                    ModelState.AddModelError("Artist", "Hey, we want to really want to give credit where credit is due on this website. You haven't listed the artist that did that artwork you tried to submit. Re-upload that artwork and list who drew it so we can all gush about it!");
                }
            }

            //error catch for if they did not upload a new picture (and said picture is NOT the default) but removed the artist text
            else if (character.PortraitFileName != null && character.Artist == null)
            {
                ModelState.AddModelError("Artist", "Hey, we want to really want to give credit where credit is due on this website. Looks like at some point during the editing of the charcter, you accidentally removed the artist text. Please re-add it!");
            }

            character.HasUnseenEdit = true;
            //now actually check the model
            if (ModelState.IsValid)
            {
                #region Image uploads

                if (picture != null)
                {
                    string[] goodExts = { ".jpg", ".jpeg", ".gif", ".png" };

                    string imgName = picture.FileName;
                    string ext = imgName.Substring(imgName.LastIndexOf('.'));

                    imgName = "character-" + character.CharacterId.ToString() + ext;

                    picture.SaveAs(Server.MapPath("~/Content/img/character/" + imgName));
                    character.PortraitFileName = imgName;
                }
                #endregion
                db.Entry(character).State = EntityState.Modified;
                db.SaveChanges();
                return RedirectToAction("Index");
            }

            ViewBag.GenderId = new SelectList(db.Genders, "GenderId", "GenderName", character.GenderId);
            ViewBag.CurrentLocationId = new SelectList(db.Locales, "LocaleId", "Name", character.CurrentLocationId);
            ViewBag.RaceId = new SelectList(db.Races, "RaceId", "RaceName", character.RaceId);
            ViewBag.TierId = new SelectList(db.Tiers, "TierId", "TierName", character.TierId);

            ViewBag.startCharacterName = 25;
            ViewBag.startDescription = 2000;
            ViewBag.startAbout = 8000;
            ViewBag.startClass = 40;
            if (character.CharacterName != null)
            {
                ViewBag.startCharacterName = 25 - character.CharacterName.Length;
            }
            if (character.Description != null)
            {
                ViewBag.startDescription = 2000 - character.Description.Length;
            }
            if (character.About != null)
            {
                ViewBag.startAbout = 8000 - character.About.Length;
            }
            if (character.ClassString != null)
            {
                ViewBag.startClass = 40 - character.ClassString.Length;
            }
            if (picture != null)
            {
                ModelState.AddModelError("PortraitFileName", "Hey, there was some error, so you have to re-upload the picture");
            }
            return View(character);
        }

        // GET: Characters/Delete/5
        public ActionResult Delete(short? id)
        {
            if (id == null)
            {
                return new HttpStatusCodeResult(HttpStatusCode.BadRequest);
            }
            Character character = db.Characters.Find(id);
            if (character == null)
            {
                return HttpNotFound();
            }
            return View(character);
        }

        // POST: Characters/Delete/5
        [HttpPost, ActionName("Delete")]
        [ValidateAntiForgeryToken]
        public ActionResult DeleteConfirmed(short id)
        {
            Character character = db.Characters.Find(id);
            db.Characters.Remove(character);
            db.SaveChanges();
            return RedirectToAction("Index");
        }

        public PartialViewResult _Secrets(short id)
        {
            var selfTags = from c in db.CharSecrets
                           where c.CharId == id
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

        [Authorize(Roles = "Mod, Admin")]
        public ActionResult Approve(int? id)
        {
            if (id == null)
            {
                return new HttpStatusCodeResult(HttpStatusCode.BadRequest);
            }
            Character character = db.Characters.Find(id);
            if (character == null)
            {
                return HttpNotFound();
            }
            return View(character);
        }

        // To protect from overposting attacks, please enable the specific properties you want to bind to, for 
        // more details see https://go.microsoft.com/fwlink/?LinkId=317598.
        [HttpPost]
        [ValidateAntiForgeryToken]
        [Authorize(Roles = "Mod, Admin")]
        public ActionResult Approve(short characterId)
        {
            Character character = db.Characters.Where(x => x.CharacterId == characterId).First();
            character.IsApproved = true;
            character.HasUnseenEdit = false;
            if (ModelState.IsValid)
            {
                db.Entry(character).State = EntityState.Modified;
                db.SaveChanges();
                return RedirectToAction("Index");
            }
            return View(character);
        }

        //[Authorize(Roles = "Player, Mod")]
        public ActionResult Hub(short? id)
        {
            if (id == null)
            {
                return new HttpStatusCodeResult(HttpStatusCode.BadRequest);
            }
            Character character = db.Characters.Find(id);
            if (character == null)
            {
                return HttpNotFound();
            }
            return View(character);
        }

        public ActionResult JournalHub(short? id)
        {
            if (id == null)
            {
                return new HttpStatusCodeResult(HttpStatusCode.BadRequest);
            }
            Character character = db.Characters.Find(id);
            if (character == null)
            {
                return HttpNotFound();
            }

            var journals = db.Journals.Where(x => x.CharacterId == id && x.IsApproved);
            return View(journals.ToList());
        }

        public PartialViewResult _Journals(short id)
        {
            var journalsHolder = db.Journals.Where(x => x.CharacterId == id && x.IsApproved).OrderByDescending(x => x.OocDateWritten);
            bool hasMore = false;
            if (journalsHolder.Count() > 5)
            {
                hasMore = true;
            }

            List<MiniJournalVM> journalsMini = new List<MiniJournalVM>();
            foreach (Journal journal in journalsHolder.Take(5))
            {
                MiniJournalVM toAdd = new MiniJournalVM(journal);
                journalsMini.Add(toAdd);
            }
            JournalVM journals = new JournalVM { Journals = journalsMini, HasMore = hasMore, CharacterId = id };
            return PartialView(journals);
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
