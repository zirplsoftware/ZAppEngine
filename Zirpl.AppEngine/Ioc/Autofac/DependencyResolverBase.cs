#if !NET35 && !NET35CLIENT
using System;
using System.Linq;
using System.Reflection;
using Autofac;

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
            
#if !SILVERLIGHT
            var assemblies = AppDomain.CurrentDomain.GetAssemblies().Where(o => o.GetName().Name.StartsWith("Zirpl")).ToArray();
#else
            var assemblies = AppDomain.CurrentDomain.GetAssemblies().Where(o => o.FullName.StartsWith("Zirpl")).ToArray();
#endif
            return assemblies;
        }
    }
}
#endif