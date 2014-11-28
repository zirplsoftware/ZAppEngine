using System;
using System.Xml.Serialization;
using Zirpl.AppEngine.Model.Xml;

namespace Zirpl.AppEngine.CodeGeneration.V1.ConfigModel
{
    public class Property : XmlModelBase
    {
        public Property()
        {
            this.GenerateIdProperty = true;
            this.MapProperty = true;
            this.IsGroupable = true;
            this.IsFilterable = true;
            this.ShowInGrid = true;
        }
        [XmlAttribute]
        public String Name { get; set; }
        [XmlAttribute]
        public String DisplayText { get; set; }
        [XmlAttribute]
        public String Type { get; set; }
        [XmlAttribute]
        public bool IsRelationship { get; set; }
        [XmlAttribute]
        public bool MapProperty { get; set; }
        [XmlAttribute]
        public bool CascadeOnDelete { get; set; }
        [XmlAttribute]
        public bool IsDefaultSort { get; set; }
        [XmlAttribute]
        public bool GenerateIdProperty { get; set; }
        [XmlAttribute]
        public String CollectionOfType { get; set; }
        [XmlAttribute]
        public bool IsCollection { get; set; }
        [XmlAttribute]
        public bool IsRequired { get; set; }
        [XmlAttribute]
        public bool IsMaxLength { get; set; }
        [XmlAttribute]
        public String MinLength { get; set; }
        [XmlAttribute]
        public String MaxLength { get; set; }
        [XmlAttribute]
        public String MinValue { get; set; }
        [XmlAttribute]
        public String MaxValue { get; set; }
        [XmlAttribute]
        public String Precision { get; set; }
        [XmlAttribute]
        public String NavigationProperty { get; set; }
        [XmlAttribute]
        public bool CreateOnNullPost { get; set; }
        [XmlAttribute]
        public int GridOrder { get; set; }
        [XmlAttribute]
        public bool IsGroupable { get; set; }
        [XmlAttribute]
        public bool IsFilterable { get; set; }
        [XmlAttribute]
        public String GridTemplate { get; set; }
        [XmlAttribute]
        public bool ShowInGrid { get; set; }
    }
}
