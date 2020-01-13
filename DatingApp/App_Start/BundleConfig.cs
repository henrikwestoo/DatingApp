using System.Web;
using System.Web.Optimization;

namespace DatingApp
{
    public class BundleConfig
    {
        // For more information on bundling, visit https://go.microsoft.com/fwlink/?LinkId=301862
        public static void RegisterBundles(BundleCollection bundles)
        {
            BundleTable.EnableOptimizations = true;

            bundles.Add(new ScriptBundle("~/bundles/jquery").Include(
                        "~/Scripts/jquery-{version}.js"));

            bundles.Add(new ScriptBundle("~/bundles/jqueryval").Include(
                        "~/Scripts/jquery.validate*"));

            // Use the development version of Modernizr to develop with and learn from. Then, when you're
            // ready for production, use the build tool at https://modernizr.com to pick only the tests you need.
            bundles.Add(new ScriptBundle("~/bundles/modernizr").Include(
                        "~/Scripts/modernizr-*"));

            bundles.Add(new ScriptBundle("~/bundles/bootstrap").Include(
                      "~/Scripts/bootstrap.js"));

            bundles.Add(new StyleBundle("~/Content/css").Include(
                      "~/Content/bootstrap.css",
                      "~/Content/Custom.css",
                      "~/Content/site.css"));
            bundles.Add(new ScriptBundle("~/bundles/custom").Include(
                       "~/Scripts/Custom/updateRequests.js"));
            bundles.Add(new ScriptBundle("~/bundles/posts").Include(
                        "~/Scripts/Custom/posts.js"));
            bundles.Add(new ScriptBundle("~/bundles/visitors").Include(
                        "~/Scripts/Custom/visitors.js"));
            bundles.Add(new ScriptBundle("~/bundles/contacts").Include(
                      "~/Scripts/Custom/contacts.js"));
            bundles.Add(new ScriptBundle("~/bundles/profile").Include(
                      "~/Scripts/Custom/profile.js"));
            bundles.Add(new ScriptBundle("~/bundles/profiles").Include(
                      "~/Scripts/Custom/profiles.js"));
        }
    }
}
