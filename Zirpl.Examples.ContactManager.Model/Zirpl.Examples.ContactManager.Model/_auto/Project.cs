using System;
using System.Collections.Generic;
using System.Linq;
using Zirpl.AppEngine.Model;
using Zirpl.AppEngine.Model.Metadata;
using Zirpl.AppEngine.Model.Extensibility;

namespace Zirpl.Examples.ContactManager.Model
{
    public partial class Project :
             IMetadataDescribed
            , IPersistable<long>
            , IAuditable
    {
        protected Project()
        {
            this.Tags = this.Tags ?? new System.Collections.Generic.List<Zirpl.Examples.ContactManager.Model.Common.Tag>();
            this.Images = this.Images ?? new System.Collections.Generic.List<Zirpl.Examples.ContactManager.Model.ProjectImage>();
            this.Urls = this.Urls ?? new System.Collections.Generic.List<Zirpl.Examples.ContactManager.Model.ProjectUrl>();
        }
        public virtual string Title { get; set; }
        public virtual string SubTitle { get; set; }
        public virtual ushort Year { get; set; }
        public virtual string Description { get; set; }
        public virtual string UrlSuffix { get; set; }
        public virtual System.Collections.Generic.IList<Zirpl.Examples.ContactManager.Model.Common.Tag> Tags { get; set; }
        public virtual System.Collections.Generic.IList<Zirpl.Examples.ContactManager.Model.ProjectImage> Images { get; set; }
        public virtual System.Collections.Generic.IList<Zirpl.Examples.ContactManager.Model.ProjectUrl> Urls { get; set; }

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