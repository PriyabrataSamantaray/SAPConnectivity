using System.Web;
using System.Web.Optimization;

namespace PRMLoader
{
    public class BundleConfig
    {
        // For more information on Bundling, visit http://go.microsoft.com/fwlink/?LinkId=254725
        public static void RegisterBundles(BundleCollection bundles)
        {
            bundles.Add(new ScriptBundle("~/bundles/jquery").Include(
                        "~/Scripts/jquery-{version}.js"));

            bundles.Add(new ScriptBundle("~/bundles/angular").Include(
            "~/Scripts/angular/angular.js",
                "~/Scripts/angular/angular-route.js",
                "~/Scripts/angular/angular-touch.js",
                "~/Scripts/angular/angular-animate.js",
                "~/Scripts/angular/angular-cookies.js",
                "~/Scripts/angular/angular-block-ui.js",
                "~/Scripts/angular/angular-loader.js",
                "~/Scripts/angular/angular-validator.js",
                "~/Scripts/angular/angular-ui-router.js",
                "~/Scripts/angular/angular-sanitize.js",
                "~/Scripts/angular-ui/ui-bootstrap.js",
                "~/Scripts/angular-ui/ui-bootstrap-tpls.js",
                "~/Scripts/common/app.js"
                ));

            // Use the development version of Modernizr to develop with and learn from. Then, when you're
            // ready for production, use the build tool at http://modernizr.com to pick only the tests you need.
            bundles.Add(new ScriptBundle("~/bundles/modernizr").Include(
                        "~/Scripts/modernizr-*"));

            bundles.Add(new ScriptBundle("~/bundles/bootstrap").Include(
                      "~/Scripts/bootstrap.js",
                      "~/Scripts/respond.js"));

            bundles.Add(new StyleBundle("~/Content/css").Include(
                      "~/Content/bootstrap.css",
                      "~/Content/site.css"));
        }
    }
}