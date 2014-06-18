using System.Web.Mvc;

namespace Zirpl.AppEngine.Web.Mvc
{
    public class AjaxOnlyAttribute : ActionFilterAttribute
    {
        public override void OnActionExecuting(ActionExecutingContext filterContext)
        {
            if (!filterContext.HttpContext.Request.IsAjaxRequest())
            {
                filterContext.Result = new HttpNotFoundResult("Non-Ajax method not found");
            }
        }
    }
}