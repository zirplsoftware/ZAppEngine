using System;

namespace Zirpl.Reflection
{
    /// <summary>
    /// Represents a dynamic property and field accessor that implements functions to dynamically get 
    /// and set property and field values on an object. 
    /// </summary>    
    public interface ITypeAccessor
    {
        /// <summary>
        /// Gets a field member value.
        /// </summary>
        /// <param name="target">The target object for to get the value.</param>
        /// <param name="fieldName">The name of the member variable containing the value</param>
        /// <returns>The value as contained in the field member</returns>
        object GetFieldValue(object target, string fieldName);

        /// <summary>
        /// Invokes the property getter on the given property name.
        /// </summary>
        /// <param name="target">The target object for to get the property value</param>
        /// <param name="propertyName">The name of the property.</param>
        /// <returns>The property value.</returns>
        object GetPropertyValue(object target, string propertyName);

        /// <summary>
        /// Sets a field member value.
        /// </summary>
        /// <param name="target">The target object for to set the value.</param>
        /// <param name="fieldName">The name of the member variable to hold the value.</param>
        /// <param name="value">The value to be set.</param>
        void SetFieldValue(object target, string fieldName, object value);

        /// <summary>
        /// Invokes the property setter on the given property name.
        /// </summary>
        /// <param name="target">The target object for to set the value.</param>
        /// <param name="propertyName">The name of the property.</param>
        /// <param name="value">The property value to set.</param>
        void SetPropertyValue(object target, string propertyName, object value);

        bool HasPropertySetter(string propertyName);
        bool HasPropertySetter(string propertyName, Type valueType);
        bool HasPropertySetter<T>(String propertyName);
        bool HasPropertyGetter(String propertyName);
        bool HasPropertyGetter(string propertyName, Type valueType);
        bool HasPropertyGetter<T>(String propertyName);
        bool HasField(string fieldName);
        bool HasField(string fieldName, Type valueType);
        bool HasField<T>(string fieldName);

        void InvokeMethod(Object target, String methodName, params Object[] parameters);
        bool HasMethod(String methodName);
    }
}
