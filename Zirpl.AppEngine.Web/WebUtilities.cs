using System;
using System.ServiceModel;
using System.Web;

namespace Zirpl.AppEngine.Web
{
    public static class WebUtilities
    {
        public static string ToAbsoluteUrl(this string relativeUrl)
        {
            if (string.IsNullOrEmpty(relativeUrl))
                return relativeUrl;

            if (HttpContext.Current == null
                && (OperationContext.Current == null
                    || OperationContext.Current.EndpointDispatcher == null
                    || OperationContext.Current.EndpointDispatcher.EndpointAddress == null
                    || OperationContext.Current.EndpointDispatcher.EndpointAddress.Uri == null))
            {
                return relativeUrl;
            }

            if (relativeUrl.StartsWith("/"))
            {
                relativeUrl = relativeUrl.Insert(0, "~");
            }
            if (!relativeUrl.StartsWith("~/"))
            {
                relativeUrl = relativeUrl.Insert(0, "~/");
            }

            var url = HttpContext.Current == null 
                ? OperationContext.Current.EndpointDispatcher.EndpointAddress.Uri 
                : HttpContext.Current.Request.Url;
            var port = url.Port != 80 ? (":" + url.Port) : String.Empty;

            return String.Format("{0}://{1}{2}{3}",
                   url.Scheme, url.Host, port, VirtualPathUtility.ToAbsolute(relativeUrl));
        }

        public static String GetUserIpAddress(this HttpRequest request)
        {
            string ipAddress = null;
            if (request.ServerVariables["HTTP_X_FORWARDED_FOR"] != null)
            {
                ipAddress = request.ServerVariables["HTTP_X_FORWARDED_FOR"];
            }
            else if (!String.IsNullOrEmpty(request.UserHostAddress))
            {
                ipAddress = request.UserHostAddress;
            }
            return ipAddress;
        }
    }
}
