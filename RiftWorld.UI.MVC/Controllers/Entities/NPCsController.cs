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
    public class NPCsController : Controller
    {
        private RiftWorldEntities db = new RiftWorldEntities();

        //----------------Yeah model binding does jack diddly. Validation is going to have to be hand coded. 
        //private bool Validate(AWSmol aWSmol)
        //{
        //    if (ModelState.IsValid)
        //    {
        //        return true;
        //    }
        //    return false;
        //}

        /*//public ActionResult Confirmed()
        //{
        //    return View();
        //}
        //// GET: Rumors/Create
        //[OverrideAuthorization]
        //public PartialViewResult CreateRumor(short id)
        //{
        //    //ViewBag.HasChar = null;
        //    var userId = User.Identity.GetUserId();
        //    var character = db.UserDetails.Where(u => u.UserId == userId).Select(u => u.CurrentCharacterId).FirstOrDefault();
        //    RumorCreateVM model = new RumorCreateVM { RumorOfId = id, AuthorId = character, IsApproved = false };
        //    return PartialView(model);
        //}

        //// POST: Rumors/Create
        //// To protect from overposting attacks, please enable the specific properties you want to bind to, for 
        //// more details see https://go.microsoft.com/fwlink/?LinkId=317598.
        //[HttpPost]
        //[ValidateAntiForgeryToken]
        //[OverrideAuthorization]
        //[Authorize(Roles ="Player, Mod")]
        //public ActionResult CreateRumor([Bind(Include = "RumorOfId,RumorText, AuthorId, IsApproved")] RumorCreateVM rumor)
        //{
        //    if (ModelState.IsValid)
        //    {
        //        Rumor daRumor = new Rumor { RumorOfId = rumor.RumorOfId, IsApproved = rumor.IsApproved, RumorText = rumor.RumorText, AuthorId = (short)rumor.AuthorId };
        //        db.Rumors.Add(daRumor);
        //        db.SaveChanges();
        //        return RedirectToAction("Confirmed");
        //    }
        //    else
        //    {
        //        return RedirectToAction("Error");
        //    }
        //}*/

        //public bool IsRealOrg(List<AssociationWork> asso)
        //{
        //    foreach (AssociationWork a in asso)
        //    {
        //        Org org = db.Orgs.Where(o => o.OrgId == a.OrgId).FirstOrDefault();
        //        if (org == null)
        //        {
        //            return false;
        //        }
        //    }

        //    return true;
        //}
        ////todo all of these privates should go into the ctor for the model 
        //private IEnumerable<SelectListItem> GetAsso()
        //{
        //    List<SelectListItem> list = new List<SelectListItem>();

        //    var orgs = db.Orgs.ToList();
        //    foreach (var o in orgs)
        //    {
        //        list.Add(new SelectListItem { Text = o.Name, Value = o.OrgId.ToString() });
        //    }
        //    return list;
        //}

        //private IEnumerable<SelectListItem> GetOrgs()
        //{
        //    List<SelectListItem> list = new List<SelectListItem>();

        //    var orgs = db.Orgs.ToList();
        //    foreach (var o in orgs)
        //    {
        //        list.Add(new SelectListItem { Text = o.Name, Value = o.OrgId.ToString() });
        //    }
        //    return list;
        //}

        //private IEnumerable<SelectListItem> GetClasses()
        //{
        //    List<SelectListItem> list = new List<SelectListItem>();

        //    var classes = db.Classes.ToList();
        //    foreach (var o in classes)
        //    {
        //        list.Add(new SelectListItem { Text = o.ClassName, Value = o.ClassId.ToString() });
        //    }
        //    return list;
        //}

        //private IEnumerable<SelectListItem> GetTags()
        //{
        //    List<SelectListItem> list = new List<SelectListItem>();

        //    var tags = db.Tags.ToList();
        //    foreach (var o in tags)
        //    {
        //        list.Add(new SelectListItem { Text = o.TagName, Value = o.TagId.ToString() });
        //    }
        //    return list;
        //}

        //public ActionResult Test3()
        //{
        //    //all the multi-selects with extra options
        //    //var model = new NpcCreateGetVM
        //    //{
        //    //    //OptionsOrgs = GetOrgs(),
        //    //    //OptionsClasses = GetClasses(),
        //    //    //OptionsTags = GetTags()
        //    //};

        //    //race and location dropdowns
        //    ViewBag.LastLocationId = new SelectList(db.Locales, "LocaleId", "Name");
        //    ViewBag.RaceId = new SelectList(db.Races, "RaceId", "RaceName");
        //    ViewBag.Orgs = db.Orgs.ToList();
        //    ViewBag.Classes = db.Classes.ToList();
        //    ViewBag.Tags = db.Tags.ToList();

        //    return View();
        //}

        //[HttpPost]
        //public ActionResult Test3(string name, string alias, string quote, byte? raceId, string apperanceText, string aboutText, byte? lastLocationId, List<Npc_Class> assoClasses, List<Npc_Org> assoOrgs, List<Npc_Tag> assoTags)
        //{

        //    System.Diagnostics.Debug.WriteLine(name);
        //    foreach (Npc_Class a in assoClasses)
        //    {
        //        System.Diagnostics.Debug.WriteLine("ClassID: " + a.ClassId);
        //        System.Diagnostics.Debug.WriteLine("Order: " + a.ClassOrder);
        //    }
        //    foreach (Npc_Org a in assoOrgs)
        //    {
        //        System.Diagnostics.Debug.WriteLine("OrgId: " + a.OrgId);
        //        System.Diagnostics.Debug.WriteLine("OrderNpc: " + a.OrderNpc);
        //        System.Diagnostics.Debug.WriteLine("OrderOrg: " + a.OrderOrg);

        //    }
        //    foreach (Npc_Tag a in assoTags)
        //    {
        //        System.Diagnostics.Debug.WriteLine("TagId: " + a.TagId);
        //    }

        //    //System.Diagnostics.Debug.WriteLine(dave);
        //    //System.Diagnostics.Debug.WriteLine(dave);
        //    //System.Diagnostics.Debug.WriteLine(dave);
        //    //System.Diagnostics.Debug.WriteLine(dave);
        //    //System.Diagnostics.Debug.WriteLine(dave);

        //    return View();
        //}

        ////[HttpPost]
        ////public ActionResult Test3([Bind(Include = "Name,Alias,Quote,PortraitFileName,RaceId,CrestFileName,ApperanceText,AboutText,LastLocationId,IsPublished, Blurb")] NpcCreatePostVM npc,
        ////List<Npc_Class> classes,
        ////List<Npc_Org> orgs,
        ////List<Npc_Tag> tags)
        ////{
        ////    return View();
        ////}

        //public ActionResult Test2()
        //{
        //    var model = new AssociationWork_Full
        //    {
        //        AssociationOptions = GetAsso()
        //    };


        //    return View(model);
        //}

        //[HttpPost]
        //public ActionResult Test2(string name, List<AssociationWork> associations)
        //{
        //    AssociationWork_Full returnOnError = new AssociationWork_Full { Name = name, AssociationOptions = GetAsso(), Associations = associations };
        //    System.Diagnostics.Debug.WriteLine(name);

        //    foreach (AssociationWork a in associations)
        //    {
        //        System.Diagnostics.Debug.WriteLine("OrgId: " + a.OrgId);
        //        System.Diagnostics.Debug.WriteLine("Order: " + a.Order);
        //    }

        //    AWSmol test = new AWSmol(name, associations);
        //    //bool dave = Validate(test);
        //    bool dave = test.Validate();
        //    System.Diagnostics.Debug.WriteLine(dave);

        //    if (!dave || !IsRealOrg(associations)) //<---- something done messed up, the data is invalid, second part checks that all orgids given were actually real
        //    {
        //        //here is where I have those values to return to the create menu again cause it was invalid
        //        //or just an error page if I'm lazy.....
        //        ViewBag.Message = "Look, the curse is a hard thing to deal with. Something went wrong. What? I can't tell you. But you need to try again.";
        //        return View(returnOnError);
        //    }

        //    //NPC npc;
        //    //npc = new NPC();
        //    //npc.Name = name;

        //    //Info info;

        //    //short oldLargestInfoId = db.Infos.Max(i => i.InfoId);

        //    //info = new Info
        //    //{
        //    //    InfoTypeId = 2, // <----- type 2 = NPC
        //    //    IdWithinType = (short)(db.NPCs.Max(n => n.NpcId) + 1), //hack in order to not have to come back to this AGAIN later. But if needed cna be done after NPC actually made
        //    //    Blurb = "filler", //npc.Blurb,
        //    //    Name = npc.Name,
        //    //    IsPublished = npc.IsPublished //TODO rewrite logic for a save and publish button scheme
        //    //};
        //    //db.Infos.Add(info);

        //    //db.SaveChanges();

        //    //short infoID = db.Infos.Max(i => i.InfoId);

        //    //npc.InfoId = infoID;
        //    //npc.Alias = "filler";
        //    //npc.Quote = "filler";
        //    //npc.PortraitFileName = null;
        //    //npc.RaceId = 1;
        //    //npc.CrestFileName = null;
        //    //npc.ApperanceText = "filler";
        //    //npc.AboutText = "filler";
        //    //npc.LastLocationId = 1;

        //    //db.NPCs.Add(npc);
        //    //db.SaveChanges();
        //    //short npcId = db.NPCs.Max(i => i.NpcId);

        //    ////now make the connections 
        //    //foreach (AssociationWork a in associations)
        //    //{
        //    //    var org = new NpcOrg
        //    //    {
        //    //        NpcId = npcId,
        //    //        OrgId = a.OrgId,
        //    //        IsCurrent = true,
        //    //        OrgOrder = a.Order
        //    //    };

        //    //    db.NpcOrgs.Add(org);
        //    //    db.SaveChanges();
        //    //}//end foreach

        //    return RedirectToAction("Index");
        //}

        //public ActionResult Test()
        //{
        //    //var model = new TesterVM
        //    //{
        //    //    OrgAssociations = GetOrgs()
        //    //};


        //    return View();
        //}

        //// GET: NPCs
        //public ActionResult Index()
        //{
        //    //DO NOT, under any circumstances, not include Info. Not until you experiment and figure out if you don't need it when doing the secrets, rumors, stories, and tags
        //    var nPCs = db.NPCs.Include(n => n.Info).Include(n => n.Locale).Include(n => n.Race);

        //    //var npcAndClass = from n in db.NPCs
        //    //            join cn in db.ClassNPCs on n.NpcId equals cn.NpcId
        //    //            join c in db.Classes on cn.ClassId equals c.ClassId
        //    //            select new
        //    //            {
        //    //                NpcId = n.NpcId,
        //    //                ClassId = c.ClassId
        //    //                //other assignments
        //    //            }
        //    //        ;

        //    return View(nPCs.ToList());
        //}

        //// GET: NPCs/Details/5
        //public ActionResult Details(short? id)
        //{
        //    if (id == null)
        //    {
        //        return new HttpStatusCodeResult(HttpStatusCode.BadRequest);
        //    }
        //    NPC nPC = db.NPCs.Find(id);
        //    if (nPC == null)
        //    {
        //        return HttpNotFound();
        //    }
        //    return View(nPC);
        //}

        //// GET: NPCs/Details/5
        //public ActionResult DetailsV2(short? id)
        //{
        //    if (id == null)
        //    {
        //        return new HttpStatusCodeResult(HttpStatusCode.BadRequest);
        //    }
        //    NPC nPC = db.NPCs.Find(id);
        //    if (nPC == null)
        //    {
        //        return HttpNotFound();
        //    }
        //    var npcAndClass = from n in db.NPCs
        //                      join cn in db.ClassNPCs on n.NpcId equals cn.NpcId
        //                      join c in db.Classes on cn.ClassId equals c.ClassId
        //                      select new
        //                      {
        //                          NpcId = n.NpcId,
        //                          ClassId = c.ClassId
        //                          //other assignments
        //                      }
        //            ;
        //    //add the npc's classes
        //    //var classes = db.ClassNPCs.Where(c => c.NpcId == id).OrderBy(c => c.ClassOrder.HasValue ).Select(c => c.Class).ToList();
        //    List<Class> classes = (from cn in db.ClassNPCs
        //                           where cn.NpcId == id
        //                           orderby cn.ClassOrder.HasValue descending, cn.ClassOrder, cn.Class.ClassName
        //                           select cn.Class)
        //                   .ToList()
        //                   ;

        //    int classLength = classes.Count();
        //    string holder;
        //    switch (classLength)
        //    {
        //        case 0:
        //            holder = "Unknown";
        //            break;
        //        case 1:
        //            holder = classes[0].ClassName;
        //            break;
        //        case 2:
        //            holder = classes[0].ClassName + "/" + classes[1].ClassName;
        //            break;
        //        default:
        //            holder = "Lots";
        //            break;
        //    }
        //    ViewBag.Classes = holder;
        //    //rumors section
        //    ViewBag.HasChar = null;
        //    if (User.IsInRole("Player") || User.IsInRole("Mod"))
        //    {
        //        var userId = User.Identity.GetUserId();
        //        var character = db.UserDetails.Where(u => u.UserId == userId).Select(u => u.CurrentCharacterId).FirstOrDefault();
        //        ViewBag.HasChar = character;
        //        ViewBag.Approved = db.Characters.Where(c => c.CharacterId == character).Select(c => c.IsApproved);
        //    }
        //    return View(nPC);
        //}

        //[HttpGet]
        //public PartialViewResult _OrgsPartial(short id)
        //{
        //    List<OrgVM> orgs = (from no in db.NpcOrgs
        //                        join o in db.Orgs on no.OrgId equals o.OrgId
        //                        where no.NpcId == id && no.IsCurrent == true && o.IsPublished == true
        //                        orderby no.OrgOrder.HasValue descending, no.OrgOrder, o.Name
        //                        select new OrgVM
        //                        {
        //                            Name = o.Name,
        //                            Blurb = no.BlurbNpcPage
        //                        })
        //               .ToList()
        //                ;

        //    return PartialView(orgs);
        //}


        //// GET: NPCs/Create
        //public ActionResult CreateV2()
        //{
        //    ViewBag.LastLocationId = new SelectList(db.Locales, "LocaleId", "Name");
        //    ViewBag.RaceId = new SelectList(db.Races, "RaceId", "RaceName");

        //    return View();
        //}

        //[HttpPost]
        //[ValidateAntiForgeryToken]
        ////public ActionResult Create([Bind(Include = "NpcId,InfoId,Name,Alias,Quote,PortraitFileName,RaceId,CrestFileName,ApperanceText,AboutText,LastLocationId,IsPublished")] NPC nPC, string blurb)
        //public ActionResult CreateV2([Bind(Include = "Name,Alias,Quote,PortraitFileName,RaceId,CrestFileName,ApperanceText,AboutText,LastLocationId,IsPublished, Blurb")] NpcCreatePostVM nPC)
        //{


        //    //make corresponding info 
        //    Info info;

        //    if (ModelState.IsValid) //actual making of the info
        //    {
        //        short oldLargestInfoId = db.Infos.Max(i => i.InfoId);

        //        info = new Info
        //        {
        //            InfoTypeId = 2, // <----- type 2 = NPC
        //            IdWithinType = (short)(db.NPCs.Max(n => n.NpcId) + 1), //hack in order to not have to come back to this AGAIN later. But if needed cna be done after NPC actually made
        //            Blurb = nPC.Blurb,
        //            Name = nPC.Name,
        //            IsPublished = nPC.IsPublished
        //        };
        //        db.Infos.Add(info);

        //        db.SaveChanges();

        //    }
        //    else //if blurb is too long (and thus not valid)
        //    {
        //        ViewBag.LastLocationId = new SelectList(db.Locales, "LocaleId", "Name", nPC.LastLocationId);
        //        ViewBag.RaceId = new SelectList(db.Races, "RaceId", "RaceName", nPC.RaceId);
        //        ViewBag.Blurb = nPC.Blurb;
        //        return View(nPC);
        //    }

        //    //adding proper InfoId to NPC before checking if Model is valid (cause till now, it wasn't)
        //    short infoID = db.Infos.Max(i => i.InfoId);



        //    if (ModelState.IsValid)
        //    {
        //        NPC actualNpc = new NPC
        //        {
        //            InfoId = infoID,
        //            Name = nPC.Name,
        //            Alias = nPC.Alias,
        //            Quote = nPC.Quote,
        //            PortraitFileName = nPC.PortraitFileName,
        //            RaceId = nPC.RaceId,
        //            CrestFileName = nPC.CrestFileName,
        //            ApperanceText = nPC.ApperanceText,
        //            AboutText = nPC.AboutText,
        //            LastLocationId = nPC.LastLocationId,
        //            IsPublished = nPC.IsPublished
        //        };
        //        db.NPCs.Add(actualNpc);
        //        db.SaveChanges();
        //        return RedirectToAction("Index");
        //    }

        //    ViewBag.LastLocationId = new SelectList(db.Locales, "LocaleId", "Name", nPC.LastLocationId);
        //    ViewBag.RaceId = new SelectList(db.Races, "RaceId", "RaceName", nPC.RaceId);
        //    return View(nPC);
        //}

        // GET: NPCs/Create
        public ActionResult Create()
        {
            ViewBag.InfoId = new SelectList(db.Infos, "InfoId", "Blurb");
            ViewBag.LastLocationId = new SelectList(db.Locales, "LocaleId", "Name");
            ViewBag.RaceId = new SelectList(db.Races, "RaceId", "RaceName");
            return View();
        }

        // POST: NPCs/Create
        // To protect from overposting attacks, please enable the specific properties you want to bind to, for 
        // more details see https://go.microsoft.com/fwlink/?LinkId=317598.
        [HttpPost]
        [ValidateAntiForgeryToken]
        //public ActionResult Create([Bind(Include = "NpcId,InfoId,Name,Alias,Quote,PortraitFileName,RaceId,CrestFileName,ApperanceText,AboutText,LastLocationId,IsPublished")] NPC nPC, string blurb)
        public ActionResult Create([Bind(Include = "NpcId,InfoId,Name,Alias,Quote,PortraitFileName,RaceId,CrestFileName,ApperanceText,AboutText,LastLocationId,IsPublished")] NPC nPC,
            HttpPostedFileBase portraitPic,
            HttpPostedFileBase crestPic)
        {
            //note: add blurb to creation form and binding (also figure out how ModelState and Bind actually work. This is pseudo code
            //TODO - make this real code. It is making a Info at same time as new NPC
            Info info;
            short npcId = (short)(db.NPCs.Max(n => n.NpcId) + 1);
            string[] goodExts = { ".jpg", ".jpeg", ".gif", ".png" };

            //note to self - this really should be placed after the modelstat is checked valid. After all, null is valid for the portraits. 
            #region Image Upload -Portrait
            if (portraitPic != null)
            {
                string imgName = portraitPic.FileName;

                //string[] goodExts = { ".jpg", ".jpeg", ".gif", ".png" };
                string ext = imgName.Substring(imgName.LastIndexOf('.'));

                if (goodExts.Contains(ext.ToLower()))
                {
                    imgName = "npc-portrait-" + npcId.ToString() + ext;

                    //portraitPic.SaveAs(Path.Combine("~/Content/img/npc/", imgName));
                    portraitPic.SaveAs(Server.MapPath("~/Content/img/npc/" + imgName));
                    nPC.PortraitFileName = imgName;
                }
                else
                {
                    imgName = "default.jpg";
                }

            }
            #endregion

            #region Image Upload -Crest
            if (crestPic != null)
            {
                string imgName = crestPic.FileName;

                string ext = imgName.Substring(imgName.LastIndexOf('.'));

                if (goodExts.Contains(ext.ToLower()))
                {
                    imgName = "npc-crest-" + npcId.ToString() + ext;

                    crestPic.SaveAs(Server.MapPath("~/Content/img/npc/" + imgName));
                    nPC.CrestFileName = imgName;
                }
                else
                {
                    imgName = "org_default.jpg";
                }

            }
            #endregion


            string blurb = "things";
            if (blurb.Length <= 100)
            {
                short oldLargestInfoId = db.Infos.Max(i => i.InfoId);

                info = new Info
                {
                    InfoTypeId = 2,
                    IdWithinType = (short)(db.NPCs.Max(n => n.NpcId) + 1),
                    Blurb = blurb,
                    Name = nPC.Name,
                    IsPublished = nPC.IsPublished
                };
                db.Infos.Add(info);
                //short newLargestInfoId = db.Infos.Max(i => i.InfoId);
                //if (oldLargestInfoId != newLargestInfoId)
                //{
                //    //ViewBag.Message = "yup. Old = " + oldLargestInfoId + ". New = " + newLargestInfoId;
                //    ViewBag.Message = nPC.NpcId;
                //    return View("test");
                //}
                //else
                //{
                //    //ViewBag.Message = "NOOOOOOOOOOOOOOO. Old = " + oldLargestInfoId + ". New = " + newLargestInfoId;
                //    ViewBag.Message = nPC.NpcId;
                //    return View("test");

                //}

                db.SaveChanges();

                //short newLargestInfoId = db.Infos.Max(i => i.InfoId);
                //if (oldLargestInfoId != newLargestInfoId)
                //{
                //    ViewBag.Message = "yup. Old = " + oldLargestInfoId + ". New = " + newLargestInfoId;
                //}
                //else
                //{
                //    ViewBag.Message = "NOOOOOOOOOOOOOOO. Old = " + oldLargestInfoId + ". New = " + newLargestInfoId;
                //}

            }
            else
            {
                ViewBag.Message = "Blurb was too long, try again";
                ViewBag.InfoId = new SelectList(db.Infos, "InfoId", "Blurb", nPC.InfoId);
                ViewBag.LastLocationId = new SelectList(db.Locales, "LocaleId", "Name", nPC.LastLocationId);
                ViewBag.RaceId = new SelectList(db.Races, "RaceId", "RaceName", nPC.RaceId);
                ViewBag.Blurb = blurb;
                return View(nPC);
            }

            short blue = db.Infos.Max(i => i.InfoId);
            nPC.InfoId = blue;
            ViewBag.SecondMessage = blue;


            if (ModelState.IsValid)
            {
                db.NPCs.Add(nPC);
                db.SaveChanges();
                return RedirectToAction("Index");
            }

            ViewBag.InfoId = new SelectList(db.Infos, "InfoId", "Blurb", nPC.InfoId);
            ViewBag.LastLocationId = new SelectList(db.Locales, "LocaleId", "Name", nPC.LastLocationId);
            ViewBag.RaceId = new SelectList(db.Races, "RaceId", "RaceName", nPC.RaceId);
            return View(nPC);
        }

        // GET: NPCs/Edit/5
        public ActionResult Edit(short? id)
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
            ViewBag.InfoId = new SelectList(db.Infos, "InfoId", "Blurb", nPC.InfoId);
            ViewBag.LastLocationId = new SelectList(db.Locales, "LocaleId", "Name", nPC.LastLocationId);
            ViewBag.RaceId = new SelectList(db.Races, "RaceId", "RaceName", nPC.RaceId);
            return View(nPC);
        }

        // POST: NPCs/Edit/5
        // To protect from overposting attacks, please enable the specific properties you want to bind to, for 
        // more details see https://go.microsoft.com/fwlink/?LinkId=317598.
        [HttpPost]
        [ValidateAntiForgeryToken]
        public ActionResult Edit([Bind(Include = "NpcId,InfoId,Name,Alias,Quote,PortraitFileName,RaceId,CrestFileName,ApperanceText,AboutText,LastLocationId,IsPublished")] NPC nPC)
        {
            if (ModelState.IsValid)
            {
                db.Entry(nPC).State = EntityState.Modified;
                db.SaveChanges();
                return RedirectToAction("Index");
            }
            ViewBag.InfoId = new SelectList(db.Infos, "InfoId", "Blurb", nPC.InfoId);
            ViewBag.LastLocationId = new SelectList(db.Locales, "LocaleId", "Name", nPC.LastLocationId);
            ViewBag.RaceId = new SelectList(db.Races, "RaceId", "RaceName", nPC.RaceId);
            return View(nPC);
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
            db.NPCs.Remove(nPC);
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
