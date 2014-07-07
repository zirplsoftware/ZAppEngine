using System;
using Zirpl.AppEngine.CodeGeneration.TextTemplating;
using Zirpl.AppEngine.CodeGeneration.V1.ConfigModel;

namespace Zirpl.AppEngine.CodeGeneration.V1
{
    public class CodeHelper
    {
        private TransformationHelper transformationHelper;

        public CodeHelper(TransformationHelper transformationHelper)
        {
            this.transformationHelper = transformationHelper;
        }

        #region Model-related methods
        public string GetModelNamespace(DomainType domainType)
        {
            return transformationHelper.ProjectProvider.ModelProject.GetDefaultNamespace() + (String.IsNullOrEmpty(domainType.SubNamespace) ? null : "." + domainType.SubNamespace);
        }
        public String GetModelClassName(DomainType domainType)
        {
            return domainType.Name;
        }
        public string GetModelClassFullName(DomainType domainType)
        {
            return String.Format("{0}.{1}", this.GetModelNamespace(domainType), this.GetModelClassName(domainType));
        }
        public string GetModelBaseClassName(DomainType domainType)
        {
            return !String.IsNullOrEmpty(domainType.BaseClassOverride)
                    ? domainType.BaseClassOverride
                    : domainType.IsDictionary
                        ? string.Format("DictionaryEntityBase<{0}, {1}Enum>", this.GetModelIdClassName(domainType), domainType.Name)
                        : string.Format("AuditableBase<{0}>", this.GetModelIdClassName(domainType));
        }
        public string GetModelIdClassName(DomainType domainType)
        {
            return !String.IsNullOrEmpty(domainType.IdTypeOverride)
                                    ? domainType.IdTypeOverride
                                    : domainType.IsDictionary
                                        ? "byte"
                                        : "int";
        }
        #endregion
        
        #region ModelMetadata-related methods
        public string GetModelMetadataNamespace(DomainType domainType)
        {
            // use the same
            return this.GetModelNamespace(domainType);
        }
        public String GetModelMetadataClassName(DomainType domainType)
        {
            return domainType.Name + "Metadata";
        }
        public String GetModelMetadataClassFullName(DomainType domainType)
        {
            return String.Format("{0}.{1}", this.GetModelMetadataNamespace(domainType), this.GetModelMetadataClassName(domainType));
        }
        public string GetModelMetadataBaseClassName(DomainType domainType)
        {
            return !String.IsNullOrEmpty(domainType.BaseClassOverride)
                ? domainType.BaseClassOverride + "Metadata"
                : domainType.IsDictionary
                     ? "DictionaryEntityBaseMetadataBase"
                     : "MetadataBase";
        }
        #endregion

        #region ModelEnum-related methods
        public string GetModelEnumNamespace(DomainType domainType)
        {
            // use the same
            return this.GetModelNamespace(domainType);
        }
        public String GetModelEnumClassName(DomainType domainType)
        {
            return domainType.Name + "Enum";
        }
        public String GetModelEnumClassFullName(DomainType domainType)
        {
            return String.Format("{0}.{1}", this.GetModelEnumNamespace(domainType), this.GetModelEnumClassName(domainType));
        }
        public string GetModelEnumBaseClassName(DomainType domainType)
        {
            // it should match the Model's ID type
            return this.GetModelIdClassName(domainType);
        }
        #endregion

        #region Property-related methods
        public String GetPropertyClassNameString(Property property)
        {
            string propertyType = property.Type;
            propertyType = propertyType.ToLowerInvariant() == "currency" ? "decimal" : propertyType;
            if (!property.IsRequired
                && (propertyType.ToLowerInvariant() == "datetime"
                    || propertyType.ToLowerInvariant() == "byte"
                    || propertyType.ToLowerInvariant() == "bool"
                    || propertyType.ToLowerInvariant() == "int"
                    || propertyType.ToLowerInvariant() == "decimal"
                    || propertyType.ToLowerInvariant() == "double"
                    || propertyType.ToLowerInvariant() == "guid"))
            {
                propertyType += "?";
            }
            return propertyType;
        }
        public String GetRelationshipIdPropertyClassNameString(Property property)
        {
            var relationshipModel = this.transformationHelper.DomainTypeFilters.GetDomainTypeByFullTypeName(property.Type);
            string relationshipIdTypeString = this.GetModelIdClassName(relationshipModel);
            relationshipIdTypeString += (property.IsRequired ? "" : "?");
            return relationshipIdTypeString;
        }
        public string GetPluralPropertyName(DomainType domainType)
        {
            if (!String.IsNullOrEmpty(domainType.PluralNameOverride))
            {
                return domainType.PluralNameOverride;
            }
            else if (domainType.Name.EndsWith("s"))
            {
                return domainType.Name + "es";
            }
            else if (domainType.Name.EndsWith("y"))
            {
                return domainType.Name.Substring(0, domainType.Name.Length - 1) + "ies";
            }
            else
            {
                return domainType.Name + "s";
            }
        }
        #endregion






