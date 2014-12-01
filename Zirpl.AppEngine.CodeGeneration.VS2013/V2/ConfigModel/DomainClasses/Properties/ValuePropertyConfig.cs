using System;
using System.Xml.Serialization;
using Zirpl.AppEngine.Model.Metadata;

namespace Zirpl.AppEngine.CodeGeneration.V2.ConfigModel
{
    public class ValuePropertyConfig : PropertyConfig
    {
        public bool IsNullable { get; set; }
        public bool IsRequired { get; set; }
        public bool IsMaxLength { get; set; }
        public String MinLength { get; set; }
        public String MaxLength { get; set; }
        public String MinValue { get; set; }
        public String MaxValue { get; set; }
        public String Precision { get; set; }
        public UniquenessTypeEnum UniquenessType { get; set; }


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
