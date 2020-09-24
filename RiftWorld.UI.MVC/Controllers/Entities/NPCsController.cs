using System;
using System.Collections.Generic;
using System.Data;
using System.Data.Entity;
using System.IO;
using System.Linq;
using System.Net;
using System.Web;
using System.Web.Mvc;
using Microsoft.AspNet.Identity;
using RiftWorld.DATA.EF;
using RiftWorld.UI.MVC.Models;


namespace RiftWorld.UI.MVC.Controllers.Entities
{
    [Authorize(Roles = "Admin")]
    public class NPCsController : Controller
    {
        private RiftWorldEntities db = new RiftWorldEntities();

        // GET: NPCs
        [OverrideAuthorization]
        public ActionResult Index()
        {
            //admin sees all (client request)
            List<NPC> npcs = new List<NPC> { };
            if (User.IsInRole("Admin"))
            {
                npcs = db.NPCs.Include(n => n.Info).Include(n => n.Locale).Include(n => n.Race).OrderBy(n=>n.Info.Name).ToList();
            }
            //everyone else doesn't see unpublished work
            else
            {
                npcs = db.NPCs.Include(n => n.Info).Include(n => n.Locale).Include(n => n.Race).Where(n => n.Info.IsPublished).OrderBy(n=>n.Info.Name).ToList();
            }
            return View(npcs);
        }

        // GET: NPCs/Details/5
        [OverrideAuthorization]
        public ActionResult Details(short? id, short? story)
        {
            if (id == null)
            {
                return new HttpStatusCodeResult(HttpStatusCode.BadRequest);
            }
            NPC nPC = db.NPCs.Find(id);
            if (nPC == null)
            {
                return HttpNotFound();
            }

            //prevent users from seeing un-published work
            if (!nPC.Info.IsPublished && !User.IsInRole("Admin"))
            {
                ViewBag.Message = "Whatever you think exists, doesn't yet.";
                return View("TheForbiddenZone");
            }


            //add the npc's classes
            List<Class> classes = (from cn in db.ClassNPCs
                                   where cn.NpcId == id
                                   orderby cn.ClassOrder.HasValue descending, cn.ClassOrder, cn.Class.ClassName
                                   select cn.Class)
                           .ToList()
                           ;

            int classLength = classes.Count();
            string holder = "";
            switch (classLength)
            {
                case 0:
                    holder = "Unknown";
                    break;
                case 1:
                    holder = classes[0].ClassName;
                    break;
                default:
                    for (int i = 0; i < classLength; i++)
                    {
                        holder += classes[i].ClassName;
                        if (i != classLength -1)
                        {
                            holder += "/ ";
                        }
                    }
                    break;
            }
            ViewBag.Classes = holder;
            ViewBag.OpenStory = story;
            return View(nPC);
        }

        [HttpGet]
        [OverrideAuthorization]
        public PartialViewResult _OrgsPartial(short id)
        {
            List<_NpcOrgsVM> orgs = (from no in db.NpcOrgs
                                    join o in db.Orgs on no.OrgId equals o.OrgId
                                    join i in db.Infos on o.InfoId equals i.InfoId
                                    where no.NpcId == id && no.IsCurrent == true && i.IsPublished == true
                                    orderby no.OrgOrder.HasValue descending, no.OrgOrder, i.Name
                                    select new _NpcOrgsVM
                                    {
                                        Name = i.Name,
                                        Blurb = no.BlurbNpcPage,
                                        Id = o.OrgId
                                    })
                       .ToList()
                        ;

            return PartialView(orgs);
        }

        [OverrideAuthorization]
        public PartialViewResult _ClassNpc(short id)
        {
            return PartialView();
        }

        // GET: NPCs/Create
        public ActionResult Create()
        {
            ViewBag.LastLocationId = new SelectList(db.Locales, "LocaleId", "Name");
            ViewBag.RaceId = new SelectList(db.Races, "RaceId", "RaceName");
            ViewBag.GenderId = new SelectList(db.Genders, "GenderId", "GenderName");
            ViewBag.Tags = new MultiSelectList(db.Tags, "TagId", "TagName");
            return View();
        }

