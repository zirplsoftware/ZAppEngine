#if !SILVERLIGHT && !PORTABLE
using System;
using System.Collections.Generic;

namespace Zirpl.Reflection
{
    /// <summary>
    /// Factory class used to create new instances of the <seealso cref="DynamicAccessor"/> class.
    /// </summary>
    /// <remarks>
    /// The factory class will only create a DynamicAccessor once for each type.
    /// If the request for a type is made twice, the cached DynamicAccessor will be returned.
    /// </remarks>
    public static class DynamicAccessorFactory
    {
        /// <summary>
        /// A collection of DynamicAccessors indexed by type
        /// </summary>
        private readonly static Dictionary<Type, IDynamicAccessor> _DynamicAccessors = new Dictionary<Type, IDynamicAccessor>();

        /// <summary>
        /// Returns a reference to a DynamicAccessor for the requested type represented by <seealso cref="IDynamicAccessor"/>.
        /// </summary>
        /// <param name="type">The for to get the DynamicAccessor</param>
        /// <returns><see cref="DynamicAccessor"/></returns>
        public static IDynamicAccessor GetDynamicAccessor(Type type)
        {
            if (!_DynamicAccessors.ContainsKey(type))
                _DynamicAccessors.Add(type, new DynamicAccessor(type));

            return _DynamicAccessors[type];
        }
    }
}
#endif