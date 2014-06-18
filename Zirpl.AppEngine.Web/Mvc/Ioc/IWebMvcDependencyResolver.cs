using Zirpl.AppEngine.Ioc;

namespace Zirpl.AppEngine.Web.Mvc.Ioc
{
    public interface IWebDependencyResolver :IDependencyResolver
    {
        System.Web.Mvc.IDependencyResolver MvcDependencyResolver { get; }
    }
}
