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

namespace RiftWorld.UI.MVC.Controllers.Entities
{
    //todo - uncomment to lockdown controller
    //[Authorize(Roles = "Admin")]
    public class LocalesController : Controller
    {
        private RiftWorldEntities db = new RiftWorldEntities();

        // GET: Locales
        [OverrideAuthorization]
        public ActionResult Index()
        {
            //v1 - for testing so I don't have to constantly switch accounts
            var locales = db.Locales.Include(l => l.Info).Include(l => l.LocaleLevel).Include(l => l.Locale1).Include(l => l.Locale2).Include(l => l.NPC);

            //v2 - prevent non-admin from seeing unpublished work
            //todo - uncomment below to prevent users from seeing un-published work
            //var locales = db.Locales.Include(l => l.Info).Include(l => l.LocaleLevel).Include(l => l.Locale1).Include(l => l.Locale2).Include(l => l.NPC).Where(l =>l.IsPublished);

            return View(locales.ToList());
        }

        // GET: Locales/Details/5
        [OverrideAuthorization]
        public ActionResult Details(short? id)
        {
            if (id == null)
            {
                return new HttpStatusCodeResult(HttpStatusCode.BadRequest);
            }
            Locale locale = db.Locales.Find(id);
            if (locale == null)
            {
                return HttpNotFound();
            }

            //todo - uncomment below to prevent users from seeing un-published work
            //if (!locale.IsPublished && !User.IsInRole("Admin"))
            //{
            //    return View("Error");
            //    //todo change redirect to a error 404 page
            //}

            return View(locale);
        }

        [OverrideAuthorization]
        public PartialViewResult _Majorities(short id)
        {
            return PartialView();
        }

        [OverrideAuthorization]
        public PartialViewResult _Events(short id)
        {
            return PartialView();
        }

        // GET: Locales/Create
        public ActionResult Create()
        {
            ViewBag.LevelOfLocaleId = new SelectList(db.LocaleLevels, "LocaleLevelId", "LocaleName");
            ViewBag.RegionId = new SelectList(db.Locales.Where(l => l.LevelOfLocaleId == 1), "LocaleId", "Name"); //<-- only locales of region type
            ViewBag.ClosestCityId = new SelectList(db.Locales.Where(l => l.LevelOfLocaleId == 2), "LocaleId", "Name"); //<-- only locales of city type
            ViewBag.CouncilDelegateId = new SelectList(db.NPCs, "NpcId", "Name");
            ViewBag.Tags = new MultiSelectList(db.Tags, "TagId", "TagName");

            return View();
        }

