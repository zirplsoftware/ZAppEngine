using System;
using System.Reflection;
using System.Runtime.InteropServices;

namespace Zirpl.Reflection.Fluent
{
    public static class ExtensionMethods
    {
        internal static FluentType Fluent(this Type type)
        {
            if (type == null) throw new ArgumentNullException("type");

            return new FluentType(type);
        }
        
        public static Object GetStaticValue(this FieldInfo fieldInfo)
        {
            if (fieldInfo == null) throw new ArgumentNullException("fieldInfo");

            return fieldInfo.GetValue(null);
        }

        public static void SetStaticValue(this FieldInfo fieldInfo, Object value)
        {
            if (fieldInfo == null) throw new ArgumentNullException("fieldInfo");

            fieldInfo.SetValue(null, value);
        }
#if !PORTABLE && !SILVERLIGHT
        public static Object GetStaticValue(this PropertyInfo propertyInfo)
        {
            if (propertyInfo == null) throw new ArgumentNullException("propertyInfo");

            return propertyInfo.GetValue(null);
        }

        public static void SetStaticValue(this PropertyInfo propertyInfo, Object value)
        {
            if (propertyInfo == null) throw new ArgumentNullException("propertyInfo");

            propertyInfo.SetValue(null, value);
        }
#endif
    }
}
