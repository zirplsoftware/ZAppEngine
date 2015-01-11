using Autofac;

namespace Zirpl.AppEngine.Ioc.Autofac
{
    public interface IAutofacDependencyResolver :IDependencyResolver
    {
        IContainer Container { get; }
    }
}
