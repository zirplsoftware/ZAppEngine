using System;
using System.Collections;
using System.Collections.Generic;
using System.IO;

namespace Zirpl.AppEngine.DataService.Csv
{
    public class EntityExporter<TObject>
    {
        private const String TO_STRING_TEXT_FORMAT = "{0}";
        private const String TO_STRING_ACCOUNTING_FORMAT = "{0:#,##0.00}";
        private const String TO_STRING_PERCENT_FORMAT = "{0:P}";

        protected List<EntityPropertyDefinition<TObject>> ColumnDefinitions { get; private set; }
        private delegate void AsyncExportAsCsvFileCaller(IEnumerable enumerable, Stream stream);

        public Action<Exception> ErrorHandler { get; set; }
        public Action<bool> CompleteHandler { get; set; }
        
        public EntityExporter()
        {
            this.ColumnDefinitions = new List<EntityPropertyDefinition<TObject>>();
        }

        public void AddColumn(EntityPropertyDefinition<TObject> columnDefinition)
        {
            this.ColumnDefinitions.Add(columnDefinition);
        }
        public void AddColumn(String headerText, Func<TObject, Object> valueFunction)
        {
            this.ColumnDefinitions.Add(new EntityPropertyDefinition<TObject>() { HeaderText = headerText, ValueFunction = valueFunction });
        }
        public void AddColumn(String headerText, Func<TObject, Object> valueFunction, PropertyType propertyType)
        {
            this.ColumnDefinitions.Add(new EntityPropertyDefinition<TObject>() { HeaderText = headerText, ValueFunction = valueFunction, PropertyType = propertyType });
        }
        public void AddTextColumn(String headerText, Func<TObject, Object> valueFunction)
        {
            this.ColumnDefinitions.Add(new EntityPropertyDefinition<TObject>() { HeaderText = headerText, ValueFunction = valueFunction, PropertyType = PropertyType.TEXT });
        }
        public void AddAccountingColumn(String headerText, Func<TObject, Object> valueFunction)
        {
            this.ColumnDefinitions.Add(new EntityPropertyDefinition<TObject>() { HeaderText = headerText, ValueFunction = valueFunction, PropertyType = PropertyType.ACCOUNTING });
        }
        public void AddPercentColumn(String headerText, Func<TObject, Object> valueFunction)
        {
            this.ColumnDefinitions.Add(new EntityPropertyDefinition<TObject>() { HeaderText = headerText, ValueFunction = valueFunction, PropertyType = PropertyType.PERCENT });
        }

        #region Export as CSV file

        public void ExportAsCsvFile(IEnumerable enumerable, Stream file)
        {
            // TODO: test how this method performs with an exception

            // Initialize a writer
            var writer = new System.IO.StreamWriter(file);
            writer.AutoFlush = true;
            this.AppendCsvHeaderLine(writer);

            IEnumerator enumerator = enumerable.GetEnumerator();
            while (enumerator.MoveNext())
            {
                this.AppendCsvValueLine((TObject)enumerator.Current, writer);
            }
        }

        // TODO: this is not a viable method unless the calling method can get notified when finished
        // so that it can close the stream
        //
        //public virtual void ExportAsCsvFileAsync(IEnumerable enumerable, Stream file)
        //{
        //    AsyncExportAsCsvFileCaller caller = new AsyncExportAsCsvFileCaller(this.ExportAsCsvFile);

        //    // Initiate the asychronous call.
        //    IAsyncResult result = caller.BeginInvoke(enumerable, file, null, null);
        //}

        public void TryExportAsCsvFile(IEnumerable enumerable, Stream file)
        {
            // TODO: test how this method performs with an exception
            
            bool failed = false;
            Action<Exception> errorHandler = this.ErrorHandler;
            Action<Boolean> completeHandler = this.CompleteHandler;

            try
            {
                this.ExportAsCsvFile(enumerable, file);
            }
            catch (Exception e)
            {
                failed = true;
                if (errorHandler != null)
                {
                    try
                    {
                        errorHandler(e);
                    }
                    catch (Exception ex)
                    {
                        // nothing we can do about this- eat it
                    }
                }
            }
            finally
            {
                if (completeHandler != null)
                {
                    try
                    {
                        completeHandler(!failed);
                    }
                    catch (Exception ex)
                    {
                        // nothing we can do about this- eat it
                    }
                }
            }
        }

