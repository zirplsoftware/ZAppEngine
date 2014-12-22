using System;

namespace Zirpl.Reflection
{
    /// <summary>
    /// Represents a dynamic property and field accessor that implements functions to dynamically get 
    /// and set property and field values on an object. 
    /// </summary>    
    public interface ITypeAccessor
    {
        T GetFieldValue<T>(object target, string fieldName);
        T GetPropertyValue<T>(object target, string propertyName);
        void SetFieldValue<T>(object target, string fieldName, T value);
        void SetPropertyValue<T>(object target, string propertyName, T value);
        void InvokeMethod(Object target, String methodName, params Object[] parameters);
        T InvokeMethod<T>(Object target, String methodName, params Object[] parameters);

        bool HasPropertySetter(string propertyName);
        bool HasPropertySetter(string propertyName, Type valueType);
        bool HasPropertySetter<T>(String propertyName);
        bool HasPropertyGetter(String propertyName);
        bool HasPropertyGetter(string propertyName, Type valueType);
        bool HasPropertyGetter<T>(String propertyName);
        bool HasField(string fieldName);
        bool HasField(string fieldName, Type valueType);
        bool HasField<T>(string fieldName);
        bool HasMethod(String methodName);
        bool HasMethod<T>(string methodName);
    }
}