        // POST: Locales/Create
        // To protect from overposting attacks, please enable the specific properties you want to bind to, for 
        // more details see https://go.microsoft.com/fwlink/?LinkId=317598.
        [HttpPost]
        [ValidateAntiForgeryToken]
        public ActionResult Create([Bind(Include = "Name,LevelOfLocaleId,RegionId,ClosestCityId,CouncilDelegateId,Appointed,Environment,About, AvgLifestyle, Blurb")] LocaleCreateVM locale,
            List<short> tags,
            string submit)
        {
            //so this proves that the base submit can get the list of items on select. I'd have to figure out how to get the additional data while still not preventing the default of submit 

            locale.IsPublished = false;
            //note the save/publish is intentionally NOT here in order to prevent posting it unintentionally
            if (ModelState.IsValid)
            {
                #region Info
                Info info;
                info = new Info
                {
                    InfoTypeId = 4,  //<-------------------Info Type ID. CHANGE UPON COPY
                    IdWithinType = null,
                    Blurb = locale.Blurb,
                    Name = locale.Name,
                    IsPublished = locale.IsPublished
                };
                db.Infos.Add(info);
                db.SaveChanges(); //this has to go here in order to ensure that the infoId short below is accurate. Also at this point I am doing no further gets on validity so there is no point to not saving 
                #endregion
                short infoId = db.Infos.Max(i => i.InfoId);

                #region Adding Tags
                if (tags != null)
                {
                    foreach (short t in tags)
                    {
                        InfoTag infoTag = new InfoTag { InfoId = infoId, TagId = t };
                        db.InfoTags.Add(infoTag);
                    }
                }
                #endregion

                #region Locale
                Locale daLocale = new Locale
                {
                    InfoId = infoId,
                    Name = locale.Name,
                    LevelOfLocaleId = locale.LevelOfLocaleId,
                    RegionId = locale.RegionId,
                    ClosestCityId = locale.ClosestCityId,
                    CouncilDelegateId = locale.CouncilDelegateId,
                    Appointed = locale.Appointed,
                    Environment = locale.Environment,
                    About = locale.About,
                    IsPublished = locale.IsPublished,
                    AvgLifestyle = locale.AvgLifestyle
                };
                db.Locales.Add(daLocale);
                db.SaveChanges();
                #endregion

                #region give info the IdWithinType
                short maxi = db.Locales.Max(l => l.LocaleId);
                info.IdWithinType = maxi;
                db.Entry(info).State = EntityState.Modified;
                db.SaveChanges();
                #endregion

                //move to step 2
                return RedirectToAction("AssoCreate", new { id = maxi, submit = submit });
            }

            //if model is not valid
            ViewBag.LevelOfLocaleId = new SelectList(db.LocaleLevels, "LocaleLevelId", "LocaleName", locale.LevelOfLocaleId);
            ViewBag.RegionId = new SelectList(db.Locales.Where(l => l.LevelOfLocaleId == 1), "LocaleId", "Name", locale.RegionId);
            ViewBag.ClosestCityId = new SelectList(db.Locales.Where(l => l.LevelOfLocaleId == 2), "LocaleId", "Name", locale.ClosestCityId);
            ViewBag.CouncilDelegateId = new SelectList(db.NPCs, "NpcId", "Name", locale.CouncilDelegateId);

            ViewBag.Tags = new MultiSelectList(db.Tags, "TagId", "TagName", tags);

            ModelState.AddModelError("", "Something has gone wrong. Look for red text to see where is went wrong");
            return View(locale);
        }

        public ActionResult AssoCreate(short? id, string submit)
        {
            if (id == null)
            {
                return new HttpStatusCodeResult(HttpStatusCode.BadRequest);
            }
            Locale locale = db.Locales.Find(id);
            if (locale == null)
            {
                return HttpNotFound();
            }

            ViewBag.Majs = db.Races.ToList();
            var selected = db.Majorities.Where(t => t.LocaleId == locale.LocaleId).ToList();
            List<AssoMaj> assoMajs = new List<AssoMaj>();
            foreach (Majority majority in selected)
            {
                AssoMaj toAdd = new AssoMaj(majority);
                assoMajs.Add(toAdd);
            }

            ViewBag.Events = db.Events.ToList();
            var selected2 = db.LocaleEvents.Where(i => i.LocaleId == locale.LocaleId).ToList();
            List<AssoEvent_Locale> assoEvents = new List<AssoEvent_Locale>();
            foreach (LocaleEvent localeEvent in selected2)
            {
                AssoEvent_Locale toAdd = new AssoEvent_Locale(localeEvent);
                assoEvents.Add(toAdd);
            }

            var infoId = locale.InfoId;
            AssoLocaleVM model = new AssoLocaleVM { InfoId = infoId, LocaleId = locale.LocaleId, Submit = submit, AssoMajs = assoMajs, AssoEvents = assoEvents, Name = locale.Name };
            return View(model);
        }

