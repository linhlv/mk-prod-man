using System.Web;
using System.Web.Optimization;

namespace Kenrapid.CRM.Web
{
    public class BundleConfig
    {
        // For more information on bundling, visit http://go.microsoft.com/fwlink/?LinkId=301862
        public static void RegisterBundles(BundleCollection bundles)
        {

            bundles.Add(new StyleBundle("~/Content/all.css")
                .Include("~/Content/bootstrap.css")
                //.Include("~/Content/bootswatch/yeti/bootstrap.css")
                .Include("~/css/font-awesome.css")
                .Include("~/Content/nine/nivo-lightbox.css")
                .Include("~/Content/nine/nivo-lightbox-theme/default/default.css")
                .Include("~/Content/nine/animate.css")
                .Include("~/Content/layout.css")
                .Include("~/Content/ui-grid.css")
                .Include("~/Content/checkbox.css")
                .Include("~/Content/site.css")
                //.Include("~/Content/dp/daterangepicker.min.css")
                .Include("~/Content/dp/bootstrap-datetimepicker.css"));

            bundles.Add(new StyleBundle("~/Content/login.css")
               .Include("~/Content/bootstrap.css")
               .Include("~/css/font-awesome.css")
               .Include("~/Content/uniform/css/uniform.default.min.css")
               .Include("~/Content/animatecss/animate.min.css")
               .Include("~/Content/cloudadmin/cloud-admin.css")
               .Include("~/Content/site.css"));

            bundles.Add(new ScriptBundle("~/jq.js").Include("~/Scripts/jquery-{version}.js"));

            bundles.Add(new ScriptBundle("~/bundles/all.js")
               .Include("~/Scripts/jquery-{version}.js")
               .Include("~/Scripts/bootstrap.min.js")
               .Include("~/Content/dp/moment.js")
               //.Include("~/Content/dp/daterangepicker.min.js")
               .Include("~/Content/dp/bootstrap-datetimepicker.js")
               .Include("~/Scripts/jquery.easing.min.js")
               .Include("~/Scripts/jssor.slider.mini.js")
               .Include("~/Scripts/classie.js")
               .Include("~/Scripts/gnmenu.js")
               .Include("~/Scripts/jquery.scrollTo.js")
               .Include("~/Scripts/nivo-lightbox.min.js")
               .Include("~/Scripts/stellar.js")
               .Include("~/Scripts/ninez.js")
               .Include("~/Scripts/respond.js")
               .Include("~/Scripts/rowlink.js")
               .Include("~/Scripts/underscore.js")
               .Include("~/Scripts/angular.js")
               .Include("~/Scripts/angular-animate.js")
               .Include("~/Scripts/angular-ui/ui-bootstrap.js")
               .Include("~/Scripts/angular-ui/ui-bootstrap-tpls.js")
               .Include("~/Scripts/angular-ui-router.js")
               //.Include("~/Content/dp/angular-daterangepicker.min.js")
               .Include("~/Scripts/str/string.js")
               .Include("~/Scripts/ui-grid.js"));

            bundles.Add(new ScriptBundle("~/bundles/app.js")
                .Include("~/app/app.js")
                .IncludeDirectory("~/app/common/directives", "*.js")
                .IncludeDirectory("~/app/common/utilities", "*.js")
                .IncludeDirectory("~/app/common/services", "*.js")
                .IncludeDirectory("~/app/home", "*.js")
                .IncludeDirectory("~/app/failtracker", "*.js")
                .IncludeDirectory("~/app/profile", "*.js")
                .IncludeDirectory("~/app/order", "*.js")
                .IncludeDirectory("~/app/product", "*.js")
                .IncludeDirectory("~/app/color", "*.js")
                .IncludeDirectory("~/app/quotation", "*.js")
                .IncludeDirectory("~/app/customer", "*.js")
                .IncludeDirectory("~/app/opportunity", "*.js")
                .IncludeDirectory("~/app/risk", "*.js")
                .IncludeDirectory("~/app/vendor", "*.js")
                .IncludeDirectory("~/app/category", "*.js")
                .IncludeDirectory("~/app/material", "*.js")
                .IncludeDirectory("~/app/user", "*.js")
            );

            bundles.Add(new ScriptBundle("~/bundles/login.js")
               .Include("~/Scripts/jquery-{version}.js")
               .Include("~/Scripts/jquery.cookie.js")
               .Include("~/Scripts/jquery-ui-cu/jquery-ui-1.10.3.custom.min.js")
               .Include("~/Scripts/bootstrap.min.js")
               .Include("~/Scripts/uniform/jquery.uniform.min.js")
               .Include("~/Scripts/uniform.js")
           );

           BundleTable.EnableOptimizations = false;

        }
    }
}
