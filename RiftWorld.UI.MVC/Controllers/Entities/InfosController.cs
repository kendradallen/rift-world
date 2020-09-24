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

        public ActionResult TogglePub(short id, string submit)
        {
            Info info = db.Infos.Where(i => i.InfoId == id).First();
            if (submit == "Publish")
            {
                info.IsPublished = true;
            }
            else
            {
                info.IsPublished = false;
            }
            db.Entry(info).State = EntityState.Modified;
            db.SaveChanges();

            return RedirectToAction("Details", new { id = id });
        }


        [Authorize(Roles = "Admin")]
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
            var characterEdits = db.Characters.Where(c => c.HasUnseenEdit && c.IsApproved).ToList();
            var journalEdits = db.Journals.Where(j => j.HasUnseenEdit && j.IsApproved).ToList();
            var retireRequests = db.Characters.Where(c => c.IsRequestingRetire).ToList();

            ApprovalVM model = new ApprovalVM { Rumors = rumors, Characters = characters, Journals = journals, CharacterEdits = characterEdits, JournalEdits = journalEdits, RetireRequests = retireRequests };
            if (User.IsInRole("Admin"))
            {
                var users = db.UserDetails.Where(x => !x.IsApproved).ToList();
                model.Users = users;
                var orgs = db.CharOrgs.Where(o => !o.KatherineApproved).ToList();
                model.Orgs = orgs;
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

            List<Info> infos = (from it in db.InfoTags
                                join i in db.Infos on it.InfoId equals i.InfoId
                                where it.TagId == id
                                orderby i.Name
                                select i)
                .ToList()
                ;
            List<Story> stories = (from st in db.StoryTags
                                   join s in db.Stories on st.StoryId equals s.StoryId
                                   where st.TagId == id
                                   orderby s.Title
                                   select s)
                                   .ToList()
                                   ;

            if (!User.IsInRole("Admin"))
            {
                infos = infos.Where(i => i.IsPublished).ToList();
                stories = stories.Where(s => s.Info.IsPublished).ToList();
            }
            ComboResult model = new ComboResult { Infos = infos, Stories = stories };
            return View(model);
        }

        // GET: Infos
        public ActionResult Index()
        {
            var infos = db.Infos.Include(i => i.InfoType);
            return View(infos.ToList());
        }

        // GET: Infos/Details/5
        public ActionResult Details(short? id, short? story)
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

            return RedirectToAction("Details", info.Controller + 's', new { id = info.IdWithinType, story = story });
        }

        // GET: Infos/Create
        [Authorize(Roles = "Admin")]
        public ActionResult CreateWhat()
        {
            return View();
        }

        //todo - make ultimate search
        [HttpGet]
        public ActionResult Search(string search)
        {
            string comparer = search.ToLower().Trim();

            //todo - ask katherine if she wants me to search blurb as well (noting the performance drop)
            #region Infos
            #region Comparers
            List<short> peps = (from n in db.NPCs
                                where n.Alias.ToLower().Contains(comparer)
                                select n.InfoId
                                )
                                .Distinct()
                                .ToList()
                                ;
            List<short> rifts = (from r in db.Rifts
                                 where r.Location.ToLower().Contains(comparer)
                                 select r.InfoId
                                )
                                .Distinct()
                                .ToList()
                                ;
            #endregion

            List<Info> infosTag = (from i in db.Infos
                                   join spaner in db.InfoTags on i.InfoId equals spaner.InfoId
                                   join t in db.Tags on spaner.TagId equals t.TagId
                                   where t.TagName.ToLower().Contains(comparer)
                                   select i
                                )
                                .ToList()
                                ;
            List<Info> infosName = (from i in db.Infos
                                    where i.Name.ToLower().Contains(comparer)
                                        || peps.Contains(i.InfoId)
                                        || rifts.Contains(i.InfoId)
                                        || i.Blurb.ToLower().Contains(comparer)
                                    select i
                                    )
                                    .Distinct()
                                    .ToList()
                                    ;
            List<Info> infos = infosName.Union(infosTag).Distinct().ToList();

            #endregion

            #region Stories
            List<Story> storiesTag = (from s in db.Stories
                                      join spaner in db.StoryTags on s.StoryId equals spaner.StoryId
                                      join t in db.Tags on spaner.TagId equals t.TagId
                                      where t.TagName.ToLower().Contains(comparer)
                                      select s
                                    )
                                    .ToList()
                                    ;
            List<Story> storiesName = (from s in db.Stories
                                       where s.Title.ToLower().Contains(comparer)
                                       select s
                                    )
                                    .ToList()
                                    ;
            List<Story> stories = storiesTag.Union(storiesName).Distinct().ToList();
            #endregion

            List<Character> characters = (from c in db.Characters
                                          where c.CharacterName.ToLower().Contains(comparer) && c.IsApproved
                                          select c
                                        )
                                        .Distinct()
                                        .ToList()
                                        ;

            if (!User.IsInRole("Admin"))
            {
                infos = infos.Where(i => i.IsPublished).ToList();
                stories = stories.Where(s => s.Info.IsPublished).ToList();
            }

            ComboResultUltra model = new ComboResultUltra { Infos = infos, Stories = stories, Characters = characters };

            ViewBag.Query = search;
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
        [Authorize(Roles = "Admin")]
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

        public PartialViewResult _AssoTips()
        {
            return PartialView();
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