        [HttpPost]
        [ValidateAntiForgeryToken]
        public ActionResult Create([Bind(Include = "Name,Alias,Quote,PortraitFileName,RaceId,CrestFileName,ApperanceText,AboutText,LastLocationId,PortraitArtist,CrestArtist,IsDead,GenderId, Blurb, IsSecret")] NpcCreateVM npc,
            List<short> tags,
            HttpPostedFileBase portraitPic,
            HttpPostedFileBase crestPic,
            string submit)
        {
            #region Pre-model picture check
            if (portraitPic != null)
            {
                string[] goodExts = { ".jpg", ".jpeg", ".gif", ".png" };
                string imgName = portraitPic.FileName;

                string ext = imgName.Substring(imgName.LastIndexOf('.'));

                if (!goodExts.Contains(ext.ToLower()))
                {
                    ModelState.AddModelError("PortraitFileName", "You have submitted a incorrect file type for your portrait. Please use either: .jpg, .jpeg, .gif, or .png");
                }

                if (npc.PortraitArtist == null)
                {
                    ModelState.AddModelError("PortraitArtist", "Katherine, you're trying to submit something with a picture without an artist. That's a no-no! But seriously, if something came up that means you need to change this rule, you know who to call.");
                }
            }

            if (crestPic != null)
            {
                string[] goodExts = { ".jpg", ".jpeg", ".gif", ".png" };
                string imgName = crestPic.FileName;

                string ext = imgName.Substring(imgName.LastIndexOf('.'));

                if (!goodExts.Contains(ext.ToLower()))
                {
                    ModelState.AddModelError("CrestFileName", "You have submitted a incorrect file type for your portrait. Please use either: .jpg, .jpeg, .gif, or .png");
                }

                if (npc.CrestArtist == null)
                {
                    ModelState.AddModelError("CrestArtist", "Katherine, you're trying to submit something with a picture without an artist. That's a no-no! But seriously, if something came up that means you need to change this rule, you know who to call.");
                }
            }
            #endregion

            npc.IsPublished = false;
            if (ModelState.IsValid)
            {
                #region Info
                Info info;
                info = new Info
                {
                    InfoTypeId = 2,  //<-------------------Info Type ID. CHANGE UPON COPY
                    IdWithinType = null,
                    Blurb = npc.Blurb,
                    Name = npc.Name,
                    IsPublished = npc.IsPublished,
                    IsSecret = npc.IsSecret
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

                #region Image Uploads
                string[] goodExts = { ".jpg", ".jpeg", ".gif", ".png" };

                #region Image Upload -Portrait    
                npc.PortraitFileName = "default.jpg";
                if (portraitPic != null)
                {
                    string imgName = portraitPic.FileName;

                    string ext = imgName.Substring(imgName.LastIndexOf('.'));

                    if (goodExts.Contains(ext.ToLower()))
                    {
                        imgName = "npc-portrait-" + infoId.ToString() + ext;
                        portraitPic.SaveAs(Server.MapPath("~/Content/img/npc/" + imgName));
                    }
                    npc.PortraitFileName = imgName;
                }
                #endregion

                #region Image Upload -Crest
                npc.CrestFileName = "org_default.jpg";
                if (crestPic != null)
                {
                    string imgName = crestPic.FileName;

                    string ext = imgName.Substring(imgName.LastIndexOf('.'));

                    if (goodExts.Contains(ext.ToLower()))
                    {
                        imgName = "npc-crest-" + infoId.ToString() + ext;

                        crestPic.SaveAs(Server.MapPath("~/Content/img/npc/" + imgName));
                    }
                    npc.CrestFileName = imgName;
                }
                #endregion

                #endregion

                #region NPC
                NPC daNpc = new NPC
                {
                    InfoId = infoId,
                    Alias = npc.Alias,
                    Quote = npc.Quote,
                    PortraitFileName = npc.PortraitFileName,
                    RaceId = npc.RaceId,
                    CrestFileName = npc.CrestFileName,
                    ApperanceText = npc.ApperanceText,
                    AboutText = npc.AboutText,
                    LastLocationId = npc.LastLocationId,
                    PortraitArtist = npc.PortraitArtist,
                    CrestArtist = npc.CrestArtist,
                    IsDead = npc.IsDead,
                    GenderId = npc.GenderId
                };
                db.NPCs.Add(daNpc);
                db.SaveChanges();
                #endregion

                #region give info the IdWithinType
                short maxi = db.NPCs.Max(i => i.NpcId);
                info.IdWithinType = maxi;
                db.Entry(info).State = EntityState.Modified;
                db.SaveChanges();
                #endregion

                return RedirectToAction("AssoCreate", new { id = maxi, submit = submit });
            }

            #region model invalid
            ViewBag.LastLocationId = new SelectList(db.Locales, "LocaleId", "Name", npc.LastLocationId);
            ViewBag.RaceId = new SelectList(db.Races, "RaceId", "RaceName", npc.RaceId);
            ViewBag.GenderId = new SelectList(db.Genders, "GenderId", "GenderName", npc.GenderId);
            ViewBag.Tags = new MultiSelectList(db.Tags, "TagId", "TagName", tags);

            if (portraitPic != null)
            {
                ModelState.AddModelError("PortraitFileName", "Hey, there was some error, so you have to re-upload the picture");
            }
            if (crestPic != null)
            {
                ModelState.AddModelError("CrestFileName", "Hey, there was some error, so you have to re-upload the picture");
            }
            ModelState.AddModelError("", "Something has gone wrong. Look for red text to see where is went wrong");

            #endregion
            return View(npc);
        }

        public ActionResult AssoCreate(short? id, string submit)
        {
            if (id == null)
            {
                return new HttpStatusCodeResult(HttpStatusCode.BadRequest);
            }
            NPC npc = db.NPCs.Find(id);
            if (npc == null)
            {
                return HttpNotFound();
            }
            ViewBag.Orgs = db.Orgs.OrderBy(o=>o.Info.Name).ToList();
            var selected = db.NpcOrgs.Where(x => x.NpcId == npc.NpcId).ToList();
            List<AssoOrg_Npc> assoOrgs = new List<AssoOrg_Npc>();
            foreach (NpcOrg npcOrg in selected)
            {
                AssoOrg_Npc toAdd = new AssoOrg_Npc(npcOrg);
                assoOrgs.Add(toAdd);
            }

            ViewBag.Classes = db.Classes.OrderBy(c=>c.ClassName).ToList();
            var selected2 = db.ClassNPCs.Where(x => x.NpcId == npc.NpcId).ToList();
            List<AssoClass_Npc> assoClasses = new List<AssoClass_Npc>();
            foreach (ClassNPC asso in selected2)
            {
                AssoClass_Npc toAdd = new AssoClass_Npc(asso);
                assoClasses.Add(toAdd);
            }

            var infoid = npc.InfoId;
            AssoNpcVM model = new AssoNpcVM { InfoId = infoid, NpcId = npc.NpcId, Submit = submit, Name = npc.Info.Name, AssoClasses = assoClasses, AssoOrgs = assoOrgs };
            return View(model);
        }

        [HttpPost]
        [ValidateAntiForgeryToken]
        public ActionResult AssoCreate(short infoId, short npcId, string submit,
            List<AssoClass_Npc> assoClasses,
            List<AssoOrg_Npc> orgs)
        {
            var npc = db.NPCs.Where(i => i.NpcId == npcId).FirstOrDefault();
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

                #region Add Classes
                List<byte> currentClassId = db.ClassNPCs.Where(x => x.NpcId == npcId).Select(x => (byte)x.ClassId).ToList();
                if (assoClasses != null)
                {
                    foreach (AssoClass_Npc cn in assoClasses)
                    {
                        //if the association already exists
                        if (currentClassId.Contains(cn.ClassId))
                        {
                            ClassNPC toEdit = db.ClassNPCs.Where(x => x.ClassId == cn.ClassId && x.NpcId == npcId).First();
                            toEdit.ClassOrder = cn.ClassOrder;
                            db.Entry(toEdit).State = EntityState.Modified;
                            db.SaveChanges();
                            currentClassId.Remove(cn.ClassId);
                        }
                        else
                        {
                            ClassNPC toAdd = new ClassNPC
                            {
                                NpcId = npcId,
                                ClassId = cn.ClassId,
                                ClassOrder = cn.ClassOrder
                            };
                            db.ClassNPCs.Add(toAdd);
                        }
                    }
                }
                if (currentClassId.Count != 0)
                {
                    foreach (byte id in currentClassId)
                    {
                        ClassNPC gone = db.ClassNPCs.Where(x => x.NpcId == npcId && x.ClassId == id).FirstOrDefault();
                        db.ClassNPCs.Remove(gone);
                    }
                }
                db.SaveChanges();
                #endregion

                #region Add Orgs
                List<short> currentOrgId = db.NpcOrgs.Where(x => x.NpcId == npcId).Select(x => x.OrgId).ToList();
                if (orgs != null)
                {
                    foreach (AssoOrg_Npc assoOrg in orgs)
                    {
                        //if the association already exists
                        if (currentOrgId.Contains(assoOrg.OrgId))
                        {
                            NpcOrg toEdit = db.NpcOrgs.Where(x => x.OrgId == assoOrg.OrgId && x.NpcId == npcId).First();
                            toEdit.OrgOrder = assoOrg.OrgOrder;
                            toEdit.BlurbNpcPage = assoOrg.BlurbNpcPage;
                            toEdit.BlurbOrgPage = assoOrg.BlurbOrgPage;
                            toEdit.MemberOrder = assoOrg.MemberOrder;
                            toEdit.IsCurrent = assoOrg.IsCurrent;

                            db.Entry(toEdit).State = EntityState.Modified;
                            db.SaveChanges();
                            currentOrgId.Remove(assoOrg.OrgId);
                        }
                        else
                        {
                            NpcOrg toAdd = new NpcOrg
                            {
                                NpcId = npcId,
                                OrgId = assoOrg.OrgId,
                                OrgOrder = assoOrg.OrgOrder,
                                BlurbNpcPage = assoOrg.BlurbNpcPage,
                                BlurbOrgPage = assoOrg.BlurbOrgPage,
                                MemberOrder = assoOrg.MemberOrder,
                                IsCurrent = assoOrg.IsCurrent
                            };
                            db.NpcOrgs.Add(toAdd);
                        }
                    }
                }
                if (currentOrgId.Count != 0)
                {
                    foreach (short id in currentOrgId)
                    {
                        NpcOrg gone = db.NpcOrgs.Where(x => x.NpcId == npcId & x.OrgId == id).FirstOrDefault();
                        db.NpcOrgs.Remove(gone);
                    }
                }
                db.SaveChanges();
                #endregion
                return Json(true);
            }
            //if model fails
            ViewBag.Orgs = db.Orgs.OrderBy(o => o.Info.Name).ToList();
            ViewBag.Classes = db.Classes.OrderBy(c => c.ClassName).ToList();
            AssoNpcVM model = new AssoNpcVM { InfoId = infoId, NpcId = npcId, Submit = submit, Name = npc.Name, AssoClasses = assoClasses, AssoOrgs = orgs };
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

        // GET: NPCs/Edit/5
        public ActionResult Edit(short? id)
        {
            if (id == null)
            {
                return new HttpStatusCodeResult(HttpStatusCode.BadRequest);
            }
            NPC npc = db.NPCs.Find(id);
            if (npc == null)
            {
                return HttpNotFound();
            }
            ViewBag.LastLocationId = new SelectList(db.Locales.OrderBy(l=>l.Info.Name), "LocaleId", "Name", npc.LastLocationId);
            ViewBag.RaceId = new SelectList(db.Races.OrderBy(r=> r.RaceName), "RaceId", "RaceName", npc.RaceId);
            ViewBag.GenderId = new SelectList(db.Genders, "GenderId", "GenderName", npc.GenderId);

            short infoid = npc.InfoId;
            Info info = db.Infos.Find(infoid);
            NpcEditVM model = new NpcEditVM(npc, info);

            List<short> selectedTags = db.InfoTags.Where(t => t.InfoId == infoid).Select(t => t.TagId).ToList();
            ViewBag.Selected = selectedTags;
            ViewBag.Tags = db.Tags.OrderBy(t=>t.TagName).ToList();

            return View(model);
        }

        // POST: NPCs/Edit/5
        // To protect from overposting attacks, please enable the specific properties you want to bind to, for 
        // more details see https://go.microsoft.com/fwlink/?LinkId=317598.
        [HttpPost]
        [ValidateAntiForgeryToken]
        public ActionResult Edit([Bind(Include = "NpcId,InfoId,Name,Alias,Quote,PortraitFileName,RaceId,CrestFileName,ApperanceText,AboutText,LastLocationId,PortraitArtist,CrestArtist,IsDead,GenderId, Blurb, IsSecret, IsPublished")] NpcEditPostVM npc,
            List<short> tags,
            HttpPostedFileBase portraitPic,
            HttpPostedFileBase crestPic,
            string submit,
            bool removePic1,
            bool removePic2)
        {

            #region Pre-model picture check
            if (portraitPic != null)
            {
                string[] goodExts = { ".jpg", ".jpeg", ".gif", ".png" };
                string imgName = portraitPic.FileName;

                string ext = imgName.Substring(imgName.LastIndexOf('.'));

                if (!goodExts.Contains(ext.ToLower()))
                {
                    ModelState.AddModelError("PortraitFileName", "You have submitted a incorrect file type for your portrait. Please use either: .jpg, .jpeg, .gif, or .png");
                }

                if (npc.PortraitArtist == null)
                {
                    ModelState.AddModelError("PortraitArtist", "Katherine, you're trying to submit something with a picture without an artist. That's a no-no! But seriously, if something came up that means you need to change this rule, you know who to call.");
                }
            }
            else if ((npc.PortraitFileName != "default.jpg" && npc.PortraitFileName != null) && npc.PortraitArtist == null && !removePic1)
            {
                ModelState.AddModelError("PortraitArtist", "Yo bud, you tired? Seems you deleted the artist by accident. Why don't ya fix that?");
            }

            if (crestPic != null)
            {
                string[] goodExts = { ".jpg", ".jpeg", ".gif", ".png" };
                string imgName = crestPic.FileName;

                string ext = imgName.Substring(imgName.LastIndexOf('.'));

                if (!goodExts.Contains(ext.ToLower()))
                {
                    ModelState.AddModelError("CrestFileName", "You have submitted a incorrect file type for your portrait. Please use either: .jpg, .jpeg, .gif, or .png");
                }

                if (npc.CrestArtist == null)
                {
                    ModelState.AddModelError("CrestArtist", "Katherine, you're trying to submit something with a picture without an artist. That's a no-no! But seriously, if something came up that means you need to change this rule, you know who to call.");
                }
            }
            else if ((npc.CrestFileName != "org_default.jpg" && npc.CrestFileName != null) && npc.CrestArtist == null && !removePic2)
            {
                ModelState.AddModelError("CrestArtist", "Yo bud, you tired? Seems you deleted the artist by accident. Why don't ya fix that?");
            }
            #endregion

            if (ModelState.IsValid)
            {
                #region Save or Publish?
                switch (submit)
                {
                    case "Save Progress":
                    case "Un-Publish":
                        npc.IsPublished = false;
                        break;
                    case "Publish":
                    case "Save":
                        npc.IsPublished = true;
                        break;
                    case "Save and go to complex edit":
                        break;
                }
                #endregion

                var infoid = npc.InfoId;
                #region Info Update
                //Info info = db.Infos.Find(infoid);
                Info info = db.Infos.Where(i => i.InfoId == infoid).FirstOrDefault();
                info.Name = npc.Name;
                info.Blurb = npc.Blurb;
                info.IsPublished = npc.IsPublished;
                info.IsSecret = npc.IsSecret;
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

                #region Image Update/Upload
                string[] goodExts = { ".jpg", ".jpeg", ".gif", ".png" };

                #region Image Update -Portrait    
                if (portraitPic != null)
                {
                    string imgName = portraitPic.FileName;

                    string ext = imgName.Substring(imgName.LastIndexOf('.'));

                    if (goodExts.Contains(ext.ToLower()))
                    {
                        imgName = "npc-portrait-" + infoid.ToString() + ext;
                        portraitPic.SaveAs(Server.MapPath("~/Content/img/npc/" + imgName));
                    }
                    //remove old picture if it had a different extension (and thus would not be overridden)
                    string oldName = npc.PortraitFileName;
                    if (!String.IsNullOrEmpty(oldName) && oldName != "default.jpg")
                    {
                        string oldExt = oldName.Substring(oldName.LastIndexOf('.'));
                        if (oldExt != ext)
                        {
                            string fullPath = Request.MapPath("~/Content/img/npc/" + oldName);
                            if (System.IO.File.Exists(fullPath))
                            {
                                System.IO.File.Delete(fullPath);
                            }
                        }
                    }
                    //assign new PortraitFileName
                    npc.PortraitFileName = imgName;
                }
                #endregion

                #region Image Update -Crest
                if (crestPic != null)
                {
                    string imgName = crestPic.FileName;

                    string ext = imgName.Substring(imgName.LastIndexOf('.'));

                    if (goodExts.Contains(ext.ToLower()))
                    {
                        imgName = "npc-crest-" + infoid.ToString() + ext;

                        crestPic.SaveAs(Server.MapPath("~/Content/img/npc/" + imgName));
                    }
                    //remove old picture if it had a different extension (and thus would not be overridden)
                    string oldName = npc.CrestFileName;
                    if (!String.IsNullOrEmpty(oldName) && oldName != "org_default.jpg")
                    {
                        string oldExt = oldName.Substring(oldName.LastIndexOf('.'));
                        if (oldExt != ext)
                        {
                            string fullPath = Request.MapPath("~/Content/img/npc/" + oldName);
                            if (System.IO.File.Exists(fullPath))
                            {
                                System.IO.File.Delete(fullPath);
                            }
                        }
                    }
                    //assign new CrestFileName
                    npc.CrestFileName = imgName;
                }
                #endregion

                #endregion

                #region Potential Removal of Pics
                #region Portrait
                if (removePic1 && npc.PortraitFileName != "default.jpg")
                {
                    string picName = npc.PortraitFileName;
                    string fullPath = Request.MapPath("~/Content/img/npc/" + picName);
                    if (System.IO.File.Exists(fullPath))
                    {
                        System.IO.File.Delete(fullPath);
                    }
                    npc.PortraitFileName = "default.jpg";
                    npc.PortraitArtist = null;
                }

                #endregion
                #region Crest
                if (removePic2 && npc.CrestFileName != "org_default.jpg")
                {
                    string picName = npc.CrestFileName;
                    string fullPath = Request.MapPath("~/Content/img/npc/" + picName);
                    if (System.IO.File.Exists(fullPath))
                    {
                        System.IO.File.Delete(fullPath);
                    }
                    npc.CrestFileName = "org_default.jpg";
                    npc.CrestArtist = null;
                }
                #endregion
                #endregion


                #region Update Npc
                NPC daNpc = new NPC
                {
                    NpcId = npc.NpcId,
                    InfoId = npc.InfoId,
                    Alias = npc.Alias,
                    Quote = npc.Quote,
                    PortraitFileName = npc.PortraitFileName,
                    RaceId = npc.RaceId,
                    CrestFileName = npc.CrestFileName,
                    ApperanceText = npc.ApperanceText,
                    AboutText = npc.AboutText,
                    LastLocationId = npc.LastLocationId,
                    PortraitArtist = npc.PortraitArtist,
                    CrestArtist = npc.CrestArtist,
                    IsDead = npc.IsDead,
                    GenderId = npc.GenderId
                };
                db.Entry(daNpc).State = EntityState.Modified;
                db.Entry(info).State = EntityState.Modified;
                db.SaveChanges();
                #endregion
                if (submit == "Save and go to complex edit")
                {
                    return RedirectToAction("AssoEdit", new { id = npc.NpcId });
                }
                return RedirectToAction("Details", new { id = npc.NpcId });
            }

            #region if model invalid
            ViewBag.LastLocationId = new SelectList(db.Locales.OrderBy(l=>l.Info.Name), "LocaleId", "Name", npc.LastLocationId);
            ViewBag.RaceId = new SelectList(db.Races.OrderBy(r=>r.RaceName), "RaceId", "RaceName", npc.RaceId);
            ViewBag.GenderId = new SelectList(db.Genders, "GenderId", "GenderName", npc.GenderId);
            ViewBag.Tags = db.Tags.OrderBy(t=>t.TagName).ToList();
            if (tags != null)
            {
                ViewBag.Selected = tags;
            }
            else
            {
                ViewBag.Selected = new List<short>();
            }

            if (portraitPic != null)
            {
                ModelState.AddModelError("PortraitFileName", "Hey, there was some error, so you have to re-upload the picture");
            }
            if (crestPic != null)
            {
                ModelState.AddModelError("CrestFileName", "Hey, there was some error, so you have to re-upload the picture");
            }
            ModelState.AddModelError("", "Something has gone wrong. Look for red text to see where is went wrong");
            #endregion

            NpcEditVM aNpc = new NpcEditVM(npc);
            return View(aNpc);
        }

        public ActionResult AssoEdit(short? id)
        {
            if (id == null)
            {
                return new HttpStatusCodeResult(HttpStatusCode.BadRequest);
            }
            NPC npc = db.NPCs.Find(id);
            if (npc == null)
            {
                return HttpNotFound();
            }
            ViewBag.Orgs = db.Orgs.OrderBy(o=>o.Info.Name).ToList();
            var selected = db.NpcOrgs.Where(x => x.NpcId == npc.NpcId).ToList();
            List<AssoOrg_Npc> assoOrgs = new List<AssoOrg_Npc>();
            foreach (NpcOrg npcOrg in selected)
            {
                AssoOrg_Npc toAdd = new AssoOrg_Npc(npcOrg);
                assoOrgs.Add(toAdd);
            }

            ViewBag.Classes = db.Classes.OrderBy(c=>c.ClassName).ToList();
            var selected2 = db.ClassNPCs.Where(x => x.NpcId == npc.NpcId).ToList();
            List<AssoClass_Npc> assoClasses = new List<AssoClass_Npc>();
            foreach (ClassNPC asso in selected2)
            {
                AssoClass_Npc toAdd = new AssoClass_Npc(asso);
                assoClasses.Add(toAdd);
            }

            var infoid = npc.InfoId;
            AssoNpcVM model = new AssoNpcVM { InfoId = infoid, NpcId = npc.NpcId, Submit = "Save", Name = npc.Info.Name, AssoClasses = assoClasses, AssoOrgs = assoOrgs };
            return View(model);
        }

        [HttpPost]
        [ValidateAntiForgeryToken]
        public ActionResult AssoEdit(short infoId, short npcId, string submit,
            List<AssoClass_Npc> assoClasses,
            List<AssoOrg_Npc> orgs)
        {
            if (ModelState.IsValid)
            {
                #region Add Classes
                List<byte> currentClassId = db.ClassNPCs.Where(x => x.NpcId == npcId).Select(x => (byte)x.ClassId).ToList();
                if (assoClasses != null)
                {
                    foreach (AssoClass_Npc cn in assoClasses)
                    {
                        //if the association already exists
                        if (currentClassId.Contains(cn.ClassId))
                        {
                            ClassNPC toEdit = db.ClassNPCs.Where(x => x.ClassId == cn.ClassId && x.NpcId == npcId).First();
                            toEdit.ClassOrder = cn.ClassOrder;
                            db.Entry(toEdit).State = EntityState.Modified;
                            db.SaveChanges();
                            currentClassId.Remove(cn.ClassId);
                        }
                        else
                        {
                            ClassNPC toAdd = new ClassNPC
                            {
                                NpcId = npcId,
                                ClassId = cn.ClassId,
                                ClassOrder = cn.ClassOrder
                            };
                            db.ClassNPCs.Add(toAdd);
                        }
                    }
                }
                if (currentClassId.Count != 0)
                {
                    foreach (byte id in currentClassId)
                    {
                        ClassNPC gone = db.ClassNPCs.Where(x => x.NpcId == npcId && x.ClassId == id).FirstOrDefault();
                        db.ClassNPCs.Remove(gone);
                    }
                }
                db.SaveChanges();
                #endregion

                #region Add Orgs
                List<short> currentOrgId = db.NpcOrgs.Where(x => x.NpcId == npcId).Select(x => x.OrgId).ToList();
                if (orgs != null)
                {
                    foreach (AssoOrg_Npc assoOrg in orgs)
                    {
                        //if the association already exists
                        if (currentOrgId.Contains(assoOrg.OrgId))
                        {
                            NpcOrg toEdit = db.NpcOrgs.Where(x => x.OrgId == assoOrg.OrgId && x.NpcId == npcId).First();
                            toEdit.OrgOrder = assoOrg.OrgOrder;
                            toEdit.BlurbNpcPage = assoOrg.BlurbNpcPage;
                            toEdit.BlurbOrgPage = assoOrg.BlurbOrgPage;
                            toEdit.MemberOrder = assoOrg.MemberOrder;
                            toEdit.IsCurrent = assoOrg.IsCurrent;

                            db.Entry(toEdit).State = EntityState.Modified;
                            db.SaveChanges();
                            currentOrgId.Remove(assoOrg.OrgId);
                        }
                        else
                        {
                            NpcOrg toAdd = new NpcOrg
                            {
                                NpcId = npcId,
                                OrgId = assoOrg.OrgId,
                                OrgOrder = assoOrg.OrgOrder,
                                BlurbNpcPage = assoOrg.BlurbNpcPage,
                                BlurbOrgPage = assoOrg.BlurbOrgPage,
                                MemberOrder = assoOrg.MemberOrder,
                                IsCurrent = assoOrg.IsCurrent
                            };
                            db.NpcOrgs.Add(toAdd);
                        }
                    }
                }
                if (currentOrgId.Count != 0)
                {
                    foreach (short id in currentOrgId)
                    {
                        NpcOrg gone = db.NpcOrgs.Where(x => x.NpcId == npcId & x.OrgId == id).FirstOrDefault();
                        db.NpcOrgs.Remove(gone);
                    }
                }
                db.SaveChanges();
                #endregion
                return Json(true);
            }
            //if model fails
            ViewBag.Orgs = db.Orgs.OrderBy(o => o.Info.Name).ToList();
            ViewBag.Classes = db.Classes.OrderBy(c => c.ClassName).ToList();
            var npc = db.NPCs.Find(npcId);
            AssoNpcVM model = new AssoNpcVM { InfoId = infoId, NpcId = npcId, Submit = submit, Name = npc.Info.Name, AssoClasses = assoClasses, AssoOrgs = orgs };
            return View(model);
        }

        // GET: NPCs/Delete/5
        public ActionResult Delete(short? id)
        {
            if (id == null)
            {
                return new HttpStatusCodeResult(HttpStatusCode.BadRequest);
            }
            NPC nPC = db.NPCs.Find(id);
            if (nPC == null)
            {
                return HttpNotFound();
            }
            return View(nPC);
        }

        // POST: NPCs/Delete/5
        [HttpPost, ActionName("Delete")]
        [ValidateAntiForgeryToken]
        public ActionResult DeleteConfirmed(short id)
        {
            NPC nPC = db.NPCs.Find(id);
            short infoId = nPC.InfoId;
            string picture1 = nPC.PortraitFileName;
            string picture2 = nPC.CrestFileName;

            #region Remove Associations

            #region Remove ClassNpcs
            List<ClassNPC> classes = db.ClassNPCs.Where(x => x.NpcId == id).ToList();
            foreach (ClassNPC gone in classes)
            {
                db.ClassNPCs.Remove(gone);
            }
            #endregion

            #region Remove NpcOrgs
            List<NpcOrg> orgs = db.NpcOrgs.Where(x => x.NpcId == id).ToList();
            foreach (NpcOrg gone in orgs)
            {
                db.NpcOrgs.Remove(gone);
            }
            #endregion
            #endregion

            #region Remove as Council Member
            List<Locale> locales = db.Locales.Where(l => l.CouncilDelegateId == id).ToList();
            foreach (Locale locale in locales)
            {
                locale.CouncilDelegateId = null;
                db.Entry(locale).State = EntityState.Modified;
                db.SaveChanges();
            }
            #endregion

            db.NPCs.Remove(nPC);

            #region Remove Pictures
            //portrait
            if (picture1 != "default.jpg" && !String.IsNullOrEmpty(picture1))
            {
                string fullPath = Request.MapPath("~/Content/img/npc/" + picture1);
                if (System.IO.File.Exists(fullPath))
                {
                    System.IO.File.Delete(fullPath);
                }
            }
            //crest
            if (picture2 != "org_default.jpg" && !String.IsNullOrEmpty(picture2))
            {
                string fullPath2 = Request.MapPath("~/Content/img/npc/" + picture2);
                if (System.IO.File.Exists(fullPath2))
                {
                    System.IO.File.Delete(fullPath2);
                }
            }
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
