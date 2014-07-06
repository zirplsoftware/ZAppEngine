#if !SILVERLIGHT && !PORTABLE
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Zirpl.Reflection
{
    public interface IDynamicAccessorWrapper
    {
        Object Context { get;  }
        IDynamicAccessor DynamicAccessor { get; }

        /// <summary>
        /// Gets a field member value.
        /// </summary>
        /// <param name="fieldName">The name of the member variable containing the value</param>
        /// <returns>The value as contained in the field member</returns>
        T GetFieldValue<T>(string fieldName);

        /// <summary>
        /// Invokes the property getter on the given property name.
        /// </summary>
        /// <param name="propertyName">The name of the property.</param>
        /// <returns>The property value.</returns>
        T GetPropertyValue<T>(string propertyName);

        /// <summary>
        /// Sets a field member value.
        /// </summary>
        /// <param name="fieldName">The name of the member variable to hold the value.</param>
        /// <param name="value">The value to be set.</param>
        void SetFieldValue<T>(string fieldName, T value);

        /// <summary>
        /// Invokes the property setter on the given property name.
        /// </summary>
        /// <param name="propertyName">The name of the property.</param>
        /// <param name="value">The property value to set.</param>
        void SetPropertyValue<T>(string propertyName, object value);

        /// <summary>
        /// Gets or sets the value on the property.
        /// </summary>
        /// <param name="propertyName">Tha name of the property.</param>
        /// <returns></returns>
        object this[string propertyName] { get; set; }
    }
}
#endif
