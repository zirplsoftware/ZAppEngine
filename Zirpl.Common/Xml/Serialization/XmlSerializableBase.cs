#if !SILVERLIGHT && !PORTABLE
using System.Xml;
using System.Xml.Serialization;

namespace Zirpl.Xml.Serialization
{
    public abstract class XmlSerializableBase
    {
        [XmlAnyAttribute]
        public XmlAttribute[] AnyAttributes;

        [XmlAnyElement]
        public XmlElement[] AnyElements;
    }
}
#endif