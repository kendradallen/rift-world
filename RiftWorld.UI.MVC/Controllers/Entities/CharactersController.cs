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

namespace RiftWorld.UI.MVC.Controllers
{
    public class CharactersController : Controller
    {
        //todo change delete to use the backup name instead
        private RiftWorldEntities db = new RiftWorldEntities();

        // GET: Characters
        public ActionResult Index()
        {
            //v1 - for testing so I don't have to constantly switch accounts
            //var characters = db.Characters.Include(c => c.Gender).Include(c => c.Locale).Include(c => c.Race).Include(c => c.Tier).Include(c => c.UserDetails);

            //v2 - don't list unapproved characters
            var characters = db.Characters.Include(c => c.Gender).Include(c => c.Locale).Include(c => c.Race).Include(c => c.Tier).Include(c => c.UserDetails).Where(c => c.IsApproved);

            return View(characters.ToList());
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
            //prevent getting an un-approved character
            if (!character.IsApproved && !User.IsInRole("Mod") && !User.IsInRole("Admin"))
            {
                return View("Error");
                //todo change redirect to a error page
            }
            return View(character);
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

        [Authorize(Roles = "Admin")]
        public ActionResult KnownSecrets(short? id)
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
            var tags = db.CharSecrets.Include(c => c.SecretTag).Where(c => c.CharId == id).Select(c => c.SecretTag).ToList();
            SeePlayerSecrets model = new SeePlayerSecrets { CharacterName = character.CharacterName, CharId = character.CharacterId, Secrets = list, Tags = tags };
            return View(model);
        }

        // GET: Characters/Create
        [Authorize(Roles = "Player, Mod")]
        public ActionResult Create()
        {
            string userId = User.Identity.GetUserId();
            UserDetail currentUser = db.UserDetails.Where(u => u.UserId == userId).FirstOrDefault();
            if (User.IsInRole("Character"))
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
            ViewBag.CurrentLocationId = new SelectList(db.Locales.OrderBy(l => l.Name), "LocaleId", "Name");
            ViewBag.RaceId = new SelectList(db.Races.Where(r => r.IsPlayerEnabled), "RaceId", "RaceName");
            ViewBag.TierId = new SelectList(db.Tiers, "TierId", "TierName");

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
            if (User.IsInRole("Character"))
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
            character.IsPlayerDemo = false;
            character.IsRequestingRetire = false;
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
                if (length > 75000) //todo - figure out a preciese number this file size limiter should be. I'm thinking either 50kb or 75kb
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
                //todo - change to a confirmation screen awaiting approval
                return View("Confirmed");
            }

            ViewBag.GenderId = new SelectList(db.Genders, "GenderId", "GenderName", character.GenderId);
            ViewBag.CurrentLocationId = new SelectList(db.Locales.OrderBy(l => l.Name), "LocaleId", "Name", character.CurrentLocationId);
            ViewBag.RaceId = new SelectList(db.Races.Where(r => r.IsPlayerEnabled).OrderBy(r => r.RaceName), "RaceId", "RaceName", character.RaceId);
            ViewBag.TierId = new SelectList(db.Tiers, "TierId", "TierName", character.TierId);
            if (picture != null)
            {
                ModelState.AddModelError("PortraitFileName", "Hey, there was some error, so you have to re-upload the picture");
            }
            return View(character);
        }

        public ActionResult Confirmed()
        {
            return View();
        }

        [Authorize(Roles = "Mod, Admin")]
        public ActionResult Approve(short? id)
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