        [HttpPost]
        [ValidateAntiForgeryToken]
        public ActionResult AssoCreate(short infoId, short localeId, string submit,
            List<AssoMaj> majs,
            List<AssoEvent_Locale> events)
        {
            var locale = db.Locales.Where(i => i.LocaleId == localeId).FirstOrDefault();

            if (ModelState.IsValid)
            {
                var info = db.Infos.Where(i => i.InfoId == infoId).FirstOrDefault();
                #region Save or Publish?
                switch (submit)
                {
                    case "Save Progress":
                    case "Un-Publish":
                    case "Save and Continue":
                        info.IsPublished = false;
                        locale.IsPublished = false;
                        break;
                    case "Publish":
                    case "Save":
                        info.IsPublished = true;
                        locale.IsPublished = true;
                        break;
                    case "Save and associate":
                        break;
                    default:
                        break;
                }
                db.Entry(locale).State = EntityState.Modified;
                db.Entry(info).State = EntityState.Modified;
                #endregion

                #region Add majorities
                List<byte> currentMajId = db.Majorities.Where(x => x.LocaleId == localeId).Select(x => x.RaceId).ToList();
                if (majs != null)
                {
                    foreach (AssoMaj maj in majs)
                    {
                        //if the association already exists
                        if (currentMajId.Contains(maj.RaceId))
                        {
                            Majority toEdit = db.Majorities.Where(x => x.RaceId == maj.RaceId && x.LocaleId == localeId).First();
                            toEdit.IsFirst = maj.IsFirst;
                            db.Entry(toEdit).State = EntityState.Modified;
                            db.SaveChanges();
                            currentMajId.Remove(maj.RaceId);
                        }
                        else
                        {
                            Majority toAdd = new Majority
                            {
                                LocaleId = localeId,
                                IsFirst = maj.IsFirst,
                                RaceId = maj.RaceId
                            };
                            db.Majorities.Add(toAdd);
                            db.SaveChanges();
                        }
                    }
                }
                if (currentMajId.Count != 0)
                {
                    foreach (byte id in currentMajId)
                    {
                        Majority gone = db.Majorities.Where(x => x.LocaleId == localeId && x.RaceId == id).FirstOrDefault();
                        db.Majorities.Remove(gone);
                        db.SaveChanges();
                    }
                }
                #endregion

                #region Add LocaleEvents
                List<short> currentEventId = db.LocaleEvents.Where(x => x.LocaleId == localeId).Select(x => x.EventId).ToList();
                if (events != null)
                {
                    foreach (AssoEvent_Locale assoEvent in events)
                    {
                        //if the association already exists
                        if (currentEventId.Contains(assoEvent.EventId))
                        {
                            LocaleEvent toEdit = db.LocaleEvents.Where(x => x.LocaleId == localeId && x.EventId == assoEvent.EventId).First();
                            //if I ever add more columns to LocaleEvent, edit them here
                            //db.Entry(toEdit).State = EntityState.Modified;
                            //db.SaveChanges();
                            currentEventId.Remove(assoEvent.EventId);
                        }
                        else
                        {
                            LocaleEvent toAdd = new LocaleEvent
                            {
                                LocaleId = localeId,
                                EventId = assoEvent.EventId
                            };
                            db.LocaleEvents.Add(toAdd);
                        }
                    }
                }
                if (currentEventId.Count != 0)
                {
                    foreach (short id in currentEventId)
                    {
                        LocaleEvent gone = db.LocaleEvents.Where(x => x.LocaleId == localeId && x.EventId == id).FirstOrDefault();
                        db.LocaleEvents.Remove(gone);
                    }
                }
                db.SaveChanges();
                #endregion
                return Json(true);
            }
            //having a lot of trouble with the return so the ajax right now
            ViewBag.Majs = db.Races.ToList();
            ViewBag.Events = db.Events.ToList();
            AssoLocaleVM model = new AssoLocaleVM { InfoId = infoId, LocaleId = localeId, Submit = submit, AssoMajs = majs, AssoEvents = events, Name = locale.Name };
            return View(model);
        }

        // GET: Locales/Edit/5
        public ActionResult Edit(short? id)
        {
            if (id == null)
            {
                return new HttpStatusCodeResult(HttpStatusCode.BadRequest);
            }
            Locale locale = db.Locales.Find(id);
            if (locale == null)
            {
                return HttpNotFound();
            }
            ViewBag.LevelOfLocaleId = new SelectList(db.LocaleLevels, "LocaleLevelId", "LocaleName", locale.LevelOfLocaleId);
            ViewBag.RegionId = new SelectList(db.Locales, "LocaleId", "Name", locale.RegionId);
            ViewBag.ClosestCityId = new SelectList(db.Locales, "LocaleId", "Name", locale.ClosestCityId);
            ViewBag.CouncilDelegateId = new SelectList(db.NPCs, "NpcId", "Name", locale.CouncilDelegateId);

            short infoid = locale.InfoId;
            string blurb = db.Infos.Where(i => i.InfoId == infoid).Select(i => i.Blurb).First();
            LocaleEditVM model = new LocaleEditVM(locale, blurb);

            List<short> selectedTags = db.InfoTags.Where(t => t.InfoId == infoid).Select(t => t.TagId).ToList();
            ViewBag.Selected = selectedTags;
            ViewBag.Tags = db.Tags.ToList();

            return View(model);
        }

