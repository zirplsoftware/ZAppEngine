using System;
using System.Web;

namespace Zirpl.AppEngine.Web
{
    public class WebContextDataProvider
    {
        protected T GetSessionData<T>(string key)
        {
            return (T)HttpContext.Current.Session[key];
        }

        protected void SetSessionData<T>(string key, T data)
        {
            HttpContext.Current.Session[key] = data;
        }

        protected T GetApplicationData<T>(string key)
        {
            return (T)HttpContext.Current.Application[key];
        }

        protected void SetApplicationData<T>(string key, Object data)
        {
            HttpContext.Current.Application[key] = data;
        }

        protected String GetQueryStringData(string key)
        {
            return HttpContext.Current.Request.QueryString[key];
        }
    }
}
