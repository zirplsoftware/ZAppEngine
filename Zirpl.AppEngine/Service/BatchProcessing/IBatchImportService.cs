using System;
using Zirpl.AppEngine.Model.BatchProcessing;

namespace Zirpl.AppEngine.Service.BatchProcessing
{
    public interface IBatchImportService
    {
        IBatch Import(Object batchData);
        IBatch ImportAsync(Object batchData);
    }
}
