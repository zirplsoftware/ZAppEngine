using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Zirpl.Reflection
{
    public interface IAccessor
    {
        T GetField<T>(string fieldName);
        T GetProperty<T>(string propertyName);
        void SetField<T>(string fieldName, T value);
        void SetProperty<T>(string propertyName, T value);
        void InvokeMethod(String methodName, params Object[] parameters);
        T InvokeMethod<T>(String methodName, params Object[] parameters);

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
    }
}
