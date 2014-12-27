//using System;
//using System.Collections;
//using System.Collections.Generic;
//using System.Linq;
//using System.Text;
//using System.Threading.Tasks;
//using Zirpl.AppEngine.DataService.Csv;
//using Zirpl.AppEngine.EntityToCsv;

//namespace Zirpl.AppEngine.DataService.Excel
//{
//    public class ExcelExporter<TObject> : EntityExporter<TObject>
//    {
//        private const String EXCEL_TEXT_FORMAT = "@";
//        private const String EXCEL_ACCOUNTING_FORMAT = "$#,##0.00;$-#,##0.00";
//        private const String EXCEL_PERCENT_FORMAT = "#,##0%";


//        public ExcelExporter()
//            : base()
//        {
//        }


//        // TODO: this is not a viable method unless the calling method can get notified when finished
//        // so that it can close the stream
//        //
//        //public override void ExportAsCsvFileAsync(IEnumerable enumerable, Stream file)
//        //{
//        //    Dispatchers.Current.BeginInvoke(() =>
//        //    {
//        //        this.ExportAsCsvFile(enumerable, file);
//        //    });
//        //}

//        // TODO: this is not a viable method unless the calling method can get notified when finished
//        // so that it can close the stream
//        //
//        //public override void TryExportAsCsvFileAsync(IEnumerable enumerable, Stream file)
//        //{
//        //    Dispatchers.Current.BeginInvoke(() =>
//        //    {
//        //        this.TryExportAsCsvFile(enumerable, file);
//        //    });
//        //}

//        public void ExportAsExcelFile(IEnumerable enumerable)
//        {
//            // TODO: test how this method performs with an exception

//            dynamic excel = AutomationFactory.CreateObject("Excel.Application");
//            excel.workbooks.Add();
//            dynamic sheet = excel.ActiveSheet;

//            for (int i = 0; i < ColumnDefinitions.Count; i++)
//            {
//                var columnDefinition = ColumnDefinitions[i];
//                sheet.Cells[1, i + 1] = columnDefinition.HeaderText;
//                String numberFormat = this.ConvertPropertyTypeToExcelNumberFormat(columnDefinition.PropertyType);
//                if (!String.IsNullOrWhiteSpace(numberFormat))
//                {
//                    sheet.Cells[1, i + 1].EntireColumn.NumberFormat = numberFormat;
//                }
//                //sheet.Cells[1, i + 1].Font.Bold = true;
//            }

//            int rowIndex = 1;
//            IEnumerator enumerator = enumerable.GetEnumerator();
//            while (enumerator.MoveNext())
//            {
//                rowIndex++;
//                for (int i = 0; i < ColumnDefinitions.Count; i++)
//                {
//                    sheet.Cells[rowIndex, i + 1] = ColumnDefinitions[i].ValueFunction((TObject)enumerator.Current);
//                }
//            }

//            excel.Visible = true;
//            // excel.Save(object filename);
//        }

//        public void ExportAsExcelFileAsync(IEnumerable enumerable)
//        {
//            // TODO: test how this method performs with an exception

//            Dispatchers.Current.BeginInvoke(() =>
//            {
//                this.ExportAsExcelFile(enumerable);
//            });
//        }

//        public void TryExportAsExcelFile(IEnumerable enumerable)
//        {
//            // TODO: test how this method performs with an exception

//            ActionRunner runner = new ActionRunner();
//            runner.Action = () => this.ExportAsExcelFile(enumerable);
//            runner.CompleteHandler = this.CompleteHandler;
//            runner.ErrorHandler = this.ErrorHandler;
//            runner.TryAction();
//        }

//        public void TryExportAsExcelFileAsync(IEnumerable enumerable)
//        {
//            // TODO: test how this method performs with an exception

//            Dispatchers.Current.BeginInvoke(() =>
//            {
//                this.TryExportAsExcelFile(enumerable);
//            });
//        }

//        public void SaveAsCsvFile(IEnumerable enumerable)
//        {
//            // THIS METHOD IS SAFE EVEN WITH EXCEPTIONS
//            //
//            System.IO.Stream stream = null;

//            try
//            {
//                // SaveFileDialog() must be opened on the UI thread
//                Dispatchers.Main.Invoke(() =>
//                {
//                    System.Windows.Controls.SaveFileDialog dlg = new System.Windows.Controls.SaveFileDialog();
//                    dlg.Filter = "CSV Files (*.csv)|*.csv|Text Files (*.txt)|*.txt";
//                    dlg.DefaultExt = "csv";

//                    if (dlg.ShowDialog() == true)
//                    {
//                        stream = dlg.OpenFile();
//                    }
//                });

//                if (stream != null)
//                {
//                    this.ExportAsCsvFile(enumerable, stream);
//                }

//                // for testing only
//                //throw new Exception("blah blah");
//            }
//            catch
//            {
//                throw;
//            }
//            finally
//            {
//                if (stream != null)
//                {
//                    Dispatchers.Main.Invoke(() =>
//                    {
//                        // Need to close the file on the UI thread as well
//                        //
//                        try
//                        {
//                            stream.Close();
//                        }
//                        catch (Exception e)
//                        {
//                            // nothing we can do about this- eat it
//                        }
//                    });
//                }
//            }
//        }

//        public void SaveAsCsvFileAsync(IEnumerable enumerable)
//        {
//            // THIS METHOD IS SAFE EVEN WITH EXCEPTIONS
//            //
//            Dispatchers.Current.BeginInvoke(() =>
//            {
//                this.SaveAsCsvFile(enumerable);
//            });
//        }

//        public void TrySaveAsCsvFile(IEnumerable enumerable)
//        {
//            // THIS METHOD IS SAFE EVEN WITH EXCEPTIONS
//            //
//            ActionRunner runner = new ActionRunner();
//            runner.Action = () => this.SaveAsCsvFile(enumerable);
//            runner.CompleteHandler = this.CompleteHandler;
//            runner.ErrorHandler = this.ErrorHandler;
//            runner.TryAction();
//        }

//        public void TrySaveAsCsvFileAsync(IEnumerable enumerable)
//        {
//            // THIS METHOD IS SAFE EVEN WITH EXCEPTIONS
//            //
//            Dispatchers.Current.BeginInvoke(() =>
//            {
//                this.TrySaveAsCsvFile(enumerable);
//            });
//        }

//        private String ConvertPropertyTypeToExcelNumberFormat(PropertyType propertyType)
//        {
//            String stringFormat = null;
//            switch (propertyType)
//            {
//                case PropertyType.TEXT:
//                    stringFormat = EXCEL_TEXT_FORMAT;
//                    break;
//                case PropertyType.ACCOUNTING:
//                    stringFormat = EXCEL_ACCOUNTING_FORMAT;
//                    break;
//                case PropertyType.PERCENT:
//                    stringFormat = EXCEL_PERCENT_FORMAT;
//                    break;
//                default:
//                    throw new ArgumentOutOfRangeException("propertyType", "Unsupported property type");
//            }
//            return stringFormat;
//        }
//    }
//}
