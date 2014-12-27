#if !SILVERLIGHT && !PORTABLE
using System;
using System.Collections.Generic;
using System.Linq;
using System.Reflection;
using System.Reflection.Emit;

namespace Zirpl.Reflection
{
    /// <summary>
    /// Implements the <see cref="ITypeAccessor"/> and provides fast dynamic access to an object's fields and properties.
    /// </summary>
    /// <remarks>The first time you attempt to get or set a property, it will dynamically generate the get and/or set 
    /// methods and cache them internally.  Subsequent calls uses the dynamic methods without having to query the type's 
    /// meta data.</remarks>
    internal sealed class DynamicTypeAccessor : ITypeAccessor
    {
        #region Private Fields
        private readonly Type _type;
        private readonly DynamicTypeAccessor _baseTypeAccessor;
        private readonly Dictionary<string, GetHandler> _propertyGetters;
        private readonly Dictionary<string, SetHandler> _propertySetters;
        private readonly Dictionary<string, GetHandler> _fieldGetters;
        private readonly Dictionary<string, SetHandler> _fieldSetters;
        #endregion

        #region Constructors

        internal DynamicTypeAccessor(Type type, DynamicTypeAccessor baseTypeAccessor)
        {
            _type = type;
            _baseTypeAccessor = baseTypeAccessor;
            _propertyGetters = new Dictionary<string, GetHandler>();
            _propertySetters = new Dictionary<string, SetHandler>();
            _fieldGetters = new Dictionary<string, GetHandler>();
            _fieldSetters = new Dictionary<string, SetHandler>();
        }

        #endregion

        #region Public Accessors

        
        public void SetPropertyValue<T>(object target, string propertyName, T value)
        {
            EnsurePropertySetterKey(propertyName);
            var handler = _propertySetters[propertyName];
            if (handler != null)
            {
                handler(target, value);
            }
            else if (this._baseTypeAccessor != null)
            {
                this._baseTypeAccessor.SetPropertyValue(target, propertyName, value);
            }
            else
            {
                throw new ArgumentOutOfRangeException("propertyName", propertyName, "Property not found on " + target.GetType());
            }
        }

        public T GetPropertyValue<T>(object target, string propertyName)
        {
            EnsurePropertyGetterKey(propertyName);
            var handler = _propertyGetters[propertyName];
            if (handler != null)
            {
                return (T)handler(target);
            }
            else if (this._baseTypeAccessor != null)
            {
                return this._baseTypeAccessor.GetPropertyValue<T>(target, propertyName);
            }
            else
            {
                throw new ArgumentOutOfRangeException("propertyName", propertyName, "Property not found on " + target.GetType());
            }
        }


        public void SetFieldValue<T>(object target, string fieldName, T value)
        {
            EnsureFieldSetterKey(fieldName);
            var handler = _fieldSetters[fieldName];
            if (handler != null)
            {
                handler(target, value);
            }
            else if (this._baseTypeAccessor != null)
            {
                this._baseTypeAccessor.SetFieldValue(target, fieldName, value);
            }
            else
            {
                throw new ArgumentOutOfRangeException("fieldName", fieldName, "Field not found on " + target.GetType());
            }
        }

        public T GetFieldValue<T>(object target, string fieldName)
        {
            EnsureFieldGetterKey(fieldName);
            var handler = _fieldGetters[fieldName];
            if (handler != null)
            {
                return (T)handler(target);
            }
            else if (this._baseTypeAccessor != null)
            {
                return this._baseTypeAccessor.GetFieldValue<T>(target, fieldName);
            }
            else
            {
                throw new ArgumentOutOfRangeException("fieldName", fieldName, "Field not found on " + target.GetType());
            }
        }

        #endregion

        #region Private Helpers


        private void EnsureFieldSetterKey(string fieldName)
        {
            if (!_fieldSetters.ContainsKey(fieldName))
            {
                lock (_fieldSetters)
                {
                    if (!_fieldSetters.ContainsKey(fieldName))
                    {
                        FieldInfo fieldInfo = _type.GetField(fieldName, BindingFlags.NonPublic | BindingFlags.Instance | BindingFlags.Public);
                        _fieldSetters.Add(fieldName, fieldInfo == null ? null : DynamicMethodCompiler.CreateSetHandler(_type, fieldInfo));
                    }
                }
            }
        }

        private void EnsurePropertySetterKey(string propertyName)
        {
            if (!_propertySetters.ContainsKey(propertyName))
            {
                lock (_propertySetters)
                {
                    if (!_propertySetters.ContainsKey(propertyName))
                    {
                        var propertyInfo = _type.GetProperty(propertyName, BindingFlags.Instance | BindingFlags.NonPublic | BindingFlags.Public);
                        _propertySetters.Add(propertyName, (propertyInfo == null || !propertyInfo.CanWrite) ? null : DynamicMethodCompiler.CreateSetHandler(_type, propertyInfo));
                    }
                }
            }
        }

