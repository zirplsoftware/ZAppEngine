using System;
using Zirpl.AppEngine.Model.Metadata;

namespace Zirpl.AppEngine.VisualStudioAutomation.AppGeneration.Config
{
    public class DomainProperty
    {
        public String Name { get; set; }
        public DataTypeEnum DataType { get; set; }
        public String DataTypeString { get; set; }
        public bool IsPrimaryKey { get; set; }
        public bool IsForeignKey { get; set; }
        public AutoGenerationBehaviorTypeEnum AutoGenerationBehavior { get; set; }
        public bool IsRowVersion { get; set; }
        public bool IsForAuditableInterface { get; set; }
        public bool IsForMarkDeletedInterface { get; set; }
        public bool IsForExtensibleInterface { get; set; }
        public bool IsForExtendedEntityFieldValueInterface { get; set; }
        public bool IsForIsStaticLookupInterface { get; set; }


        public bool IsNullable { get; set; }
        public bool IsRequired { get; set; }
        public bool IsMaxLength { get; set; }
        public long MinLength { get; set; }
        public long MaxLength { get; set; }
        public String MinValue { get; set; }
        public String MaxValue { get; set; }
        public String Precision { get; set; }
        public UniquenessTypeEnum UniquenessType { get; set; }

        public DomainType Owner { get; set; }

        public Relationship Relationship { get; set; }

        //public String FormatPattern { get; set; }
        //public String Regex { get; set; }

        //public bool IsCalculated { get; set; }

        //public String DisplayText { get; set; }
        //public bool CreateOnNullPost { get; set; }
        //public int GridOrder { get; set; }
        //public bool IsGroupable { get; set; }
        //public bool IsFilterable { get; set; }
        //public String GridTemplate { get; set; }
        //public bool ShowInGrid { get; set; }
    }
}
