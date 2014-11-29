using System;
using System.Collections.Generic;
using System.Xml.Serialization;
using EnvDTE;
using Zirpl.Xml.Serialization;

namespace Zirpl.AppEngine.CodeGeneration.V2.ConfigModel
{
    public class AppConfig
    {
        public AppConfig()
        {
            this.DomainTypes = new List<DomainClassConfigBase>();
        }

        public ProjectItem SourceProjectItem { get; set; }
        public String GeneratedCodeRootFolderName { get; set; }
        public String GeneratedCSFileExtension { get; set; }
        public String ModelProjectName { get; set; }
        public String DataServiceProjectName { get; set; }
        public String ServiceProjectName { get; set; }
        public String WebProjectName { get; set; }
        public String WebCoreProjectName { get; set; }
        public String DataServiceTestsProjectName { get; set; }
        public String ServiceTestsProjectName { get; set; }
        public String TestsCommonProjectName { get; set; }
        public String DataContextName { get; set; }
        public List<DomainClassConfigBase> DomainTypes { get; set; }
    }
}
