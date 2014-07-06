using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using System.Xml.Serialization;

namespace Zirpl.AppEngine.CodeGeneration.ConfigModel
{
    public class DataServiceOptions : XmlModelBase
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
