using System;
using System.Collections.Generic;
using EnvDTE;

namespace Zirpl.AppEngine.CodeGeneration.V2.ConfigModel
{
    public class PersistableDomainClassConfig : PersistableDomainClassConfigBase
    {
        public PersistableDomainClassConfig()
        {
            this.ChildDomainClasses = new List<PersistableDomainClassConfig>();
        
            this.IsAuditable = true;
            this.IsVersionable = true;
            this.IsInsertable = true;
            this.IsUpdatable = true;
            this.IsDeletable = true;
        }
        public bool IsVersionable { get; set; }
        public bool IsAuditable { get; set; }
        public bool IsCustomizable { get; set; }
        public bool IsCustomFieldValue { get; set; }
        public bool IsInsertable { get; set; }
        public bool IsUpdatable { get; set; }
        public bool IsDeletable { get; set; }
        public bool IsMarkDeletable { get; set; }
        public PersistableDomainClassConfig ParentDomainClass { get; set; }
        public IList<PersistableDomainClassConfig> ChildDomainClasses { get; set; } 
    }
}
