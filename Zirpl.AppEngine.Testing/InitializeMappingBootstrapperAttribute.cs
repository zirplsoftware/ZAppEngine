using System;
using System.Collections.Generic;
using System.Linq;
using System.Reflection;
using System.Text;
using System.Threading.Tasks;
using Zirpl.AppEngine.Logging;
using Zirpl.AppEngine.Mapping.AutoMapper;
using Zirpl.AppEngine.Web.Mvc.Ioc.Autofac;
using Zirpl.Logging;

namespace Zirpl.AppEngine.Testing
{
    [AttributeUsage(AttributeTargets.Method | AttributeTargets.Class |
                       AttributeTargets.Interface | AttributeTargets.Assembly,
                       AllowMultiple = false)]
    public class InitializeMappingBootstrapperAttribute : Attribute, NUnit.Framework.ITestAction
    {
        public void AfterTest(NUnit.Framework.TestDetails testDetails)
        {
        }

        public void BeforeTest(NUnit.Framework.TestDetails testDetails)
        {
            try
            {
                MappingBootstrapper.Initialize(IocUtils.DependencyResolver);
            }
            catch (ReflectionTypeLoadException e)
            {
                foreach (var ex in e.LoaderExceptions)
                {
                    this.GetLog().Error(ex);
                }
                throw e;
            }
        }

        public NUnit.Framework.ActionTargets Targets
        {
            get { return NUnit.Framework.ActionTargets.Suite; }
        }
    }
}
