using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using System.Xml.Serialization;

namespace Zirpl.AppEngine.CodeGeneration.ConfigModel
{
    public class Entry : XmlModelBase
    {
        [XmlAttribute]
        public String Key { get; set; }
        [XmlAttribute]
        public String Value { get; set; }
    }
}
