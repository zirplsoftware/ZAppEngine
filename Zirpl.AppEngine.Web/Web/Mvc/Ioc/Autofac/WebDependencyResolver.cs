using System;
using Zirpl.AppEngine.Ioc.Autofac;

namespace Zirpl.AppEngine.Web.Mvc.Ioc.Autofac
{
    public sealed class WebDependencyResolver : DependencyResolverBase<WebDependencyResolver, IAutofacWebMvcDependencyResolver>, IAutofacWebMvcDependencyResolver
    {
        private WebDependencyResolver()
        {
        }

        public System.Web.Mvc.IDependencyResolver MvcDependencyResolver
        {
            get { return new global::Autofac.Integration.Mvc.AutofacDependencyResolver(this.Container); }
        }

        public override T Resolve<T>()
        {
            return (T)System.Web.Mvc.DependencyResolver.Current.GetService(typeof(T));
        }

        public override Object Resolve(Type type)
        {
            return System.Web.Mvc.DependencyResolver.Current.GetService(type);
        }
    }
}