        // POST: Locales/Edit/5
        // To protect from overposting attacks, please enable the specific properties you want to bind to, for 
        // more details see https://go.microsoft.com/fwlink/?LinkId=317598.
        [HttpPost]
        [ValidateAntiForgeryToken]
        public ActionResult Edit([Bind(Include = "LocaleId,InfoId,Name,LevelOfLocaleId,RegionId,ClosestCityId,CouncilDelegateId,Appointed,Environment,About,AvgLifestyle, Blurb, IsPublished")] LocaleEditPostVM locale,
            List<short> tags,
            string submit)
        {
            #region Save or Publish?
            switch (submit)
            {
                case "Save Progress":
                case "Un-Publish":
                    locale.IsPublished = false;
                    break;
                case "Publish":
                case "Save":
                    locale.IsPublished = true;
                    break;
                case "Save and go to complex edit":
                    break;
            }
            #endregion

            if (ModelState.IsValid)
            {
                var infoid = locale.InfoId;
                #region Info Update
                //Info info = db.Infos.Find(infoid);
                Info info = db.Infos.Where(i => i.InfoId == infoid).FirstOrDefault();
                info.Name = locale.Name;
                info.Blurb = locale.Blurb;
                info.IsPublished = locale.IsPublished;
                #endregion

                #region Update tags
                List<short> currentTagIds = db.InfoTags.Where(x => x.InfoId == infoid).Select(x => x.TagId).ToList();

                if (tags != null)
                {
                    foreach (short tag in tags)
                    {
                        //if this is an already existing tag 
                        if (currentTagIds.Contains(tag))
                        {
                            currentTagIds.Remove(tag);
                        }
                        //if this is a newly added tag
                        else
                        {
                            InfoTag newTag = new InfoTag { InfoId = infoid, TagId = tag };
                            db.InfoTags.Add(newTag);
                        }
                    }
                }

                if (currentTagIds.Count != 0)
                {
                    foreach (short id in currentTagIds)
                    {
                        InfoTag gone = db.InfoTags.Where(x => x.InfoId == infoid & x.TagId == id).FirstOrDefault();
                        db.InfoTags.Remove(gone);
                    }
                }

                #endregion

                #region Update Locale
                Locale daLocale = new Locale
                {
                    InfoId = locale.InfoId,
                    LocaleId = locale.LocaleId,
                    Name = locale.Name,
                    LevelOfLocaleId = locale.LevelOfLocaleId,
                    RegionId = locale.RegionId,
                    ClosestCityId = locale.ClosestCityId,
                    CouncilDelegateId = locale.CouncilDelegateId,
                    Appointed = locale.Appointed,
                    Environment = locale.Environment,
                    About = locale.About,
                    IsPublished = locale.IsPublished,
                    AvgLifestyle = locale.AvgLifestyle
                };
                db.Entry(daLocale).State = EntityState.Modified;
                db.Entry(info).State = EntityState.Modified;
                db.SaveChanges();
                #endregion
                if (submit == "Save and go to complex edit")
                {
                    return RedirectToAction("AssoEdit", new { id = locale.LocaleId });
                }
                return RedirectToAction("Details", new { id = locale.LocaleId });
            }

            //if model invalid
            ViewBag.LevelOfLocaleId = new SelectList(db.LocaleLevels, "LocaleLevelId", "LocaleName", locale.LevelOfLocaleId);
            ViewBag.RegionId = new SelectList(db.Locales, "LocaleId", "Name", locale.RegionId);
            ViewBag.ClosestCityId = new SelectList(db.Locales, "LocaleId", "Name", locale.ClosestCityId);
            ViewBag.CouncilDelegateId = new SelectList(db.NPCs, "NpcId", "Name", locale.CouncilDelegateId);

            ViewBag.Tags = db.Tags.ToList();
            if (tags != null)
            {
                ViewBag.Selected = tags;
            }
            else
            {
                ViewBag.Selected = new List<short>();
            }
            ModelState.AddModelError("", "Something has gone wrong. Look for red text to see where is went wrong");
            LocaleEditVM aLocale = new LocaleEditVM(locale);
            return View(aLocale);
        }

