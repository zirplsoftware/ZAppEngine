using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Zirpl.AppEngine.Model.Customization
{
    public class CustomFieldDefinition : AuditableBase<long>, ICustomFieldDefinition<long>
    {
        public virtual String ExtendedEntityTypeName { get; set; }
        public virtual String Label { get; set; }
        public virtual CustomFieldDefinitionType Type { get; set; }
    }
}
