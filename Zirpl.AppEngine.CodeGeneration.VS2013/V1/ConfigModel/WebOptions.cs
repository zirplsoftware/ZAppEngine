using System.Xml.Serialization;

namespace Zirpl.AppEngine.CodeGeneration.V1.ConfigModel
{
    public class WebOptions : XmlModelBase
    {
        public WebOptions()
        {
            this.GenerateSupportViewModel = true;
            this.GenerateLookupsController = true;
            this.GenerateSupportController = true;
            this.GenerateSupportIndexView = true;
        }
        [XmlAttribute]
        public bool GenerateSupportViewModel { get; set; }
        [XmlAttribute]
        public bool GenerateSupportIndexView { get; set; }
        [XmlAttribute]
        public bool GenerateSupportController { get; set; }
        [XmlAttribute]
        public bool GenerateLookupsController { get; set; }
        public Property[] AdditionalProperties { get; set; }
    }
}
