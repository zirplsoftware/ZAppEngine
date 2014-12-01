using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using Zirpl.AppEngine.Model.Metadata;

namespace Zirpl.AppEngine.CodeGeneration.V2.ConfigModel.DomainClasses.Properties
{
    public class RowVersionPropertyConfig : PropertyConfig
    {
        public RowVersionPropertyConfig()
        {
            this.DataType = DataTypeEnum.RowVersion;
        }
    }
}
