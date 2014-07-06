using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using System.Xml.Serialization;

namespace Zirpl.AppEngine.CodeGeneration.ConfigModel
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
        public bool IsAbstract { get; set; }
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
