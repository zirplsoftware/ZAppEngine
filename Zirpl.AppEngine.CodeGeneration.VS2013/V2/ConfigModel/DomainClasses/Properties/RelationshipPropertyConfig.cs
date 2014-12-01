using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using Zirpl.AppEngine.Model.Metadata;

namespace Zirpl.AppEngine.CodeGeneration.V2.ConfigModel
{
    public class RelationshipPropertyConfig : PropertyConfig
    {
        public RelationshipTypeEnum? RelationshipType { get; set; }
        public RelationshipDeletionBehaviorTypeEnum? RelationshipDeletionBehaviorType { get; set; }
        public String RelatedTo { get; set; }
        public String RelationshipReflexivePropertyName { get; set; }
    }
}