        private void EnsurePropertyGetterKey(string propertyName)
        {
            if (!_propertyGetters.ContainsKey(propertyName))
            {
                lock (_propertyGetters)
                {
                    if (!_propertyGetters.ContainsKey(propertyName))
                    {
                        var propertyInfo = _type.GetProperty(propertyName, BindingFlags.Instance | BindingFlags.NonPublic | BindingFlags.Public);
                        _propertyGetters.Add(propertyName, (propertyInfo == null || !propertyInfo.CanRead) ? null : DynamicMethodCompiler.CreateGetHandler(_type, propertyInfo));
                    }
                }
            }
        }
        private void EnsureFieldGetterKey(string fieldName)
        {
            if (!_fieldGetters.ContainsKey(fieldName))
            {
                lock (_fieldGetters)
                {
                    if (!_fieldGetters.ContainsKey(fieldName))
                    {
                        FieldInfo fieldInfo = _type.GetField(fieldName, BindingFlags.NonPublic | BindingFlags.Instance | BindingFlags.Public);
                        _fieldGetters.Add(fieldName, fieldInfo == null ? null : DynamicMethodCompiler.CreateGetHandler(_type, fieldInfo));
                    }
                }
            }
        }
        #endregion

        #region Contained Classes
        internal delegate object GetHandler(object source);
        internal delegate void SetHandler(object source, object value);
        internal delegate object InstantiateObjectHandler();

        /// <summary>
        /// provides helper functions for late binding a class
        /// </summary>
        /// <remarks>
        /// Class found here:
        /// http://www.codeproject.com/useritems/Dynamic_Code_Generation.asp
        /// </remarks>
        internal sealed class DynamicMethodCompiler
        {
            // DynamicMethodCompiler
            private DynamicMethodCompiler() { }


            internal static InstantiateObjectHandler CreateInstantiateObjectHandler(Type type)
            {
                ConstructorInfo constructorInfo = type.GetConstructor(BindingFlags.Public | BindingFlags.NonPublic | BindingFlags.Instance, null, new Type[0], null);
                if (constructorInfo == null)
                {
                    throw new ApplicationException(string.Format("The type {0} must declare an empty constructor (the constructor may be private, internal, protected, protected internal, or public).", type));
                }

                DynamicMethod dynamicMethod = new DynamicMethod("InstantiateObject", MethodAttributes.Static | MethodAttributes.Public, CallingConventions.Standard, typeof(object), null, type, true);
                ILGenerator generator = dynamicMethod.GetILGenerator();
                generator.Emit(OpCodes.Newobj, constructorInfo);
                generator.Emit(OpCodes.Ret);
                return (InstantiateObjectHandler)dynamicMethod.CreateDelegate(typeof(InstantiateObjectHandler));
            }

            // CreateGetDelegate
            internal static GetHandler CreateGetHandler(Type type, PropertyInfo propertyInfo)
            {
                MethodInfo getMethodInfo = propertyInfo.GetGetMethod(true);
                DynamicMethod dynamicGet = CreateGetDynamicMethod(type);
                ILGenerator getGenerator = dynamicGet.GetILGenerator();

                getGenerator.Emit(OpCodes.Ldarg_0);
                getGenerator.Emit(OpCodes.Call, getMethodInfo);
                BoxIfNeeded(getMethodInfo.ReturnType, getGenerator);
                getGenerator.Emit(OpCodes.Ret);

                return (GetHandler)dynamicGet.CreateDelegate(typeof(GetHandler));
            }

            // CreateGetDelegate
            internal static GetHandler CreateGetHandler(Type type, FieldInfo fieldInfo)
            {
                DynamicMethod dynamicGet = CreateGetDynamicMethod(type);
                ILGenerator getGenerator = dynamicGet.GetILGenerator();

                getGenerator.Emit(OpCodes.Ldarg_0);
                getGenerator.Emit(OpCodes.Ldfld, fieldInfo);
                BoxIfNeeded(fieldInfo.FieldType, getGenerator);
                getGenerator.Emit(OpCodes.Ret);

                return (GetHandler)dynamicGet.CreateDelegate(typeof(GetHandler));
            }

