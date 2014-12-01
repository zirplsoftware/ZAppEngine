using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Zirpl.AppEngine.Model.Metadata
{
    public enum AutoGenerationBehaviorTypeEnum : byte
    {
        None = 0,
        ByDataStore = 1,
        ByFramework = 2,
        Custom = 3
    }
}
