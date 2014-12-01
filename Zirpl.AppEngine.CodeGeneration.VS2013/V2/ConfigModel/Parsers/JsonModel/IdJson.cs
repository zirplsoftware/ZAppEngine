using Zirpl.AppEngine.Model.Metadata;

namespace Zirpl.AppEngine.CodeGeneration.V2.ConfigModel.Parsers.JsonModel
{
    public class IdJson
    {
        public DataTypeEnum? DataType { get; set; }
        public AutoGenerationBehaviorTypeEnum? AutoGenerationBehavior { get; set; }
        public bool? IsNullable { get; set; }
    }
}
