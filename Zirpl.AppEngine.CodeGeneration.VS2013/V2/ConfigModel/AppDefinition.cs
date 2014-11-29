using System;
using System.Collections.Generic;
using System.Xml.Serialization;
using Zirpl.AppEngine.Model.Xml;

namespace Zirpl.AppEngine.CodeGeneration.V2.ConfigModel
{
    [XmlRoot("AppDefinition", Namespace = "")]
    public class AppDefinition : XmlModelBase
    {
        public AppDefinition()
        {
            this.PersistableTypes = new List<PersistableDomainType>();
            this.GeneratedCodeRootFolderName = @"_auto\";
            this.GeneratedCSFileExtension = ".auto.cs";
        }

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
        public List<PersistableDomainType> PersistableTypes { get; set; }



        public String DataContextName { get; set; }
    }
}
