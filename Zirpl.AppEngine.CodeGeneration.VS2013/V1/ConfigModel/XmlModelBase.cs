using System.Xml;
using System.Xml.Serialization;

namespace Zirpl.AppEngine.CodeGeneration.V1.ConfigModel
{
    public abstract class XmlModelBase
    {
        [XmlAnyAttribute]
        public XmlAttribute[] AnyAttributes;

        [XmlAnyElement]
        public XmlElement[] AnyElements;
    }
}
