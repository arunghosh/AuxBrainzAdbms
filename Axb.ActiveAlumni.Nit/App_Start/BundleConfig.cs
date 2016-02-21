using System.Web;
using System.Web.Optimization;

namespace Axb.ActiveAlumni.Nit
{
    public class BundleConfig
    {
        // For more information on Bundling, visit http://go.microsoft.com/fwlink/?LinkId=254725
        public static void RegisterBundles(BundleCollection bundles)
        {
            bundles.Add(new ScriptBundle("~/bundles/jqueryAndAxb")
                        .Include(
                        "~/Scripts/jquery.slimscroll.js",
                        "~/Scripts/jquery-{version}.js",
                        "~/Scripts/jquery-ui-{version}.js",
                        "~/Scripts/jquery.validate.min.js",
                        "~/Scripts/_ajax-pop.js",
                        "~/Scripts/_common.js",
                        "~/Scripts/_time.js"));

            // Use the development version of Modernizr to develop with and learn from. Then, when you're
            // ready for production, use the build tool at http://modernizr.com to pick only the tests you need.
            bundles.Add(new ScriptBundle("~/bundles/modernizr").Include(
                        "~/Scripts/modernizr-*"));


            bundles.Add(new StyleBundle("~/Content/guestcss")
                    .Include(
                        "~/Content/themes/base/jquery.ui.accordion.css",
                        "~/Content/themes/base/jquery.ui.all.css",
                        "~/Content/themes/base/jquery.ui.base.css",
                        "~/Content/themes/base/jquery.ui.button.css",
                        "~/Content/themes/base/jquery.ui.core.css",
                        "~/Content/themes/base/jquery.ui.datepicker.css",
                        "~/Content/themes/base/jquery.ui.dialog.css",
                        "~/Content/themes/base/jquery.ui.menu.css",
                        "~/Content/themes/base/jquery.ui.progressbar.css",
                        "~/Content/themes/base/jquery.ui.resizable.css",
                        "~/Content/themes/base/jquery.ui.selectable.css",
                        "~/Content/themes/base/jquery.ui.slider.css",
                        "~/Content/themes/base/jquery.ui.spinner.css",
                        "~/Content/themes/base/jquery.ui.tabs.css",
                        "~/Content/themes/base/jquery.ui.theme.css",
                        "~/Content/themes/base/jquery.ui.autocomplete.css",
                        "~/Content/themes/base/jquery.ui.css",
                        "~/Content/themes/base/jquery.ui.tooltip.css")
                .Include("~/Content/bootstrap.css")
                .Include("~/Content/site.css")
                .Include("~/Content/guest.css"));

            bundles.Add(new StyleBundle("~/Content/css")
                    .Include(
                        "~/Content/themes/base/jquery.ui.accordion.css",
                        "~/Content/themes/base/jquery.ui.all.css",
                        "~/Content/themes/base/jquery.ui.base.css",
                        "~/Content/themes/base/jquery.ui.button.css",
                        "~/Content/themes/base/jquery.ui.core.css",
                        "~/Content/themes/base/jquery.ui.datepicker.css",
                        "~/Content/themes/base/jquery.ui.dialog.css",
                        "~/Content/themes/base/jquery.ui.progressbar.css",
                        "~/Content/themes/base/jquery.ui.resizable.css",
                        "~/Content/themes/base/jquery.ui.selectable.css",
                        "~/Content/themes/base/jquery.ui.slider.css",
                        "~/Content/themes/base/jquery.ui.spinner.css",
                        "~/Content/themes/base/jquery.ui.tabs.css",
                        "~/Content/themes/base/jquery.ui.theme.css",
                        "~/Content/themes/base/jquery.ui.menu.css",
                        "~/Content/themes/base/jquery.ui.autocomplete.css",
                        "~/Content/themes/base/jquery.ui.css",
                        "~/Content/themes/base/jquery.ui.tooltip.css")
                .Include("~/Content/bootstrap.css")
                .Include("~/Content/site.css"));

            bundles.Add(new StyleBundle("~/Content/themes/base/css")
                .Include("~/Content/themes/base/*.css"));
        }
    }
}