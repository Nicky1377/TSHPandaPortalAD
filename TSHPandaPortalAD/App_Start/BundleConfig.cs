using System.Web;
using System.Web.Optimization;

namespace TSHPandaPortalAD
{
    public class BundleConfig
    {
        // For more information on bundling, visit https://go.microsoft.com/fwlink/?LinkId=301862
        public static void RegisterBundles(BundleCollection bundles)
        {
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

            bundles.Add(new ScriptBundle("~/bundles/PluginBundle").Include(
                "~/assets/plugins/global/plugins.bundle.js"));

            bundles.Add(new ScriptBundle("~/bundles/ScriptBundle").Include(
                "~/assets/js/scripts.bundle.js"));

            bundles.Add(new ScriptBundle("~/bundles/fullcalendar").Include(
                "~/assets/plugins/custom/fullcalendar/fullcalendar.bundle.js"));

            bundles.Add(new ScriptBundle("~/bundles/dashboard").Include(
                "~/assets/js/pages/dashboard.js"));

            bundles.Add(new ScriptBundle("~/bundles/MasterScript").Include(
                "~/CustomScript/Master.js"));

            bundles.Add(new StyleBundle("~/Content/css").Include(
                      "~/Content/bootstrap.css",
                      "~/Content/site.css"));

            bundles.Add(new StyleBundle("~/fullcalendar/rtl").Include(
                "~/assets/plugins/custom/fullcalendar/fullcalendar.bundle.rtl.css"));

            bundles.Add(new StyleBundle("~/plugins/rtl").Include(
                "~/assets/plugins/global/plugins.bundle.rtl.css"));

            bundles.Add(new StyleBundle("~/style/rtl").Include(
                "~/assets/css/style.bundle.rtl.css"));

            bundles.Add(new StyleBundle("~/base/light/rtl").Include(
                "~/assets/css/skins/header/base/light.rtl.css"));

            bundles.Add(new StyleBundle("~/menu/light/rtl").Include(
                "~/assets/css/skins/header/menu/light.rtl.css"));

            bundles.Add(new StyleBundle("~/brand/dark/rtl").Include(
                "~/assets/css/skins/brand/dark.rtl.css"));

            bundles.Add(new StyleBundle("~/aside/dark/rtl").Include(
                "~/assets/css/skins/aside/dark.rtl.css"));

            bundles.Add(new StyleBundle("~/Custom").Include(
                "~/assets/css/Custome.css"));
        }
    }
}
