using System;
using System.Reflection;

namespace Zirpl.Reflection.Fluent
{
    public static class ExtensionMethods
    {
        public static IPropertyQuery PropertyQuery(this Type type)
        {
            return new PropertyQuery(type);
        }

        public static IFieldQuery FieldQuery(this Type type)
        {
            return new FieldQuery(type);
        }

        public static IMethodQuery MethodQuery(this Type type)
        {
            return new MethodQuery(type);
        }

        public static IConstructorQuery ConstructorQuery(this Type type)
        {
            return new ConstructorQuery(type);
        }

        public static INestedTypeQuery NestedTypeQuery(this Type type)
        {
            return new NestedTypeQuery(type);
        }

        public static IEventQuery EventQuery(this Type type)
        {
            return new EventQuery(type);
        }

        public static IMemberQuery MemberQuery(this Type type)
        {
            return new MemberQuery(type);
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
