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

        public bool IsRealOrg(List<AssociationWork> asso)
        {
            foreach (AssociationWork a in asso)
            {
                Org org = db.Orgs.Where(o => o.OrgId == a.OrgId).FirstOrDefault();
                if (org == null)
                {
                    return false;
                }
            }

            return true;
        }

        private IEnumerable<SelectListItem> GetAsso()
        {
            List<SelectListItem> list = new List<SelectListItem>();

            var orgs = db.Orgs.ToList();
            foreach (var o in orgs)
            {
                list.Add(new SelectListItem { Text = o.Name, Value = o.OrgId.ToString() });
            }
            return list;
        }

        public ActionResult Test2()
        {
            var model = new AssociationWork_Full
            {
                AssociationOptions = GetAsso()
            };


            return View(model);
        }

        [HttpPost]
        public ActionResult Test2(string name, List<AssociationWork> associations)
        {
            AssociationWork_Full returnOnError = new AssociationWork_Full { Name = name, AssociationOptions = GetAsso(), Associations = associations };
            System.Diagnostics.Debug.WriteLine(name);

            foreach (AssociationWork a in associations)
            {
                System.Diagnostics.Debug.WriteLine("OrgId: " + a.OrgId);
                System.Diagnostics.Debug.WriteLine("Order: " + a.Order);
            }

            AWSmol test = new AWSmol(name, associations);
            //bool dave = Validate(test);
            bool dave = test.Validate();
            System.Diagnostics.Debug.WriteLine(dave);

            if (!dave || !IsRealOrg(associations)) //<---- something done messed up, the data is invalid, second part checks that all orgids given were actually real
            {
                //here is where I have those values to return to the create menu again cause it was invalid
                //or just an error page if I'm lazy.....
                ViewBag.Message = "Look, the curse is a hard thing to deal with. Something went wrong. What? I can't tell you. But you need to try again.";
                return View(returnOnError);
            }

            NPC npc;
            npc = new NPC();
            npc.Name = name;

            Info info;

            short oldLargestInfoId = db.Infos.Max(i => i.InfoId);

            info = new Info
            {
                InfoTypeId = 2, // <----- type 2 = NPC
                IdWithinType = (short)(db.NPCs.Max(n => n.NpcId) + 1), //hack in order to not have to come back to this AGAIN later. But if needed cna be done after NPC actually made
                Blurb = "filler", //npc.Blurb,
                Name = npc.Name,
                IsPublished = npc.IsWorkInProgress //TODO rewrite logic for a save and publish button scheme
            };
            db.Infos.Add(info);

            db.SaveChanges();

            short infoID = db.Infos.Max(i => i.InfoId);

            npc.InfoId = infoID;
            npc.Alias = "filler";
            npc.Quote = "filler";
            npc.PortraitFileName = null;
            npc.RaceId = 1;
            npc.CrestFileName = null;
            npc.ApperanceText = "filler";
            npc.AboutText = "filler";
            npc.LastLocationId = 1;

            db.NPCs.Add(npc);
            db.SaveChanges();
            short npcId = db.NPCs.Max(i => i.NpcId);

            //now make the connections 
            foreach (AssociationWork a in associations)
            {
                var org = new NpcOrg
                {
                    NpcId = npcId,
                    OrgId = a.OrgId,
                    IsCurrent = true,
                    OrgOrder = a.Order
                };

                db.NpcOrgs.Add(org);
                db.SaveChanges();
            }//end foreach

            return RedirectToAction("Index");
        }

        public ActionResult Test()
        {
            //var model = new TesterVM
            //{
            //    OrgAssociations = GetOrgs()
            //};
           

            return View();
        }

        // GET: NPCs
        public ActionResult Index()
        {
            //DO NOT, under any circumstances, not include Info. Not until you experiment and figure out if you don't need it when doing the secrets, rumors, stories, and tags
            var nPCs = db.NPCs.Include(n => n.Info).Include(n => n.Locale).Include(n => n.Race);

            //var npcAndClass = from n in db.NPCs
            //            join cn in db.ClassNPCs on n.NpcId equals cn.NpcId
            //            join c in db.Classes on cn.ClassId equals c.ClassId
            //            select new
            //            {
            //                NpcId = n.NpcId,
            //                ClassId = c.ClassId
            //                //other assignments
            //            }
            //        ;

            return View(nPCs.ToList());
        }

        // GET: NPCs/Details/5
        public ActionResult Details(short? id)
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

        // GET: NPCs/Details/5
        public ActionResult DetailsV2(short? id)
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
            //var npcAndClass = from n in db.NPCs
            //            join cn in db.ClassNPCs on n.NpcId equals cn.NpcId
            //            join c in db.Classes on cn.ClassId equals c.ClassId
            //            select new
            //            {
            //                NpcId = n.NpcId,
            //                ClassId = c.ClassId
            //                //other assignments
            //            }
            //        ;
            var orgs = from no in db.NpcOrgs
                             join o in db.Orgs on no.OrgId equals o.OrgId
                             where no.NpcId == id && no.IsCurrent == true && o.IsWorkInProgress == false
                             orderby no.OrgOrder
                             select new
                             {
                                 Name = o.Name,
                                 Blurb = no.BlurbNpcPage
                             }
                        ;
            orgs.ToList();
            return View(nPC);
        }

        [HttpGet]
        public PartialViewResult _OrgsPartial(short id)
        {
            List<OrgVM> orgs = (from no in db.NpcOrgs
                       join o in db.Orgs on no.OrgId equals o.OrgId
                       where no.NpcId == id && no.IsCurrent == true && o.IsWorkInProgress == false
                       orderby no.OrgOrder.HasValue descending, no.OrgOrder, o.Name
                       select new OrgVM
                       {
                           Name = o.Name,
                           Blurb = no.BlurbNpcPage
                       })
                       .ToList()
                        ;

            return PartialView(orgs);
        }


        // GET: NPCs/Create
        public ActionResult CreateV2()
        {
            ViewBag.LastLocationId = new SelectList(db.Locales, "LocaleId", "Name");
            ViewBag.RaceId = new SelectList(db.Races, "RaceId", "RaceName");
            
            return View();
        }

        [HttpPost]
        [ValidateAntiForgeryToken]
        //public ActionResult Create([Bind(Include = "NpcId,InfoId,Name,Alias,Quote,PortraitFileName,RaceId,CrestFileName,ApperanceText,AboutText,LastLocationId,IsWorkInProgress")] NPC nPC, string blurb)
        public ActionResult CreateV2([Bind(Include = "Name,Alias,Quote,PortraitFileName,RaceId,CrestFileName,ApperanceText,AboutText,LastLocationId,IsWorkInProgress, Blurb")] NpcCreateViewModel nPC)
        {
            

            //make corresponding info 
            Info info;
            
            if (ModelState.IsValid) //actual making of the info
            {
                short oldLargestInfoId = db.Infos.Max(i => i.InfoId);

                info = new Info
                {
                    InfoTypeId = 2, // <----- type 2 = NPC
                    IdWithinType = (short)(db.NPCs.Max(n => n.NpcId) + 1), //hack in order to not have to come back to this AGAIN later. But if needed cna be done after NPC actually made
                    Blurb = nPC.Blurb,
                    Name = nPC.Name,
                    IsPublished = nPC.IsWorkInProgress
                };
                db.Infos.Add(info);

                db.SaveChanges();

            }
            else //if blurb is too long (and thus not valid)
            {
                ViewBag.LastLocationId = new SelectList(db.Locales, "LocaleId", "Name", nPC.LastLocationId);
                ViewBag.RaceId = new SelectList(db.Races, "RaceId", "RaceName", nPC.RaceId);
                ViewBag.Blurb = nPC.Blurb;
                return View(nPC);
            }

            //adding proper InfoId to NPC before checking if Model is valid (cause till now, it wasn't)
            short infoID = db.Infos.Max(i => i.InfoId); 

   

            if (ModelState.IsValid)
            {
                NPC actualNpc = new NPC
                {
                    InfoId = infoID,
                    Name = nPC.Name,
                    Alias = nPC.Alias,
                    Quote = nPC.Quote,
                    PortraitFileName = nPC.PortraitFileName,
                    RaceId = nPC.RaceId,
                    CrestFileName = nPC.CrestFileName,
                    ApperanceText = nPC.ApperanceText,
                    AboutText = nPC.AboutText,
                    LastLocationId = nPC.LastLocationId,
                    IsWorkInProgress = nPC.IsWorkInProgress
                };
                db.NPCs.Add(actualNpc);
                db.SaveChanges();
                return RedirectToAction("Index");
            }

            ViewBag.LastLocationId = new SelectList(db.Locales, "LocaleId", "Name", nPC.LastLocationId);
            ViewBag.RaceId = new SelectList(db.Races, "RaceId", "RaceName", nPC.RaceId);
            return View(nPC);
        }

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
        //public ActionResult Create([Bind(Include = "NpcId,InfoId,Name,Alias,Quote,PortraitFileName,RaceId,CrestFileName,ApperanceText,AboutText,LastLocationId,IsWorkInProgress")] NPC nPC, string blurb)
        public ActionResult Create([Bind(Include = "NpcId,InfoId,Name,Alias,Quote,PortraitFileName,RaceId,CrestFileName,ApperanceText,AboutText,LastLocationId,IsWorkInProgress")] NPC nPC)
        {
            //note: add blurb to creation form and binding (also figure out how ModelState and Bind actually work. This is pseudo code
            //TODO - make this real code. It is making a Info at same time as new NPC
            Info info;

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
                    IsPublished = nPC.IsWorkInProgress
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
        public ActionResult Edit([Bind(Include = "NpcId,InfoId,Name,Alias,Quote,PortraitFileName,RaceId,CrestFileName,ApperanceText,AboutText,LastLocationId,IsWorkInProgress")] NPC nPC)
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