        public ActionResult AssoEdit(short? id)
        {
            if (id == null)
            {
                return new HttpStatusCodeResult(HttpStatusCode.BadRequest);
            }
            Locale locale = db.Locales.Find(id);
            if (locale == null)
            {
                return HttpNotFound();
            }

            ViewBag.Majs = db.Races.ToList();
            var selected = db.Majorities.Where(t => t.LocaleId == locale.LocaleId).ToList();
            List<AssoMaj> assoMajs = new List<AssoMaj>();
            foreach (Majority majority in selected)
            {
                AssoMaj toAdd = new AssoMaj(majority);
                assoMajs.Add(toAdd);
            }

            ViewBag.Events = db.Events.ToList();
            var selected2 = db.LocaleEvents.Where(i => i.LocaleId == locale.LocaleId).ToList();
            List<AssoEvent_Locale> assoEvents = new List<AssoEvent_Locale>();
            foreach (LocaleEvent localeEvent in selected2)
            {
                AssoEvent_Locale toAdd = new AssoEvent_Locale(localeEvent);
                assoEvents.Add(toAdd);
            }

            var infoId = locale.InfoId;
            AssoLocaleVM model = new AssoLocaleVM { InfoId = infoId, LocaleId = locale.LocaleId, Submit = "Save", AssoMajs = assoMajs, AssoEvents = assoEvents, Name = locale.Name };
            return View(model);
        }

        [HttpPost]
        [ValidateAntiForgeryToken]
        public ActionResult AssoEdit(short infoId, short localeId, string submit,
            List<AssoMaj> majs,
            List<AssoEvent_Locale> events)
        {
            if (ModelState.IsValid)
            {
                #region Add majorities
                List<byte> currentMajId = db.Majorities.Where(x => x.LocaleId == localeId).Select(x => x.RaceId).ToList();
                if (majs != null)
                {
                    foreach (AssoMaj maj in majs)
                    {
                        //if the association already exists
                        if (currentMajId.Contains(maj.RaceId))
                        {
                            Majority toEdit = db.Majorities.Where(x => x.RaceId == maj.RaceId && x.LocaleId == localeId).First();
                            toEdit.IsFirst = maj.IsFirst;
                            db.Entry(toEdit).State = EntityState.Modified;
                            db.SaveChanges();
                            currentMajId.Remove(maj.RaceId);
                        }
                        else
                        {
                            Majority toAdd = new Majority
                            {
                                LocaleId = localeId,
                                IsFirst = maj.IsFirst,
                                RaceId = maj.RaceId
                            };
                            db.Majorities.Add(toAdd);
                            db.SaveChanges();
                        }
                    }
                }
                if (currentMajId.Count != 0)
                {
                    foreach (byte id in currentMajId)
                    {
                        Majority gone = db.Majorities.Where(x => x.LocaleId == localeId && x.RaceId == id).FirstOrDefault();
                        db.Majorities.Remove(gone);
                        db.SaveChanges();
                    }
                }
                #endregion

                #region Add LocaleEvents
                List<short> currentEventId = db.LocaleEvents.Where(x => x.LocaleId == localeId).Select(x => x.EventId).ToList();
                if (events != null)
                {
                    foreach (AssoEvent_Locale assoEvent in events)
                    {
                        //if the association already exists
                        if (currentEventId.Contains(assoEvent.EventId))
                        {
                            LocaleEvent toEdit = db.LocaleEvents.Where(x => x.LocaleId == localeId && x.EventId == assoEvent.EventId).First();
                            //if I ever add more columns to LocaleEvent, edit them here
                            //db.Entry(toEdit).State = EntityState.Modified;
                            //db.SaveChanges();
                            currentEventId.Remove(assoEvent.EventId);
                        }
                        else
                        {
                            LocaleEvent toAdd = new LocaleEvent
                            {
                                LocaleId = localeId,
                                EventId = assoEvent.EventId
                            };
                            db.LocaleEvents.Add(toAdd);
                        }
                    }
                }
                if (currentEventId.Count != 0)
                {
                    foreach (short id in currentEventId)
                    {
                        LocaleEvent gone = db.LocaleEvents.Where(x => x.LocaleId == localeId && x.EventId == id).FirstOrDefault();
                        db.LocaleEvents.Remove(gone);
                    }
                }
                db.SaveChanges();
                #endregion
                return Json(true);
            }
            //having a lot of trouble with the return so the ajax right now
            ViewBag.Majs = db.Races.ToList();
            ViewBag.Events = db.Events.ToList();
            var locale = db.Locales.Find(localeId);
            AssoLocaleVM model = new AssoLocaleVM { InfoId = infoId, LocaleId = localeId, Submit = submit, AssoMajs = majs, AssoEvents = events, Name = locale.Name };
            return View(model);
        }


