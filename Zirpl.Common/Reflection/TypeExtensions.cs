using System;
using System.ComponentModel;
using System.Reflection;

namespace Zirpl.Reflection
{
    public static class TypeExtensions
    {
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
        public static IDynamicAccessor GetDynamicAccessor(this Type type)
        {
            return DynamicAccessorFactory.GetDynamicAccessor(type);
        }
#endif

        public static bool HasDefaultConstructor(this Type type)
        {
            bool hasDefaultConstructor = false;
            if (type.IsValueType
                || type.GetConstructor(new Type[]{}) != null)
            {
                hasDefaultConstructor = true;
            }

            return hasDefaultConstructor;
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
