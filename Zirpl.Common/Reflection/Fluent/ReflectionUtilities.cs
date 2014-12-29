using System;
using System.Reflection;

namespace Zirpl.Reflection.Fluent
{
    public static class ReflectionUtilities
    {
        public static ITypeQuery QueryTypes(this Assembly assembly)
        {
            if (assembly == null) throw new ArgumentNullException("assembly");
            return new AssemblyTypeQuery(assembly);
        }
#if !PORTABLE
        public static ITypeQuery QueryTypes(this AppDomain appDomain)
        {
            if (appDomain == null) throw new ArgumentNullException("appDomain");
            return new AssemblyTypeQuery(appDomain);
        }
#endif

        public static IPropertyQuery QueryProperties(this Type type)
        {
            if (type == null) throw new ArgumentNullException("type");
            return new PropertyQuery(type);
        }

        public static IFieldQuery QueryFields(this Type type)
        {
            if (type == null) throw new ArgumentNullException("type");

            return new FieldQuery(type);
        }

        public static IMethodQuery QueryMethods(this Type type)
        {
            if (type == null) throw new ArgumentNullException("type");

            return new MethodQuery(type);
        }

        public static IConstructorQuery QueryConstructors(this Type type)
        {
            if (type == null) throw new ArgumentNullException("type");

            return new ConstructorQuery(type);
        }

        public static INestedTypeQuery QueryNestedTypes(this Type type)
        {
            if (type == null) throw new ArgumentNullException("type");

            return new NestedTypeQuery(type);
        }

        public static IEventQuery QueryEvents(this Type type)
        {
            if (type == null) throw new ArgumentNullException("type");

            return new EventQuery(type);
        }

        public static IMemberQuery QueryMembers(this Type type)
        {
            if (type == null) throw new ArgumentNullException("type");

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