        public string GetDataServiceNamespace(DomainType domainType)
        {
            return transformationHelper.ProjectProvider.DataServiceProject.GetDefaultNamespace() + (String.IsNullOrEmpty(domainType.SubNamespace) ? null : "." + domainType.SubNamespace);
        }
        public string GetDataServiceTestsNamespace(DomainType domainType)
        {
            return transformationHelper.ProjectProvider.DataServiceTestsProject.GetDefaultNamespace() + (String.IsNullOrEmpty(domainType.SubNamespace) ? null : "." + domainType.SubNamespace);
        }
        public string GetServiceNamespace(DomainType domainType)
        {
            return transformationHelper.ProjectProvider.ServiceProject.GetDefaultNamespace() + (String.IsNullOrEmpty(domainType.SubNamespace) ? null : "." + domainType.SubNamespace);
        }
        public string GetTestingNamespace(DomainType domainType)
        {
            return transformationHelper.ProjectProvider.TestingProject.GetDefaultNamespace() + (String.IsNullOrEmpty(domainType.SubNamespace) ? null : "." + domainType.SubNamespace);
        }
        public string GetServiceTestsNamespace(DomainType domainType)
        {
            return transformationHelper.ProjectProvider.ServiceTestsProject.GetDefaultNamespace() + (String.IsNullOrEmpty(domainType.SubNamespace) ? null : "." + domainType.SubNamespace);
        }
        public string GetSupportViewModelNamespace(DomainType domainType)
        {
            return domainType.IsDictionary
                ? transformationHelper.ProjectProvider.WebProject.GetDefaultNamespace() + ".Models" // + (String.IsNullOrEmpty(domainType.SubNamespace) ? null : "." + domainType.SubNamespace)
                : transformationHelper.ProjectProvider.ModelProject.GetDefaultNamespace() + ".Areas.Support.Models"; // + (String.IsNullOrEmpty(domainType.SubNamespace) ? null : "." + domainType.SubNamespace);
        }
        public string GetSupportViewModelFullTypeName(DomainType domainType)
        {
            return String.Format("{0}.{1}Model", this.GetSupportViewModelNamespace(domainType), domainType.Name);
        }
        public string GetSupportControllerNamespace(DomainType domainType)
        {
            return transformationHelper.ProjectProvider.ModelProject.GetDefaultNamespace() + ".Areas.Support.Controllers";
        }










