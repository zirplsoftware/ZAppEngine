using Zirpl.AppEngine.Model.BatchProcessing;

namespace Zirpl.AppEngine.Service.BatchProcessing
{
    /// <summary>
    /// Strategy for handling the import of MedicalEvents
    /// </summary>
    public interface IBatchImportStrategy
    {
        /// <summary>
        /// </summary>
        void ImportItem(IBatch batch, IBatchItem batchItem, object itemData);

        IBatch CreateBatch();

        IBatchItem CreateBatchItem(IBatch batch);

        IBatchItemError CreateBatchItemError(IBatch batch, IBatchItem batchItem);
    }
}
