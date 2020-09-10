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
    [Authorize(Roles = "Admin")]
    public class EventsController : Controller
    {
        private RiftWorldEntities db = new RiftWorldEntities();

        // GET: Events
        [OverrideAuthorization]
        public ActionResult Index()
        {
            //admin sees all (client request)
            List<Event> events = new List<Event> { };
            if (User.IsInRole("Admin"))
            {
                events = db.Events.Include(i => i.Info).OrderBy(e=> e.Info.Name).ToList();
            }
            //everyone else doesn't see unpublished work
            else
            {
                events = db.Events.Include(i => i.Info).Where(i => i.Info.IsPublished).OrderBy(e => e.Info.Name).ToList();
            }

            return View(events.ToList());
        }

        // GET: Events/Details/5
        [OverrideAuthorization]
        public ActionResult Details(short? id)
        {
            if (id == null)
            {
                return new HttpStatusCodeResult(HttpStatusCode.BadRequest);
            }
            Event taevent = db.Events.Find(id);
            if (taevent == null)
            {
                return HttpNotFound();
            }

            //prevent users from seeing un-published work
            if (!taevent.IsPublished && !User.IsInRole("Admin"))
            {
                return View("Error");
                //todo change redirect to a error 404 page
            }

            return View(taevent);
        }

        [OverrideAuthorization]
        public PartialViewResult _Locales(short id)
        {
            List<_EventLocalesVM> locales = (from le in db.LocaleEvents
                                             join l in db.Locales on le.LocaleId equals l.LocaleId
                                             join i in db.Infos on l.InfoId equals i.InfoId
                                             where i.IsPublished && le.EventId == id
                                             select new _EventLocalesVM
                                             {
                                                 Name = i.Name,
                                                 Id = l.LocaleId
                                             })
                                             .ToList()
                                             ;
            return PartialView(locales);
        }

        [OverrideAuthorization]
        public PartialViewResult _Orgs(short id)
        {
            List<_EventOrgsVM> orgs = (from oe in db.OrgEvents
                                       join o in db.Orgs on oe.OrgId equals o.OrgId
                                       join i in db.Infos on o.InfoId equals i.InfoId
                                       where i.IsPublished && oe.EventId == id
                                       select new _EventOrgsVM
                                       {
                                           Name = i.Name,
                                           Id = o.OrgId
                                       })
                                       .ToList()
                                       ;
            return PartialView(orgs);
        }

        // GET: Events/Create
        public ActionResult Create()
        {
            ViewBag.Tags = new MultiSelectList(db.Tags, "TagId", "TagName");
            return View();
        }

        // POST: Events/Create
        // To protect from overposting attacks, please enable the specific properties you want to bind to, for 
        // more details see https://go.microsoft.com/fwlink/?LinkId=317598.
        [HttpPost]
        [ValidateAntiForgeryToken]
        public ActionResult Create([Bind(Include = "Name,IsHistory,AboutText,NormalParticipants,DateMonth,DateSeason, Blurb, IsSecret")] EventCreateVM taevent,
            List<short> tags,
            string submit)
        {
            taevent.IsPublished = false;
            if (ModelState.IsValid)
            {
                #region Info
                Info info;
                info = new Info
                {
                    InfoTypeId = 6,  //<-------------------Info Type ID. CHANGE UPON COPY
                    IdWithinType = null,
                    Blurb = taevent.Blurb,
                    Name = taevent.Name,
                    IsPublished = taevent.IsPublished,
                    IsSecret = taevent.IsSecret
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

                #region Event
                Event daEvent = new Event
                {
                    InfoId = infoId,
                    IsHistory = taevent.IsHistory,
                    AboutText = taevent.AboutText,
                    NormalParticipants = taevent.NormalParticipants,
                    DateMonth = taevent.DateMonth,
                    DateSeason = taevent.DateSeason
                };
                db.Events.Add(daEvent);
                db.SaveChanges();
                #endregion

                #region give info the IdWithinType
                short maxi = db.Events.Max(l => l.EventId);
                info.IdWithinType = maxi;
                db.Entry(info).State = EntityState.Modified;
                db.SaveChanges();
                #endregion

                //move to step 2
                return RedirectToAction("AssoCreate", new { id = maxi, submit = submit });
            }

            //if model is not valid
            ViewBag.Tags = new MultiSelectList(db.Tags, "TagId", "TagName", tags);

            ModelState.AddModelError("", "Something has gone wrong. Look for red text to see where is went wrong");
            return View(taevent);
        }

        public ActionResult AssoCreate(short? id, string submit)
        {
            if (id == null)
            {
                return new HttpStatusCodeResult(HttpStatusCode.BadRequest);
            }
            Event taevent = db.Events.Find(id);
            if (taevent == null)
            {
                return HttpNotFound();
            }
            ViewBag.Locales = db.Locales.OrderBy(l=>l.Info.Name).ToList();
            var selected = db.LocaleEvents.Where(i => i.EventId == taevent.EventId).ToList();
            List<AssoLocale_Event> assoLocales = new List<AssoLocale_Event>();
            foreach (LocaleEvent locale in selected)
            {
                AssoLocale_Event toAdd = new AssoLocale_Event(locale);
                assoLocales.Add(toAdd);
            }

            ViewBag.Orgs = db.Orgs.OrderBy(l => l.Info.Name).ToList();
            var selected2 = db.OrgEvents.Where(i => i.EventId == taevent.EventId).ToList();
            List<AssoOrg_Event> assoOrgs = new List<AssoOrg_Event>();
            foreach (OrgEvent org in selected2)
            {
                AssoOrg_Event toAdd = new AssoOrg_Event(org);
                assoOrgs.Add(toAdd);
            }

            var infoid = taevent.InfoId;
            AssoEventVM model = new AssoEventVM { InfoId = infoid, EventId = taevent.EventId, Submit = submit, Name = taevent.Name, AssoLocales = assoLocales, AssoOrgs = assoOrgs };
            return View(model);
        }

        [HttpPost]
        [ValidateAntiForgeryToken]
        public ActionResult AssoCreate(short infoId, short eventId, string submit,
            List<AssoLocale_Event> locales,
            List<AssoOrg_Event> orgs)
        {
            var taevent = db.Events.Where(i => i.EventId == eventId).FirstOrDefault();
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
                        break;
                    case "Publish":
                    case "Save":
                        info.IsPublished = true;
                        break;
                    default:
                        break;
                }
                db.Entry(info).State = EntityState.Modified;
                #endregion

                #region Add Locales
                List<short> currentLocaleId = db.LocaleEvents.Where(x => x.EventId == eventId).Select(x => x.LocaleId).ToList();
                if (locales != null)
                {
                    foreach (AssoLocale_Event le in locales)
                    {
                        //if the association already exists
                        if (currentLocaleId.Contains(le.LocaleId))
                        {
                            LocaleEvent toEdit = db.LocaleEvents.Where(x => x.LocaleId == le.LocaleId && x.EventId == eventId).First();
                            //if I ever add more columns to LocaleEvent, edit them here
                            //db.Entry(toEdit).State = EntityState.Modified;
                            //db.SaveChanges();
                            currentLocaleId.Remove(le.LocaleId);
                        }
                        else
                        {
                            LocaleEvent toAdd = new LocaleEvent
                            {
                                EventId = eventId,
                                LocaleId = le.LocaleId
                            };
                            db.LocaleEvents.Add(toAdd);
                        }
                    }
                }
                if (currentLocaleId.Count != 0)
                {
                    foreach (short id in currentLocaleId)
                    {
                        LocaleEvent gone = db.LocaleEvents.Where(x => x.EventId == eventId && x.LocaleId == id).FirstOrDefault();
                        db.LocaleEvents.Remove(gone);
                    }
                }
                db.SaveChanges();
                #endregion

                #region Add Orgs
                List<short> currentOrgId = db.OrgEvents.Where(x => x.EventId == eventId).Select(x => x.OrgId).ToList();
                if (orgs != null)
                {
                    foreach (AssoOrg_Event assoOrg in orgs)
                    {
                        //if the association already exists
                        if (currentOrgId.Contains(assoOrg.OrgId))
                        {
                            OrgEvent toEdit = db.OrgEvents.Where(x => x.OrgId == assoOrg.OrgId && x.EventId == eventId).First();
                            toEdit.Blurb = assoOrg.Blurb;
                            db.Entry(toEdit).State = EntityState.Modified;
                            db.SaveChanges();
                            currentOrgId.Remove(assoOrg.OrgId);
                        }
                        else
                        {
                            OrgEvent toAdd = new OrgEvent
                            {
                                EventId = eventId,
                                OrgId = assoOrg.OrgId,
                                Blurb = assoOrg.Blurb
                            };
                            db.OrgEvents.Add(toAdd);
                        }
                    }

                }
                if (currentOrgId.Count != 0)
                {
                    foreach (short id in currentOrgId)
                    {
                        OrgEvent gone = db.OrgEvents.Where(x => x.EventId == eventId && x.OrgId == id).FirstOrDefault();
                        db.OrgEvents.Remove(gone);
                    }
                }
                db.SaveChanges();
                #endregion
                return Json(true);
            }

            //if model fails
            ViewBag.Locales = db.Locales.OrderBy(l => l.Info.Name).ToList();
            ViewBag.Orgs = db.Orgs.OrderBy(l => l.Info.Name).ToList();
            //if I actually was handling if the model failed (as currently I'm using the bandaid solution of just preventing the submition ) this would not cut it. It does not account for orgs or locales being null
            AssoEventVM model = new AssoEventVM { InfoId = infoId, EventId = eventId, Submit = submit, Name = taevent.Name, AssoOrgs = orgs, AssoLocales = locales };
            return View(model);
        }

        public ActionResult Skip(short infoId, string submit)
        {
            var info = db.Infos.Where(i => i.InfoId == infoId).FirstOrDefault();

            #region Save or Publish?
            switch (submit)
            {
                case "Save Progress":
                case "Un-Publish":
                case "Save and Continue":
                    info.IsPublished = false;
                    break;
                case "Publish":
                case "Save":
                    info.IsPublished = true;
                    break;
                case "Save and associate":
                    break;
                default:
                    break;
            }
            #endregion
            db.Entry(info).State = EntityState.Modified;
            db.SaveChanges();
            return RedirectToAction("Details", new { id = info.IdWithinType });
        }


        // GET: Events/Edit/5
        public ActionResult Edit(short? id)
        {
            if (id == null)
            {
                return new HttpStatusCodeResult(HttpStatusCode.BadRequest);
            }
            Event taevent = db.Events.Find(id);
            if (taevent == null)
            {
                return HttpNotFound();
            }

            short infoid = taevent.InfoId;
            Info info = db.Infos.Find(infoid);
            EventEditVM model = new EventEditVM(taevent, info);

            List<short> selectedTags = db.InfoTags.Where(t => t.InfoId == infoid).Select(t => t.TagId).ToList();
            ViewBag.Selected = selectedTags;
            ViewBag.Tags = db.Tags.ToList();

            return View(model);
        }

        // POST: Events/Edit/5
        // To protect from overposting attacks, please enable the specific properties you want to bind to, for 
        // more details see https://go.microsoft.com/fwlink/?LinkId=317598.
        [HttpPost]
        [ValidateAntiForgeryToken]
        public ActionResult Edit([Bind(Include = "EventId,InfoId,Name,IsHistory,AboutText,NormalParticipants,IsPublished,DateMonth,DateSeason, Blurb, IsSecret")] EventEditPostVM taevent,
            List<short> tags,
            string submit)
        {
            if (ModelState.IsValid)
            {
                #region Save or Publish?
                switch (submit)
                {
                    case "Save Progress":
                    case "Un-Publish":
                        taevent.IsPublished = false;
                        break;
                    case "Publish":
                    case "Save":
                        taevent.IsPublished = true;
                        break;
                    case "Save and go to complex edit":
                        break;
                }
                #endregion

                var infoid = taevent.InfoId;
                #region Info Update
                //Info info = db.Infos.Find(infoid);
                Info info = db.Infos.Where(i => i.InfoId == infoid).FirstOrDefault();
                info.Name = taevent.Name;
                info.Blurb = taevent.Blurb;
                info.IsPublished = taevent.IsPublished;
                info.IsSecret = taevent.IsSecret;
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

                #region Update Event
                Event daEvent = new Event
                {
                    InfoId = taevent.InfoId,
                    EventId = taevent.EventId,
                    IsHistory = taevent.IsHistory,
                    AboutText = taevent.AboutText,
                    NormalParticipants = taevent.NormalParticipants,
                    DateMonth = taevent.DateMonth,
                    DateSeason = taevent.DateSeason
                };
                db.Entry(daEvent).State = EntityState.Modified;
                db.Entry(info).State = EntityState.Modified;
                db.SaveChanges();
                #endregion
                if (submit == "Save and go to complex edit")
                {
                    return RedirectToAction("AssoEdit", new { id = taevent.EventId });
                }
                return RedirectToAction("Details", new { id = taevent.EventId });
            }

            //if model invalid
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
            EventEditVM aEvent = new EventEditVM(taevent);
            return View(aEvent);
        }

        public ActionResult AssoEdit(short? id)
        {
            if (id == null)
            {
                return new HttpStatusCodeResult(HttpStatusCode.BadRequest);
            }
            Event taevent = db.Events.Find(id);
            if (taevent == null)
            {
                return HttpNotFound();
            }
            ViewBag.Locales = db.Locales.OrderBy(l => l.Info.Name).ToList();
            var selected = db.LocaleEvents.Where(i => i.EventId == taevent.EventId).ToList();
            List<AssoLocale_Event> assoLocales = new List<AssoLocale_Event>();
            foreach (LocaleEvent locale in selected)
            {
                AssoLocale_Event toAdd = new AssoLocale_Event(locale);
                assoLocales.Add(toAdd);
            }

            ViewBag.Orgs = db.Orgs.OrderBy(l => l.Info.Name).ToList();
            var selected2 = db.OrgEvents.Where(i => i.EventId == taevent.EventId).ToList();
            List<AssoOrg_Event> assoOrgs = new List<AssoOrg_Event>();
            foreach (OrgEvent org in selected2)
            {
                AssoOrg_Event toAdd = new AssoOrg_Event(org);
                assoOrgs.Add(toAdd);
            }

            var infoid = taevent.InfoId;
            AssoEventVM model = new AssoEventVM { InfoId = infoid, EventId = taevent.EventId, Submit = "Save", Name = taevent.Name, AssoLocales = assoLocales, AssoOrgs = assoOrgs };
            return View(model);
        }

        [HttpPost]
        [ValidateAntiForgeryToken]
        public ActionResult AssoEdit(short infoId, short eventId, string submit,
            List<AssoLocale_Event> locales,
            List<AssoOrg_Event> orgs)
        {
            if (ModelState.IsValid)
            {
                #region Add Locales
                List<short> currentLocaleId = db.LocaleEvents.Where(x => x.EventId == eventId).Select(x => x.LocaleId).ToList();
                if (locales != null)
                {
                    foreach (AssoLocale_Event le in locales)
                    {
                        //if the association already exists
                        if (currentLocaleId.Contains(le.LocaleId))
                        {
                            LocaleEvent toEdit = db.LocaleEvents.Where(x => x.LocaleId == le.LocaleId && x.EventId == eventId).First();
                            //if I ever add more columns to LocaleEvent, edit them here
                            //db.Entry(toEdit).State = EntityState.Modified;
                            //db.SaveChanges();
                            currentLocaleId.Remove(le.LocaleId);
                        }
                        else
                        {
                            LocaleEvent toAdd = new LocaleEvent
                            {
                                EventId = eventId,
                                LocaleId = le.LocaleId
                            };
                            db.LocaleEvents.Add(toAdd);
                        }
                    }
                }
                if (currentLocaleId.Count != 0)
                {
                    foreach (short id in currentLocaleId)
                    {
                        LocaleEvent gone = db.LocaleEvents.Where(x => x.EventId == eventId && x.LocaleId == id).FirstOrDefault();
                        db.LocaleEvents.Remove(gone);
                    }
                }
                db.SaveChanges();
                #endregion

                #region Add Orgs
                List<short> currentOrgId = db.OrgEvents.Where(x => x.EventId == eventId).Select(x => x.OrgId).ToList();
                if (orgs != null)
                {
                    foreach (AssoOrg_Event assoOrg in orgs)
                    {
                        //if the association already exists
                        if (currentOrgId.Contains(assoOrg.OrgId))
                        {
                            OrgEvent toEdit = db.OrgEvents.Where(x => x.OrgId == assoOrg.OrgId && x.EventId == eventId).First();
                            toEdit.Blurb = assoOrg.Blurb;
                            db.Entry(toEdit).State = EntityState.Modified;
                            db.SaveChanges();
                            currentOrgId.Remove(assoOrg.OrgId);
                        }
                        else
                        {
                            OrgEvent toAdd = new OrgEvent
                            {
                                EventId = eventId,
                                OrgId = assoOrg.OrgId,
                                Blurb = assoOrg.Blurb
                            };
                            db.OrgEvents.Add(toAdd);
                        }
                    }

                }
                if (currentOrgId.Count != 0)
                {
                    foreach (short id in currentOrgId)
                    {
                        OrgEvent gone = db.OrgEvents.Where(x => x.EventId == eventId && x.OrgId == id).FirstOrDefault();
                        db.OrgEvents.Remove(gone);
                    }
                }
                db.SaveChanges();
                #endregion
                return Json(true);
            }

            //if model fails
            ViewBag.Locales = db.Locales.OrderBy(l => l.Info.Name).ToList();
            ViewBag.Orgs = db.Orgs.OrderBy(l => l.Info.Name).ToList();
            var taevent = db.Events.Find(eventId);
            //if I actually was handling if the model failed (as currently I'm using the bandaid solution of just preventing the submition ) this would not cut it. It does not account for orgs or locales being null
            AssoEventVM model = new AssoEventVM { InfoId = infoId, EventId = eventId, Submit = submit, Name = taevent.Name, AssoOrgs = orgs, AssoLocales = locales };
            return View(model);
        }

        // GET: Events/Delete/5
        public ActionResult Delete(short? id)
        {
            if (id == null)
            {
                return new HttpStatusCodeResult(HttpStatusCode.BadRequest);
            }
            Event taevent = db.Events.Find(id);
            if (taevent == null)
            {
                return HttpNotFound();
            }
            return View(taevent);
        }

        // POST: Events/Delete/5
        [HttpPost, ActionName("Delete")]
        [ValidateAntiForgeryToken]
        public ActionResult DeleteConfirmed(short id)
        {
            Event taevent = db.Events.Find(id);
            short infoId = taevent.InfoId;

            #region Remove Associations

            #region Remove LocaleEvents
            List<LocaleEvent> locales = db.LocaleEvents.Where(x => x.EventId == id).ToList();
            foreach (LocaleEvent gone in locales)
            {
                db.LocaleEvents.Remove(gone);
            }
            #endregion

            #region Remove OrgEvents
            List<OrgEvent> orgs = db.OrgEvents.Where(x => x.EventId == id).ToList();
            foreach (OrgEvent gone in orgs)
            {
                db.OrgEvents.Remove(gone);
            }
            #endregion
            #endregion

            db.Events.Remove(taevent);

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
