using System;
using Zirpl.AppEngine.Web.Mvc.Ioc.Autofac;

namespace Zirpl.AppEngine.Testing
{
    [AttributeUsage(AttributeTargets.Method | AttributeTargets.Class |
                   AttributeTargets.Interface | AttributeTargets.Assembly,
                   AllowMultiple = false)]
    public class SetAspNetMvcDependencyResolverAttribute : Attribute, NUnit.Framework.ITestAction
    {
        public System.Web.Mvc.IDependencyResolver MvcDependencyResolver { get; private set; }

        public SetAspNetMvcDependencyResolverAttribute()
        {
            this.MvcDependencyResolver = IocUtils.DependencyResolver.MvcDependencyResolver;
        }
        public SetAspNetMvcDependencyResolverAttribute(System.Web.Mvc.IDependencyResolver mvcDependencyResolver)
        {
            this.MvcDependencyResolver = mvcDependencyResolver;
        }

        public void AfterTest(NUnit.Framework.TestDetails testDetails)
        {
        }

        public void BeforeTest(NUnit.Framework.TestDetails testDetails)
        {
            System.Web.Mvc.DependencyResolver.SetResolver(this.MvcDependencyResolver);
        }

        public NUnit.Framework.ActionTargets Targets
        {
            get { return NUnit.Framework.ActionTargets.Default; }
        }
    }
}