        public string GetDataServiceInterfaceBaseInterface(DomainType domainType)
        {
            return domainType.IsDictionary
                ? string.Format("IDictionaryEntityDataService<{0}, {1}, {0}Enum>", domainType.Name, this.GetModelIdClassName(domainType))
                : string.Format("ICompleteDataService<{0}, {1}>", domainType.Name, this.GetModelIdClassName(domainType));
        }
        public string GetDataServiceBaseClass(DomainType domainType)
        {
            return domainType.IsDictionary
                ? string.Format("DictionaryEntityDataService<{0}, {1}, {0}Enum>, I{0}DataService", domainType.Name, this.GetModelIdClassName(domainType))
                : string.Format("DbContextDataServiceBase<{2}, {0}, {1}>, I{0}DataService", domainType.Name, this.GetModelIdClassName(domainType), transformationHelper.AppDefinition.DataContextName);
        }
        public string GetDataServiceTestsBaseClass(DomainType domainType)
        {
            return domainType.IsDictionary
                ? string.Format("DictionaryEntityLayerTestFixtureBase<{0}, {1}, {0}Enum>", domainType.Name, this.GetModelIdClassName(domainType))
                : string.Format("EntityLayerTestFixtureBase<{0}, {1}, {0}EntityWrapper, {0}TestsStrategy>", domainType.Name, this.GetModelIdClassName(domainType));
        }
        public string GetDataServiceTestsStrategyBaseClass(DomainType domainType)
        {
            return string.Format("IEntityLayerTestsStrategy<{0}, {1}, {0}EntityWrapper>", domainType.Name, this.GetModelIdClassName(domainType));
        }
        //IEntityLayerTestFixtureStrategy<TEntity, TId, TEntityWrapper>
        public string GetEntityFrameworkMappingBaseClass(DomainType domainType)
        {
            return domainType.IsDictionary
                ? string.Format("DictionaryEntityMapping<{0}, {1}, {0}Enum>", domainType.Name, this.GetModelIdClassName(domainType))
                : string.Format("CoreEntityMappingBase<{0}, {1}>", domainType.Name, this.GetModelIdClassName(domainType));
        }
        public string GetServiceInterfaceBaseInterface(DomainType domainType)
        {
            return domainType.IsDictionary
                ? string.Format("IDictionaryEntityService<{0}, {1}, {0}Enum>", domainType.Name, this.GetModelIdClassName(domainType))
                : string.Format("ICompleteService<{0}, {1}>", domainType.Name, this.GetModelIdClassName(domainType));
        }
        public string GetServiceBaseClass(DomainType domainType)
        {
            return domainType.IsDictionary
                ? string.Format("DictionaryEntityService<{0}, {1}, {0}Enum>, I{0}Service", domainType.Name, this.GetModelIdClassName(domainType))
                : string.Format("DbContextServiceBase<{2}, {0}, {1}>, I{0}Service", domainType.Name, this.GetModelIdClassName(domainType), transformationHelper.AppDefinition.DataContextName);
        }
        public string GetServiceTestsBaseClass(DomainType domainType)
        {
            return domainType.IsDictionary
                ? string.Format("DictionaryEntityLayerTestFixtureBase<{0}, {1}, {0}Enum>", domainType.Name, this.GetModelIdClassName(domainType))
                : string.Format("EntityLayerTestFixtureBase<{0}, {1}, {0}EntityWrapper, {0}TestsStrategy>", domainType.Name, this.GetModelIdClassName(domainType));
        }
        public string GetSupportViewModelBaseClass(DomainType domainType)
        {
            return !String.IsNullOrEmpty(domainType.BaseClassOverride)
                    ? transformationHelper.DomainTypeFilters.GetDomainTypeByFullTypeName(domainType.BaseClassOverride).Name + "Model"
                    : domainType.IsDictionary
                        ? string.Format("DictionaryEntityModelBase<{0}>", this.GetModelIdClassName(domainType))
                        : string.Format("AuditableModelBase<{0}>", this.GetModelIdClassName(domainType));
        }
        public string GetSupportViewModelMetadataBaseClass(DomainType domainType)
        {
            return !String.IsNullOrEmpty(domainType.BaseClassOverride)
                    ? transformationHelper.DomainTypeFilters.GetDomainTypeByFullTypeName(domainType.BaseClassOverride).Name + "ModelMetadata"
                    : domainType.IsDictionary
                        ? ""
                        : "AuditableModelBaseMetadata";
        }
        public string GetValidatorBaseClass(DomainType domainType)
        {
            return !String.IsNullOrEmpty(domainType.BaseClassOverride)
                    ? string.Format("{0}Validator<{1}>", transformationHelper.DomainTypeFilters.GetDomainTypeByFullTypeName(domainType.BaseClassOverride).Name, domainType.Name)
                    : domainType.IsAbstract
                        ? string.Format("DbEntityValidatorBase<T> where T: {0}", domainType.Name)
                        : string.Format("DbEntityValidatorBase<{0}>", domainType.Name);
        }



















        public string GetKendoUIModelDeclarationForProperty(Property property, string returnValue = "")
        {
            if (property.IsRelationship)
            {
                var type = this.transformationHelper.DomainTypeFilters.GetDomainTypeByFullTypeName(property.Type);
                if (type.WebOptions.GenerateSupportIndexView)
                {
                    returnValue = String.Format("{0}{1}: {{   }}", returnValue, property.Name);
                    foreach (var prop in type.Properties)
                    {
                        //returnValue = this.GetKendoUIModelDeclarationForProperty(prop, returnValue) + ",";
                    }
                }
            }
            else
            {
                var propertyType = this.GetKendoUIModelPropertyType(property.Type);
                returnValue = String.Format("{0}{1}:  {{ type: {3}\"{2}{3}\", editable: false }}", returnValue, property.Name, propertyType, @"\");
            }
            return returnValue;
        }
        private string GetKendoUIModelPropertyType(string propertyTypeName)
        {
            var propertyType = propertyTypeName.ToLowerInvariant();
            switch (propertyType)
            {
                case "currency":
                case "decimal":
                case "int":
                case "double":
                case "byte":
                    propertyType = "number";
                    break;
                case "datetime":
                    propertyType = "date";
                    break;
                case "bool":
                    propertyType = "boolean";
                    break;
                case "guid":
                    propertyType = "string";
                    break;
            }
            return propertyType;
        }
    }
}
