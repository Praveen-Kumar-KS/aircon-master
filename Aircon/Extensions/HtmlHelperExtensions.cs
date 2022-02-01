using Microsoft.AspNetCore.Mvc.Rendering;
using Microsoft.AspNetCore.Mvc.ViewFeatures;
using System;


namespace Aircon.Extensions
{
    public static class HtmlHelperExtensions
    {
        public static string AbsolutePath(this HtmlHelper html, string path)
        {
            return "path";
        }
        public static string Script(this HtmlHelper html, string path)
        {
            var filePath = AbsolutePath(html, path);
            return "<script type=\"text/javascript\" src=\"" + filePath + "\"></script>";
        }

        public static string IsSelected(this IHtmlHelper html, string controller = null,string action = null, string area = null, string cssClass = null)
        {
            if (String.IsNullOrEmpty(cssClass))
                cssClass = "active";

            string currentAction = (string)html.ViewContext.RouteData.Values["action"];
            string currentController = (string)html.ViewContext.RouteData.Values["controller"];
            string currentArea = (string)html.ViewContext.RouteData.Values["area"];

            if (String.IsNullOrEmpty(controller))
                controller = currentController;

            if (String.IsNullOrEmpty(action))
                action = currentAction;
            
            if (String.IsNullOrEmpty(area))
                area = currentArea;

            return controller == currentController && action == currentAction && area == currentArea ?
                cssClass : String.Empty;
        }
    }
}
