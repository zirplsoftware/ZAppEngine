using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Zirpl.AppEngine.Model.Customization
{
    public abstract class CustomFieldDefinitionBase<TId> : EntityBase<TId>, ICustomFieldDefinition<TId>
        where TId : IEquatable<TId>
    {
        public virtual String ExtendedEntityTypeName { get; set; }
        public virtual String Label { get; set; }
        public virtual CustomFieldDefinitionTypeEnum Type { get; set; }
    }
}
