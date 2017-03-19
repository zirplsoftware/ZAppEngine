using System;

namespace Zirpl.AppEngine.VisualStudioAutomation.AppGeneration.Model
{
    public class Relationship
    {
        public RelationshipTypeEnum Type { get; set; }
        public RelationshipDeletionBehaviorTypeEnum DeletionBehavior { get; set; }
        public DomainType From { get; set; }
        public DomainType To { get; set; }
        public DomainProperty ForeignKeyOnFrom { get; set; }
        public DomainProperty ForeignKeyOnTo { get; set; }
        public DomainProperty NavigationPropertyOnFrom { get; set; }
        public DomainProperty NavigationPropertyOnTo { get; set; }
        public String ManyToManyTableName { get; set; }
    }
}
