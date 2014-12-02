using System;

namespace Zirpl.AppEngine.Model.Extensibility
{
    public abstract class CustomFieldDefinitionBase<TId> : EntityBase<TId>, ICustomFieldDefinition<TId>
        where TId : IEquatable<TId>
    {
        public virtual String ExtendedEntityTypeName { get; set; }
        public virtual String Label { get; set; }
        public virtual CustomFieldDefinitionTypeEnum Type { get; set; }
    }
}
