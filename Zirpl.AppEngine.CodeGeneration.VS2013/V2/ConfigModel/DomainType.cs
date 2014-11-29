using System;
using EnvDTE;

namespace Zirpl.AppEngine.CodeGeneration.V2.ConfigModel
{
    public class DomainType
    {
        public DomainType()
        {
            this.IsAuditable = true;
            this.IsVersionable = true;
            this.IsInsertable = true;
            this.IsUpdateable = true;
            this.IsDeletable = true;
        }

        // these properties are calculated
        public String ConfigFilePath { get; set; }
        public String UniqueName { get; set; }
        public String Namespace { get; set; }
        public String FullClassName { get; set; }
        public bool IsPersistable { get; set; }
        public Project Project { get; set; }



        public String ClassName { get; set; }
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
        public bool IsMarkDeletable { get; set; }

        public PersistableProperty[] Properties { get; set; }

        public EnumValueEntry[] EnumValueEntries { get; set; }
    }
}
