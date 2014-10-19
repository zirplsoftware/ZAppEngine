using System;
#if !NET35CLIENT
using System.ComponentModel.DataAnnotations;
#endif
#if !NET35 && !NET35CLIENT && !NET40 && !NET40CLIENT
using System.ComponentModel.DataAnnotations.Schema;
#endif

namespace Zirpl.AppEngine.Model
{
#if !SILVERLIGHT
    [Serializable]
#endif
    public abstract class EntityBase<TId> : IPersistable<TId>, IAuditable, IVersionable 
        where TId : IEquatable<TId> 
    {
        #region IPersistable

#if !NET35 && !NET35CLIENT
        [Key]
#endif
        public virtual TId Id { get; set; }

        public virtual Object GetId()
        {
            return Id;
        }

        public virtual void SetId(Object id)
        {
            Id = (TId)id;
        }

        //[ScaffoldColumn(false)]
#if !NET35 && !NET35CLIENT && !NET40 && !NET40CLIENT && !SILVERLIGHT
        [NotMapped]
#endif
        public virtual bool IsPersisted
        {
            get { return this.EvaluateIsPersisted(); }
        }

        #endregion

        #region IAuditable
        public virtual DateTime? CreatedDate { get; set; }
        public virtual String CreatedUserId { get; set; }
        public virtual DateTime? UpdatedDate { get; set; }
        public virtual String UpdatedUserId { get; set; }

        public virtual bool IsCreatedDateUsed { get { return true; } }
        public virtual bool IsCreatedUserIdUsed { get { return true; } }
        public virtual bool IsUpdatedDateUsed { get { return true; } }
        public virtual bool IsUpdatedUserIdUsed { get { return true; } }
        #endregion

        #region IVersionable
        public virtual byte[] RowVersion { get; set; }
        public virtual bool IsRowVersionUsed { get { return true; } }
        #endregion

        #region Object overrides
        public override bool Equals(object other)
        {
            return this.EvaluateEquals(other);
        }

        public override int GetHashCode()
        {
            return this.EvaluateGetHashCode();
        }
        #endregion
    }
}
