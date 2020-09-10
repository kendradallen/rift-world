using System.Web.Optimization;

namespace RiftWorld.UI.MVC
{
    public class BundleConfig
    {
        // For more information on bundling, visit http://go.microsoft.com/fwlink/?LinkId=301862
        public static void RegisterBundles(BundleCollection bundles)
        {
            bundles.Add(new ScriptBundle("~/bundles/jquery").Include(
                        "~/Scripts/jquery-{version}.js"));

            bundles.Add(new ScriptBundle("~/bundles/jqueryval").Include(
                        "~/Scripts/jquery.validate*"));

            // Use the development version of Modernizr to develop with and learn from. Then, when you're
            // ready for production, use the build tool at http://modernizr.com to pick only the tests you need.
            bundles.Add(new ScriptBundle("~/bundles/modernizr").Include(
                        "~/Scripts/modernizr-*"));

            bundles.Add(new ScriptBundle("~/bundles/bootstrap").Include(
                      "~/Scripts/bootstrap.bundle.min.js"));

            bundles.Add(new StyleBundle("~/Content/css").Include(
                      "~/Content/bootstrap.min.css",
                      "~/Content/site.css"));

            //bundles.Add(new ScriptBundle("~/bundles/genTemScript.js").Include(
            //          "~/Scripts/js/jquery.magnific-popup.min.js",
            //          "~/Scripts/js/jquery.slicknav.js",
            //          "~/Scripts/js/main.js"));

            //bundles.Add(new ScriptBundle("~/bundles/edit").Include(
            //            "~/Scripts/tinymce/tinymce.min.js",
            //            "~/Scripts/SelectBoxes/slimselect.min.js",
            //            "~/Scripts/MyJS/edit.js"));

            //bundles.Add(new ScriptBundle("~/bundles/assoedit").Include(
            //            "~/Scripts/SelectBoxes/multi.js",
            //            "~/Scripts/MyJS/assoedit.js"));

            //bundles.Add(new ScriptBundle("~/bundles/deets").Include(
            //            "~/Scripts/MyJS/story.js",
            //            "~/Scripts/MyJS/rumor.js",
            //            "~/Scripts/MyJS/secret.js"));

        }
    }
}