        // GET: Locales/Delete/5
        public ActionResult Delete(short? id)
        {
            if (id == null)
            {
                return new HttpStatusCodeResult(HttpStatusCode.BadRequest);
            }
            Locale locale = db.Locales.Find(id);
            if (locale == null)
            {
                return HttpNotFound();
            }
            return View(locale);
        }

        // POST: Locales/Delete/5
        [HttpPost, ActionName("Delete")]
        [ValidateAntiForgeryToken]
        public ActionResult DeleteConfirmed(short id)
        {
            Locale locale = db.Locales.Find(id);
            short infoId = locale.InfoId;
            db.Locales.Remove(locale);

            #region Remove Associations

            #region Remove Majorities
            List<Majority> majs = db.Majorities.Where(x => x.LocaleId == id).ToList();
            foreach (Majority gone in majs)
            {
                db.Majorities.Remove(gone);
            }
            #endregion

            #region Remove LocaleEvents
            List<LocaleEvent> events = db.LocaleEvents.Where(x => x.LocaleId == id).ToList();
            foreach (LocaleEvent gone in events)
            {
                db.LocaleEvents.Remove(gone);
            }
            #endregion
            #endregion

            #region Remove Rumors
            var rumors = db.Rumors.Where(r => r.RumorOfId == infoId);
            foreach (Rumor r in rumors)
            {
                db.Rumors.Remove(r);
            }
            #endregion

            #region Remove Secrets
            var secrets = db.Secrets.Where(s => s.IsAboutId == infoId);
            List<short> secretIds = db.Secrets.Where(s => s.IsAboutId == infoId).Select(s => s.SecretId).ToList();
            List<SecretSecretTag> ssts = db.SecretSecretTags.Where(s => secretIds.Contains(s.SecretId)).ToList();

            //remove sst
            foreach (SecretSecretTag secretSecretTag in ssts)
            {
                db.SecretSecretTags.Remove(secretSecretTag);
            }

            //remove secrets
            foreach (Secret secret in secrets)
            {
                db.Secrets.Remove(secret);
            }

            #endregion

            #region remove stories
            var stories = db.Stories.Where(s => s.IsAboutId == infoId);
            List<short> storyIds = db.Stories.Where(s => s.IsAboutId == infoId).Select(s => s.StoryId).ToList();
            List<StoryTag> st = db.StoryTags.Where(s => storyIds.Contains(s.StoryId)).ToList();

            //remove story tags
            foreach (StoryTag storyTag in st)
            {
                db.StoryTags.Remove(storyTag);
            }

            //remove stories
            foreach (Story story in stories)
            {
                db.Stories.Remove(story);
            }
            #endregion

            #region Remove info
            Info info = db.Infos.Where(i => i.InfoId == infoId).First();
            var infoTags = db.InfoTags.Where(it => it.InfoId == infoId).ToList();
            foreach (InfoTag infoTag in infoTags)
            {
                db.InfoTags.Remove(infoTag);
            }

            db.Infos.Remove(info);
            #endregion

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
