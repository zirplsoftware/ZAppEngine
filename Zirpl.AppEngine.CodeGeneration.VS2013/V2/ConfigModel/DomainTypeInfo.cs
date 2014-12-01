using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using EnvDTE;
using Zirpl.AppEngine.CodeGeneration.V2.ConfigModel.Parsers.JsonModel;
using Zirpl.AppEngine.Model.Metadata;

namespace Zirpl.AppEngine.CodeGeneration.V2.ConfigModel
{
    public class DomainTypeInfo
    {
        public DomainTypeInfo()
        {
            this.Properties = new List<DomainPropertyInfo>();
            this.InheritedBy = new List<DomainTypeInfo>();

        }

        public static DomainTypeInfo CustomValue(DomainTypeInfo classBeingCustomized)
        {
            var custom = new DomainTypeInfo()
            {
                IsCustomValue = true,
                Name = classBeingCustomized.Name + "CustomValue",
                Namespace = classBeingCustomized.Namespace,
                PluralName = classBeingCustomized.Name + "CustomValues",
                DestinationProject = classBeingCustomized.DestinationProject,
                IsAuditable = classBeingCustomized.IsAuditable,
                IsDeletable = classBeingCustomized.IsDeletable,
                IsInsertable = classBeingCustomized.IsInsertable,
                IsMarkDeletable = classBeingCustomized.IsMarkDeletable,
                IsUpdatable = classBeingCustomized.IsUpdatable,
                IsVersionable = classBeingCustomized.IsVersionable,
                Customizes = classBeingCustomized
            };
            classBeingCustomized.CustomizedBy = custom;
            return custom;
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
        public bool IsCustomizable { get; set; }
        public bool IsCustomValue { get; set; }
        public bool IsInsertable { get; set; }
        public bool IsUpdatable { get; set; }
        public bool IsDeletable { get; set; }
        public bool IsMarkDeletable { get; set; }
        public IList<DomainPropertyInfo> Properties { get; set; }
        public DomainPropertyInfo IdProperty { get; set; }
        public DomainTypeInfo InheritsFrom { get; set; }
        public IList<DomainTypeInfo> InheritedBy { get; set; }
        public DomainTypeInfo Customizes { get; set; }
        public DomainTypeInfo CustomizedBy { get; set; }
        public DomainTypeInfo EnumDescribes { get; set; }
        public DomainTypeInfo DescribedByEnum { get; set; }
        public DataTypeEnum EnumDataType { get; set; }
        public IDictionary<String, String> EnumValues { get; set; }
    }
}
