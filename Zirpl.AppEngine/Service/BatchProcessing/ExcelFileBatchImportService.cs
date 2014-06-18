using System;
using System.Collections.Generic;
using System.Globalization;
using System.IO;
using System.Threading;
using Zirpl.AppEngine.Data;
using Zirpl.AppEngine.Data.Excel;
using Zirpl.AppEngine.Logging;
using Zirpl.AppEngine.Mapping;
using Zirpl.AppEngine.Model.BatchProcessing;

namespace Zirpl.AppEngine.Service.BatchProcessing
{
    public class ExcelFileBatchImportService<TImportData, TBatch, TBatchId, TBatchItem, TBatchItemId> :
        IBatchImportService
        where TBatch : IBatch
        where TBatchItem : IBatchItem
    {
        public IBatchImportStrategy Strategy { get; set; }
        public ISupportsSave<TBatch> BatchService { get; set; }
        public ISupportsSave<TBatchItem> BatchItemService { get; set; }
        public StringReflectedRowMapper<TImportData> RowMapper { get; set; }
        public string ExcelFileReaderConnectionStringTemplate { get; set; }
        public String ExcelSheetName { get; set; }
        public int HeaderRowCount { get; set; }
        public IList<String> ColumnNames { get; set; }
        public bool DeleteFileOnComplete { get; set; }
        public IDictionary<Int32, String> ValidationErrorMessageMap { get; set; }


        public virtual IBatch Import(object batchData)
        {
            return this.DoImport((String)batchData, false);
        }

        public virtual IBatch ImportAsync(object batchData)
        {
            return this.DoImport((String)batchData, true);
        }

        protected virtual TBatch DoImport(String filePath, bool async)
        {
            if (String.IsNullOrEmpty(filePath))
            {
                throw new ArgumentNullException("filePath", "filePathObject Parameter must be a string containing Excel FilePath.");
            }

            ExcelFileReader<TImportData> reader = null;
            TBatch batch = default(TBatch);

            try
            {
                this.GetLog().Debug("Initializing Batch");
                batch = (TBatch)this.Strategy.CreateBatch();
                batch.Status = (int) BatchStatus.InProgress;
                batch.StartDate = DateTime.Now;
                this.BatchService.Save(batch);

                this.GetLog().DebugFormat("Creating ExcelFileReader for file: {0}", filePath);
                reader = new ExcelFileReader<TImportData>(
                    String.Format(CultureInfo.InvariantCulture, this.ExcelFileReaderConnectionStringTemplate, (String)filePath),
                    this.RowMapper,
                    this.ColumnNames,
                    this.ExcelSheetName,
                    this.HeaderRowCount);
                reader.OpenReader();
                batch.TotalItemCount = reader.TotalRowCount;
            }
            catch (InvalidMappingDataException invalidMappingException)
            {
                this.GetLog().TryInfo(invalidMappingException, "An error occured as the file was not in the right format.");
                if (batch != null)
                {
                    batch.Status = (int) BatchStatus.Failed;
                }
                else
                {
                    this.GetLog().TryWarn("Batch is null. There is no way to persist the results of this Batch Import.");
                }
                throw;
            }
            catch (Exception ex)
            {
                this.GetLog().TryInfo(ex, "An error occurred attempting to start the import process.");
                if (batch != null)
                {
                    batch.Status = (int) BatchStatus.Failed;
                }
                else
                {
                    this.GetLog().TryWarn("Batch is null. There is no way to persist the results of this Batch Import.");
                }
                try
                {
                    if (reader != null)
                    {
                        reader.Dispose();
                    }
                }
                catch
                {
                    // okay to eat this exception- we are just cleaning up
                }
                try
                {
                    if (this.DeleteFileOnComplete
                        && File.Exists((String)filePath))
                    {
                        File.Delete((String)filePath);
                    }
                }
                catch
                {
                    // okay to eat this, we are just cleaning up
                }
            }

            if (batch == null)
            {
                this.GetLog().Warn("Batch is null. There is no way to persist the results of this Batch Import.");
            }
            else if (batch.Status != (int)BatchStatus.InProgress)
            {
                try
                {
                    this.BatchService.Save(batch);
                }
                catch (Exception ex)
                {
                    this.GetLog().TryWarn("Could not Save Batch. There is no way to update the results of this Batch Import.");
                }
            }
            else
            {
                if (async)
                {
                    ThreadStart threadStart =
                        delegate { ProcessImportThread(filePath, reader, batch); };
                    Thread thread = new Thread(threadStart);
                    thread.Start();
                }
                else
                {
                    // leave try/catch in ProcessImportThread because it needs to be used in this case
                    // AND case of new thread above, so obvious place of try/catch here cannot always suffice
                    //
                    this.ProcessImportThread(filePath, reader, batch);
                }
            }
            return batch;
        }

        protected virtual void ProcessImportThread(object filePathObject, object importFileReaderObject, object batchObject)
        {
            ExcelFileReader<TImportData> reader = (ExcelFileReader<TImportData>)importFileReaderObject;
            TBatch batch = (TBatch)batchObject;

            try
            {
                int i = -1;
                while (reader.MoveNext())
                {
                    i++;
                    this.GetLog().DebugFormat("Process BatchItem {0}", i);
                    this.GetLog().Debug("Initializing BatchItem");
                    TBatchItem item = (TBatchItem)this.Strategy.CreateBatchItem(batch);
                    item.Batch = batch;
                    try
                    {
                        this.GetLog().Debug("Importing BatchItem");
                        this.Strategy.ImportItem(batch, item, reader.CurrentData);

                        this.GetLog().Debug("Import BatchItem Succeeded");
                        item.Result = (int)BatchItemResult.Succeeded;
                    }
                    catch (ImportValidationException validationException)
                    {
                        this.GetLog().TryDebug(validationException, "Import BatchItem Failed");
                        item.Result = (int)BatchItemResult.Failed;
                        foreach (int error in validationException.Errors)
                        {
                            var batchItemError = this.Strategy.CreateBatchItemError(batch, item);
                            batchItemError.Code = error;
                            if (this.ValidationErrorMessageMap != null)
                            {
                                batchItemError.Description = this.ValidationErrorMessageMap.ContainsKey(error)
                                                                          ? this.ValidationErrorMessageMap[error]
                                                                          : null;
                                item.Errors.Add(batchItemError);
                            }
                        }
                    }
                    catch (Exception ex)
                    {
                        this.GetLog().TryDebug(ex, "Import BatchItem Failed");
                        item.Result = (int)BatchItemResult.Failed;
                        if (item.Errors == null)
                        {
                            item.Errors = new List<IBatchItemError>();
                        } 
                        var batchItemError = this.Strategy.CreateBatchItemError(batch, item);
                        batchItemError.Code = (int) CoreBatchItemErrorCode.ExceptionThrown;
                        batchItemError.Description = ex.ToString();
                        item.Errors.Add(batchItemError);
                    }
                    finally
                    {
                        batch.CompletedItemCount += 1;

                        // TODO: these should happen in 1 transaction
                        //
                        this.BatchItemService.Save(item);
                        this.BatchService.Save(batch);
                    }
                }
                batch.Status = (int)BatchStatus.Succeeded;
            }
            catch (Exception ex)
            {
                batch.Status = (int)BatchStatus.Failed;
                this.GetLog().TryInfoFormat(ex, "An error occurred attempting to import from '{0}'", filePathObject);
            }
            finally
            {
                // this in a try/catch as we do not want to accidentally miss
                // our cleanup down below if this should fail
                try
                {
                    batch.EndDate = DateTime.Now;
                    this.BatchService.Save(batch);
                }
                catch (Exception ex)
                {
                    this.GetLog().TryErrorFormat(ex, "Batch failed to save '{0}'", filePathObject);
                }
                try
                {
                    if (reader != null)
                    {
                        reader.Dispose();
                    }
                }
                catch
                {
                    // okay to eat this exception- we are just cleaning up
                }
                try
                {
                    if (this.DeleteFileOnComplete
                        && File.Exists((String)filePathObject))
                    {
                        File.Delete((String)filePathObject);
                    }
                }
                catch
                {
                    // okay to eat this, we are just cleaning up
                }
            }

        }
    }
}
