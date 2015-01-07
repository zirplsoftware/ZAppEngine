using System;

namespace Zirpl.AppEngine.Ioc
{
    public interface IDependencyResolver
    {
        T Resolve<T>();
        Object Resolve(Type type);
    }
}
