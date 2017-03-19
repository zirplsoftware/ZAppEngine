using System;
using System.Collections.Generic;
using System.Linq;
using Zirpl.AppEngine.Model;
using Zirpl.AppEngine.Model.Metadata;
using Zirpl.AppEngine.Model.Extensibility;

namespace Zirpl.Examples.ContactManager.Model.Common
{
    public partial class Tag :
             IMetadataDescribed
            , IPersistable<long>
            , IAuditable
    {
        protected Tag()
        {
        }
        public virtual string Name { get; set; }
        public virtual string Description { get; set; }

        #region Interface implementations

        public virtual long Id { get; set; }
        public virtual byte[] RowVersion { get; set; }
        public virtual string CreatedUserId { get; set; }
        public virtual string UpdatedUserId { get; set; }
        public virtual DateTime? CreatedDate { get; set; }
        public virtual DateTime? UpdatedDate { get; set; }

        public virtual Object GetId()
        {
            return Id;
        }

        public virtual void SetId(Object id)
        {
            Id = (long)id;
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
        #endregion
    }
}