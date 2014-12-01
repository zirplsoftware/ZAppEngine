using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using Zirpl.AppEngine.Model.Metadata;

namespace Zirpl.AppEngine.CodeGeneration.V2.ConfigModel
{
    public abstract class PropertyConfig
    {
        protected PropertyConfig()
        {
            this.DataType = DataTypeEnum.String;
        }

        public String Name { get; set; }
        public DataTypeEnum DataType { get; set; }
    }
}
