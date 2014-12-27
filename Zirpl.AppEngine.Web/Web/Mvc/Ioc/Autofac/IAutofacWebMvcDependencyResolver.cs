using System.Web.Mvc;
using Zirpl.AppEngine.Ioc;
using Zirpl.AppEngine.Ioc.Autofac;

namespace Zirpl.AppEngine.Web.Mvc.Ioc.Autofac
{
    public interface IAutofacWebMvcDependencyResolver : IAutofacDependencyResolver, IWebDependencyResolver
    {
    }
}
