using System;
using System.Collections.Generic;
using System.Linq;
using Zirpl.AppEngine.Model;
using Zirpl.AppEngine.Model.Metadata;
using Zirpl.AppEngine.Model.Extensibility;

namespace Zirpl.Examples.ContactManager.Model
{
    public partial class ProjectUrl :
             IMetadataDescribed
            , IPersistable<long>
            , IAuditable
    {
        protected ProjectUrl()
        {
        }
        public virtual string Url { get; set; }
        public virtual Zirpl.Examples.ContactManager.Model.Project Project { get; set; }
        public virtual long ProjectId { get; set; }

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