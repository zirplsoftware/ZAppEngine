using System;
using Zirpl.AppEngine.Model.Metadata;
using Zirpl.AppEngine.VisualStudioAutomation.AppGeneration.Model;

namespace Zirpl.AppEngine.VisualStudioAutomation.AppGeneration.Parsing.Json
{
    internal sealed partial class JsonTypes
    {
        public class PropertyJson
        {
            public String Name { get; set; }
            public DataTypeEnum? DataType { get; set; }
            public bool? IsNullable { get; set; }
            public bool? IsRequired { get; set; }
            public bool? IsMaxLength { get; set; }
            public String MinLength { get; set; }
            public String MaxLength { get; set; }
            public String MinValue { get; set; }
            public String MaxValue { get; set; }
            public String Precision { get; set; }
            public UniquenessTypeEnum? Uniqueness { get; set; }

            public RelationshipJson Relationship { get; set; }
        }
    }
}
