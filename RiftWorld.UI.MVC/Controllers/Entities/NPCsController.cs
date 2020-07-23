using System;
using System.Collections.Generic;
using System.Data;
using System.Data.Entity;
using System.Linq;
using System.Net;
using System.Web;
using System.Web.Mvc;
using RiftWorld.DATA.EF;

namespace RiftWorld.UI.MVC.Controllers.Entities
{
    public class NPCsController : Controller
    {
        private RiftWorldEntities db = new RiftWorldEntities();

        public ActionResult Test()
        {
            return View("test");
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

        // GET: NPCs/Create
        public ActionResult CreateV2()
        {
            ViewBag.InfoId = new SelectList(db.Infos, "InfoId", "Blurb");
            ViewBag.LastLocationId = new SelectList(db.Locales, "LocaleId", "Name");
            ViewBag.RaceId = new SelectList(db.Races, "RaceId", "RaceName");
            return View();
        }

        [HttpPost]
        [ValidateAntiForgeryToken]
        //public ActionResult Create([Bind(Include = "NpcId,InfoId,Name,Alias,Quote,PortraitFileName,RaceId,CrestFileName,ApperanceText,AboutText,LastLocationId,IsWorkInProgress")] NPC nPC, string blurb)
        public ActionResult CreateV2([Bind(Include = "NpcId,InfoId,Name,Alias,Quote,PortraitFileName,RaceId,CrestFileName,ApperanceText,AboutText,LastLocationId,IsWorkInProgress")] NPC nPC, string blurb)
        {
            //make corresponding info 
            Info info;
            
            if (blurb.Length <= 100) //actual making of the info
            {
                short oldLargestInfoId = db.Infos.Max(i => i.InfoId);

                info = new Info
                {
                    InfoTypeId = 2, // <----- type 2 = NPC
                    IdWithinType = (short)(db.NPCs.Max(n => n.NpcId) + 1), //hack in order to not have to come back to this AGAIN later. But if needed cna be done after NPC actually made
                    Blurb = blurb,
                    Name = nPC.Name,
                    IsPublished = nPC.IsWorkInProgress
                };
                db.Infos.Add(info);

                db.SaveChanges();

            }
            else //if blurb is too long (and thus not valid)
            {
                ViewBag.Message = "Blurb was too long, try again";
                ViewBag.InfoId = new SelectList(db.Infos, "InfoId", "Blurb", nPC.InfoId);
                ViewBag.LastLocationId = new SelectList(db.Locales, "LocaleId", "Name", nPC.LastLocationId);
                ViewBag.RaceId = new SelectList(db.Races, "RaceId", "RaceName", nPC.RaceId);
                ViewBag.Blurb = blurb;
                return View(nPC);
            }

            //adding proper InfoId to NPC before checking if Model is valid (cause till now, it wasn't)
            nPC.InfoId = db.Infos.Max(i => i.InfoId); 

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
