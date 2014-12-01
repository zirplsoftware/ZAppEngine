using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using Zirpl.AppEngine.Model.Metadata;

namespace Zirpl.AppEngine.CodeGeneration.V2.Parsers.JsonModel
{
    public class IdJson
    {
        public DataTypeEnum? DataType { get; set; }
        public AutoGenerationBehaviorTypeEnum? AutoGenerationBehavior { get; set; }
        public bool? IsNullable { get; set; }
    }
}
