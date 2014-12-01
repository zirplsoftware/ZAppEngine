using System;
using EnvDTE;
using Zirpl.AppEngine.CodeGeneration.V2.ConfigModel.DomainClasses.Properties;

namespace Zirpl.AppEngine.CodeGeneration.V2.ConfigModel
{
    public abstract class PersistableDomainClassConfigBase : DomainClassConfigBase
    {
        public IdPropertyConfig IdProperty { get; set; }
    }
}
