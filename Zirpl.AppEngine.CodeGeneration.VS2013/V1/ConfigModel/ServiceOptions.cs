using System.Xml.Serialization;

namespace Zirpl.AppEngine.CodeGeneration.V1.ConfigModel
{
    public class ServiceOptions : XmlModelBase
    {
        public ServiceOptions()
        {
            this.GenerateService = true;
            this.GenerateServiceInterface = true;
            this.GenerateValidator = true;
        }
        [XmlAttribute]
        public bool GenerateService { get; set; }
        [XmlAttribute]
        public bool GenerateServiceInterface { get; set; }
        [XmlAttribute]
        public bool GenerateValidator { get; set; }
    }
}
