using System;

namespace Zirpl.AppEngine.Model.BatchProcessing
{
    public interface IBatchItemError
    {
        int Code { get; set; }
        String Description { get; set; }
    }
}
