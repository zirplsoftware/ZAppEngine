using System;
using System.Collections.Generic;
using EnvDTE;
using Zirpl.AppEngine.Model.Metadata;
using Zirpl.AppEngine.VisualStudioAutomation.AppGeneration.ConfigModel.Parsers.JsonModel;

namespace Zirpl.AppEngine.VisualStudioAutomation.AppGeneration.ConfigModel
{
    public class DomainTypeInfo
    {
        public DomainTypeInfo()
        {
            this.Properties = new List<DomainPropertyInfo>();
            this.InheritedBy = new List<DomainTypeInfo>();
            this.Relationships = new List<RelationshipInfo>();
            this.EnumValues = new List<EnumValueInfo>();
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
        public bool IsEnum { get; set; }
        public bool IsVersionable { get; set; }
        public bool IsAuditable { get; set; }
        public bool IsExtensible { get; set; }
        public bool IsExtendedEntityFieldValue { get; set; }
        public bool IsInsertable { get; set; }
        public bool IsUpdatable { get; set; }
        public bool IsDeletable { get; set; }
        public bool IsMarkDeletable { get; set; }
        public IList<DomainPropertyInfo> Properties { get; set; }
        public DomainPropertyInfo IdProperty { get; set; }
        public DomainTypeInfo InheritsFrom { get; set; }
        public IList<DomainTypeInfo> InheritedBy { get; set; }
        public DomainTypeInfo Extends { get; set; }
        public DomainTypeInfo ExtendedBy { get; set; }
        public DomainTypeInfo EnumDescribes { get; set; }
        public DomainTypeInfo DescribedByEnum { get; set; }
        public DataTypeEnum EnumDataType { get; set; }
        public IList<EnumValueInfo> EnumValues { get; set; }
        public IList<RelationshipInfo> Relationships { get; set; } 
    }
}
