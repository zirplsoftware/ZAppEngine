using Zirpl.AppEngine.Ioc;
using Zirpl.AppEngine.Ioc.Autofac;

namespace Zirpl.AppEngine.Web.Mvc.Ioc.Autofac
{
    public interface IAutofacWebMvcDependencyResolver : IDependencyResolver, IAutofacDependencyResolver, IWebDependencyResolver
    {
    }
}
