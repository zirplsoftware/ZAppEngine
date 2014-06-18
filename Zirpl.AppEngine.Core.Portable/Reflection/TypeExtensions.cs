using System;
using System.Reflection;

namespace Zirpl.AppEngine.Core.Reflection
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
    }
}
