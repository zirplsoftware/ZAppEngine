using System;
using System.Xml.Serialization;

namespace Zirpl.AppEngine.CodeGeneration.V1.ConfigModel
{
    public class Entry : XmlModelBase
    {
        [XmlAttribute]
        public String Key { get; set; }
        [XmlAttribute]
        public String Value { get; set; }
    }
}
