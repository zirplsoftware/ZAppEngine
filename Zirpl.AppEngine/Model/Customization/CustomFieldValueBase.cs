using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Zirpl.AppEngine.Model.Customization
{
    public abstract class CustomFieldValueBase<TExtendedEntity, TId> : EntityBase<TId>, ICustomFieldValue<TExtendedEntity, TId>
        where TExtendedEntity : IPersistable<TId>
        where TId : IEquatable<TId>
    {
        public virtual String Value { get; set; }
        public virtual TExtendedEntity ExtendedEntity { get; set; }
        public virtual  TId ExtendedEntityId { get; set; }

        public virtual Object GetExtendedEntityId()
        {
            return this.ExtendedEntityId;
        }
        public virtual Object GetExtendedEntity()
        {
            return this.ExtendedEntity;
        }
    }
}
