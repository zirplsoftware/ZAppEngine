using System.Xml.Serialization;
using Zirpl.Xml.Serialization;

namespace Zirpl.AppEngine.CodeGeneration.V1.ConfigModel
{
    public class DataServiceOptions : XmlSerializableBase
    {
        public DataServiceOptions()
        {
            this.GenerateDataService = true;
            this.GenerateDataServiceInterface = true;
            this.GenerateEntityFrameworkMapping = true;
            this.GenerateDataContextProperty = true;
        }
        [XmlAttribute]
        public bool GenerateDataService { get; set; }
        [XmlAttribute]
        public bool GenerateDataServiceInterface { get; set; }
        [XmlAttribute]
        public bool GenerateEntityFrameworkMapping { get; set; }
        [XmlAttribute]
        public bool GenerateDataContextProperty { get; set; }
    }
}
