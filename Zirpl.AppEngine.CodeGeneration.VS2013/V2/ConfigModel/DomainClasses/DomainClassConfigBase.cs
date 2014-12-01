using System;
using System.Collections.Generic;
using EnvDTE;
using Microsoft.VisualStudio.Shell.Interop;
using Zirpl.AppEngine.CodeGeneration.V2.Parsers.JsonModel;

namespace Zirpl.AppEngine.CodeGeneration.V2.ConfigModel
{
    public abstract class DomainClassConfigBase
    {
        protected DomainClassConfigBase()
        {
            this.Properties = new List<PropertyConfig>();
        }
        public ProjectItem SourceProjectItem { get; set; }
        public String ClassName { get; set; }
        public String Namespace { get; set; }

        public String FullClassName
        {
            get { return this.Namespace + "." + this.ClassName; }
        }
        public Project DestinationProject { get; set; }
        public String PluralName { get; set; }
        public bool IsAbstract { get; set; }
        public DomainClassConfigJson Config { get; set; }
        public IList<PropertyConfig> Properties { get; set; }
    }
}
