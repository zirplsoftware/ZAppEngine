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
        public String GetModelTypeName(DomainType domainType)
        {
            return domainType.Name;
        }
        public string GetModelTypeFullName(DomainType domainType)
        {
            return String.Format("{0}.{1}", this.GetModelNamespace(domainType), this.GetModelTypeName(domainType));
        }
        public string GetModelBaseDeclaration(DomainType domainType)
        {
            return !String.IsNullOrEmpty(domainType.BaseClassOverride)
                    ? domainType.BaseClassOverride
                    : domainType.IsDictionary
                        ? string.Format("DictionaryEntityBase<{0}, {1}>", this.GetModelIdTypeName(domainType), this.GetModelEnumTypeName(domainType))
                        : string.Format("AuditableBase<{0}>", this.GetModelIdTypeName(domainType));
        }
        public string GetModelIdTypeName(DomainType domainType)
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
        public String GetModelMetadataTypeName(DomainType domainType)
        {
            return domainType.Name + "Metadata";
        }
        public String GetModelMetadataTypeFullName(DomainType domainType)
        {
            return String.Format("{0}.{1}", this.GetModelMetadataNamespace(domainType), this.GetModelMetadataTypeName(domainType));
        }
        public string GetModelMetadataBaseDeclaration(DomainType domainType)
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
        public String GetModelEnumTypeName(DomainType domainType)
        {
            return domainType.Name + "Enum";
        }
        public String GetModelEnumTypeFullName(DomainType domainType)
        {
            return String.Format("{0}.{1}", this.GetModelEnumNamespace(domainType), this.GetModelEnumTypeName(domainType));
        }
        public string GetModelEnumBaseDeclaration(DomainType domainType)
        {
            // it should match the Model's ID type
            return this.GetModelIdTypeName(domainType);
        }
        #endregion

        #region DataServiceInterface related methods
        public string GetDataServiceInterfaceNamespace(DomainType domainType)
        {
            return transformationHelper.ProjectProvider.DataServiceProject.GetDefaultNamespace() + (String.IsNullOrEmpty(domainType.SubNamespace) ? null : "." + domainType.SubNamespace);
        }
        public String GetDataServiceInterfaceTypeName(DomainType domainType)
        {
            return "I" + domainType.Name + "DataService";
        }
        public String GetDataServiceInterfaceTypeFullName(DomainType domainType)
        {
            return String.Format("{0}.{1}", this.GetDataServiceInterfaceNamespace(domainType), this.GetDataServiceInterfaceTypeName(domainType));
        }
        public string GetDataServiceInterfaceBaseDeclaration(DomainType domainType)
        {
            return domainType.IsDictionary
                ? string.Format("IDictionaryEntityDataService<{0}, {1}, {2}>", this.GetModelTypeName(domainType), this.GetModelIdTypeName(domainType), this.GetModelEnumTypeName(domainType))
                : string.Format("ICompleteDataService<{0}, {1}>", this.GetModelTypeName(domainType), this.GetModelIdTypeName(domainType));
        }
        #endregion

        #region DataService related methods
        public string GetDataServiceNamespace(DomainType domainType)
        {
            return transformationHelper.ProjectProvider.DataServiceProject.GetDefaultNamespace() + (String.IsNullOrEmpty(domainType.SubNamespace) ? null : "." + domainType.SubNamespace);
        }
        public String GetDataServiceTypeName(DomainType domainType)
        {
            return domainType.Name + "DataService";
        }
        public String GetDataServiceTypeFullName(DomainType domainType)
        {
            return String.Format("{0}.{1}", this.GetDataServiceNamespace(domainType), this.GetDataServiceTypeName(domainType));
        }
        public string GetDataServiceBaseDeclaration(DomainType domainType)
        {
            return domainType.IsDictionary
                ? string.Format("ReadOnlyDbContextDataServiceBase<{0}, {1}, {2}>, {3}", transformationHelper.AppDefinition.DataContextName, this.GetModelTypeName(domainType), this.GetModelIdTypeName(domainType), this.GetDataServiceInterfaceTypeName(domainType))
                : string.Format("DbContextDataServiceBase<{0}, {1}, {2}>, {3}", transformationHelper.AppDefinition.DataContextName, this.GetModelTypeName(domainType), this.GetModelIdTypeName(domainType), this.GetDataServiceInterfaceTypeName(domainType));
        }
        #endregion

        #region DataContext related methods
        public string GetDataContextNamespace()
        {
            return transformationHelper.ProjectProvider.DataServiceProject.GetDefaultNamespace();
        }
        public String GetDataContextTypeName()
        {
            return this.transformationHelper.AppDefinition.DataContextName;
        }
        public String GetDataContextTypeFullName()
        {
            return String.Format("{0}.{1}", this.GetDataContextNamespace(), this.GetDataContextTypeName());
        }
        #endregion

        #region Entity Framework mapping related methods
        public string GetEntityFrameworkMappingNamespace(DomainType domainType)
        {
            return transformationHelper.ProjectProvider.DataServiceProject.GetDefaultNamespace() + ".Mapping" +(String.IsNullOrEmpty(domainType.SubNamespace) ? null : "." + domainType.SubNamespace);
        }
        public String GetEntityFrameworkMappingTypeName(DomainType domainType)
        {
            return domainType.Name + "Mapping";
        }
        public String GetEntityFrameworkMappingTypeFullName(DomainType domainType)
        {
            return String.Format("{0}.{1}", this.GetEntityFrameworkMappingNamespace(domainType), this.GetEntityFrameworkMappingTypeName(domainType));
        }
        public string GetEntityFrameworkMappingBaseDeclaration(DomainType domainType)
        {
            return domainType.IsDictionary
                ? string.Format("DictionaryEntityMapping<{0}, {1}, {2}>", this.GetModelTypeName(domainType), this.GetModelIdTypeName(domainType), this.GetModelEnumTypeName(domainType))
                : string.Format("CoreEntityMappingBase<{0}, {1}>", this.GetModelTypeName(domainType), this.GetModelIdTypeName(domainType));
        }
        #endregion

        #region Property-related methods
        public String GetPropertyTypeName(Property property)
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
        public String GetRelationshipIdPropertyTypeName(Property property)
        {
            var relationshipModel = this.transformationHelper.DomainTypeFilters.GetDomainTypeByFullTypeName(property.Type);
            string relationshipIdTypeString = this.GetModelIdTypeName(relationshipModel);
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










        public string GetDataServiceTestsBaseClass(DomainType domainType)
        {
            return domainType.IsDictionary
                ? string.Format("DictionaryEntityLayerTestFixtureBase<{0}, {1}, {0}Enum>", domainType.Name, this.GetModelIdTypeName(domainType))
                : string.Format("EntityLayerTestFixtureBase<{0}, {1}, {0}EntityWrapper, {0}TestsStrategy>", domainType.Name, this.GetModelIdTypeName(domainType));
        }
        public string GetDataServiceTestsStrategyBaseClass(DomainType domainType)
        {
            return string.Format("IEntityLayerTestsStrategy<{0}, {1}, {0}EntityWrapper>", domainType.Name, this.GetModelIdTypeName(domainType));
        }
        //IEntityLayerTestFixtureStrategy<TEntity, TId, TEntityWrapper>
        public string GetEntityFrameworkMappingBaseClass(DomainType domainType)
        {
            return domainType.IsDictionary
                ? string.Format("DictionaryEntityMapping<{0}, {1}, {0}Enum>", domainType.Name, this.GetModelIdTypeName(domainType))
                : string.Format("CoreEntityMappingBase<{0}, {1}>", domainType.Name, this.GetModelIdTypeName(domainType));
        }
        public string GetServiceInterfaceBaseInterface(DomainType domainType)
        {
            return domainType.IsDictionary
                ? string.Format("IDictionaryEntityService<{0}, {1}, {0}Enum>", domainType.Name, this.GetModelIdTypeName(domainType))
                : string.Format("ICompleteService<{0}, {1}>", domainType.Name, this.GetModelIdTypeName(domainType));
        }
        public string GetServiceBaseClass(DomainType domainType)
        {
            return domainType.IsDictionary
                ? string.Format("DictionaryEntityService<{0}, {1}, {0}Enum>, I{0}Service", domainType.Name, this.GetModelIdTypeName(domainType))
                : string.Format("DbContextServiceBase<{2}, {0}, {1}>, I{0}Service", domainType.Name, this.GetModelIdTypeName(domainType), transformationHelper.AppDefinition.DataContextName);
        }
        public string GetServiceTestsBaseClass(DomainType domainType)
        {
            return domainType.IsDictionary
                ? string.Format("DictionaryEntityLayerTestFixtureBase<{0}, {1}, {0}Enum>", domainType.Name, this.GetModelIdTypeName(domainType))
                : string.Format("EntityLayerTestFixtureBase<{0}, {1}, {0}EntityWrapper, {0}TestsStrategy>", domainType.Name, this.GetModelIdTypeName(domainType));
        }
        public string GetSupportViewModelBaseClass(DomainType domainType)
        {
            return !String.IsNullOrEmpty(domainType.BaseClassOverride)
                    ? transformationHelper.DomainTypeFilters.GetDomainTypeByFullTypeName(domainType.BaseClassOverride).Name + "Model"
                    : domainType.IsDictionary
                        ? string.Format("DictionaryEntityModelBase<{0}>", this.GetModelIdTypeName(domainType))
                        : string.Format("AuditableModelBase<{0}>", this.GetModelIdTypeName(domainType));
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
