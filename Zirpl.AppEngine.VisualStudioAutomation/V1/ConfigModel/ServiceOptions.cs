using System.Xml.Serialization;
using Zirpl.Xml.Serialization;

namespace Zirpl.AppEngine.CodeGeneration.V1.ConfigModel
{
    public class ServiceOptions : XmlSerializableBase
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