        // TODO: this is not a viable method unless the calling method can get notified when finished
        // so that it can close the stream
        //
        //public virtual void TryExportAsCsvFileAsync(IEnumerable enumerable, Stream file)
        //{
        //    AsyncExportAsCsvFileCaller caller = new AsyncExportAsCsvFileCaller(this.TryExportAsCsvFile);

        //    // Initiate the asychronous call.
        //    IAsyncResult result = caller.BeginInvoke(enumerable, file, null, null);
        //}

        #endregion

        public String CreateHeadersAndValuesText(TObject obj, String headerValueSeparators, String fieldSeparators, bool excludeEmptyValues)
        {
            String returnText = null;
            using (var writer = new StringWriter())
            {
                for (int i = 0; i < ColumnDefinitions.Count; i++)
                {
                    var columnDefinition = ColumnDefinitions[i];
                    Object value = columnDefinition.ValueFunction(obj);
                    String valueAsString = this.ConvertValueToString(value, columnDefinition.PropertyType);

                    if (!String.IsNullOrEmpty(valueAsString)
                        || !excludeEmptyValues)
                    {
                        writer.Write("{0}{1}{2}{3}", columnDefinition.HeaderText, headerValueSeparators, valueAsString, fieldSeparators);
                    }
                }
                returnText = writer.ToString();
            }
            return returnText;
        }

        private String ConvertPropertyTypeToFormatString(PropertyType propertyType)
        {
            String stringFormat = null;
            switch (propertyType)
            {
                case PropertyType.TEXT:
                    stringFormat = TO_STRING_TEXT_FORMAT;
                    break;
                case PropertyType.ACCOUNTING:
                    stringFormat = TO_STRING_ACCOUNTING_FORMAT;
                    break;
                case PropertyType.PERCENT:
                    stringFormat = TO_STRING_PERCENT_FORMAT;
                    break;
                default:
                    throw new ArgumentOutOfRangeException("propertyType", "Unsupported property type");
            }
            return stringFormat;
        }

        private String ConvertValueToString(Object value, PropertyType propertyType)
        {
            String valueAsString = null;

            if (value != null)
            {
                String formatString = this.ConvertPropertyTypeToFormatString(propertyType);
                if (!String.IsNullOrEmpty(formatString))
                {
                    valueAsString = String.Format(formatString, value);
                }
                else
                {
                    valueAsString = value.ToString();
                }
            }
            return valueAsString;
        }

        private void AppendCsvValueLine(TObject obj, TextWriter writer)
        {
            for (int i = 0; i < ColumnDefinitions.Count; i++)
            {
                var columnDefinition = ColumnDefinitions[i];
                Object value = columnDefinition.ValueFunction(obj);
                String valueAsString = this.ConvertValueToString(value, columnDefinition.PropertyType);
                this.AppendCsvField(valueAsString, writer);
            }
            writer.WriteLine();
        }

        private void AppendCsvHeaderLine(TextWriter writer)
        {
            for (int i = 0; i < ColumnDefinitions.Count; i++)
            {
                var columnDefinition = ColumnDefinitions[i];
                this.AppendCsvField(columnDefinition.HeaderText, writer);
            }
            writer.WriteLine();
        }

        private void AppendCsvField(String value, TextWriter writer)
        {
            value = value ?? "";
            bool containsCommas = value.Contains(",");
            if (containsCommas)
            {
                writer.Write("\"");
                bool containsQuotes = value.Contains("\"");
                if (containsQuotes)
                {
                    // make them double quotes
                    value = value.Replace("\"", "\"\"");
                }
            }
            writer.Write(value);
            if (containsCommas)
            {
                writer.Write("\"");
            }
            writer.Write(",");
        }
    }
}
