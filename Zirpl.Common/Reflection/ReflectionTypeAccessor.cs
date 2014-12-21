using System;
using System.Collections.Generic;
using System.Linq;
using System.Reflection;
using System.Text;
using System.Threading.Tasks;

namespace Zirpl.Reflection
{
    internal sealed class ReflectionTypeAccessor : ITypeAccessor
    {
        private readonly Type _type;
        private readonly Dictionary<string, PropertyInfo> _propertyInfos;
        private readonly Dictionary<string, FieldInfo> _fieldInfos;
        private readonly ReflectionTypeAccessor _baseTypeAccessor;

        internal ReflectionTypeAccessor(Type type, ReflectionTypeAccessor baseTypeAccessor)
        {
            this._type = type;
            this._baseTypeAccessor = baseTypeAccessor;
            this._propertyInfos = new Dictionary<string, PropertyInfo>();
            this._fieldInfos = new Dictionary<string, FieldInfo>();
        }

        public T GetFieldValue<T>(object target, string fieldName)
        {
            this.EnsureFieldInfoKey(fieldName);
            var fieldInfo = this._fieldInfos[fieldName];
            if (fieldInfo != null)
            {
                return (T)fieldInfo.GetValue(target);
            }
            else if (this._baseTypeAccessor != null)
            {
                return this._baseTypeAccessor.GetFieldValue<T>(target, fieldName);
            }
            else
            {
                throw new ArgumentOutOfRangeException("fieldName", "Field " + fieldName + " not found on " + target.GetType());
            }
        }

        public void SetFieldValue<T>(object target, string fieldName, T value)
        {
            this.EnsureFieldInfoKey(fieldName);
            var fieldInfo = this._fieldInfos[fieldName];
            if (fieldInfo != null)
            {
                fieldInfo.SetValue(target, value);
            }
            else if (this._baseTypeAccessor != null)
            {
                this._baseTypeAccessor.SetFieldValue(target, fieldName, value);
            }
            else
            {
                throw new ArgumentOutOfRangeException("fieldName", "Field " + fieldName + " not found on " + target.GetType());
            }
        }

        private void EnsureFieldInfoKey(String fieldName)
        {
            if (!this._fieldInfos.ContainsKey(fieldName))
            {
                lock (this._fieldInfos)
                {
                    if (!this._fieldInfos.ContainsKey(fieldName))
                    {
                        var fieldInfo = this._type.GetField(fieldName, BindingFlags.Instance | BindingFlags.NonPublic | BindingFlags.Public);
                        // NOTE: this COULD be null, but that is ok. we handle that below
                        this._fieldInfos.Add(fieldName, fieldInfo);
                    }
                }
            }
        }

        public T GetPropertyValue<T>(object target, string propertyName)
        {
            this.EnsurePropertyInfoKey(propertyName);
            var propertyInfo = this._propertyInfos[propertyName];
            if (propertyInfo != null)
            {
                return (T)propertyInfo.GetValue(target, null);
            }
            else if (this._baseTypeAccessor != null)
            {
                return this._baseTypeAccessor.GetPropertyValue<T>(target, propertyName);
            }
            else
            {
                throw new ArgumentOutOfRangeException("propertyName", "Property " + propertyName + " not found on " + target.GetType());
            }
        }

        public void SetPropertyValue<T>(object target, string propertyName, T value)
        {
            this.EnsurePropertyInfoKey(propertyName);
            var propertyInfo = this._propertyInfos[propertyName];
            if (propertyInfo != null)
            {
                propertyInfo.SetValue(target, value, null);
            }
            else if (this._baseTypeAccessor != null)
            {
                this._baseTypeAccessor.SetPropertyValue(target, propertyName, value);
            }
            else
            {
                throw new ArgumentOutOfRangeException("propertyName", "Property " + propertyName + " not found on " + target.GetType());
            }
        }

