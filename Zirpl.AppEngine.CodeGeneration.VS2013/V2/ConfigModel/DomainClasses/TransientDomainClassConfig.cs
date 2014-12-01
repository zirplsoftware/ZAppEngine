using System;
using System.Collections.Generic;
using EnvDTE;

namespace Zirpl.AppEngine.CodeGeneration.V2.ConfigModel
{
    public class TransientDomainClassConfig : DomainClassConfigBase
    {
        public TransientDomainClassConfig()
        {
            this.ChildDomainClasses = new List<TransientDomainClassConfig>();
        }
        public TransientDomainClassConfig ParentDomainClass { get; set; }
        public IList<TransientDomainClassConfig> ChildDomainClasses { get; set; } 
    }
}
