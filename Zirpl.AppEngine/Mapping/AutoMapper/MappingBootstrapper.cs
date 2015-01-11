#if !PORTABLE
using System;
using System.Linq;
using System.Reflection;
using Zirpl.AppEngine.Ioc;

namespace Zirpl.AppEngine.Mapping.AutoMapper
{
    public sealed class MappingBootstrapper
    {
        public static void Initialize(IDependencyResolver dependencyResolver, Func<Assembly, bool> predicateForAssembliesToCheck = null)
        {
            var assemblies = AppDomain.CurrentDomain.GetAssemblies().ToList();
            if (predicateForAssembliesToCheck != null)
            {
                assemblies = assemblies.Where(predicateForAssembliesToCheck).ToList();
            }

            var moduleType = typeof (MappingModuleBase);
            foreach (var assembly in assemblies)
            {
                foreach (var type in assembly.GetTypes())
                {
                    if (type.IsClass
                        && !type.IsAbstract
                        && type.IsSubclassOf(moduleType))
                    {
                        var instance = (MappingModuleBase)Activator.CreateInstance(type);
                        instance.CreateMaps(dependencyResolver);
                    }
                }
            }

        }
    }
}

#endif