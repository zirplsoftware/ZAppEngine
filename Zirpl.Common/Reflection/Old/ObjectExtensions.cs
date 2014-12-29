using System;

namespace Zirpl.Reflection
{
    public static class ObjectExtensions
    {
        public static IAccessor Access(this Object obj)
        {
            if (obj == null) throw new ArgumentNullException("obj");

            return new Accessor(obj, obj.GetType().GetTypeAccessor());
        }
    }
}