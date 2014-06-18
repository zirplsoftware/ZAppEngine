using System;
using System.Collections.Generic;
using System.Reflection;
using System.Reflection.Emit;

namespace Zirpl.AppEngine.Reflection
{
    /// <summary>
    /// Implements the <see cref="IDynamicAccessor"/> and provides fast dynamic access to an object's fields and properties.
    /// </summary>
    /// <remarks>The first time you attempt to get or set a property, it will dynamically generate the get and/or set 
    /// methods and cache them internally.  Subsequent calls uses the dynamic methods without having to query the type's 
    /// meta data.</remarks>
    internal sealed class DynamicAccessor : IDynamicAccessor
    {
        #region Private Fields
        private readonly Type _type;
        private readonly Dictionary<string, GetHandler> _propertyGetters;
        private readonly Dictionary<string, SetHandler> _propertySetters;
        private readonly Dictionary<string, GetHandler> _fieldGetters;
        private readonly Dictionary<string, SetHandler> _fieldSetters;
        #endregion

        #region Constructors




        internal DynamicAccessor(Type type)
        {
            _type = type;
            _propertyGetters = new Dictionary<string, GetHandler>();
            _propertySetters = new Dictionary<string, SetHandler>();
            _fieldGetters = new Dictionary<string, GetHandler>();
            _fieldSetters = new Dictionary<string, SetHandler>();
        }
        #endregion

        #region Public Accessors


        /// <summary>
        /// Gets or Sets the supplied property on the supplied target
        /// </summary>
        public object this[object target, string propertyName]
        {
            get
            {
                ValidatePropertyGetter(propertyName);
                return _propertyGetters[propertyName](target);
            }
            set
            {
                ValidatePropertySetter(propertyName);
                _propertySetters[propertyName](target, value);
            }
        }

        public void SetPropertyValue(object target, string propertyName, object value)
        {
            ValidatePropertySetter(propertyName);
            _propertySetters[propertyName](target, value);
        }

        public object GetPropertyValue(object target, string propertyName)
        {
            ValidatePropertyGetter(propertyName);
            return _propertyGetters[propertyName](target);
        }


        public void SetFieldValue(object target, string fieldName, object value)
        {
            ValidateFieldSetter(fieldName);
            _fieldSetters[fieldName](target, value);
        }

        public object GetFieldValue(object target, string fieldName)
        {
            ValidateFieldGetter(fieldName);
            return _fieldGetters[fieldName](target);
        }

        #endregion

        #region Private Helpers


        private void ValidateFieldSetter(string fieldName)
        {
            if (!_fieldSetters.ContainsKey(fieldName))
            {
                FieldInfo fieldInfo = _type.GetField(fieldName, BindingFlags.NonPublic | BindingFlags.Instance);

                if (fieldInfo == null)
                    throw new ArgumentOutOfRangeException(fieldName, "Unable to find fieldname");
                _fieldSetters.Add(fieldName, DynamicMethodCompiler.CreateSetHandler(_type, fieldInfo));
            }
        }

        private void ValidatePropertySetter(string propertyName)
        {
            if (!_propertySetters.ContainsKey(propertyName))
            {
                _propertySetters.Add(propertyName, DynamicMethodCompiler.CreateSetHandler(_type, _type.GetProperty(propertyName)));
            }
        }

        private void ValidatePropertyGetter(string propertyName)
        {
            if (!_propertyGetters.ContainsKey(propertyName))
            {
                _propertyGetters.Add(propertyName, DynamicMethodCompiler.CreateGetHandler(_type, _type.GetProperty(propertyName)));
            }
        }
        private void ValidateFieldGetter(string fieldName)
        {
            if (!_fieldGetters.ContainsKey(fieldName))
            {
                FieldInfo fieldInfo = _type.GetField(fieldName, BindingFlags.NonPublic | BindingFlags.Instance);
                if (fieldInfo == null)
                    throw new ArgumentOutOfRangeException(fieldName, "Unable to find fieldname");
                _fieldGetters.Add(fieldName, DynamicMethodCompiler.CreateGetHandler(_type, fieldInfo));
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
            internal static GetHandler CreateGetHandler(Type type, FieldInfo Season)
            {
                DynamicMethod dynamicGet = CreateGetDynamicMethod(type);
                ILGenerator getGenerator = dynamicGet.GetILGenerator();

                getGenerator.Emit(OpCodes.Ldarg_0);
                getGenerator.Emit(OpCodes.Ldfld, Season);
                BoxIfNeeded(Season.FieldType, getGenerator);
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
            internal static SetHandler CreateSetHandler(Type type, FieldInfo Season)
            {
                DynamicMethod dynamicSet = CreateSetDynamicMethod(type);
                ILGenerator setGenerator = dynamicSet.GetILGenerator();

                setGenerator.Emit(OpCodes.Ldarg_0);
                setGenerator.Emit(OpCodes.Ldarg_1);
                UnboxIfNeeded(Season.FieldType, setGenerator);
                setGenerator.Emit(OpCodes.Stfld, Season);
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
    }
}