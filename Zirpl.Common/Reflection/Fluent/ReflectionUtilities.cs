using System;
using System.ComponentModel;
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

        public static bool HasDefaultConstructor(this Type type)
        {
            bool hasDefaultConstructor = false;
            if (type.IsValueType
                || type.GetConstructor(new Type[] { }) != null)
            {
                hasDefaultConstructor = true;
            }

            return hasDefaultConstructor;
        }
        /// <summary>
        /// Searches for a property in the given property path
        /// </summary>
        /// <param name="type">The root/starting point to start searching</param>
        /// <param name="propertyPath">The path to the property. Ex Customer.Address.City</param>
        /// <returns>A <see cref="PropertyInfo"/> describing the property in the property path.</returns>
        public static PropertyInfo GetPropertyFromPath(this Type type, string propertyPath)
        {
            if (type == null)
                throw new ArgumentNullException("type");

            Type propertyType = type;
            PropertyInfo propertyInfo = null;
            string[] pathElements = propertyPath.Split(new char[1] { '.' });
            for (int i = 0; i < pathElements.Length; i++)
            {
                propertyInfo = propertyType.GetProperty(pathElements[i], BindingFlags.Public | BindingFlags.Instance);
                if (propertyInfo != null)
                {
                    propertyType = propertyInfo.PropertyType;
                }
                else
                {
                    throw new ArgumentOutOfRangeException("propertyPath", "Invalid property path");
                }
            }
            return propertyInfo;
        }


#if !SILVERLIGHT && !PORTABLE
        public static PropertyDescriptor GetPropertyDescriptorFromPath(this Type rootType, string propertyPath)
        {
            string propertyName;
            bool lastProperty = false;
            if (rootType == null)
                throw new ArgumentNullException("rootType");

            if (propertyPath.Contains("."))
                propertyName = propertyPath.Substring(0, propertyPath.IndexOf("."));
            else
            {
                propertyName = propertyPath;
                lastProperty = true;
            }

            PropertyDescriptor propertyDescriptor = TypeDescriptor.GetProperties(rootType)[propertyName];
            if (propertyDescriptor == null)
                throw new ArgumentOutOfRangeException("propertyPath", propertyPath, string.Format("Invalid property path for type '{0}' ", rootType.Name));


            if (!lastProperty)
                return GetPropertyDescriptorFromPath(propertyDescriptor.PropertyType, propertyPath.Substring(propertyPath.IndexOf(".") + 1));
            else
                return propertyDescriptor;
        }
#endif
    }
}
