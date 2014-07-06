#if !SILVERLIGHT && !PORTABLE
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Zirpl.Reflection
{
    public class DynamicAccessorWrapper :IDynamicAccessorWrapper
    {
        public DynamicAccessorWrapper(Object context, IDynamicAccessor dynamicAccessor)
        {
            this.Context = context;
            this.DynamicAccessor = dynamicAccessor;
        }

        public Object Context { get; private set; }
        public IDynamicAccessor DynamicAccessor { get; private set; }

        /// <summary>
        /// Gets a field member value.
        /// </summary>
        /// <param name="fieldName">The name of the member variable containing the value</param>
        /// <returns>The value as contained in the field member</returns>
        public T GetFieldValue<T>(string fieldName)
        {
            return (T)this.DynamicAccessor.GetFieldValue(this.Context, fieldName);
        }

        /// <summary>
        /// Invokes the property getter on the given property name.
        /// </summary>
        /// <param name="propertyName">The name of the property.</param>
        /// <returns>The property value.</returns>
        public T GetPropertyValue<T>(string propertyName)
        {
            return (T)this.DynamicAccessor.GetPropertyValue(this.Context, propertyName);
        }

        /// <summary>
        /// Sets a field member value.
        /// </summary>
        /// <param name="fieldName">The name of the member variable to hold the value.</param>
        /// <param name="value">The value to be set.</param>
        public void SetFieldValue<T>(string fieldName, T value)
        {
            this.DynamicAccessor.SetFieldValue(this.Context, fieldName, value);
        }

        /// <summary>
        /// Invokes the property setter on the given property name.
        /// </summary>
        /// <param name="propertyName">The name of the property.</param>
        /// <param name="value">The property value to set.</param>
        public void SetPropertyValue<T>(string propertyName, object value)
        {
            this.DynamicAccessor.SetPropertyValue(this.Context, propertyName, value);
        }

        /// <summary>
        /// Gets or sets the value on the property.
        /// </summary>
        /// <param name="propertyName">Tha name of the property.</param>
        /// <returns></returns>
        public object this[string propertyName]
        {
            get { return this.GetPropertyValue<Object>(propertyName); }
            set { this.SetPropertyValue<Object>(propertyName, value); }
        }
    }
}
#endif