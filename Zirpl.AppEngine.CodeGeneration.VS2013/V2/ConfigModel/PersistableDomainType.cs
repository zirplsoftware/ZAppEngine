using System;

namespace Zirpl.AppEngine.CodeGeneration.V2.ConfigModel
{
    public class PersistableDomainType
    {
        public PersistableDomainType()
        {
            this.IsAuditable = true;
            this.IsVersionable = true;
            this.IsInsertable = true;
            this.IsUpdateable = true;
            this.IsDeletable = true;
        }

        public String UniqueName { get; set; }
        public String PluralNameOverride { get; set; }
        public String InheritsFrom { get; set; }
        public bool IsStaticLookup { get; set; }
        public bool IsAbstract { get; set; }
        public bool IsVersionable { get; set; }
        public bool IsAuditable { get; set; }
        public bool IsUserCustomizable { get; set; }
        public bool IsInsertable { get; set; }
        public bool IsUpdateable { get; set; }
        public bool IsDeletable { get; set; }

        public PersistableProperty[] Properties { get; set; }

        public EnumValueEntry[] EnumValueEntries { get; set; }
    }
}
