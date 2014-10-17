using System;
using System.Xml.Serialization;

namespace Zirpl.AppEngine.CodeGeneration.V1.ConfigModel
{
    [XmlRoot("Type", Namespace = "")]
    public class DomainType : XmlModelBase
    {
        public DomainType()
        {
            this.ModelOptions = new ModelOptions();
            this.DataServiceOptions = new DataServiceOptions();
            this.ServiceOptions = new ServiceOptions();
            this.WebOptions = new WebOptions();
            this.IsPersistable = true;
        }

        [XmlAttribute]
        public String Name { get; set; }
        [XmlAttribute]
        public String BaseClassOverride { get; set; }
        [XmlAttribute]
        public String PluralNameOverride { get; set; }
        [XmlAttribute]
        public String SubNamespace { get; set; }
        [XmlAttribute]
        public bool IsDictionary { get; set; }
        [XmlAttribute]
        public bool IsPersistable { get; set; }
        [XmlAttribute]
        public bool IsAbstract { get; set; }
        [XmlAttribute]
        public bool IsCustomizable { get; set; }
        [XmlAttribute]
        public string IdTypeOverride { get; set; }
        public ModelOptions ModelOptions { get; set; }
        public DataServiceOptions DataServiceOptions { get; set; }
        public ServiceOptions ServiceOptions { get; set; }
        public WebOptions WebOptions { get; set; }
        public Property[] Properties { get; set; }
        public Entry[] EnumValueEntries { get; set; }
    }
}
