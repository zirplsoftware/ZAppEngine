using System;
using System.Collections.Generic;
using System.Data;
using System.Data.OleDb;
using System.Globalization;
using System.Linq;
using System.Text;
using Zirpl.AppEngine.Mapping;

namespace Zirpl.AppEngine.Data.Excel
{
    /// <summary>
    /// This class can be used to import data from an Excel file (either .xls or .xlsx)
    /// </summary>
    /// <typeparam name="TData">A data structure the excel data will be mapped to</typeparam>
    public class ExcelFileReader<TData> : IDisposable
    {
        public ExcelFileReader(String connectionString, IRowMapper<TData> rowMapper, IList<String> columnNames = null, String excelSheetName = null, int headerRowCount = 1)
        {
            if (String.IsNullOrEmpty(connectionString))
            {
                throw new ArgumentNullException("connectionString");
            }
            if (rowMapper == null)
            {
                throw new ArgumentNullException("rowMapper");
            }
            if (headerRowCount < 1)
            {
                throw new ArgumentOutOfRangeException("headerRowCount", "headerRowCount must be greater than or equal to 1");
            }

            this._columnNames = columnNames ?? new List<string>();
            this.ConnectionString = connectionString;
            this._rowMapper = rowMapper;
            this.ExcelSheetName = excelSheetName;
            this.HeaderRowCount = headerRowCount;
            this.TotalRowCount = -1;
        }

        private OleDbConnection _connection;
        private OleDbDataReader _reader;
        private bool _hasCurrent;
        private int _rowIndex = 0;
        private IRowMapper<TData> _rowMapper;
        private IList<String> _columnNames;

        /// <summary>
        /// Gets the ConnectionString to use to access the Excel file
        /// </summary>
        public string ConnectionString { get; private set; }

        /// <summary>
        /// Indicates how many extra header rows (beyond the top header row) exist in the file and should be skipped
        /// </summary>
        public int HeaderRowCount { get; private set; }

        /// <summary>
        /// Name of the sheet to import from.  This can be set or injected.
        /// </summary>
        public string ExcelSheetName { get; private set; }

        /// <summary>
        /// Total count of all rows to be imported, populated after OpenReader()
        /// </summary>
        public int TotalRowCount { get; private set; }

        /// <summary>
        /// Gets the list of column names that the 
        /// </summary>
        public String[] ColumnNames { get { return this._columnNames == null ? new string[0] : this._columnNames.ToArray(); }}
        

        /// <summary>
        /// Data structure containing the currently mapped value from the reader
        /// </summary>
        public TData CurrentData
        {
            get
            {
                TData data = default(TData);
                if (_hasCurrent)
                {
                    try
                    {
                        data = this._rowMapper.MapRow(_reader, _rowIndex + this.HeaderRowCount);
                    }
                    catch (Exception ex)
                    {
                        throw new ExcelFileReaderException("Error reading the file. ", ex);
                    }
                }

                return data;
            }
        }

        public bool MoveNext()
        {
            try
            {
                // the first read is actually rowIndex 1 as the headers are skipped
                _hasCurrent = _reader.Read();
                _rowIndex = _hasCurrent ? _rowIndex + 1 : -1;
                while (_hasCurrent
                    && _rowIndex < this.HeaderRowCount)
                {
                    _hasCurrent = _reader.Read();
                    _rowIndex = _hasCurrent ? _rowIndex + 1 : -1;
                }
            }
            catch (Exception ex)
            {
                throw new ExcelFileReaderException("Error reading the file. ", ex);
            }
            return _hasCurrent;
        }
        


        /// <summary>
        /// Open the connection to the Excel file to start the reading process
        /// </summary>
        public void OpenReader()
        {
            if (_reader != null 
                || _connection != null)
            {
                throw new InvalidOperationException("Reader or Connection have already been opened");
            }

            try
            {
                _connection = new OleDbConnection(this.ConnectionString);
                _connection.Open();
                string whereClause = GetWhereClause();
                string countSql = "Select count(*) from [{0}]" + whereClause;
                DataTable schema = _connection.GetOleDbSchemaTable(OleDbSchemaGuid.Tables, new object[] { null, null, null, "TABLE" });
                if (String.IsNullOrEmpty(this.ExcelSheetName))
                {
                    ExcelSheetName = schema.Rows[0]["TABLE_NAME"].ToString();
                }
                string selectSql = "Select * from [{0}]" + whereClause;
                schema = _connection.GetOleDbSchemaTable(OleDbSchemaGuid.Columns, null);
                VerifySchema(schema);

                OleDbCommand command = new OleDbCommand(String.Format(CultureInfo.InvariantCulture, countSql, ExcelSheetName), _connection);
                object objCount = command.ExecuteScalar();
                TotalRowCount = Convert.ToInt32(objCount, CultureInfo.InvariantCulture);

                command.CommandText = String.Format(CultureInfo.InvariantCulture, selectSql, ExcelSheetName);
                _reader = command.ExecuteReader();
            }
            catch (InvalidMappingDataException)
            {
                if (_connection != null)
                {
                    try
                    {
                        _connection.Close();
                    }
                    catch
                    {
                    }
                    finally
                    {
                        _connection = null;
                    }
                }
                throw;
            }
            catch (Exception ex)
            {
                if (_connection != null)
                {
                    try
                    {
                        _connection.Close();
                    }
                    catch
                    {
                    }
                    finally
                    {
                        _connection = null;
                    }
                }
                throw new ExcelFileReaderException("Error Opening Reader", ex);
            }
        }


        private void VerifySchema(DataTable schema)
        {
            IList<string> columns = new List<string>();
            foreach (DataRow row in schema.Rows)
            {
                columns.Add(row[3].ToString());
            }

            StringBuilder columnsMissing = new StringBuilder();
            
            foreach (string key in this._columnNames)
            {
                if (!columns.Contains(key))
                {
                    columnsMissing.Append(key + ", ");
                }
            }

            if (columnsMissing.Length > 0)
            {
                columnsMissing.Remove(columnsMissing.Length - 2, 2);
                throw new InvalidMappingDataException(columnsMissing.ToString());
            }

        }

        private string GetWhereClause()
        {
            StringBuilder whereClause = new StringBuilder();
            if (this._columnNames != null
                && this._columnNames.Count > 0)
            {
                whereClause.Append(" WHERE ");
                foreach (string key in this._columnNames)
                {
                    if (key.IndexOf(' ') > -1)
                    {
                        whereClause.Append("[");
                        whereClause.Append(key);
                        whereClause.Append("]");
                    }
                    else
                    {
                        whereClause.Append(key);
                    }
                    whereClause.Append(" is not null OR ");
                }
                whereClause.Remove(whereClause.Length - 4, 3);
            }
            return whereClause.ToString();
        }

        #region IDisposable

        public void Dispose()
        {
            Dispose(true);
            GC.SuppressFinalize(this);
        }

        protected virtual void Dispose(bool disposing)
        {
            if (disposing)
            {
                try
                {
                    if (_connection != null)
                    {
                        _connection.Dispose();
                    }
                }
                catch (Exception)
                {
                    //eat the error to ensure GC isn't disrupted
                }
                try
                {
                    if (_reader != null)
                    {
                        _reader.Dispose();
                    }
                }
                catch (Exception)
                {
                    //eat the error to ensure GC isn't disrupted
                }
            }

        }

        ~ExcelFileReader()
        {
            Dispose(false);
        }
        #endregion
    }
}
