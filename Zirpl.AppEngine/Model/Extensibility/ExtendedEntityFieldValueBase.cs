using System;

namespace Zirpl.AppEngine.Model.Extensibility
{
    public abstract class ExtendedEntityFieldValueBase<TExtendedEntity, TId> : EntityBase<TId>, IExtendedEntityFieldValue<TExtendedEntity, TId>
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