        [HttpPost]
        [ValidateAntiForgeryToken]
        [Authorize(Roles = "Mod, Admin")]
        public ActionResult Approve(short characterId)
        {
            //todo - add logic to check if the approved thing and what's on the db match
            Character character = db.Characters.Where(x => x.CharacterId == characterId).FirstOrDefault();
            //check to see if other mod/admin has already denied this (thus it won't exist)
            if (character == null)
            {
                ViewBag.Message = "Looks like another mod or the admin denied this character's existance";
                return View("Error");
            }
            //check to see if other mod/admin has already approved this
            if (character.IsApproved == true)
            {
                ViewBag.Message = "Looks like another mod or the admin approved this";
                return View("Approvals", "Infos");
            }
            character.IsApproved = true;
            character.HasUnseenEdit = false;

            UserDetail playerDetail = db.UserDetails.Where(u => u.UserId == character.PlayerId).FirstOrDefault();
            //check if the player actually exists
            if (playerDetail == null)
            {
                character.IsRetired = true;
                db.Entry(character).State = EntityState.Modified;
                db.SaveChanges();

                ViewBag.Message = "The player assigned to this character no longer exists, but the character has been approved. It will be labeled as retired";
                return View("Approvals", "Infos");
            }
            playerDetail.CurrentCharacterId = character.CharacterId;

            //role grabbing in preperation for changing acess throughout website (through adding of role "Character")
            var userManager = new UserManager<ApplicationUser>(new UserStore<ApplicationUser>(new ApplicationDbContext()));

            var player = db.AspNetUsers.Find(character.PlayerId);
            var userRoles = userManager.GetRoles(player.Id);

            var charRole = db.AspNetRoles.FirstOrDefault(u => u.Name.Equals("Character"));
            var charRoleConvert = Convert.ToString(charRole.Name);
            if (charRole != null && !userRoles.Contains(charRoleConvert))
            {
                userManager.AddToRole(player.Id, charRoleConvert);
            }

            db.Entry(character).State = EntityState.Modified;
            db.Entry(playerDetail).State = EntityState.Modified;
            db.SaveChanges();
            return RedirectToAction("Details", new { id = characterId });
        }

        [HttpPost]
        [ValidateAntiForgeryToken]
        [Authorize(Roles = "Mod, Admin")]
        public ActionResult Deny(short characterId)
        {
            Character character = db.Characters.Where(x => x.CharacterId == characterId).FirstOrDefault();
            //check to see if other mod/admin has already denied this (thus it won't exist)
            if (character == null)
            {
                ViewBag.Message = "Looks like another mod or the admin denied this character's existance";
                return View("Approvals", "Infos");
            }
            //check to see if other mod/admin has already approved this
            if (character.IsApproved == true)
            {
                ViewBag.Message = "Looks like another mod or the admin approved this. If Katherine is reading this, you'll have to manually delete the character in order to undo this";
                return View("Error");
            }

            //remove character from db
            string picture = character.PortraitFileName;
            db.Characters.Remove(character);

            #region Remove Picture
            string fullPath = Request.MapPath("~/Content/img/character/" + picture);
            if (System.IO.File.Exists(fullPath))
            {
                System.IO.File.Delete(fullPath);
            }
            #endregion

            db.SaveChanges();
            return RedirectToAction("Approvals", "Infos");
        }

        [Authorize(Roles = "Admin")]
        public ActionResult AdminCreate()
        {
            ViewBag.GenderId = new SelectList(db.Genders, "GenderId", "GenderName");
            ViewBag.CurrentLocationId = new SelectList(db.Locales.OrderBy(l => l.Name), "LocaleId", "Name");
            ViewBag.RaceId = new SelectList(db.Races.Where(r => r.IsPlayerEnabled), "RaceId", "RaceName");
            ViewBag.TierId = new SelectList(db.Tiers, "TierId", "TierName");
            ViewBag.PlayerId = new SelectList(db.UserDetails.Where(u => u.IsApproved).OrderBy(u => u.DiscordFull), "UserId", "DiscordFull");
            return View();
        }

