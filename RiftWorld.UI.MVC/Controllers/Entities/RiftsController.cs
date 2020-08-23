﻿using System;
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
    public class RiftsController : Controller
    {
        private RiftWorldEntities db = new RiftWorldEntities();

        // GET: Rifts
        public ActionResult Index()
        {
            var rifts = db.Rifts.Include(r => r.Info);
            return View(rifts.ToList());
        }

        [HttpGet]
        public string Variety(short id)
        {
            List<Race> variety = (from no in db.VarietyOfInhabitants
                                join r in db.Races on no.RaceId equals r.RaceId
                                where no.RiftId == id 
                                orderby no.RaceOrder.HasValue descending, no.RaceOrder, r.RaceName
                                select r)
                       .ToList()
                        ;

            string str ="";
            int length = variety.Count;

            for (int i = 0; i < length; i++)
            {
                str += variety[i].Plural;
                if (i != length-1)
                {
                    str += ", ";
                }
            }

            return str;
            //return PartialView(variety);
        }

        // GET: Rifts/Details/5
        public ActionResult Details(short? id)
        {
            if (id == null)
            {
                return new HttpStatusCodeResult(HttpStatusCode.BadRequest);
            }
            Rift rift = db.Rifts.Find(id);
            if (rift == null)
            {
                return HttpNotFound();
            }
            ViewBag.Variety = Variety((short)id);
            return View(rift);
        }
        //CreateCombo is a work in progress and should not be actually used.
        //public ActionResult CreateCombo()
        //{
        //    ViewBag.Tags = new MultiSelectList(db.Tags, "TagId", "TagName");
        //    ViewBag.startNickname = 50;
        //    ViewBag.startLocation = 300;
        //    ViewBag.startBlurb = 100;

        //    ViewBag.Races = db.Races.ToList();
        //    List<byte> selected = db.VarietyOfInhabitants.Where(v => v.RiftId == rift.RiftId).Select(v => v.RaceId).ToList();
        //    ViewBag.Selected = selected;


        //    return View();
        //}


        // GET: Rifts/Create
        public ActionResult Create()
        {
            ViewBag.Tags = new MultiSelectList(db.Tags, "TagId", "TagName");
            ViewBag.startNickname = 50;
            ViewBag.startLocation = 300;
            ViewBag.startBlurb = 100;

            return View();
        }

        // POST: Rifts/Create
        // To protect from overposting attacks, please enable the specific properties you want to bind to, for 
        // more details see https://go.microsoft.com/fwlink/?LinkId=317598.
        [HttpPost]
        [ValidateAntiForgeryToken]
        public ActionResult Create([Bind(Include = "Nickname,Location,Environment,Hazards,Blurb")] RiftCreateVM rift,
            List<short> tags,
            string submit)
        {
            rift.IsPublished = false;
            if (ModelState.IsValid)
            {
                #region Info
                Info info;
                info = new Info
                {
                    InfoTypeId = 5,  //<-------------------Info Type ID. CHANGE UPON COPY
                    IdWithinType = null,
                    Blurb = rift.Blurb,
                    Name = rift.Nickname,
                    IsPublished = rift.IsPublished
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

                #region Rift
                Rift daRift = new Rift
                {
                    InfoId = infoId,
                    Nickname = rift.Nickname,
                    Location = rift.Location,
                    Environment = rift.Environment,
                    Hazards = rift.Hazards,
                    IsPublished = rift.IsPublished
                };
                db.Rifts.Add(daRift);
                db.SaveChanges();
                #endregion

                #region give info the IdWithinType
                short riftId = db.Rifts.Max(l => l.RiftId);
                info.IdWithinType = riftId;
                db.Entry(info).State = EntityState.Modified;
                db.SaveChanges();
                #endregion

                return RedirectToAction("AssoCreate", new { id = riftId, submit = submit });
            }

            ViewBag.Tags = new MultiSelectList(db.Tags, "TagId", "TagName", tags);

            ViewBag.startNickname = 50;
            ViewBag.startLocation = 300;
            ViewBag.startBlurb = 100;
            if (rift.Nickname != null)
            {
                ViewBag.startNickname = 50 - rift.Nickname.Length;
            }
            if (rift.Location != null)
            {
                ViewBag.startLocation = 300 - rift.Location.Length;
            }
            if (rift.Blurb != null)
            {
                ViewBag.startBlurb = 100 - rift.Blurb.Length;
            }
            return View(rift);
        }

        public ActionResult AssoCreate(short? id, string submit)
        {
            if (id == null)
            {
                return new HttpStatusCodeResult(HttpStatusCode.BadRequest);
            }
            Rift rift = db.Rifts.Find(id);
            if (rift == null)
            {
                return HttpNotFound();
            }
            ViewBag.Races = db.Races.ToList();
            //List<byte> selected = db.VarietyOfInhabitants.Where(v => v.RiftId == rift.RiftId).Select(v => v.RaceId).ToList();
            //ViewBag.Selected = selected;

            var selected = db.VarietyOfInhabitants.Where(v => v.RiftId == rift.RiftId).ToList();
            List<AssoVar> theSelected = new List<AssoVar>();
            foreach (VarietyOfInhabitant variety in selected)
            {
                AssoVar toAdd = new AssoVar(variety);
                theSelected.Add(toAdd);
            }

            var infoId = rift.InfoId;
            //AssoRiftVM model = new AssoRiftVM { InfoId = infoId, RiftId = rift.RiftId, Submit = submit };
            AssoRiftVM model = new AssoRiftVM { InfoId = infoId, RiftId = rift.RiftId, Submit = submit, Assos = theSelected, Name = rift.Nickname };
            return View(model);
        }

        [HttpPost]
        [ValidateAntiForgeryToken]
        public ActionResult AssoCreate(short infoId, short riftId, string submit,
            List<AssoVar> varieties)
        {
            var rift = db.Rifts.Where(i => i.RiftId == riftId).FirstOrDefault();
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
                        rift.IsPublished = false;
                        break;
                    case "Publish":
                    case "Save":
                        info.IsPublished = true;
                        rift.IsPublished = true;
                        break;
                    case "Save and associate":
                        break;
                    default:
                        break;
                }
                db.Entry(rift).State = EntityState.Modified;
                db.Entry(info).State = EntityState.Modified;
                #endregion

                List<byte> currentAssoId = db.VarietyOfInhabitants.Where(x => x.RiftId == riftId).Select(x => x.RaceId).ToList();
                if (varieties != null)
                {
                    foreach (AssoVar variety in varieties)
                    {
                        //if the association already exists
                        if (currentAssoId.Contains(variety.RaceId))
                        {
                            VarietyOfInhabitant toEdit = db.VarietyOfInhabitants.Where(x => x.RaceId == variety.RaceId && x.RiftId == riftId).First();
                            toEdit.RaceOrder = variety.RaceOrder;
                            db.Entry(toEdit).State = EntityState.Modified;
                            db.SaveChanges();
                            currentAssoId.Remove(variety.RaceId);
                        }
                        else
                        {
                            VarietyOfInhabitant toAdd = new VarietyOfInhabitant
                            {
                                RiftId = riftId,
                                RaceId = variety.RaceId,
                                RaceOrder = variety.RaceOrder
                            };
                            db.VarietyOfInhabitants.Add(toAdd);
                        }
                    }
                }
                if (currentAssoId.Count != 0)
                {
                    foreach (byte id in currentAssoId)
                    {
                        VarietyOfInhabitant gone = db.VarietyOfInhabitants.Where(x => x.RiftId == riftId && x.RaceId == id).FirstOrDefault();
                        db.VarietyOfInhabitants.Remove(gone);
                    }
                }
                db.SaveChanges();

                return Json(true);

            }
            //if model fails
            ViewBag.Races = db.Races.ToList();
            AssoRiftVM model = new AssoRiftVM { InfoId = infoId, RiftId = riftId, Submit = submit, Assos = varieties, Name =  rift.Nickname};
            ViewBag.Selected = varieties.Select(v => v.RaceId).ToList();
            return Json(model);
        }



        // GET: Rifts/Edit/5
        public ActionResult Edit(short? id)
        {
            if (id == null)
            {
                return new HttpStatusCodeResult(HttpStatusCode.BadRequest);
            }
            Rift rift = db.Rifts.Find(id);
            if (rift == null)
            {
                return HttpNotFound();
            }

            short infoid = rift.InfoId;
            string blurb = db.Infos.Where(i => i.InfoId == infoid).Select(i => i.Blurb).First();
            RiftEditVM model = new RiftEditVM(rift, blurb);

            List<short> selectedTags = db.InfoTags.Where(t => t.InfoId == infoid).Select(t => t.TagId).ToList();
            ViewBag.Selected = selectedTags;
            ViewBag.Tags = db.Tags.ToList();

            return View(model);
        }

        // POST: Rifts/Edit/5
        // To protect from overposting attacks, please enable the specific properties you want to bind to, for 
        // more details see https://go.microsoft.com/fwlink/?LinkId=317598.
        [HttpPost]
        [ValidateAntiForgeryToken]
        public ActionResult Edit([Bind(Include = "RiftId,InfoId,Nickname,Location,Environment,Hazards,Blurb, IsPublished")] RiftEditPostVM rift,
            List<short> tags,
            string submit)
        {
            #region Save or Publish?
            switch (submit)
            {
                case "Save Progress":
                case "Un-Publish":
                    rift.IsPublished = false;
                    break;
                case "Publish":
                case "Save":
                    rift.IsPublished = true;
                    break;
                case "Save and go to complex edit":
                    break;
            }
            #endregion

            if (ModelState.IsValid)
            {

                var infoid = rift.InfoId;
                #region Info Update
                //Info info = db.Infos.Find(infoid);
                Info info = db.Infos.Where(i => i.InfoId == infoid).FirstOrDefault();
                info.Name = rift.Nickname;
                info.Blurb = rift.Blurb;
                info.IsPublished = rift.IsPublished;
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

                #region Update Rift
                Rift daRift = new Rift
                {
                    InfoId = rift.InfoId,
                    RiftId = rift.RiftId,
                    Nickname = rift.Nickname,
                    Location = rift.Location,
                    Environment = rift.Environment,
                    Hazards = rift.Hazards,
                    IsPublished = rift.IsPublished
                };
                db.Entry(daRift).State = EntityState.Modified;
                db.Entry(info).State = EntityState.Modified;
                db.SaveChanges();
                #endregion
                if (submit == "Save and go to complex edit")
                {
                    return RedirectToAction("AssoEdit", new { id = rift.RiftId });
                }
                return RedirectToAction("Details", new { id = rift.RiftId });
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
            RiftEditVM aRift = new RiftEditVM(rift);
            return View(aRift);
        }

        public ActionResult AssoEdit(short? id)
        {
            if (id == null)
            {
                return new HttpStatusCodeResult(HttpStatusCode.BadRequest);
            }
            Rift rift = db.Rifts.Find(id);
            if (rift == null)
            {
                return HttpNotFound();
            }
            ViewBag.Races = db.Races.ToList();

            var selected = db.VarietyOfInhabitants.Where(v => v.RiftId == rift.RiftId).ToList();
            List<AssoVar> theSelected = new List<AssoVar>();
            foreach (VarietyOfInhabitant variety in selected)
            {
                AssoVar toAdd = new AssoVar(variety);
                theSelected.Add(toAdd);
            }

            var infoId = rift.InfoId;
            AssoRiftVM model = new AssoRiftVM { InfoId = infoId, RiftId = rift.RiftId, Submit = "Save", Assos = theSelected, Name = rift.Nickname };
            return View(model);
        }

        [HttpPost]
        [ValidateAntiForgeryToken]
        public ActionResult AssoEdit(short infoId, short riftId, string submit,
            List<AssoVar> varieties)
        {
            if (ModelState.IsValid)
            {
                List<byte> currentAssoId = db.VarietyOfInhabitants.Where(x => x.RiftId == riftId).Select(x => x.RaceId).ToList();
                if (varieties != null)
                {
                    foreach (AssoVar variety in varieties)
                    {
                        //if the association already exists
                        if (currentAssoId.Contains(variety.RaceId))
                        {
                            VarietyOfInhabitant toEdit = db.VarietyOfInhabitants.Where(x => x.RaceId == variety.RaceId && x.RiftId == riftId).First();
                            toEdit.RaceOrder = variety.RaceOrder;
                            db.Entry(toEdit).State = EntityState.Modified;
                            db.SaveChanges();
                            currentAssoId.Remove(variety.RaceId);
                        }
                        else
                        {
                            VarietyOfInhabitant toAdd = new VarietyOfInhabitant
                            {
                                RiftId = riftId,
                                RaceId = variety.RaceId,
                                RaceOrder = variety.RaceOrder
                            };
                            db.VarietyOfInhabitants.Add(toAdd);
                        }
                    }
                }
                if (currentAssoId.Count != 0)
                {
                    foreach (byte id in currentAssoId)
                    {
                        VarietyOfInhabitant gone = db.VarietyOfInhabitants.Where(x => x.RiftId == riftId && x.RaceId == id).FirstOrDefault();
                        db.VarietyOfInhabitants.Remove(gone);
                    }
                }
                db.SaveChanges();

                return Json(true);

            }
            //if model fails
            ViewBag.Races = db.Races.ToList();
            var rift = db.Rifts.Find(riftId);
            AssoRiftVM model = new AssoRiftVM { InfoId = infoId, RiftId = riftId, Submit = submit, Assos = varieties, Name = rift.Nickname };
            return View(model);
        }


        // GET: Rifts/Delete/5
        public ActionResult Delete(short? id)
        {
            if (id == null)
            {
                return new HttpStatusCodeResult(HttpStatusCode.BadRequest);
            }
            Rift rift = db.Rifts.Find(id);
            if (rift == null)
            {
                return HttpNotFound();
            }
            return View(rift);
        }

        // POST: Rifts/Delete/5
        [HttpPost, ActionName("Delete")]
        [ValidateAntiForgeryToken]
        public ActionResult DeleteConfirmed(short id)
        {
            Rift rift = db.Rifts.Find(id);
            db.Rifts.Remove(rift);
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
