using System;
using System.Collections.Generic;
using EnvDTE;

namespace Zirpl.AppEngine.CodeGeneration.V2.ConfigModel
{
    public abstract class DomainTypeDefinitionBase
    {
        protected DomainTypeDefinitionBase()
        {
            //this.Properties = new List<PersistableProperty>();
        }

        public String UniqueName { get; set; }

        public String ClassName { get; set; }
        public String Namespace { get; set; }
        public String FullClassName { get; set; }
        public Project Project { get; set; }
        public String PluralName { get; set; }
        public DomainTypeDefinitionBase InheritsFrom { get; set; }
        public bool IsAbstract { get; set; }
        //public IList<PersistableProperty> Properties { get; set; }
    }
}
