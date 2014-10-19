using System;

namespace Zirpl.AppEngine.Model.Metadata
{
    public interface IFieldMetadata
    {
        String Name { get; set; }
        DataTypeEnum DataType { get; set; }
        //Type Type { get; set; }
        //String DisplayText { get; set; }
        bool IsRequired { get; set; }
        bool IsMaxLength { get; set; }
        String MinLength { get; set; }
        String MaxLength { get; set; }
        String MinValue { get; set; }
        String MaxValue { get; set; }
        String Precision { get; set; }
        IEntityMetadata EntityMetadata { get; set; }
        RelationshipTypeEnum RelationshipType { get; set; }
        RelationshipDeletionBehaviorTypeEnum RelationshipDeletionBehaviorType { get; set; }
        String RelationshipEntityType { get; set; }
        UniquenessTypeEnum UniquenessType { get; set; }
        bool IsUpdateable { get; set; }
    }
}