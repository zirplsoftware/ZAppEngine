using System;
using System.Collections.Generic;
using EnvDTE;
using Microsoft.VisualStudio.Shell.Interop;

namespace Zirpl.AppEngine.CodeGeneration.V2.ConfigModel
{
    public class EnumConfig
    {
        public EnumConfig()
        {
            this.Values = new List<EnumValueConfig>();
        }

        public ProjectItem SourceProjectItem { get; set; }
        public IList<EnumValueConfig> Values { get; set; }

        public String EnumName { get; set; }
        public String Namespace { get; set; }
        public String FullEnumName { get; set; }
        public Project DestinationProject { get; set; }
        public String ValueType { get; set; }
        public StaticLookupPersistableDomainClassConfig StaticLookupParent { get; set; }
    }
}
