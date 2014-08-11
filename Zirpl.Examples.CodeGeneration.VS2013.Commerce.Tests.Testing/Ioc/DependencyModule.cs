using Autofac;
using Autofac.Integration.Mvc;

namespace Zirpl.Examples.CodeGeneration.VS2013.Commerce.Tests.Ioc
{
    public class DependencyModule : Autofac.Module
    {
        protected override void Load(Autofac.ContainerBuilder builder)
        {
            builder.RegisterType<DataProvider>()
                .AsSelf()
                .PropertiesAutowired()
                .InstancePerHttpRequest();

            base.Load(builder);
        }
    }
}
