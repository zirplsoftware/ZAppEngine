using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Zirpl.AppEngine.CodeGeneration.AppModel
{
    public class DomainTypeProperty
    {
        public DomainTypeProperty()
        {
            this.GenerateIdProperty = true;
            this.MapProperty = true;
            this.IsGroupable = true;
            this.IsFilterable = true;
            this.ShowInGrid = true;
        }
        public String Name { get; set; }
        public String DisplayText { get; set; }
        public String Type { get; set; }
        public bool IsRelationship { get; set; }
        public bool MapProperty { get; set; }
        public bool CascadeOnDelete { get; set; }
        public bool IsDefaultSort { get; set; }
        public bool GenerateIdProperty { get; set; }
        public String CollectionOfType { get; set; }
        public bool IsCollection { get; set; }
        public bool IsRequired { get; set; }
        public bool IsMaxLength { get; set; }
        public String MinLength { get; set; }
        public String MaxLength { get; set; }
        public String MinValue { get; set; }
        public String MaxValue { get; set; }
        public String Precision { get; set; }
        public String NavigationProperty { get; set; }
        public bool CreateOnNullPost { get; set; }
        public int GridOrder { get; set; }
        public bool IsGroupable { get; set; }
        public bool IsFilterable { get; set; }
        public String GridTemplate { get; set; }
        public bool ShowInGrid { get; set; }
    }
}
