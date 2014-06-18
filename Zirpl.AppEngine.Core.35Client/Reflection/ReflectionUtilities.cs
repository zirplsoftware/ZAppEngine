using System;

namespace Zirpl.AppEngine.Reflection
{
    public static class ReflectionUtilities
    {
        public static IDynamicAccessor GetDynamicAccessorForDeclaredType<T>(this T obj)
        {
            return typeof (T).GetDynamicAccessor();
        }
        public static IDynamicAccessor GetDynamicAccessor(this Object obj)
        {
            if (obj != null)
            {
                return obj.GetType().GetDynamicAccessor();
            }
            return typeof(Object).GetDynamicAccessor();
        }
    }
}
