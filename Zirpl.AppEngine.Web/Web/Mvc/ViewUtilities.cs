using System;
using System.Text;
using System.Web.Mvc;

namespace Zirpl.AppEngine.Web.Mvc
{
    public static class ViewUtilities
    {
        public static MvcHtmlString Css(this UrlHelper urlHelper, String path, bool removeMinDuringDebug = false)
        {
            if (removeMinDuringDebug)
            {
#if DEBUG
                path = path.Replace(".min.", ".");
#endif
            }

            //<link href="@Url.Content("~/Content/Site.css")" rel="stylesheet" type="text/css" />
            String html = String.Format("<link href=\"{0}\" rel=\"stylesheet\" type=\"text/css\" />", urlHelper.Content(path));
            return new MvcHtmlString(html);
        }
        public static MvcHtmlString Script(this UrlHelper urlHelper, String path, bool removeMinDuringDebug = false)
        {
            if (removeMinDuringDebug)
            {
#if DEBUG
                path = path.Replace(".min.", ".");
#endif
            }

            //<script src="@Url.Content("~/Scripts/jquery-1.5.1.js")" type="text/javascript"></script>
            String html = String.Format("<script src=\"{0}\" type=\"text/javascript\"></script>", urlHelper.Content(path));
            return new MvcHtmlString(html);
        }

        public static String ToErrorString(this ModelStateDictionary model)
        {
            StringBuilder sb = new StringBuilder();
            var enumerator = model.GetEnumerator();
            while (enumerator.MoveNext())
            {
                foreach (var error in enumerator.Current.Value.Errors)
                {
                    if (sb.Length > 0)
                    {
                        sb.AppendLine();
                    }
                    sb.Append(error.ErrorMessage);
                }
            }
            return sb.ToString();
        }

        public static int? GetRouteDataAsInt32IfExists(this Controller controller, String key)
        {
            return controller.ControllerContext.GetRouteDataAsInt32IfExists(key);
        }

        public static int? GetRouteDataAsInt32IfExists(this ActionExecutingContext filterContext, String key)
        {
            return filterContext.Controller.ControllerContext.GetRouteDataAsInt32IfExists(key);
        }

        public static int? GetRouteDataAsInt32IfExists(this ControllerContext controllerContext, String key)
        {
            Object obj = null;
            int? data = null;
            if (controllerContext.RouteData.Values.TryGetValue(key, out obj))
            {
                int dataAsInt = 0;
                if (Int32.TryParse((String)obj, out dataAsInt))
                {
                    data = dataAsInt;
                }
            }
            return data;
        }

        public static byte? GetRouteDataAsByteIfExists(this Controller controller, String key)
        {
            return controller.ControllerContext.GetRouteDataAsByteIfExists(key);
        }

        public static byte? GetRouteDataAsByteIfExists(this ActionExecutingContext filterContext, String key)
        {
            return filterContext.Controller.ControllerContext.GetRouteDataAsByteIfExists(key);
        }

        public static byte? GetRouteDataAsByteIfExists(this ControllerContext controllerContext, String key)
        {
            Object obj = null;
            Byte? data = null;
            if (controllerContext.RouteData.Values.TryGetValue(key, out obj))
            {
                Byte dataAsInt = 0;
                if (Byte.TryParse((String)obj, out dataAsInt))
                {
                    data = dataAsInt;
                }
            }
            return data;
        }

        public static Int64? GetRouteDataAsInt64IfExists(this Controller controller, String key)
        {
            return controller.ControllerContext.GetRouteDataAsInt64IfExists(key);
        }

        public static Int64? GetRouteDataAsInt64IfExists(this ActionExecutingContext filterContext, String key)
        {
            return filterContext.Controller.ControllerContext.GetRouteDataAsInt64IfExists(key);
        }

        public static Int64? GetRouteDataAsInt64IfExists(this ControllerContext controllerContext, String key)
        {
            Object obj = null;
            Int64? data = null;
            if (controllerContext.RouteData.Values.TryGetValue(key, out obj))
            {
                Int64 dataAsInt = 0;
                if (Int64.TryParse((String)obj, out dataAsInt))
                {
                    data = dataAsInt;
                }
            }
            return data;
        }

        public static Int16? GetRouteDataAsIntInt16IfExists(this Controller controller, String key)
        {
            return controller.ControllerContext.GetRouteDataAsIntInt16IfExists(key);
        }

        public static Int16? GetRouteDataAsIntInt16IfExists(this ActionExecutingContext filterContext, String key)
        {
            return filterContext.Controller.ControllerContext.GetRouteDataAsIntInt16IfExists(key);
        }

        public static Int16? GetRouteDataAsIntInt16IfExists(this ControllerContext controllerContext, String key)
        {
            Object obj = null;
            Int16? data = null;
            if (controllerContext.RouteData.Values.TryGetValue(key, out obj))
            {
                Int16 dataAsInt = 0;
                if (Int16.TryParse((String)obj, out dataAsInt))
                {
                    data = dataAsInt;
                }
            }
            return data;
        }

        public static String GetRouteDataAsStringIfExists(this Controller controller, String key)
        {
            return controller.ControllerContext.GetRouteDataAsStringIfExists(key);
        }

        public static String GetRouteDataAsStringIfExists(this ActionExecutingContext filterContext, String key)
        {
            return filterContext.Controller.ControllerContext.GetRouteDataAsStringIfExists(key);
        }

        public static String GetRouteDataAsStringIfExists(this ControllerContext controllerContext, String key)
        {
            Object obj = null;
            String data = null;
            if (controllerContext.RouteData.Values.TryGetValue(key, out obj))
            {
                data = (String) obj;
            }
            return data;
        }
    }
}
