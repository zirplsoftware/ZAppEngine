using Zirpl.AppEngine.Model.Metadata;

namespace Zirpl.AppEngine.VisualStudioAutomation.AppGeneration.Config.Parsing.Json
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
