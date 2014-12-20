using System;
using System.Linq;
using System.Reflection;

namespace Zirpl.Reflection
{
    public static class ObjectExtensions
    {
        public static ITypeAccessor GetTypeAccessorForDeclaredType<T>(this T obj)
        {
            if (obj == null)
            {
                throw new ArgumentNullException("obj");
            }
            return typeof(T).GetTypeAccessor();
        }
        public static ITypeAccessor GetTypeAccessor(this Object obj)
        {
            if (obj == null)
            {
                throw new ArgumentNullException("obj");
            }
            return obj.GetType().GetTypeAccessor();
        }

        public static T GetProperty<T>(this object obj, String propertyName)
        {
            return (T)obj.GetTypeAccessor().GetPropertyValue(obj, propertyName);
        }

        public static T GetField<T>(this object obj, String fieldName)
        {
            return (T)obj.GetTypeAccessor().GetFieldValue(obj, fieldName);
        }

        public static void SetProperty(this object obj, String propertyName, Object value)
        {
            obj.GetTypeAccessor().SetPropertyValue(obj, propertyName, value);
        }

        public static void SetField(this object obj, String fieldName, Object value)
        {
            obj.GetTypeAccessor().SetFieldValue(obj, fieldName, value);
        }
        public static void InvokeMethod(this Object obj, String methodName, params Object[] parameters)
        {
            TypeAccessorFactory.GetReflectionTypeAccessor(obj.GetType()).InvokeMethod(obj, methodName, parameters);
        }

        public static T InvokeMethod<T>(this Object obj, String methodName, params Object[] parameters)
        {
            return TypeAccessorFactory.GetReflectionTypeAccessor(obj.GetType()).InvokeMethod<T>(obj, methodName, parameters);
        }
    }
}