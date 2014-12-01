using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using Zirpl.AppEngine.Model.Metadata;

namespace Zirpl.AppEngine.CodeGeneration.V2.ConfigModel
{
    public class DomainPropertyInfo
    {
        public String Name { get; set; }
        public DataTypeEnum DataType { get; set; }
        public bool IsPrimaryKey { get; set; }
        public AutoGenerationBehaviorTypeEnum AutoGenerationBehavior { get; set; }
        public bool IsRowVersion { get; set; }
        public bool IsForAuditableInterface { get; set; }
        public bool IsForMarkDeletedInterface { get; set; }
        public bool IsForCustomizableInterface { get; set; }
        public bool IsForCustomValueInterface { get; set; }


        public bool IsNullable { get; set; }
        public bool IsRequired { get; set; }
        public bool IsMaxLength { get; set; }
        public String MinLength { get; set; }
        public String MaxLength { get; set; }
        public String MinValue { get; set; }
        public String MaxValue { get; set; }
        public String Precision { get; set; }
        public UniquenessTypeEnum UniquenessType { get; set; }

        public DomainTypeInfo Owner { get; set; }
        public RelationshipTypeEnum RelationshipType { get; set; }
        public RelationshipDeletionBehaviorTypeEnum RelationshipDeletionBehaviorType { get; set; }
        public DomainTypeInfo RelatedTo { get; set; }
        public DomainPropertyInfo RelationshipReflexiveProperty { get; set; }
        public DomainPropertyInfo RelationshipIdProperty { get; set; }


        // TODO: validation
        //public String FormatPattern { get; set; }
        //public String Regex { get; set; }

        // TODO: calculated
        //public bool IsCalculated { get; set; }




        // TODO: unsure
        //public String DisplayText { get; set; }
        //
        //public bool CreateOnNullPost { get; set; }
        //
        //public int GridOrder { get; set; }
        //
        //public bool IsGroupable { get; set; }
        //
        //public bool IsFilterable { get; set; }
        //
        //public String GridTemplate { get; set; }
        //
        //public bool ShowInGrid { get; set; }
    }
}
