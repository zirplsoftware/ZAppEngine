using System;
using System.Reflection;

namespace Zirpl.AppEngine.Ioc
{
    public abstract class DependencyResolverBase<TConcreteResolver, TResolverInterface> : 
        IDependencyResolver 
        where TConcreteResolver : DependencyResolverBase<TConcreteResolver, TResolverInterface>, TResolverInterface
        where TResolverInterface : IDependencyResolver
    {
        private static TResolverInterface INSTANCE;
        private static Object STATIC_SYNC_ROOT;

        protected DependencyResolverBase()
        {
        }

        private static Object StaticSyncRoot
        {
            get
            {
                if (STATIC_SYNC_ROOT == null)
                {
                    System.Threading.Interlocked.CompareExchange(ref STATIC_SYNC_ROOT, new object(), null);
                }
                return STATIC_SYNC_ROOT;
            }
        }

        public static TResolverInterface Instance
        {
            get
            {
                if (INSTANCE == null)
                {
                    lock (StaticSyncRoot)
                    {
                        var instance = (TConcreteResolver)Activator.CreateInstance(typeof(TConcreteResolver), BindingFlags.Instance | BindingFlags.NonPublic, null, null, null);
                        instance.Initialize();
                        INSTANCE = instance;
                    }
                }
                return INSTANCE;
            }
        }

        protected abstract void Initialize();
        public abstract T Resolve<T>();
        public abstract Object Resolve(Type type);
    }
}