            // CreateSetDelegate
            internal static SetHandler CreateSetHandler(Type type, PropertyInfo propertyInfo)
            {
                MethodInfo setMethodInfo = propertyInfo.GetSetMethod(true);
                DynamicMethod dynamicSet = CreateSetDynamicMethod(type);
                ILGenerator setGenerator = dynamicSet.GetILGenerator();

                setGenerator.Emit(OpCodes.Ldarg_0);
                setGenerator.Emit(OpCodes.Ldarg_1);
                UnboxIfNeeded(setMethodInfo.GetParameters()[0].ParameterType, setGenerator);
                setGenerator.Emit(OpCodes.Call, setMethodInfo);
                setGenerator.Emit(OpCodes.Ret);

                return (SetHandler)dynamicSet.CreateDelegate(typeof(SetHandler));
            }

            // CreateSetDelegate
            internal static SetHandler CreateSetHandler(Type type, FieldInfo fieldInfo)
            {
                DynamicMethod dynamicSet = CreateSetDynamicMethod(type);
                ILGenerator setGenerator = dynamicSet.GetILGenerator();

                setGenerator.Emit(OpCodes.Ldarg_0);
                setGenerator.Emit(OpCodes.Ldarg_1);
                UnboxIfNeeded(fieldInfo.FieldType, setGenerator);
                setGenerator.Emit(OpCodes.Stfld, fieldInfo);
                setGenerator.Emit(OpCodes.Ret);

                return (SetHandler)dynamicSet.CreateDelegate(typeof(SetHandler));
            }

            // CreateGetDynamicMethod
            private static DynamicMethod CreateGetDynamicMethod(Type type)
            {
                return new DynamicMethod("DynamicGet", typeof(object), new Type[] { typeof(object) }, type, true);
            }

            // CreateSetDynamicMethod
            private static DynamicMethod CreateSetDynamicMethod(Type type)
            {
                return new DynamicMethod("DynamicSet", typeof(void), new Type[] { typeof(object), typeof(object) }, type, true);
            }

            // BoxIfNeeded
            private static void BoxIfNeeded(Type type, ILGenerator generator)
            {
                if (type.IsValueType)
                {
                    generator.Emit(OpCodes.Box, type);
                }
            }

            // UnboxIfNeeded
            private static void UnboxIfNeeded(Type type, ILGenerator generator)
            {
                if (type.IsValueType)
                {
                    generator.Emit(OpCodes.Unbox_Any, type);
                }
            }
        }
        #endregion


        public bool HasPropertySetter(string propertyName)
        {
            EnsurePropertySetterKey(propertyName);
            var handler = _propertySetters[propertyName];
            if (handler != null)
            {
                return true;
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
            EnsurePropertySetterKey(propertyName);
            var handler = _propertySetters[propertyName];
            if (handler != null)
            {
                return handler.Method.GetParameters().Single(o => o.Position == 0).ParameterType.IsAssignableFrom(valueType);
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
            return this.HasPropertySetter(propertyName, typeof (T));
        }

        public bool HasPropertyGetter(string propertyName)
        {
            EnsurePropertyGetterKey(propertyName);
            var handler = _propertyGetters[propertyName];
            if (handler != null)
            {
                return true;
            }
            else if (this._baseTypeAccessor != null)
            {
                return this._baseTypeAccessor.HasPropertyGetter(propertyName);
            }
            else
            {
                return false;
            }
        }

        public bool HasPropertyGetter(string propertyName, Type valueType)
        {
            EnsurePropertyGetterKey(propertyName);
            var handler = _propertyGetters[propertyName];
            if (handler != null)
            {
                return handler.Method.ReturnType.IsAssignableFrom(valueType);
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
            EnsureFieldGetterKey(fieldName);
            var handler = _fieldGetters[fieldName];
            if (handler != null)
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
            EnsureFieldGetterKey(fieldName);
            var handler = _fieldGetters[fieldName];
            if (handler != null)
            {
                return handler.Method.ReturnType.IsAssignableFrom(valueType);
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
            return HasField(fieldName, typeof (T));
        }

        public void InvokeMethod(object target, string methodName, params object[] parameters)
        {
            TypeAccessorFactory.GetReflectionTypeAccessor(this._type).InvokeMethod(target, methodName, parameters);
        }

        public bool HasMethod(string methodName)
        {
            return TypeAccessorFactory.GetReflectionTypeAccessor(this._type).HasMethod(methodName);
        }

        public bool HasMethod<T>(string methodName)
        {
            return TypeAccessorFactory.GetReflectionTypeAccessor(this._type).HasMethod<T>(methodName);
        }


        public T InvokeMethod<T>(object target, string methodName, params object[] parameters)
        {
            return TypeAccessorFactory.GetReflectionTypeAccessor(this._type)
                .InvokeMethod<T>(target, methodName, parameters);
        }
    }
}
#endif