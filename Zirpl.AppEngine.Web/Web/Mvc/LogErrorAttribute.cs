using System;
using System.Web.Mvc;
using Zirpl.Logging;

namespace Zirpl.AppEngine.Web.Mvc
{
    public class LogErrorAttribute : FilterAttribute, IExceptionFilter
    {
        public void OnException(ExceptionContext filterContext)
        {
            try
            {
                if (filterContext.Exception != null)
                {
                    var controller = (String) filterContext.RouteData.Values["controller"];
                    var action = (String) filterContext.RouteData.Values["action"];
                    var e = filterContext.Exception;

                    if (filterContext.ExceptionHandled)
                    {
                        this.GetLog().WarnFormat(
                            e,
                            "Unexpected error calling action {1} on controller {0}",
                            controller,
                            action);
                    }
                    else
                    {
                        this.GetLog().ErrorFormat(
                            e,
                            "Unexpected error calling action {1} on controller {0}",
                            controller,
                            action);
                    }
                }
            }
            catch (Exception e)
            {
                // NOTHING WE CAN DO ABOUT THIS :-\
                this.GetLog().TryError(e, "An error occurred trying to write Action Exception to the log");
            }
        }
    }
}