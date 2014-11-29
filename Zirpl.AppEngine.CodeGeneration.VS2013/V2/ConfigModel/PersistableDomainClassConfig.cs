using System;
using EnvDTE;

namespace Zirpl.AppEngine.CodeGeneration.V2.ConfigModel
{
    public class PersistableDomainClassConfig : DomainClassConfigBase
    {
        public PersistableDomainClassConfig()
        {
            this.IsAuditable = true;
            this.IsVersionable = true;
            this.IsInsertable = true;
            this.IsUpdatable = true;
            this.IsDeletable = true;
        }
        public bool IsVersionable { get; set; }
        public bool IsAuditable { get; set; }
        public bool IsUserCustomizable { get; set; }
        public bool IsInsertable { get; set; }
        public bool IsUpdatable { get; set; }
        public bool IsDeletable { get; set; }
        public bool IsMarkDeletable { get; set; }
    }
}
