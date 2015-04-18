using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;
using System.Web.Optimization;

namespace BrowseDotNet.Web.App_Start
{
    public class BundleConfig
    {
        public static void RegisterBundles(BundleCollection bundles)
        {
            bundles.Add(new ScriptBundle("~/bundles/jquery").Include(
                        "~/content/scripts/jquery-{version}.js"));

            bundles.Add(new ScriptBundle("~/bundles/jqueryval").Include(
                        "~/content/scripts/jquery.validate.js",
                        "~/content/scripts/jquery.validate.unobtrusive.js",
                        "~/content/scripts/jquery.validate.bootstrap.js"));

            // Use the development version of Modernizr to develop with and learn from. Then, when you're
            // ready for production, use the build tool at http://modernizr.com to pick only the tests you need.
            bundles.Add(new ScriptBundle("~/bundles/modernizr").Include(
                        "~/content/scripts/modernizr-*"));

            bundles.Add(new ScriptBundle("~/bundles/bootstrap").Include(
                      "~/content/scripts/bootstrap.js",
                      "~/content/scripts/respond.js"));

            bundles.Add(new StyleBundle("~/Styles/css").Include(
                      "~/content/styles/bootstrap.css",
                      "~/content/styles/site.css"));
        }
    }
}