namespace TravelShare.Web
{
    using System.Web.Optimization;

    public static class BundleConfig
    {
        // For more information on bundling, visit http://go.microsoft.com/fwlink/?LinkId=301862
        public static void RegisterBundles(BundleCollection bundles)
        {
            RegisterScripts(bundles);
            RegisterStyles(bundles);
        }

        private static void RegisterScripts(BundleCollection bundles)
        {
            bundles.Add(new ScriptBundle("~/bundles/jquery").Include("~/Scripts/jquery-{version}.js"));
            bundles.Add(new ScriptBundle("~/bundles/jqueryval").Include("~/Scripts/jquery.validate*"));
            bundles.Add(new ScriptBundle("~/bundles/bootstrap").Include("~/Scripts/bootstrap.js"));
            bundles.Add(new ScriptBundle("~/bundles/maps").Include("~/Scripts/maps.js"));
            bundles.Add(new ScriptBundle("~/bundles/jqueryui").Include("~/Scripts/jquery-ui.js"));
            bundles.Add(new ScriptBundle("~/bundles/jqueryajax").Include(
                      "~/Scripts/jquery.unobtrusive*"));
        }

        private static void RegisterStyles(BundleCollection bundles)
        {
            bundles.Add(new StyleBundle("~/Content/css").Include("~/Content/bootstrap.css", "~/Content/site.css"));
            bundles.Add(new StyleBundle("~/Content/jqueryui").Include("~/Content/jquery-ui.css", "~/Content/jquery-ui.structure.css", "~/Content/jquery-ui.theme.css"));
            bundles.Add(new StyleBundle("~/Content/chat").Include("~/Content/chat.css"));
            bundles.Add(new StyleBundle("~/Content/news").Include("~/Content/news.css"));
            bundles.Add(new StyleBundle("~/Content/trip").Include("~/Content/trip.css", "~/Content/googlemap.css"));
            bundles.Add(new StyleBundle("~/Content/map").Include("~/Content/googlemap.css"));
            bundles.Add(new StyleBundle("~/Content/administration").Include("~/Content/administration.css"));
            bundles.Add(new StyleBundle("~/Content/inputs").Include("~/Content/inputs.css"));
        }
    }
}