        [HttpPost]
        [ValidateAntiForgeryToken]
        [Authorize(Roles = "Admin")]
        public ActionResult AdminCreate([Bind(Include = "CharacterId,CharacterName,RaceId,GenderId,PortraitFileName,Description,About,CurrentLocationId,TierId, Artist, ClassString, PlayerId, IsDead, BackupPortrayerName")] Character character,
            HttpPostedFileBase picture)
        {
            character.IsRetired = true;
            character.IsApproved = true;
            character.HasUnseenEdit = false;
            character.IsRequestingRetire = false;

            character.IsPlayerDemo = false;

            if (character.PlayerId == null && character.BackupPortrayerName != null)
            {
                //make demo the assigned player
                character.IsPlayerDemo = true;
            }
            else if (character.PlayerId == null && character.BackupPortrayerName == null)
            {
                //add error that I need it 
            }

            //check if picture is valid as if it was player making it

            if (ModelState.IsValid)
            {
                //post pic 

                db.Characters.Add(character);
                db.SaveChanges();
            }

            ViewBag.GenderId = new SelectList(db.Genders, "GenderId", "GenderName", character.GenderId);
            ViewBag.CurrentLocationId = new SelectList(db.Locales.OrderBy(l => l.Name), "LocaleId", "Name", character.CurrentLocationId);
            ViewBag.RaceId = new SelectList(db.Races.Where(r => r.IsPlayerEnabled), "RaceId", "RaceName", character.RaceId);
            ViewBag.TierId = new SelectList(db.Tiers, "TierId", "TierName", character.TierId);
            ViewBag.PlayerId = new SelectList(db.UserDetails.Where(u => u.IsApproved).OrderBy(u => u.DiscordFull), "UserId", "DiscordFull", character.PlayerId);
            return View(character);
        }

        [Authorize(Roles = "Admin")]
        public ActionResult AssignUserAsPlayer()
        {
            ViewBag.CharacterId = new SelectList(db.Characters.Where(c => c.IsPlayerDemo), "CharacterId", "CharacterName");
            ViewBag.PlayerId = new SelectList(db.UserDetails.OrderBy(u => u.DiscordFull), "UserId", "DiscordFull");
            return View();
        }

        [HttpPost]
        [ValidateAntiForgeryToken]
        [Authorize(Roles = "Admin")]
        public ActionResult AssignUserAsPlayer(short characterId, string playerId)
        {
            Character character = db.Characters.Where(c => c.CharacterId == characterId).FirstOrDefault();
            if (character == null)
            {
                //REALLY error out
            }

            UserDetail player = db.UserDetails.Find(playerId);
            if (player == null)
            {
                //error out
            }

            //if all above doesn't error out
            character.PlayerId = playerId;
            character.IsPlayerDemo = false;
            character.BackupPortrayerName = "";

            db.Entry(character).State = EntityState.Modified;
            db.SaveChanges();
            return RedirectToAction("Details", new { id = characterId });
        }


        // GET: Characters/Edit/5
        [Authorize(Roles = "Player, Admin, Mod, Character")]
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

            string userid = User.Identity.GetUserId();
            short? currentCharacter = db.UserDetails.Where(u => u.UserId == userid).Select(u => u.CurrentCharacterId).FirstOrDefault();

            if (!User.IsInRole("Character") && User.IsInRole("Player") && character.PlayerId != userid)
            {
                ViewBag.Message = "Hello Mr.Rogue. I am sorry but my amazing security skills have once again foiled yours. The character you are trying to edit does not belong to you.";
                return View("Error");
            }
            //if this isn't a mod, check that the character actually belongs to the current user (and is their 
            else if (User.IsInRole("Character") && !User.IsInRole("Mod") && currentCharacter != id)
            {
                ViewBag.Message = "Hello Mr.Rogue. I am sorry but my amazing security skills have once again foiled yours. The character you are trying to edit does not belong to you.";
                return View("Error");
            }

            ViewBag.GenderId = new SelectList(db.Genders, "GenderId", "GenderName", character.GenderId);
            ViewBag.CurrentLocationId = new SelectList(db.Locales.OrderBy(l => l.Name), "LocaleId", "Name", character.CurrentLocationId);
            ViewBag.RaceId = new SelectList(db.Races.Where(r => r.IsPlayerEnabled).OrderBy(r => r.RaceName), "RaceId", "RaceName", character.RaceId);
            ViewBag.TierId = new SelectList(db.Tiers, "TierId", "TierName", character.TierId);

