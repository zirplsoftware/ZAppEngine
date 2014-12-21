using System;
using System.Collections.Generic;
using System.Linq;
using Zirpl.AppEngine.Model;
using Zirpl.AppEngine.Model.Metadata;
using Zirpl.AppEngine.Model.Extensibility;
using Zirpl.Collections;

namespace Zirpl.Examples.Commerce.Model
{
    public abstract partial class AddressBase : System.Object
            , IMetadataDescribed
            , IPersistable<Guid>
            , IAuditable
            , IExtensible<AddressBase, AddressBaseExtendedFieldValue, Guid>
    {
        protected AddressBase()
        {
            this.ExtendedFieldValues = this.ExtendedFieldValues ?? new System.Collections.Generic.List<Zirpl.Examples.Commerce.Model.AddressBaseExtendedFieldValue>();
        }
        public virtual Guid Id { get; set; }
        public virtual byte[] RowVersion { get; set; }
        public virtual string CreatedUserId { get; set; }
        public virtual string UpdatedUserId { get; set; }
        public virtual DateTime? CreatedDate { get; set; }
        public virtual DateTime? UpdatedDate { get; set; }
        public virtual System.Collections.Generic.IList<Zirpl.Examples.Commerce.Model.AddressBaseExtendedFieldValue> ExtendedFieldValues { get; set; }

        #region Interface implementations

        public virtual Object GetId()
        {
            return Id;
        }

        public virtual void SetId(Object id)
        {
            Id = (Guid)id;
        }

        public virtual bool IsPersisted
        {
            get { return this.EvaluateIsPersisted(); }
        }

        public override bool Equals(object other)
        {
            return this.EvaluateEquals(other);
        }

        public override int GetHashCode()
        {
            return this.EvaluateGetHashCode();
        }
        public virtual IList<IExtendedEntityFieldValue> GetExtendedFieldValues()
        {
            return this.ExtendedFieldValues.Cast<IExtendedEntityFieldValue>().ToList();
        }
        public virtual void SetExtendedFieldValues(IList<IExtendedEntityFieldValue> list)
        {
            this.ExtendedFieldValues.Clear();
            this.ExtendedFieldValues.AddRange(list.Cast<Zirpl.Examples.Commerce.Model.AddressBaseExtendedFieldValue>());
        }
        #endregion
    }
}