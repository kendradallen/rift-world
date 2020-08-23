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
    public class OrgsController : Controller
    {
        private RiftWorldEntities db = new RiftWorldEntities();

        // GET: Orgs
        public ActionResult Index()
        {
            var orgs = db.Orgs.Include(o => o.Info).Include(o => o.Locale);
            return View(orgs.ToList());
        }

        // GET: Orgs/Details/5
        public ActionResult Details(short? id)
        {
            if (id == null)
            {
                return new HttpStatusCodeResult(HttpStatusCode.BadRequest);
            }
            Org org = db.Orgs.Find(id);
            if (org == null)
            {
                return HttpNotFound();
            }
            return View(org);
        }

        // GET: Orgs/Create
        public ActionResult Create()
        {
            ViewBag.BaseLocationId = new SelectList(db.Locales, "LocaleId", "Name");
            ViewBag.Tags = new MultiSelectList(db.Tags, "TagId", "TagName");

            ViewBag.startName = 50;
            ViewBag.startBlurb = 100;
            ViewBag.startArtist = 40;
            return View();
        }

        // POST: Orgs/Create
        // To protect from overposting attacks, please enable the specific properties you want to bind to, for 
        // more details see https://go.microsoft.com/fwlink/?LinkId=317598.
        [HttpPost]
        [ValidateAntiForgeryToken]
        public ActionResult Create([Bind(Include = "Name,IsPlayerEnabled,SymbolFileName,BaseLocationId,AboutText,Blurb,Artist")] OrgCreateVM org,
            List<short> tags,
            HttpPostedFileBase picture,
            string submit)
        {
            #region Pre-model picture check
            if (picture != null)
            {
                string[] goodExts = { ".jpg", ".jpeg", ".gif", ".png" };
                string imgName = picture.FileName;

                var length = picture.ContentLength;
                string ext = imgName.Substring(imgName.LastIndexOf('.'));

                if (!goodExts.Contains(ext.ToLower()))
                {
                    ModelState.AddModelError("SymbolFileName", "You have submitted a incorrect file type for your portrait. Please use either: .jpg, .jpeg, .gif, or .png");
                }

                if (org.Artist == null)
                {
                    ModelState.AddModelError("Artist", "Katherine, you're trying to submit something with a picture without an artist. That's a no-no! But seriously, if something came up that means you need to change this rule, you know who to call.");
                }
            }
            #endregion

            org.IsPublished = false;
            if (ModelState.IsValid)
            {
                #region Info
                Info info;
                info = new Info
                {
                    InfoTypeId = 3,  //<-------------------Info Type ID. CHANGE UPON COPY
                    IdWithinType = null,
                    Blurb = org.Blurb,
                    Name = org.Name,
                    IsPublished = org.IsPublished
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

                #region Image uploads
                string[] goodExts = { ".jpg", ".jpeg", ".gif", ".png" };
                org.SymbolFileName = "default.jpg";
                if (picture != null)
                {
                    string imgName = picture.FileName;

                    string ext = imgName.Substring(imgName.LastIndexOf('.'));

                    if (goodExts.Contains(ext.ToLower()))
                    {
                        imgName = "symbol-" + infoId.ToString() + ext;

                        picture.SaveAs(Server.MapPath("~/Content/img/org/" + imgName));
                    }
                    org.SymbolFileName = imgName;
                }
                #endregion

                #region Org
                Org daOrg = new Org
                {
                    InfoId = infoId,
                    Name = org.Name,
                    IsPlayerEnabled = org.IsPlayerEnabled,
                    SymbolFileName = org.SymbolFileName,
                    BaseLocationId = org.BaseLocationId,
                    AboutText = org.AboutText,
                    IsPublished = org.IsPublished,
                    Artist = org.Artist
                };
                db.Orgs.Add(daOrg);
                db.SaveChanges();
                #endregion

                #region give info the IdWithinType
                short maxi = db.Orgs.Max(i => i.OrgId);
                info.IdWithinType = maxi;
                db.Entry(info).State = EntityState.Modified;
                db.SaveChanges();
                #endregion

                return RedirectToAction("AssoCreate", new { id = maxi, submit = submit });
            }
            //if model not valid
            ViewBag.BaseLocationId = new SelectList(db.Locales, "LocaleId", "Name", org.BaseLocationId);
            ViewBag.Tags = new MultiSelectList(db.Tags, "TagId", "TagName", tags);
            ViewBag.startName = 50;
            ViewBag.startBlurb = 100;
            ViewBag.startArtist = 40;
            if (org.Name != null)
            {
                ViewBag.startName = 50 - org.Name.Length;
            }
            if (org.Blurb != null)
            {
                ViewBag.startBlurb = 100 - org.Blurb.Length;
            }
            if (org.Artist != null)
            {
                ViewBag.startArtist = 40 - org.Artist.Length;
            }

            if (picture != null)
            {
                ModelState.AddModelError("SymbolFileName", "Hey, there was some error, so you have to re-upload the picture");
            }
            ModelState.AddModelError("", "Something has gone wrong. Looks for red text to see where is went wrong");

            return View(org);
        }

        public ActionResult AssoCreate(short? id, string submit)
        {
            if (id == null)
            {
                return new HttpStatusCodeResult(HttpStatusCode.BadRequest);
            }
            Org org = db.Orgs.Find(id);
            if (org == null)
            {
                return HttpNotFound();
            }
            ViewBag.Npcs = db.NPCs.ToList();
            var selected = db.NpcOrgs.Where(x => x.OrgId == org.OrgId).ToList();
            List<AssoNpc_Org> assoNpcs = new List<AssoNpc_Org>();
            foreach (NpcOrg npcOrg in selected)
            {
                AssoNpc_Org toAdd = new AssoNpc_Org(npcOrg);
                assoNpcs.Add(toAdd);
            }

            ViewBag.Events = db.Events.ToList();
            var selected2 = db.OrgEvents.Where(i => i.OrgId == org.OrgId).ToList();
            List<AssoEvent_Org> assoEvents = new List<AssoEvent_Org>();
            foreach (OrgEvent item in selected2)
            {
                AssoEvent_Org toAdd = new AssoEvent_Org(item);
                assoEvents.Add(toAdd);
            }

            ViewBag.Characters = db.Characters.Where(i => i.IsApproved).ToList();
            var selected3 = db.CharOrgs.Where(i => i.OrgId == org.OrgId).ToList();
            List<AssoChar_Org> assoChars = new List<AssoChar_Org>();
            foreach (CharOrg item in selected3)
            {
                AssoChar_Org toAdd = new AssoChar_Org(item);
                assoChars.Add(toAdd);
            }

            var infoid = org.InfoId;
            AssoOrgVM model = new AssoOrgVM { InfoId = infoid, OrgId = org.OrgId, Submit = submit, Name = org.Name, AssoNpcs = assoNpcs, AssoEvents = assoEvents, AssoChars = assoChars };
            return View(model);
        }

        [HttpPost]
        [ValidateAntiForgeryToken]
        public ActionResult AssoCreate(short infoId, short orgId, string submit,
            List<AssoNpc_Org> npcs,
            List<AssoEvent_Org> events,
            List<AssoChar_Org> charas)
        {
            var org = db.Orgs.Where(i => i.OrgId == orgId).FirstOrDefault();
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
                        org.IsPublished = false;
                        break;
                    case "Publish":
                    case "Save":
                        info.IsPublished = true;
                        org.IsPublished = true;
                        break;
                    default:
                        break;
                }
                db.Entry(org).State = EntityState.Modified;
                db.Entry(info).State = EntityState.Modified;
                #endregion

                #region Add Members
                List<short> currentNpcId = db.NpcOrgs.Where(x => x.OrgId == orgId).Select(x => x.NpcId).ToList();
                if (npcs != null)
                {
                    foreach (AssoNpc_Org member in npcs)
                    {
                        //if the association already exists
                        if (currentNpcId.Contains(member.NpcId))
                        {
                            NpcOrg toEdit = db.NpcOrgs.Where(x => x.NpcId == member.NpcId && x.OrgId == orgId).First();

                            //make the edits
                            toEdit.OrgOrder = member.OrgOrder;
                            toEdit.BlurbNpcPage = member.BlurbNpcPage;
                            toEdit.BlurbOrgPage = member.BlurbOrgPage;
                            toEdit.MemberOrder = member.MemberOrder;
                            toEdit.IsCurrent = member.IsCurrent;

                            db.Entry(toEdit).State = EntityState.Modified;
                            db.SaveChanges();
                            currentNpcId.Remove(member.NpcId);
                        }
                        else
                        {
                            NpcOrg toAdd = new NpcOrg
                            {
                                OrgId = orgId,
                                NpcId = member.NpcId,
                                OrgOrder = member.OrgOrder,
                                BlurbNpcPage = member.BlurbNpcPage,
                                BlurbOrgPage = member.BlurbOrgPage,
                                MemberOrder = member.MemberOrder,
                                IsCurrent = member.IsCurrent
                            };
                            db.NpcOrgs.Add(toAdd);
                            db.SaveChanges();
                        }
                    }
                }
                if (currentNpcId.Count != 0)
                {
                    foreach (short id in currentNpcId)
                    {
                        NpcOrg gone = db.NpcOrgs.Where(x => x.OrgId == orgId && x.NpcId == id).FirstOrDefault();
                        db.NpcOrgs.Remove(gone);
                        db.SaveChanges();
                    }
                }
                #endregion

                #region Add Events
                List<short> currentEventId = db.OrgEvents.Where(x => x.OrgId == orgId).Select(x => x.EventId).ToList();
                if (events != null)
                {
                    foreach (AssoEvent_Org assoEvent in events)
                    {
                        //if the association already exists
                        if (currentEventId.Contains(assoEvent.EventId))
                        {
                            OrgEvent toEdit = db.OrgEvents.Where(x => x.EventId == assoEvent.EventId && x.OrgId == orgId).First();
                            toEdit.Blurb = assoEvent.Blurb;
                            db.Entry(toEdit).State = EntityState.Modified;
                            db.SaveChanges();
                            currentEventId.Remove(assoEvent.EventId);
                        }
                        else
                        {
                            OrgEvent toAdd = new OrgEvent
                            {
                                OrgId = orgId,
                                EventId = assoEvent.EventId,
                                Blurb = assoEvent.Blurb
                            };
                            db.OrgEvents.Add(toAdd);
                        }
                    }
                }
                if (currentEventId.Count != 0)
                {
                    foreach (short id in currentEventId)
                    {
                        OrgEvent gone = db.OrgEvents.Where(x => x.OrgId == orgId && x.EventId == id).FirstOrDefault();
                        db.OrgEvents.Remove(gone);
                    }
                }
                db.SaveChanges();
                #endregion

                #region Add CharacterOrgs
                List<short> currentCharId = db.CharOrgs.Where(x => x.OrgId == orgId).Select(x => x.CharId).ToList();
                if (charas != null)
                {
                    foreach (AssoChar_Org assoChar in charas)
                    {
                        if (currentCharId.Contains(assoChar.CharId))
                        {
                            CharOrg toEdit = db.CharOrgs.Where(x => x.CharId == assoChar.CharId && x.OrgId == orgId).First();
                            toEdit.BlurbOrgPage = assoChar.BlurbOrgPage;
                            toEdit.IsCurrent = assoChar.IsCurrent;
                            toEdit.IsPublic = assoChar.IsPublic;
                            toEdit.KatherineApproved = assoChar.KatherineApproved;
                            db.Entry(toEdit).State = EntityState.Modified;
                            db.SaveChanges();
                            currentCharId.Remove(assoChar.CharId);
                        }
                        else
                        {
                            CharOrg toAdd = new CharOrg
                            {
                                OrgId = orgId,
                                CharId = assoChar.CharId,
                                BlurbOrgPage = assoChar.BlurbOrgPage,
                                IsPublic = assoChar.IsPublic,
                                IsCurrent = assoChar.IsCurrent,
                                KatherineApproved = assoChar.KatherineApproved
                            };
                            db.CharOrgs.Add(toAdd);
                        }
                    }
                }
                if (currentCharId.Count != 0)
                {
                    foreach (short id in currentCharId)
                    {
                        CharOrg gone = db.CharOrgs.Where(x => x.OrgId == orgId && x.CharId == id).FirstOrDefault();
                        db.CharOrgs.Remove(gone);
                    }
                }
                db.SaveChanges();
                #endregion
                return Json(true);
            }
            //if model fails
            ViewBag.Npcs = db.NPCs.ToList();
            ViewBag.Events = db.Events.ToList();
            ViewBag.Characters = db.Characters.Where(i => i.IsApproved).ToList();
            AssoOrgVM model = new AssoOrgVM { InfoId = infoId, OrgId = orgId, Submit = submit, Name = org.Name, AssoNpcs = npcs, AssoEvents = events, AssoChars = charas };
            return View(model);
        }

        // GET: Orgs/Edit/5
        public ActionResult Edit(short? id)
        {
            if (id == null)
            {
                return new HttpStatusCodeResult(HttpStatusCode.BadRequest);
            }
            Org org = db.Orgs.Find(id);
            if (org == null)
            {
                return HttpNotFound();
            }
            ViewBag.BaseLocationId = new SelectList(db.Locales, "LocaleId", "Name", org.BaseLocationId);

            short infoid = org.InfoId;
            string blurb = db.Infos.Where(i => i.InfoId == infoid).Select(i => i.Blurb).First();
            OrgEditVM model = new OrgEditVM(org, blurb);

            List<short> selectedTags = db.InfoTags.Where(t => t.InfoId == infoid).Select(t => t.TagId).ToList();
            ViewBag.Selected = selectedTags;
            ViewBag.Tags = db.Tags.ToList();

            return View(model);
        }

        // POST: Orgs/Edit/5
        // To protect from overposting attacks, please enable the specific properties you want to bind to, for 
        // more details see https://go.microsoft.com/fwlink/?LinkId=317598.
        [HttpPost]
        [ValidateAntiForgeryToken]
        public ActionResult Edit([Bind(Include = "OrgId,InfoId,Name,IsPlayerEnabled,SymbolFileName,BaseLocationId,AboutText,IsPublished, Blurb, Artist")] OrgEditPostVM org,
            List<short> tags,
            HttpPostedFileBase picture,
            string submit
            )
        {
            #region Save or Publish?
            switch (submit)
            {
                case "Save Progress":
                case "Un-Publish":
                    org.IsPublished = false;
                    break;
                case "Publish":
                case "Save":
                    org.IsPublished = true;
                    break;
                case "Save and go to complex edit":
                    break;
            }
            #endregion

            #region Pre-model picture check
            if (picture != null)
            {
                string[] goodExts = { ".jpg", ".jpeg", ".gif", ".png" };
                string imgName = picture.FileName;

                var length = picture.ContentLength;
                string ext = imgName.Substring(imgName.LastIndexOf('.'));

                if (!goodExts.Contains(ext.ToLower()))
                {
                    ModelState.AddModelError("SymbolFileName", "You have submitted a incorrect file type for your portrait. Please use either: .jpg, .jpeg, .gif, or .png");
                }

                if (org.Artist == null)
                {
                    ModelState.AddModelError("Artist", "Katherine, you're trying to submit something with a picture without an artist. That's a no-no! But seriously, if something came up that means you need to change this rule, you know who to call.");
                }
            }
            else if (org.SymbolFileName != null && org.Artist == null)
            {
                ModelState.AddModelError("Artist", "Yo bud, you tired? Seems you deleted the artist by accident. Why don't ya fix that?");

            }

            #endregion

            if (ModelState.IsValid)
            {
                var infoid = org.InfoId;
                #region Info Update
                //Info info = db.Infos.Find(infoid);
                Info info = db.Infos.Where(i => i.InfoId == infoid).FirstOrDefault();
                info.Name = org.Name;
                info.Blurb = org.Blurb;
                info.IsPublished = org.IsPublished;
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

                #region Image update/upload
                string[] goodExts = { ".jpg", ".jpeg", ".gif", ".png" };

                #region Image Upload - picture
                if (picture != null)
                {
                    string imgName = picture.FileName;

                    string ext = imgName.Substring(imgName.LastIndexOf('.'));

                    if (goodExts.Contains(ext.ToLower()))
                    {
                        imgName = "symbol-" + infoid + ext;

                        picture.SaveAs(Server.MapPath("~/Content/img/org/" + imgName));
                    }
                    org.SymbolFileName = imgName;
                }
                #endregion
                #endregion

                #region Update Org
                Org daOrg = new Org
                {
                    OrgId = org.OrgId,
                    InfoId = org.InfoId,
                    Name = org.Name,
                    IsPlayerEnabled = org.IsPlayerEnabled,
                    SymbolFileName = org.SymbolFileName,
                    BaseLocationId = org.BaseLocationId,
                    AboutText = org.AboutText,
                    IsPublished = org.IsPublished,
                    Artist = org.Artist
                };
                db.Entry(daOrg).State = EntityState.Modified;
                db.Entry(info).State = EntityState.Modified;
                db.SaveChanges();
                #endregion
                if (submit == "Save and go to complex edit")
                {
                    return RedirectToAction("AssoEdit", new { id = org.OrgId });
                }
                return RedirectToAction("Details", new { id = org.OrgId });
            }

            //if model invalid
            ViewBag.BaseLocationId = new SelectList(db.Locales, "LocaleId", "Name", org.BaseLocationId);
            ViewBag.Tags = db.Tags.ToList();
            if (tags != null)
            {
                ViewBag.Selected = tags;
            }
            else
            {
                ViewBag.Selected = new List<short>();
            }

            if (picture != null)
            {
                ModelState.AddModelError("SymbolFileName", "Hey, there was some error, so you have to re-upload the picture");
            }

            OrgEditVM aOrg = new OrgEditVM(org);
            return View(aOrg);
        }

        public ActionResult AssoEdit(short? id)
        {
            if (id == null)
            {
                return new HttpStatusCodeResult(HttpStatusCode.BadRequest);
            }
            Org org = db.Orgs.Find(id);
            if (org == null)
            {
                return HttpNotFound();
            }
            ViewBag.Npcs = db.NPCs.ToList();
            var selected = db.NpcOrgs.Where(x => x.OrgId == org.OrgId).ToList();
            List<AssoNpc_Org> assoNpcs = new List<AssoNpc_Org>();
            foreach (NpcOrg npcOrg in selected)
            {
                AssoNpc_Org toAdd = new AssoNpc_Org(npcOrg);
                assoNpcs.Add(toAdd);
            }

            ViewBag.Events = db.Events.ToList();
            var selected2 = db.OrgEvents.Where(i => i.OrgId == org.OrgId).ToList();
            List<AssoEvent_Org> assoEvents = new List<AssoEvent_Org>();
            foreach (OrgEvent item in selected2)
            {
                AssoEvent_Org toAdd = new AssoEvent_Org(item);
                assoEvents.Add(toAdd);
            }

            ViewBag.Characters = db.Characters.Where(i => i.IsApproved).ToList();
            var selected3 = db.CharOrgs.Where(i => i.OrgId == org.OrgId).ToList();
            List<AssoChar_Org> assoChars = new List<AssoChar_Org>();
            foreach (CharOrg item in selected3)
            {
                AssoChar_Org toAdd = new AssoChar_Org(item);
                assoChars.Add(toAdd);
            }


            var infoid = org.InfoId;
            AssoOrgVM model = new AssoOrgVM { InfoId = infoid, OrgId = org.OrgId, Submit = "Save", Name = org.Name, AssoNpcs = assoNpcs, AssoEvents = assoEvents, AssoChars = assoChars };
            return View(model);
        }

        [HttpPost]
        [ValidateAntiForgeryToken]
        public ActionResult AssoEdit(short infoId, short orgId, string submit,
            List<AssoNpc_Org> npcs,
            List<AssoEvent_Org> events,
            List<AssoChar_Org> charas
            )
        {
            if (ModelState.IsValid)
            {
                #region Add Members
                List<short> currentNpcId = db.NpcOrgs.Where(x => x.OrgId == orgId).Select(x => x.NpcId).ToList();
                if (npcs != null)
                {
                    foreach (AssoNpc_Org member in npcs)
                    {
                        //if the association already exists
                        if (currentNpcId.Contains(member.NpcId))
                        {
                            NpcOrg toEdit = db.NpcOrgs.Where(x => x.NpcId == member.NpcId && x.OrgId == orgId).First();

                            //make the edits
                            toEdit.OrgOrder = member.OrgOrder;
                            toEdit.BlurbNpcPage = member.BlurbNpcPage;
                            toEdit.BlurbOrgPage = member.BlurbOrgPage;
                            toEdit.MemberOrder = member.MemberOrder;
                            toEdit.IsCurrent = member.IsCurrent;

                            db.Entry(toEdit).State = EntityState.Modified;
                            db.SaveChanges();
                            currentNpcId.Remove(member.NpcId);
                        }
                        else
                        {
                            NpcOrg toAdd = new NpcOrg
                            {
                                OrgId = orgId,
                                NpcId = member.NpcId,
                                OrgOrder = member.OrgOrder,
                                BlurbNpcPage = member.BlurbNpcPage,
                                BlurbOrgPage = member.BlurbOrgPage,
                                MemberOrder = member.MemberOrder,
                                IsCurrent = member.IsCurrent
                            };
                            db.NpcOrgs.Add(toAdd);
                        }
                    }
                }
                if (currentNpcId.Count != 0)
                {
                    foreach (short id in currentNpcId)
                    {
                        NpcOrg gone = db.NpcOrgs.Where(x => x.OrgId == orgId && x.NpcId == id).FirstOrDefault();
                        db.NpcOrgs.Remove(gone);
                    }
                }
                db.SaveChanges();
                #endregion

                #region Add Events
                List<short> currentEventId = db.OrgEvents.Where(x => x.OrgId == orgId).Select(x => x.EventId).ToList();
                if (events != null)
                {
                    foreach (AssoEvent_Org assoEvent in events)
                    {
                        //if the association already exists
                        if (currentEventId.Contains(assoEvent.EventId))
                        {
                            OrgEvent toEdit = db.OrgEvents.Where(x => x.EventId == assoEvent.EventId && x.OrgId == orgId).First();
                            toEdit.Blurb = assoEvent.Blurb;
                            db.Entry(toEdit).State = EntityState.Modified;
                            db.SaveChanges();
                            currentEventId.Remove(assoEvent.EventId);
                        }
                        else
                        {
                            OrgEvent toAdd = new OrgEvent
                            {
                                OrgId = orgId,
                                EventId = assoEvent.EventId,
                                Blurb = assoEvent.Blurb
                            };
                            db.OrgEvents.Add(toAdd);
                        }
                    }
                }
                if (currentEventId.Count != 0)
                {
                    foreach (short id in currentEventId)
                    {
                        OrgEvent gone = db.OrgEvents.Where(x => x.OrgId == orgId && x.EventId == id).FirstOrDefault();
                        db.OrgEvents.Remove(gone);
                    }
                }
                db.SaveChanges();
                #endregion

                #region Add CharacterOrgs
                List<short> currentCharId = db.CharOrgs.Where(x => x.OrgId == orgId).Select(x => x.CharId).ToList();
                if (charas != null)
                {
                    foreach (AssoChar_Org assoChar in charas)
                    {
                        if (currentCharId.Contains(assoChar.CharId))
                        {
                            CharOrg toEdit = db.CharOrgs.Where(x => x.CharId == assoChar.CharId && x.OrgId == orgId).First();
                            toEdit.BlurbOrgPage = assoChar.BlurbOrgPage;
                            toEdit.IsCurrent = assoChar.IsCurrent;
                            toEdit.IsPublic = assoChar.IsPublic;
                            toEdit.KatherineApproved = assoChar.KatherineApproved;
                            db.Entry(toEdit).State = EntityState.Modified;
                            db.SaveChanges();
                            currentCharId.Remove(assoChar.CharId);
                        }
                        else
                        {
                            CharOrg toAdd = new CharOrg
                            {
                                OrgId = orgId,
                                CharId = assoChar.CharId,
                                BlurbOrgPage = assoChar.BlurbOrgPage,
                                IsPublic = assoChar.IsPublic,
                                IsCurrent = assoChar.IsCurrent,
                                KatherineApproved = assoChar.KatherineApproved
                            };
                            db.CharOrgs.Add(toAdd);
                        }
                    }
                }
                if (currentCharId.Count != 0)
                {
                    foreach (short id in currentCharId)
                    {
                        CharOrg gone = db.CharOrgs.Where(x => x.OrgId == orgId && x.CharId == id).FirstOrDefault();
                        db.CharOrgs.Remove(gone);
                    }
                }
                db.SaveChanges();
                #endregion


                return Json(true);
            }
            //if model fails
            ViewBag.Npcs = db.NPCs.ToList();
            ViewBag.Events = db.Events.ToList();
            ViewBag.Characters = db.Characters.Where(i => i.IsApproved).ToList();
            var org = db.Orgs.Find(orgId);
            AssoOrgVM model = new AssoOrgVM { InfoId = infoId, OrgId = orgId, Submit = submit, Name = org.Name, AssoNpcs = npcs, AssoEvents = events, AssoChars = charas };
            return View(model);
        }


        // GET: Orgs/Delete/5
        public ActionResult Delete(short? id)
        {
            if (id == null)
            {
                return new HttpStatusCodeResult(HttpStatusCode.BadRequest);
            }
            Org org = db.Orgs.Find(id);
            if (org == null)
            {
                return HttpNotFound();
            }
            return View(org);
        }

        // POST: Orgs/Delete/5
        [HttpPost, ActionName("Delete")]
        [ValidateAntiForgeryToken]
        public ActionResult DeleteConfirmed(short id)
        {
            Org org = db.Orgs.Find(id);
            db.Orgs.Remove(org);
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
