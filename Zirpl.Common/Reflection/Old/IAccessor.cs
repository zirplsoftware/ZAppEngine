using System;

namespace Zirpl.Reflection
{
    public interface IAccessor
    {
        T Field<T>(string fieldName);
        void Field<T>(string fieldName, T value);
        T Property<T>(string propertyName);
        void Property<T>(string propertyName, T value);
        void Invoke(String methodName, params Object[] parameters);
        T Invoke<T>(String methodName, params Object[] parameters);


        bool HasField(string fieldName);
        bool HasField(string fieldName, Type valueType);
        bool HasField<T>(string fieldName);
        bool HasGet(String propertyName);
        bool HasGet(string propertyName, Type valueType);
        bool HasGet<T>(String propertyName);
        bool HasSet(string propertyName);
        bool HasSet(string propertyName, Type valueType);
        bool HasSet<T>(String propertyName);
        bool HasMethod(String methodName);
        bool HasMethod<T>(string methodName);
    }
}
