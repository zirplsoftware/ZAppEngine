using System;
using System.Collections.Generic;
using EnvDTE;
using Zirpl.AppEngine.VisualStudioAutomation.AppGeneration.Config.Parsers.Json;

namespace Zirpl.AppEngine.VisualStudioAutomation.AppGeneration.Config
{
    public class DomainType
    {
        public DomainType()
        {
            this.Properties = new List<DomainProperty>();
            this.InheritedBy = new List<DomainType>();
            this.Relationships = new List<Relationship>();
            this.EnumValues = new List<EnumValue>();
        }

        public DomainTypeJson Config { get; set; }
        public String ConfigFilePath { get; set; }
        public Project DestinationProject { get; set; }

        public String Name { get; set; }
        public String Namespace { get; set; }
        public String FullName
        {
            get { return this.Namespace + "." + this.Name; }
        }
        public String PluralName { get; set; }
        public bool IsAbstract { get; set; }
        public bool IsPersistable { get; set; }
        public bool IsStaticLookup { get; set; }
        public bool IsVersionable { get; set; }
        public bool IsAuditable { get; set; }
        public bool IsExtensible { get; set; }
        public bool IsExtendedEntityFieldValue { get; set; }
        public bool IsInsertable { get; set; }
        public bool IsUpdatable { get; set; }
        public bool IsDeletable { get; set; }
        public bool IsMarkDeletable { get; set; }
        public IList<DomainProperty> Properties { get; private set; }
        public DomainProperty IdProperty { get; set; }
        public DomainType InheritsFrom { get; set; }
        public IList<DomainType> InheritedBy { get; private set; }
        public DomainType Extends { get; set; }
        public DomainType ExtendedBy { get; set; }
        public IList<EnumValue> EnumValues { get; private set; }
        public IList<Relationship> Relationships { get; private set; }
        
        public IEnumerable<DomainProperty> GetAllPropertiesIncludingInherited()
        {
            var list = new List<DomainProperty>();
            var domainType = this;
            while (domainType != null)
            {
                list.AddRange(domainType.Properties);
                domainType = domainType.InheritsFrom;
            }
            return list;
        }

        public DomainType GetBaseMostDomainType()
        {
            if (InheritsFrom != null)
            {
                return InheritsFrom.GetBaseMostDomainType();
            }
            return this;
        }
    }
}
