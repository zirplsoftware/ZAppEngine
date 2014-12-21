using System;
using System.Collections.Generic;

namespace Zirpl.Reflection
{
    /// <summary>
    /// Factory class used to create new instances of the <seealso cref="DynamicTypeAccessor"/> class.
    /// </summary>
    /// <remarks>
    /// The factory class will only create a DynamicTypeAccessor once for each type.
    /// If the request for a type is made twice, the cached DynamicTypeAccessor will be returned.
    /// </remarks>
    internal static class TypeAccessorFactory
    {
        internal class TypeAccessorContainer
        {
            internal ReflectionTypeAccessor ReflectionTypeAccessor { get; set; }
#if !SILVERLIGHT && !PORTABLE
            internal DynamicTypeAccessor DynamicTypeAccessor { get; set; }
#endif
        }

        /// <summary>
        /// A collection of TypeAccessors indexed by type
        /// </summary>
        private static Dictionary<Type, TypeAccessorContainer> _typeAccessors;

        private static Dictionary<Type, TypeAccessorContainer> TypeAccessors
        {
            get
            {
                if (_typeAccessors == null)
                {
                    System.Threading.Interlocked.CompareExchange(ref _typeAccessors, new Dictionary<Type, TypeAccessorContainer>(), null);
                }
                return _typeAccessors;
            }
        }

        internal static ITypeAccessor GetTypeAccessor(Type type)
        {
#if !SILVERLIGHT && !PORTABLE
            return GetDynamicTypeAccessor(type);
#else
            return GetReflectionTypeAccessor(type);
#endif
        }

        /// <summary>
        /// Returns a reference to a DynamicTypeAccessor for the requested type represented by <seealso cref="ITypeAccessor"/>.
        /// </summary>
        /// <param name="type">The for to get the DynamicTypeAccessor</param>
        /// <returns><see cref="DynamicTypeAccessor"/></returns>
        internal static ITypeAccessor GetReflectionTypeAccessor(Type type)
        {   
            EnsureContainer(type);
            var container = TypeAccessors[type];
            if (container.ReflectionTypeAccessor == null)
            {
                lock (container)
                {
                    if (container.ReflectionTypeAccessor == null)
                    {
                        container.ReflectionTypeAccessor = new ReflectionTypeAccessor(type, type.BaseType == null ? null : (ReflectionTypeAccessor)GetReflectionTypeAccessor(type.BaseType));
                    }
                }
            }

            return container.ReflectionTypeAccessor;
        }

#if !SILVERLIGHT && !PORTABLE
        /// <summary>
        /// Returns a reference to a DynamicTypeAccessor for the requested type represented by <seealso cref="ITypeAccessor"/>.
        /// </summary>
        /// <param name="type">The for to get the DynamicTypeAccessor</param>
        /// <returns><see cref="DynamicTypeAccessor"/></returns>
        internal static ITypeAccessor GetDynamicTypeAccessor(Type type)
        {
            EnsureContainer(type);
            var container = TypeAccessors[type];
            if (container.DynamicTypeAccessor == null)
            {
                lock (container)
                {
                    if (container.DynamicTypeAccessor == null)
                    {
                        container.DynamicTypeAccessor = new DynamicTypeAccessor(type, type.BaseType == null ? null : (DynamicTypeAccessor)GetDynamicTypeAccessor(type.BaseType));
                    }
                }
            }

            return container.DynamicTypeAccessor;
        }
#endif

        private static void EnsureContainer(Type type)
        {
            if (!TypeAccessors.ContainsKey(type))
            {
                lock (TypeAccessors)
                {
                    if (!TypeAccessors.ContainsKey(type))
                    {
                        TypeAccessors.Add(type, new TypeAccessorContainer());
                    }
                }
            }
        }
    }
}