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
    public class InfosController : Controller
    {
        private RiftWorldEntities db = new RiftWorldEntities();

        [Authorize(Roles ="Admin")]
        public ActionResult Todo()
        {
            var infos = db.Infos.Where(x => !x.IsPublished).ToList();
            return View(infos);
        }

        [Authorize(Roles = "Mod, Admin")]
        public ActionResult Approvals()
        {
            var rumors = db.Rumors.Where(r => !r.IsApproved).ToList();
            var characters = db.Characters.Where(c => !c.IsApproved).ToList();
            var journals = db.Journals.Where(j => !j.IsApproved).ToList();
            var characterEdits = db.Characters.Where(c => c.HasUnseenEdit).ToList();
            var journalEdits = db.Journals.Where(j => j.HasUnseenEdit).ToList();

            ApprovalVM model = new ApprovalVM { Rumors = rumors, Characters = characters, Journals = journals, CharacterEdits = characterEdits, JournalEdits = journalEdits };
            if (User.IsInRole("Admin"))
            {
                var users = db.UserDetails.Where(x => !x.IsApproved).ToList();
                model.Users = users;
            }
            return View(model);
        }

        public PartialViewResult _TagList(short id)
        {
            List<Tag> tags = (from it in db.InfoTags
                              join t in db.Tags on it.TagId equals t.TagId
                              where it.InfoId == id
                              select t)
                              .ToList()
                              ;
            return PartialView(tags);
        }

        public ActionResult TagIndex(short? id)
        {
            if (id == null)
            {
                return new HttpStatusCodeResult(HttpStatusCode.BadRequest);
            }
            Tag tag = db.Tags.Find(id);
            if (tag == null)
            {
                return HttpNotFound();
            }
            ViewBag.Tag = tag.TagName;

            List<Info> model = (from it in db.InfoTags
                                join i in db.Infos on it.InfoId equals i.InfoId
                                where it.TagId == id && i.IsPublished == true
                                select i)
                                .ToList()
                                ;
            return View(model);
        }

        // GET: Infos
        public ActionResult Index()
        {
            var infos = db.Infos.Include(i => i.InfoType);
            return View(infos.ToList());
        }

        // GET: Infos/Details/5
        public ActionResult Details(short? id)
        {
            if (id == null)
            {
                return new HttpStatusCodeResult(HttpStatusCode.BadRequest);
            }
            Info info = db.Infos.Find(id);
            if (info == null)
            {
                return HttpNotFound();
            }

            return RedirectToAction("Details", info.Controller + 's', new { id = info.IdWithinType});
        }

        // GET: Infos/Create
        [Authorize(Roles ="Admin")]
        public ActionResult CreateWhat()
        {
            return View();
        }

        [HttpGet]
        public ActionResult Search(string search)
        {
            List<short> result = (from i in db.Infos
                                  join spaner in db.InfoTags on i.InfoId equals spaner.InfoId
                                  join t in db.Tags on spaner.TagId equals t.TagId
                                  where i.Name.ToLower().Contains(search.ToLower())
                                      || t.TagName.ToLower().Contains(search.ToLower())
                                      || i.InfoId == (from n in db.NPCs
                                                      where n.Alias.ToLower().Contains(search.ToLower())
                                                      select n.InfoId).FirstOrDefault()
                        select i.InfoId 
                        )
                        .Distinct()
                        .ToList()
                        ;
            //todo - add the story search too
            List<Info> model = new List<Info>();
            foreach (short id in result)
            {
                Info toAdd = db.Infos.Where(i => i.InfoId == id).First();
                model.Add(toAdd);
            }
            return View(model);
        }

        // GET: Infos/Edit/5
        [Authorize(Roles = "Admin")]
        public ActionResult Edit(short? id)
        {
            if (id == null)
            {
                return new HttpStatusCodeResult(HttpStatusCode.BadRequest);
            }
            Info info = db.Infos.Find(id);
            if (info == null)
            {
                return HttpNotFound();
            }
            return RedirectToAction("Edit", info.Controller + 's', new { id = info.IdWithinType });
        }

        [Authorize(Roles = "Admin")]
        public ActionResult AssoEdit(short? id)
        {
            if (id == null)
            {
                return new HttpStatusCodeResult(HttpStatusCode.BadRequest);
            }
            Info info = db.Infos.Find(id);
            if (info == null)
            {
                return HttpNotFound();
            }
            return RedirectToAction("AssoEdit", info.Controller + 's', new { id = info.IdWithinType });
        }

        // GET: Infos/Delete/5
        [Authorize(Roles ="Admin")]
        public ActionResult Delete(short? id)
        {
            if (id == null)
            {
                return new HttpStatusCodeResult(HttpStatusCode.BadRequest);
            }
            Info info = db.Infos.Find(id);
            if (info == null)
            {
                return HttpNotFound();
            }
            return RedirectToAction("Delete", info.Controller + 's', new { id = info.IdWithinType });
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
