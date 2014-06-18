using System;

namespace Zirpl.AppEngine.Model
{
    public interface IAuditable
    {
        DateTime? CreatedDate { get; set; }
        Guid? CreatedUserId { get; set; }
        DateTime? UpdatedDate { get; set; }
        Guid? UpdatedUserId { get; set; }

        bool IsCreatedDateUsed { get; }
        bool IsCreatedUserIdUsed { get; }
        bool IsUpdatedDateUsed { get; }
        bool IsUpdatedUserIdUsed { get; }
    }
}
