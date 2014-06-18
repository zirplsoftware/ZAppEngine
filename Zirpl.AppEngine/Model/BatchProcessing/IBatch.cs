using System;

namespace Zirpl.AppEngine.Model.BatchProcessing
{
    /// <summary>
    /// Represents the results of a batch import
    /// </summary>
    public interface IBatch
    {
        int TotalItemCount { get; set; }
        int CompletedItemCount { get; set; }
        int Status { get; set; }
        DateTime? StartDate { get; set; }
        DateTime? EndDate { get; set; }
    }
}
