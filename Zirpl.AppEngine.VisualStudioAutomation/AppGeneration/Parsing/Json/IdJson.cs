using Zirpl.AppEngine.Model.Metadata;
using Zirpl.AppEngine.VisualStudioAutomation.AppGeneration.Model;

namespace Zirpl.AppEngine.VisualStudioAutomation.AppGeneration.Parsing.Json
{
    internal sealed partial class JsonTypes
    {
        public class IdJson
        {
            public DataTypeEnum? DataType { get; set; }
            public AutoGenerationBehaviorTypeEnum? AutoGenerationBehavior { get; set; }
            public bool? IsNullable { get; set; }
        }
    }
}
