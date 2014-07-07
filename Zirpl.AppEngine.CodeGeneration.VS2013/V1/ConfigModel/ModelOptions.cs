using System.Xml.Serialization;

namespace Zirpl.AppEngine.CodeGeneration.V1.ConfigModel
{
    public class ModelOptions : XmlModelBase
    {
        public ModelOptions()
        {
            this.GenerateModel = true;
            this.GenerateMetadata = true;
            this.GenerateEnum = true;
            this.GenerateSupportViewModel = true;
        }
        [XmlAttribute]
        public bool GenerateModel { get; set; }
        [XmlAttribute]
        public bool GenerateMetadata { get; set; }
        [XmlAttribute]
        public bool GenerateEnum { get; set; }
        [XmlAttribute]
        public bool GenerateSupportViewModel { get; set; }
    }
}
