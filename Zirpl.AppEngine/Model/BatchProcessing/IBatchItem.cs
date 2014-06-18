using System;
using System.Collections.Generic;

namespace Zirpl.AppEngine.Model.BatchProcessing
{
    /// <summary>
    /// Represents the status of a single imported record
    /// </summary>
    public interface IBatchItem
    {
        IBatch Batch { get; set; }
        String ItemKey { get; set; }
        int Result { get; set; }
        IList<IBatchItemError> Errors { get; set; }
    }
}
