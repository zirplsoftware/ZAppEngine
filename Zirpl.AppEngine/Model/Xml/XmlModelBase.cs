#if !SILVERLIGHT
using System.Xml;
using System.Xml.Serialization;

namespace Zirpl.AppEngine.Model.Xml
{
    public abstract class XmlModelBase
    {
        [XmlAnyAttribute]
        public XmlAttribute[] AnyAttributes;

        [XmlAnyElement]
        public XmlElement[] AnyElements;
    }
}
#endif