#if !NET35 && !NET35CLIENT
using Autofac;

namespace Zirpl.AppEngine.Ioc.Autofac
{
    public interface IAutofacDependencyResolver :IDependencyResolver
    {
        IContainer Container { get; }
    }
}
#endif
