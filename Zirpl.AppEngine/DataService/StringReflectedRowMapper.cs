using System;
using System.Collections.Generic;
using System.Data;
using System.Reflection;
using Zirpl.AppEngine.Mapping;

namespace Zirpl.AppEngine.DataService
{
    /// <summary>
    /// This row mapper takes a Dictionary that maps columns from a datareader
    /// to properties on an object. It uses reflection to set the value in the reader
    /// to the value on the object. All properties must be of type string
    /// </summary>
    /// <typeparam name="TData">Data Type to map values to</typeparam>
    public class StringReflectedRowMapper<TData> : RowMapperBase<TData>
    {
        /// <summary>
        /// Maps the row to data object
        /// </summary>
        public override TData MapRow(IDataReader reader, int rowNum)
        {
            TData dataObject = base.MapRow(reader, rowNum);
            
            foreach (KeyValuePair<string, string> pair in ColumnPropertyMap)
            {
                try
                {
                    PropertyInfo info = typeof(TData).GetProperty(pair.Value);
                    object objKey = reader[pair.Key];
                    info.SetValue(dataObject, (objKey == null || objKey == DBNull.Value) ? null : objKey.ToString(), null);
                }
                catch (Exception ex)
                {
                    InvalidMappingDataException invalid = new InvalidMappingDataException(String.Empty, ex);
                    invalid.MappingField = pair.Value;
                    throw invalid;
                }
            }
            return dataObject;

        }

        /// <summary>
        /// Map to determine the property on the data structure (key) and the excel column name (value)
        /// </summary>
        public IDictionary<string, string> ColumnPropertyMap { get; set; }
    }
}
