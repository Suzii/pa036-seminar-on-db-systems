using System.Web.Optimization;

namespace RestApi
{
    public class BundleConfig
    {
        public static void RegisterBundles(BundleCollection bundles)
        {
            bundles.Add(new ScriptBundle("~/bundles/modernizr").Include(
                        "~/Scripts/libs/modernizr-*"));

            bundles.Add(new ScriptBundle("~/bundles/jquery").Include(
                        "~/Scripts/libs/jquery/jquery-1.10.2.min.js"));

            bundles.Add(new StyleBundle("~/Content/css").Include(
                      "~/Content/css/bootstrap/bootstrap.min.css",
                      "~/Content/css/bootstrap/bootstrap.min.css.map",
                      "~/Content/css/site.css"));


            // TODO add highcharts and all custom scripts for pages
            bundles.Add(new ScriptBundle("~/scripts/catalogue").Include(
                "~/Scripts/pages/catalogue/test.js"));  
        }
    }
}