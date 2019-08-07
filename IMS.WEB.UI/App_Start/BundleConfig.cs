using System.Web;
using System.Web.Optimization;

namespace SmartFleetManagementSystem
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

            bundles.Add(new StyleBundle("~/Content/css").Include(
                      "~/Content/bootstrap.css",
                      "~/Content/site.css"));
            #region Layout
            bundles.Add(new StyleBundle("~/styles/layout").Include(
                       "~/Content/Css/Shared/Header.css",
                       "~/Content/Bootstrap/css/bootstrap.min.css",
                       "~/Content/FontAwesome/css/font-awesome.css",
                       "~/Content/Jquery-ui/jquery-ui.css",
                       "~/Content/MagnificPopUp/magnific-popup.css",
                       "~/Content/perfect-scrollbar/css/perfect-scrollbar.css",
                       "~/Content/Css/Shared/Layout.css"
                       ));
            bundles.Add(new ScriptBundle("~/scripts/layout").Include(
                       "~/Content/Jquery-ui/jquery.js",
                       "~/Content/Js/Layout/utils.js",
                       "~/Content/Bootstrap/js/bootstrap.min.js",
                       //"~/Content/Jquery-ui/jquery-ui.js",
                       "~/Content/JQueryFileUpload/jquery-ui-1.9.2.min.js",
                       "~/Content/MagnificPopUp/jquery.magnific-popup.js",
                       "~/Content/perfect-scrollbar/js/perfect-scrollbar.jquery.js",
                       "~/Content/Js/Site/Validation.js",
                       "~/Content/JQuery/jquery.cookie.js"
                       ));
            #endregion
            #region PrivateLayout
            bundles.Add(new StyleBundle("~/styles/privatelayout").Include(
                      "~/Content/Css/Layout/startmin.css",
                      "~/Content/Css/Layout/Loader.css",
                      "~/Content/Css/Layout/PrivateLayout.css",
                      //"~/Content/Select2/select2.css",
                      "~/Content/datatable/dataTables.bootstrap.css",
                      "~/Content/Css/PackageSettings/bootstrap-toggle.min.css",
                      "~/Content/PikDay/css/pikaday.css"
                      ));
            bundles.Add(new ScriptBundle("~/scripts/privatelayout").Include(
                "~/Scripts/jquery-{version}.js",
                       "~/Content/Js/Layout/metisMenu.min.js",
                       "~/Content/JQueryFileUpload/jquery.ui.widget.js",
                       "~/Content/JQueryFileUpload/jquery.iframe-transport.js",
                       "~/Content/JQueryFileUpload/jquery.fileupload.js",
                       "~/Content/JQueryFileUpload/jquery.fileupload-process.js",
                       "~/Content/JQueryFileUpload/jquery.fileupload-ui.js",
                       "~/Content/Js/Layout/PrivateLayout.js",
                       //"~/Content/Select2/Select2.min.js",
                       "~/Content/datatable/jquery.dataTables.min.js",
                       "~/Content/datatable/dataTables.bootstrap.js",
                       "~/Content/Js/PackageSetup/bootstrap-toggle.min.js",
                       "~/Content/PikDay/js/moment.js",
                       "~/Content/PikDay/js/pikaday.js",
                       "~/Content/Js/Dashboard/SessionChecker.js"
                       ));
            #endregion
            #region Modals
            bundles.Add(new StyleBundle("~/styles/Modals").Include(

                    "~/Content/Css/Modals/Modals.css",
                    "~/Content/FontAwesome/css/font-awesome.css",
                    "~/Content/Css/Shared/RightModal.css"
                    ));
            bundles.Add(new ScriptBundle("~/scripts/Modals").Include(

                    "~/Content/Js/Modals/Modals.js"
                    ));
            #endregion

            #region AddFile
            bundles.Add(new StyleBundle("~/styles/AddFile").Include(

                    "~/Content/Css/AddFile/AddFile.css"
                    ));
            bundles.Add(new ScriptBundle("~/scripts/AddFile").Include(
                    "~/Content/Js/AddFile/DriverDocuments.js"
                    ));

            bundles.Add(new ScriptBundle("~/scripts/AddFileFuel").Include(
                    "~/Content/Js/AddFile/FuelDocuments.js"
                    ));
            #endregion

            #region Dashboard 
            bundles.Add(new StyleBundle("~/styles/dashboard").Include(
                      "~/Content/Css/Dashboard/timeline.css",
                      "~/Content/Css/Dashboard/morris.css",
                       "~/Content/Css/Dashboard/Dashboard.css"
                      ));
            bundles.Add(new ScriptBundle("~/scripts/dashboard").Include(
                      "~/Content/Js/Dashboard/raphael.min.js",
                      "~/Content/Js/Dashboard/morris.min.js",
                      "~/Content/Js/Dashboard/morris-data.js",
                      "~/Content/Js/Dashboard/dashboard.js"
                       ));

            #endregion
        }
    }
}
