using System;
using System.Data.Entity.Core.Objects.DataClasses;

namespace Zirpl.AppEngine.Model.EntityFramework
{
    public abstract partial class PersistableEntityBase<TId> : EntityObject, IPersistable<TId> where TId : IEquatable<TId>
    {
        public abstract TId Id { get; set; }

    	public virtual bool IsPersisted { get { return this.EvaluateIsPersisted(); } }
        public virtual Object GetId() { return this.Id; }
        public virtual void SetId(Object id) { this.Id = (TId)id; }
    
    	public override bool Equals(Object other) { return this.EvaluateEquals(other); }
    	public override int GetHashCode() { return this.EvaluateGetHashCode(); }
    }
}