        private void EnsurePropertyInfoKey(String fieldName)
        {
            if (!this._propertyInfos.ContainsKey(fieldName))
            {
                lock (this._propertyInfos)
                {
                    if (!this._propertyInfos.ContainsKey(fieldName))
                    {
                        var propertyInfo = this._type.GetProperty(fieldName, BindingFlags.Instance | BindingFlags.NonPublic | BindingFlags.Public);
                        // NOTE: this COULD be null, but that is ok. we handle that below
                        this._propertyInfos.Add(fieldName, propertyInfo);
                    }
                }
            }
        }

        public void InvokeMethod(Object target, String methodName, params Object[] parameters)
        {
            if (String.IsNullOrEmpty(methodName))
            {
                throw new ArgumentNullException("methodName");
            }

            parameters = parameters ?? new object[1] { null };
            var methodInfos = GetMethodInfos(this._type, methodName, parameters, null, true);
            if (!methodInfos.Any())
            {
                if (this._baseTypeAccessor != null)
                {
                    this._baseTypeAccessor.InvokeMethod(target, methodName, parameters);
                }
                else
                {
                    throw new ArgumentOutOfRangeException("methodName", "Method " + methodName + " not found on " + target.GetType());
                }
            }
            else if (methodInfos.Count() > 1)
            {
                throw new ArgumentException("Found more than 1 method " + methodName + " on " + target.GetType(), "methodName");
            }
            else
            {
                methodInfos.Single().Invoke(target, parameters);
            }
        }

        public T InvokeMethod<T>(Object target, String methodName, params Object[] parameters)
        {
            if (String.IsNullOrEmpty(methodName))
            {
                throw new ArgumentNullException("methodName");
            }

            parameters = parameters ?? new object[1] { null };
            var methodInfos = GetMethodInfos(this._type, methodName, parameters, typeof(T), true);
            if (!methodInfos.Any())
            {
                if (this._baseTypeAccessor != null)
                {
                    return this._baseTypeAccessor.InvokeMethod<T>(target, methodName, parameters);
                }
                else
                {
                    throw new ArgumentOutOfRangeException("methodName", "Method " + methodName + " not found on " + target.GetType());
                }
            }
            else if (methodInfos.Count() > 1)
            {
                throw new ArgumentException("Found more than 1 method " + methodName + " on " + target.GetType(), "methodName");
            }
            else
            {
                return (T)methodInfos.Single().Invoke(target, parameters);
            }
        }


        public bool HasMethod(string methodName)
        {
            if (String.IsNullOrEmpty(methodName))
            {
                throw new ArgumentNullException("methodName");
            }

            //parameters = parameters ?? new object[1] { null };
            var methodInfos = GetMethodInfos(this._type, methodName, null, null, false);
            if (!methodInfos.Any())
            {
                if (this._baseTypeAccessor != null)
                {
                    return this._baseTypeAccessor.HasMethod(methodName);
                }
                else
                {
                    return false;
                }
            }
            return true;
        }

        private IEnumerable<MethodInfo> GetMethodInfos(Type type, String methodName, Object[] parameters, Type returnType = null, bool restrictByParameters = false)
        {
            // TODO: support optional arguments

            var methodInfos = type.GetMethods(BindingFlags.Instance | BindingFlags.NonPublic | BindingFlags.Public).Where(o => o.Name == methodName && (!restrictByParameters || o.GetParameters().Count() == parameters.Count()));
            if (methodInfos.Count() > 1
                && restrictByParameters)
            {
                methodInfos = methodInfos.Where(o => DoParameterTypesMatch(o, parameters));
                if (methodInfos.Count() > 1)
                {
                    methodInfos = methodInfos.Where(o => DoParameterTypesMatch(o, parameters, returnType, true));
                }
            }

            return methodInfos;
        }