            return View(character);
        }

        // POST: Characters/Edit/5
        // To protect from overposting attacks, please enable the specific properties you want to bind to, for 
        // more details see https://go.microsoft.com/fwlink/?LinkId=317598.
        [HttpPost]
        [ValidateAntiForgeryToken]
        [Authorize(Roles = "Player, Character, Mod, Admin")]
        public ActionResult Edit([Bind(Include = "CharacterId,PlayerId,CharacterName,RaceId,GenderId,PortraitFileName,Description,About,CurrentLocationId,TierId,IsRetired,IsApproved, Artist, ClassString, IsDead")] Character character,
            HttpPostedFileBase picture)
        {
            #region Is this yours?
            //make sure that if whoever is editing this is not a mod or admin that this is actually their character
            string userid = User.Identity.GetUserId();
            short? currentCharacter = db.UserDetails.Where(u => u.UserId == userid).Select(u => u.CurrentCharacterId).FirstOrDefault();
            if (!User.IsInRole("Character") && User.IsInRole("Player") && character.PlayerId != userid)
            {
                ViewBag.Message = "Hello Mr.Rogue. I am sorry but my amazing security skills have once again foiled yours. The character you are trying to edit does not belong to you.";
                return View("Error");
            }
            else if (User.IsInRole("Character") && !User.IsInRole("Mod") && currentCharacter != character.CharacterId)
            {
                ViewBag.Message = "Hello Mr.Rogue. I am sorry but my amazing security skills have once again foiled yours. The character you are trying to edit does not belong to you.";
                return View("Error");
            }
            #endregion

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
                if (length > 75000) //todo - figure out a preciese number this file size limiter should be. I'm thinking either 50kb or 75kb
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


            //todo - make sure that this logic is correct. I.e. Dan could edit this. (where he'd edit it I have no idea)
            if (User.IsInRole("Player"))
            {
                character.HasUnseenEdit = true;
            }
            else
            {
                character.HasUnseenEdit = false;
            }
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
                    //remove old picture if it had a different extension (and thus would not be overridden)
                    string oldName = character.PortraitFileName;
                    string oldExt = oldName.Substring(oldName.LastIndexOf('.'));
                    if (oldExt != ext)
                    {
                        string fullPath = Request.MapPath("~/Content/img/character/" + oldName);
                        if (System.IO.File.Exists(fullPath))
                        {
                            System.IO.File.Delete(fullPath);
                        }
                    }
                    //assign new PortraitFileName
                    character.PortraitFileName = imgName;
                }
                #endregion
                db.Entry(character).State = EntityState.Modified;
                db.SaveChanges();
                return RedirectToAction("Index");
            }

            ViewBag.GenderId = new SelectList(db.Genders, "GenderId", "GenderName", character.GenderId);
            ViewBag.CurrentLocationId = new SelectList(db.Locales.OrderBy(l => l.Name), "LocaleId", "Name", character.CurrentLocationId);
            ViewBag.RaceId = new SelectList(db.Races.Where(r => r.IsPlayerEnabled).OrderBy(r => r.RaceName), "RaceId", "RaceName", character.RaceId);
            ViewBag.TierId = new SelectList(db.Tiers, "TierId", "TierName", character.TierId);

            if (picture != null)
            {
                ModelState.AddModelError("PortraitFileName", "Hey, there was some error, so you have to re-upload the picture");
            }
            return View(character);
        }

        [Authorize(Roles = "Mod, Admin")]
        public ActionResult RecognizeEdit(short id)
        {
            Character character = db.Characters.Where(x => x.CharacterId == id).FirstOrDefault();
            character.HasUnseenEdit = false;
            db.Entry(character).State = EntityState.Modified;
            db.SaveChanges();
            return RedirectToAction("Approvals", "Infos");
        }

        // GET: Characters/Delete/5
        [Authorize(Roles = "Admin")]
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
        [Authorize(Roles = "Admin")]
        public ActionResult DeleteConfirmed(short id)
        {
            Character character = db.Characters.Find(id);
            string playerId = character.PlayerId;
            var player = db.AspNetUsers.Find(character.PlayerId);
            string picture = character.PortraitFileName;
            db.Characters.Remove(character);

            #region Remove Picture
            string fullPath = Request.MapPath("~/Content/img/character/" + picture);
            if (System.IO.File.Exists(fullPath))
            {
                System.IO.File.Delete(fullPath);
            }

            #endregion

            #region Remove Rumors
            List<Rumor> rumors = db.Rumors.Where(x => x.AuthorId == id).ToList();
            foreach (Rumor gone in rumors)
            {
                db.Rumors.Remove(gone);
            }
            #endregion

            #region Remove CharSecrets
            List<CharSecret> secrets = db.CharSecrets.Where(x => x.CharId == id).ToList();
            foreach (CharSecret gone in secrets)
            {
                db.CharSecrets.Remove(gone);
            }
            #endregion

            #region Remove CharOrgs
            List<CharOrg> orgs = db.CharOrgs.Where(x => x.OrgId == id).ToList();
            foreach (CharOrg gone in orgs)
            {
                db.CharOrgs.Remove(gone);
            }
            #endregion

            #region Remove Journals
            List<Journal> journals = db.Journals.Where(x => x.CharacterId == id).ToList();
            foreach (Journal gone in journals)
            {
                db.Journals.Remove(gone);
            }
            #endregion

            #region Remove ClassCharacters
            List<ClassCharacter> taclasses = db.ClassCharacters.Where(x => x.CharacterId == id).ToList();
            foreach (ClassCharacter gone in taclasses)
            {
                db.ClassCharacters.Remove(gone);
            }
            #endregion

            #region Update User

            //role grabbing in preperation for changing acess throughout website (through adding of role "Character")
            var userManager = new UserManager<ApplicationUser>(new UserStore<ApplicationUser>(new ApplicationDbContext()));

            var userRoles = userManager.GetRoles(player.Id);

            var charRole = db.AspNetRoles.FirstOrDefault(u => u.Name.Equals("Character"));
            var charRoleConvert = Convert.ToString(charRole.Name);
            if (charRole != null && userRoles.Contains(charRoleConvert))
            {
                userManager.RemoveFromRole(player.Id, charRoleConvert);
            }

            UserDetail playerDetail = db.UserDetails.Where(x => x.UserId == playerId).FirstOrDefault();
            playerDetail.CurrentCharacterId = null;
            db.Entry(playerDetail).State = EntityState.Modified;
            #endregion

            db.SaveChanges();
            return RedirectToAction("Index");
        }

        [Authorize(Roles = "Mod, Admin")]
        public ActionResult DenyRetire(short id)
        {
            Character character = db.Characters.Where(x => x.CharacterId == id).FirstOrDefault();
            character.IsRequestingRetire = false;
            db.Entry(character).State = EntityState.Modified;
            db.SaveChanges();
            return RedirectToAction("Approvals", "Infos", null);
        }
        [Authorize(Roles = "Mod, Admin")]
        public ActionResult Retire(short? id, bool didTheyDie)
        {
            if (id == null)
            {
                return new HttpStatusCodeResult(HttpStatusCode.BadRequest);
            }
            Character character = db.Characters.Where(x => x.CharacterId == id).FirstOrDefault();
            if (character == null)
            {
                return HttpNotFound();
            }

            character.IsRequestingRetire = false;
            character.IsRetired = true;
            if (didTheyDie)
            {
                character.IsDead = true;
            }
            var player = db.AspNetUsers.Find(character.PlayerId);

            //role grabbing in preperation for changing acess throughout website (through adding of role "Character")
            var userManager = new UserManager<ApplicationUser>(new UserStore<ApplicationUser>(new ApplicationDbContext()));

            var userRoles = userManager.GetRoles(player.Id);

            var charRole = db.AspNetRoles.FirstOrDefault(u => u.Name.Equals("Character"));
            var charRoleConvert = Convert.ToString(charRole.Name);
            if (charRole != null && userRoles.Contains(charRoleConvert))
            {
                userManager.RemoveFromRole(player.Id, charRoleConvert);
            }

            UserDetail playerDetail = db.UserDetails.Where(x => x.UserId == player.Id).FirstOrDefault();
            playerDetail.CurrentCharacterId = null;
            db.Entry(character).State = EntityState.Modified;
            db.Entry(playerDetail).State = EntityState.Modified;

            db.SaveChanges();
            return RedirectToAction("Details", new { id = id });
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
