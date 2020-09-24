using System.Web.Mvc;
using Microsoft.AspNet.Identity;
using RiftWorld.DATA.EF;
using System.Collections.Generic;
using System.Data;
using System.Data.Entity;
using System.Linq;
using System.Net;


namespace RiftWorld.UI.MVC.Controllers
{
    [HandleError]
    public class HomeController : Controller
    {
        private RiftWorldEntities db = new RiftWorldEntities();

        [HttpGet]
        public ActionResult Index()
        {
            return View();
        }

        [HttpGet]
        [Authorize]
        public ActionResult About()
        {
            //ViewBag.Message = "Your app description page.";

            return View();
        }

        [HttpGet]
        public ActionResult Contact()
        {
            ViewBag.Message = "Your contact page.";

            return View();
        }

        public ActionResult PendingApproval()
        {
            return View();
        }

        //public ActionResult SearchSite(string search)
        //{
        //    List<Info> result =
        //        (
        //        from ti in db.InfoTags
        //        where ti.Info.Name.Contains(search)
        //            || ti.Tag.TagName.Contains(search)
        //        select ti.Info
        //        ).ToList()
        //        ;
        //    return View("Search Result", result);
        //}

        [HttpGet]
        public ActionResult Community()
        {
            return View();
        }


        [Authorize(Roles = "Admin")]
        [HttpGet]
        public ActionResult BTSIndex()
        {
            return View();
        }

        [Authorize(Roles ="Admin")]
        public ActionResult StyleGuide()
        {
            return View();
        }

        public ActionResult Explore()
        {
            ViewBag.Tags = db.Tags.ToList();
            return View();
        }
        public ActionResult PageNotFound()
        {
            return View();
        }
    }
}
