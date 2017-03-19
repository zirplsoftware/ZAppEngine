using System.Collections.Generic;
using System.Linq;

namespace Zirpl.AppEngine.VisualStudioAutomation.AppGeneration.Model
{
    public static class DomainPropertyExtensions
    {
        //public static IEnumerable<DomainProperty> GetIPersistableProperties(this IEnumerable<DomainProperty> properties)
        //{
        //    return properties.Where(o => o.Name == "Id");
        //}

        //public static IEnumerable<DomainProperty> GetIAuditableProperties(this IEnumerable<DomainProperty> properties)
        //{
        //    var names = from o in typeof(IAuditable).GetProperties() select o.Name;
        //    return properties.Where(o => names.Contains(o.Name));
        //}

        public static IEnumerable<DomainProperty> GetCollectionProperties(this IEnumerable<DomainProperty> properties)
        {
            return properties.Where(o =>
                o.DataType == DataTypeEnum.Relationship
                && (o.Relationship.Type == RelationshipTypeEnum.ManyToMany
                    || (o.Relationship.Type == RelationshipTypeEnum.OneToMany
                        && o.Relationship.NavigationPropertyOnFrom == o)
                    || (o.Relationship.Type == RelationshipTypeEnum.ManyToOne
                        && o.Relationship.NavigationPropertyOnTo == o)));
        }

        public static IEnumerable<DomainProperty> GetNonInterfaceProperties(this IEnumerable<DomainProperty> properties)
        {
            return properties.Where(o =>
                !o.IsForAuditableInterface
                && !o.IsForExtendedEntityFieldValueInterface
                && !o.IsForExtensibleInterface
                && !o.IsForIsStaticLookupInterface
                && !o.IsForMarkDeletedInterface
                && !o.IsPrimaryKey
                && !o.IsRowVersion);
        }

        public static IEnumerable<DomainProperty> GetInterfaceProperties(this IEnumerable<DomainProperty> properties)
        {
            return properties.Where(o =>
                o.IsForAuditableInterface
                || o.IsForExtendedEntityFieldValueInterface
                || o.IsForExtensibleInterface
                || o.IsForIsStaticLookupInterface
                || o.IsForMarkDeletedInterface
                || o.IsPrimaryKey
                || o.IsRowVersion);
        }
    }
}
