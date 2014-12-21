using System;
using System.Linq;
using System.Reflection;

namespace Zirpl.Reflection
{
    public static class ObjectExtensions
    {
        public static ITypeAccessor GetTypeAccessor(this Object obj)
        {
            if (obj == null) throw new ArgumentNullException("obj");

            return obj.GetType().GetTypeAccessor();
        }

        public static IAccessor GetAccessor(this Object obj)
        {
            if (obj == null) throw new ArgumentNullException("obj");

            return new Accessor(obj, obj.GetTypeAccessor());
        }
    }
}