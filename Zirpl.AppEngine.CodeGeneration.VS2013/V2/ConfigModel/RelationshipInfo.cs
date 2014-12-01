using System;
using System.Collections.Generic;
using System.Linq;
using System.Reflection;
using System.Text;
using System.Threading.Tasks;
using EnvDTE;
using Zirpl.AppEngine.Model.Metadata;

namespace Zirpl.AppEngine.CodeGeneration.V2.ConfigModel
{
    public class RelationshipInfo
    {
        public RelationshipTypeEnum Type { get; set; }
        public RelationshipDeletionBehaviorTypeEnum DeletionBehavior { get; set; }
        public DomainTypeInfo From { get; set; }
        public DomainTypeInfo To { get; set; }
        public DomainPropertyInfo ForeignKeyOnFrom { get; set; }
        public DomainPropertyInfo ForeignKeyOnTo { get; set; }
        public DomainPropertyInfo NavigationPropertyOnFrom { get; set; }
        public DomainPropertyInfo NavigationPropertyOnTo { get; set; }
    }
}
