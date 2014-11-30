using System;
using System.Collections.Generic;

namespace Zirpl.AppEngine.CodeGeneration.V2.Parsers.JsonModel
{
    public class DomainClassConfigJson
    {
        public DomainClassConfigJson()
        {
            this.EnumValues = new List<EnumValueConfigJson>();
        }
        public bool? IsPersistable { get; set; }
        public String PluralName { get; set; }
        public String InheritsFrom { get; set; }
        public bool? IsStaticLookup { get; set; }
        public bool? IsAbstract { get; set; }
        public bool? IsVersionable { get; set; }
        public bool? IsAuditable { get; set; }
        public bool? IsUserCustomizable { get; set; }
        public bool? IsInsertable { get; set; }
        public bool? IsUpdatable { get; set; }
        public bool? IsDeletable { get; set; }
        public bool? IsMarkDeletable { get; set; }
        public IList<EnumValueConfigJson> EnumValues { get; set; }
    }
}
