using System.Web;
using System.Web.Optimization;

namespace WebVansSite
{
    public class BundleConfig
    {
        // For more information on bundling, visit http://go.microsoft.com/fwlink/?LinkId=301862
        public static void RegisterBundles(BundleCollection bundles)
        {
            bundles.Add(new ScriptBundle("~/bundles/jquery").Include(
                        "~/Scripts/jquery-{version}.js"));

            bundles.Add(new ScriptBundle("~/bundles/jqueryval").Include(
                        "~/Scripts/jquery.validate*",
                        "~/Scripts/customvalidations.js"));

            // Use the development version of Modernizr to develop with and learn from. Then, when you're
            // ready for production, use the build tool at http://modernizr.com to pick only the tests you need.
            bundles.Add(new ScriptBundle("~/bundles/modernizr").Include(
                        "~/Scripts/modernizr-*"));

            bundles.Add(new ScriptBundle("~/bundles/bootstrap").Include(
                      "~/Scripts/bootstrap.js",
                      "~/Scripts/respond.js"));

            bundles.Add(new StyleBundle("~/Content/css").Include(
                      "~/bootstrap/css/bootstrap.css",
                      "~/fonts/font-awesome/css/font-awesome.css",
                      "~/fonts/fontello/css/fontello.css",
                      "~/plugins/magnific-popup/magnific-popup.css",
                      "~/css/animations.css",
                      "~/plugins/owl-carousel/owl.carousel.css",
                      "~/css/style.css",
                      "~/css/skins/blue.css",
                      "~/css/custom.css"));

            bundles.Add(new ScriptBundle("~/bundles/jquerymask").Include(
                "~/Scripts/jquery.maskedinput.min.js",
                "~/Scripts/maskedinput-binder.js"));

            bundles.Add(new ScriptBundle("~/bundles/sitejs").Include(
                "~/plugins/isotope/isotope.pkgd.min.js",
                "~/plugins/owl-carousel/owl.carousel.js",
                "~/plugins/magnific-popup/jquery.magnific-popup.min.js",
                "~/plugins/jquery.appear.js",
                "~/plugins/jquery.sharrre.js",
                "~/plugins/jquery.countTo.js",
                "~/plugins/jquery.parallax-1.1.3.js",
                "~/plugins/jquery.validate.js",
                "~/js/template.js"
            ));
        }
    }
}
