using System;

namespace Zirpl.Reflection
{
    public static class ReflectionUtilities
    {
#if !SILVERLIGHT && !PORTABLE
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
        public static IDynamicAccessorWrapper GetDynamicAccessorWrapper<T>(this T obj)
        {
            return Zirpl.Reflection.DynamicAccessorFactory.GetDynamicAccessorWrapper(obj);
        }
#endif
    }
}