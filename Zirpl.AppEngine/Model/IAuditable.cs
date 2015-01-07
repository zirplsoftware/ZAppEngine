using System;

namespace Zirpl.AppEngine.Model
{
    public interface IAuditable
    {
        String CreatedUserId { get; set; }
        String UpdatedUserId { get; set; }
        DateTime? CreatedDate { get; set; }
        DateTime? UpdatedDate { get; set; }
    }
}
