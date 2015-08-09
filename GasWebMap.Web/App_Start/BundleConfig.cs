using System.Web.Optimization;

namespace GasWebMap.Web
{
    public class BundleConfig
    {
        // 有关 Bundling 的详细信息，请访问 http://go.microsoft.com/fwlink/?LinkId=254725
        public static void RegisterBundles(BundleCollection bundles)
        {
            //easyui
            //bundles.Add(new StyleBundle("~/Content/easyUI/themes/metro/css").Include("~/Content/easyUI/themes/metro/easyui.css"));

            //bundles.Add(new StyleBundle("~/Content/easyUI/css").Include(
            //            "~/Scripts/easyUI/themes/metro/easyui.css",
            //            "~/Scripts/easyUI/themes/icon.css"));

            //bundles.Add(new ScriptBundle("~/Content/easyUI").Include("~/Scripts/easyUI/jquery.easyui.min.js", 
            //            "~/Scripts/easyUI/jquery.min.js",
            //            "~/Scripts/easyUI/locale/easyui-lang-zh_CN.js"));
        }
    }
}