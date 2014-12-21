using System;
using System.Xml.Serialization;
using Zirpl.Xml.Serialization;

namespace Zirpl.AppEngine.CodeGeneration.V1.ConfigModel
{
    public class Entry : XmlSerializableBase
    {
        [XmlAttribute]
        public String Key { get; set; }
        [XmlAttribute]
        public String Value { get; set; }
    }
}
