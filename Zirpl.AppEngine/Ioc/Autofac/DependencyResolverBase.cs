#if !NET35 && !NET35CLIENT
using Autofac;
using System;
using System.Linq;
using System.Reflection;

namespace Zirpl.AppEngine.Ioc.Autofac
{
    public abstract class DependencyResolverBase<TConcreteResolver, TResolverInterface> :
        Ioc.DependencyResolverBase<TConcreteResolver, TResolverInterface>, 
        IAutofacDependencyResolver
        where TConcreteResolver : Ioc.DependencyResolverBase<TConcreteResolver, TResolverInterface>, TResolverInterface
        where TResolverInterface : IAutofacDependencyResolver
    {
        public IContainer Container { get; private set; }

        protected DependencyResolverBase()
        {
        }

        protected override void Initialize()
        {
            var builder = new ContainerBuilder();

            builder.RegisterAssemblyModules(this.GetModuleAssemblies());
            
            this.Container = builder.Build();
        }

        protected virtual Assembly[] GetModuleAssemblies()
        {
            var assemblies = AppDomain.CurrentDomain.GetAssemblies().Where(o => o.GetName().Name.StartsWith("Zirpl")).ToArray();
            return assemblies;
        }
    }
}
#endif