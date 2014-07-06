using System;
using System.Collections.Generic;
using System.Xml.Serialization;

namespace Zirpl.AppEngine.CodeGeneration.ConfigModel
{
    [XmlRoot("AppDefinition", Namespace = "")]
    public class AppDefinition : XmlModelBase
    {
        public AppDefinition()
        {
            this.DomainTypes = new List<DomainType>();
            this.GeneratedCodeRootFolderName = @"_auto\";
        }

        public String GeneratedCodeRootFolderName { get; set; }
        public String ModelProjectName { get; set; }
        public String DataServiceProjectName { get; set; }
        public String ServiceProjectName { get; set; }
        public String WebProjectName { get; set; }
        public String WebCoreProjectName { get; set; }
        public String DataServiceTestsProjectName { get; set; }
        public String ServiceTestsProjectName { get; set; }
        public String TestingProjectName { get; set; }
        public List<DomainType> DomainTypes { get; set; }



        public String DataContextName { get; set; }
    }
}