        private bool DoParameterTypesMatch(MethodInfo methodInfo, Object[] parameters, Type returnType = null, bool strict = false)
        {
            bool parameterTypesMatch = true;
            var parameterInfos = methodInfo.GetParameters();
            for (int i = 0; i < parameters.Length; i++)
            {
                var parameterType = parameterInfos.Single(o => o.Position == i).ParameterType;
                var parameterValue = parameters[i];
                if (parameterValue == null)
                {
                    if (parameterType.IsValueType
                        && Nullable.GetUnderlyingType(parameterType) == null)
                    {
                        parameterTypesMatch = false;
                    }
                }
                else
                {
                    if (strict)
                    {
                        if (!parameterType.Equals(parameterValue.GetType()))
                        {
                            parameterTypesMatch = false;
                        }
                    }
                    else
                    {
                        if (!parameterType.IsInstanceOfType(parameterValue))
                        {
                            parameterTypesMatch = false;
                        }
                    }
                }
            }
            if (parameterTypesMatch
                && returnType != null)
            {
                if (!methodInfo.ReturnType.Equals(returnType))
                {
                    parameterTypesMatch = false;
                }
            }
            return parameterTypesMatch;
        }


        public bool HasPropertySetter(string propertyName)
        {
            this.EnsurePropertyInfoKey(propertyName);
            var propertyInfo = this._propertyInfos[propertyName];
            if (propertyInfo != null)
            {
                return propertyInfo.CanWrite;
            }
            else if (this._baseTypeAccessor != null)
            {
                return this._baseTypeAccessor.HasPropertySetter(propertyName);
            }
            else
            {
                return false;
            }
        }

        public bool HasPropertySetter(string propertyName, Type valueType)
        {
            this.EnsurePropertyInfoKey(propertyName);
            var propertyInfo = this._propertyInfos[propertyName];
            if (propertyInfo != null)
            {
                return propertyInfo.CanWrite && propertyInfo.PropertyType.IsAssignableFrom(valueType);
            }
            else if (this._baseTypeAccessor != null)
            {
                return this._baseTypeAccessor.HasPropertySetter(propertyName, valueType);
            }
            else
            {
                return false;
            }
        }

        public bool HasPropertySetter<T>(string propertyName)
        {
            return this.HasPropertySetter(propertyName, typeof(T));
        }

        public bool HasPropertyGetter(string propertyName)
        {
            this.EnsurePropertyInfoKey(propertyName);
            var propertyInfo = this._propertyInfos[propertyName];
            if (propertyInfo != null)
            {
                return propertyInfo.CanRead;
            }
            else if (this._baseTypeAccessor != null)
            {
                return this._baseTypeAccessor.HasPropertySetter(propertyName);
            }
            else
            {
                return false;
            }
        }

        public bool HasPropertyGetter(string propertyName, Type valueType)
        {
            this.EnsurePropertyInfoKey(propertyName);
            var propertyInfo = this._propertyInfos[propertyName];
            if (propertyInfo != null)
            {
                return propertyInfo.CanRead && propertyInfo.PropertyType.IsAssignableFrom(valueType);
            }
            else if (this._baseTypeAccessor != null)
            {
                return this._baseTypeAccessor.HasPropertyGetter(propertyName, valueType);
            }
            else
            {
                return false;
            }
        }

        public bool HasPropertyGetter<T>(string propertyName)
        {
            return this.HasPropertyGetter(propertyName, typeof(T));
        }

        public bool HasField(string fieldName)
        {
            this.EnsureFieldInfoKey(fieldName);
            var fieldInfo = this._fieldInfos[fieldName];
            if (fieldInfo != null)
            {
                return true;
            }
            else if (this._baseTypeAccessor != null)
            {
                return this._baseTypeAccessor.HasField(fieldName);
            }
            else
            {
                return false;
            }
        }

        public bool HasField(string fieldName, Type valueType)
        {
            this.EnsureFieldInfoKey(fieldName);
            var fieldInfo = this._fieldInfos[fieldName];
            if (fieldInfo != null)
            {
                return fieldInfo.FieldType.IsAssignableFrom(valueType);
            }
            else if (this._baseTypeAccessor != null)
            {
                return this._baseTypeAccessor.HasField(fieldName, valueType);
            }
            else
            {
                return false;
            }
        }

        public bool HasField<T>(string fieldName)
        {
            return this.HasField(fieldName, typeof(T));
        }
    }
}
