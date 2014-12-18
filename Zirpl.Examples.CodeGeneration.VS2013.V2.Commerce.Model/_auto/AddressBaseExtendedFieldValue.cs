using System;
using System.Collections.Generic;
using System.Linq;
using Zirpl.AppEngine.Model;
using Zirpl.AppEngine.Model.Metadata;
using Zirpl.AppEngine.Model.Extensibility;
using Zirpl.Collections;

namespace Zirpl.Examples.Commerce.Model
{
    public partial class AddressBaseExtendedFieldValue : System.Object
            , IMetadataDescribed
            , IPersistable<Guid>
            , IExtendedEntityFieldValue<AddressBaseExtendedFieldValue, AddressBase, Guid>
    {
        public virtual Guid Id { get; set; }
        public virtual byte[] RowVersion { get; set; }
        public virtual Zirpl.Examples.Commerce.Model.AddressBase ExtendedEntity { get; set; }
        public virtual Guid ExtendedEntityId { get; set; }
        public virtual string Value { get; set; }

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
        public virtual object GetExtendedEntityId()
        {
            return this.ExtendedEntityId;
        }

        public virtual IExtensible GetExtendedEntity()
        {
            return this.ExtendedEntity;
        }

        public virtual void SetExtendedEntityId(object id)
        {
            this.ExtendedEntityId = (Guid)id;
        }

        public virtual void SetExtendedEntity(IExtensible entity)
        {
            this.ExtendedEntity = (Zirpl.Examples.Commerce.Model.AddressBase)entity;
        }

        #endregion
    }
}
