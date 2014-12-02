using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using Zirpl.AppEngine.Model.Metadata;

namespace Zirpl.AppEngine.CodeGeneration.V2.ConfigModel.Parsers.JsonModel
{
    public class RelationshipJson
    {
        //relationship properties
        //
        public RelationshipTypeEnum? Type { get; set; }
        public RelationshipDeletionBehaviorTypeEnum? DeletionBehavior { get; set; }
        public String To { get; set; }
        public String ToPropertyName { get; set; }
        public bool? ToProperyIsRequired { get; set; }
        public UniquenessTypeEnum? ToPropertyUniqueness { get; set; }
    }
}
