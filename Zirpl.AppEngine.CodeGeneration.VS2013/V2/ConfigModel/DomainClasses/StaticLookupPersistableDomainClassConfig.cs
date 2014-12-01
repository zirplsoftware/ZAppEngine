using System;
using System.Collections;
using System.Collections.Generic;
using EnvDTE;

namespace Zirpl.AppEngine.CodeGeneration.V2.ConfigModel
{
    public class StaticLookupPersistableDomainClassConfig : PersistableDomainClassConfigBase
    {
        public EnumConfig Enum { get; set; }
    }
}
