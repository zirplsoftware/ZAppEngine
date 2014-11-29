using System;
using System.Collections.Generic;
using EnvDTE;

namespace Zirpl.AppEngine.CodeGeneration.V2.ConfigModel.JsonModel
{
    public class DomainTypeDefinitionJson
    {
        public DomainTypeDefinitionJson()
        {
            this.IsPersistable = true;
            this.IsAuditable = true;
            this.IsVersionable = true;
            this.IsInsertable = true;
            this.IsUpdatable = true;
            this.IsDeletable = true;
            this.EnumValues = new List<StaticLookupValueDefinitionJson>();
        }
        public bool IsPersistable { get; set; }
        public String PluralName { get; set; }
        public String InheritsFrom { get; set; }
        public bool IsStaticLookup { get; set; }
        public bool IsAbstract { get; set; }
        public bool IsVersionable { get; set; }
        public bool IsAuditable { get; set; }
        public bool IsUserCustomizable { get; set; }
        public bool IsInsertable { get; set; }
        public bool IsUpdatable { get; set; }
        public bool IsDeletable { get; set; }
        public bool IsMarkDeletable { get; set; }
        public IList<StaticLookupValueDefinitionJson> EnumValues { get; set; }
    }
}
