using System;
using System.Data;

namespace Zirpl.AppEngine.Data
{
    /// <summary>
    /// Base class for a RowMapper
    /// </summary>
    public abstract class RowMapperBase<TEntity> :IRowMapper<TEntity>
    {
        /// <summary>
        /// Creates the appropriate data object for the row
        /// </summary>
        /// <param name="reader">The data reader with the row's info</param>
        /// <param name="rowNumber">The row number being read</param>
        /// <returns>The data object</returns>
        protected virtual TEntity CreateDataObject(IDataReader reader, int rowNumber)
        {
            // TODO: this requires new() but that seems to cause spring problems on down the line
            return (TEntity)Activator.CreateInstance(typeof (TEntity));
        }

        #region IRowMapper Members

        /// <summary>
        /// Maps the row to the data object
        /// </summary>
        /// <param name="reader">The data reader with the row's info</param>
        /// <param name="rowNumber">The row number being read</param>
        /// <returns>The data object</returns>
        public virtual TEntity MapRow(IDataReader reader, int rowNum)
        {
            return this.CreateDataObject(reader, rowNum);
        }

        #endregion
    }
}
