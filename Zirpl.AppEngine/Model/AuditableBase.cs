using System;

namespace Zirpl.AppEngine.Model
{
    public abstract class AuditableBase<TId> : PersistableBase<TId>, IAuditable, IVersionable where TId : IEquatable<TId>
    {
        public const String CREATED_DATE_UTC_PROPERTY_NAME = "CreatedDate";
        public const String CREATED_USER_ID_PROPERTY_NAME = "CreatedUserId";
        public const String UPDATED_DATE_UTC_PROPERTY_NAME = "UpdatedDate";
        public const String UPDATED_USER_ID_PROPERTY_NAME = "UpdatedUserId";
        public const String ROW_VERSION_PROPERTY_NAME = "RowVersion";

        public virtual DateTime? CreatedDate { get; set; }
        public virtual Guid? CreatedUserId { get; set; }
        public virtual DateTime? UpdatedDate { get; set; }
        public virtual Guid? UpdatedUserId { get; set; }
        public virtual byte[] RowVersion { get; set; }

        public virtual bool IsRowVersionUsed { get { return true; } }
        public virtual bool IsCreatedDateUsed { get { return true; } }
        public virtual bool IsCreatedUserIdUsed { get { return true; } }
        public virtual bool IsUpdatedDateUsed { get { return true; } }
        public virtual bool IsUpdatedUserIdUsed { get { return true; } }
    }
}
