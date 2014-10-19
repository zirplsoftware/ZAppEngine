using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Zirpl.AppEngine.Model.Customization
{
    public interface ICustomFieldDefinition<TId> : IPersistable<TId>, ICustomFieldDefinition where TId : IEquatable<TId>
    {
    }

    public interface ICustomFieldDefinition : IPersistable
    {
        String ExtendedEntityTypeName { get; set; }
        String Label { get; set; }
        CustomFieldDefinitionTypeEnum Type { get; set; }
    }
}
