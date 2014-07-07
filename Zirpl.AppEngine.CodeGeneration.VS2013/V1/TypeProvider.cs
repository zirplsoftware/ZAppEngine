using System;
using Zirpl.AppEngine.CodeGeneration.V1.ConfigModel;

namespace Zirpl.AppEngine.CodeGeneration.V1
{
    public class TypeProvider
    {
        private TransformationHelper templateHelper;

        public TypeProvider(TransformationHelper templateHelper)
        {
            this.templateHelper = templateHelper;
        }

        public string GetModelBaseClass(DomainType domainType)
        {
            return !String.IsNullOrEmpty(domainType.BaseClassOverride)
                    ? domainType.BaseClassOverride
                    : domainType.IsDictionary
                        ? string.Format("DictionaryEntityBase<{0}, {1}Enum>", this.GetIdClass(domainType), domainType.Name)
                        : string.Format("AuditableBase<{0}>", this.GetIdClass(domainType));
        }
        public string GetMetadataBaseClass(DomainType domainType)
        {
            return !String.IsNullOrEmpty(domainType.BaseClassOverride)
                ? domainType.BaseClassOverride + "Metadata"
                : domainType.IsDictionary
                    ? "DictionaryEntityBaseMetadataBase"
                    : "MetadataBase";
        }
        public string GetDataServiceInterfaceBaseInterface(DomainType domainType)
        {
            return domainType.IsDictionary
                ? string.Format("IDictionaryEntityDataService<{0}, {1}, {0}Enum>", domainType.Name, this.GetIdClass(domainType))
                : string.Format("ICompleteDataService<{0}, {1}>", domainType.Name, this.GetIdClass(domainType));
        }
        public string GetDataServiceBaseClass(DomainType domainType)
        {
            return domainType.IsDictionary
                ? string.Format("DictionaryEntityDataService<{0}, {1}, {0}Enum>, I{0}DataService", domainType.Name, this.GetIdClass(domainType))
                : string.Format("DbContextDataServiceBase<{2}, {0}, {1}>, I{0}DataService", domainType.Name, this.GetIdClass(domainType), templateHelper.AppDefinition.DataContextName);
        }
        public string GetDataServiceTestsBaseClass(DomainType domainType)
        {
            return domainType.IsDictionary
                ? string.Format("DictionaryEntityLayerTestFixtureBase<{0}, {1}, {0}Enum>", domainType.Name, this.GetIdClass(domainType))
                : string.Format("EntityLayerTestFixtureBase<{0}, {1}, {0}EntityWrapper, {0}TestsStrategy>", domainType.Name, this.GetIdClass(domainType));
        }
        public string GetDataServiceTestsStrategyBaseClass(DomainType domainType)
        {
            return string.Format("IEntityLayerTestsStrategy<{0}, {1}, {0}EntityWrapper>", domainType.Name, this.GetIdClass(domainType));
        }
        //IEntityLayerTestFixtureStrategy<TEntity, TId, TEntityWrapper>
        public string GetEntityFrameworkMappingBaseClass(DomainType domainType)
        {
            return domainType.IsDictionary
                ? string.Format("DictionaryEntityMapping<{0}, {1}, {0}Enum>", domainType.Name, this.GetIdClass(domainType))
                : string.Format("CoreEntityMappingBase<{0}, {1}>", domainType.Name, this.GetIdClass(domainType));
        }
        public string GetServiceInterfaceBaseInterface(DomainType domainType)
        {
            return domainType.IsDictionary
                ? string.Format("IDictionaryEntityService<{0}, {1}, {0}Enum>", domainType.Name, this.GetIdClass(domainType))
                : string.Format("ICompleteService<{0}, {1}>", domainType.Name, this.GetIdClass(domainType));
        }
        public string GetServiceBaseClass(DomainType domainType)
        {
            return domainType.IsDictionary
                ? string.Format("DictionaryEntityService<{0}, {1}, {0}Enum>, I{0}Service", domainType.Name, this.GetIdClass(domainType))
                : string.Format("DbContextServiceBase<{2}, {0}, {1}>, I{0}Service", domainType.Name, this.GetIdClass(domainType), templateHelper.AppDefinition.DataContextName);
        }
        public string GetServiceTestsBaseClass(DomainType domainType)
        {
            return domainType.IsDictionary
                ? string.Format("DictionaryEntityLayerTestFixtureBase<{0}, {1}, {0}Enum>", domainType.Name, this.GetIdClass(domainType))
                : string.Format("EntityLayerTestFixtureBase<{0}, {1}, {0}EntityWrapper, {0}TestsStrategy>", domainType.Name, this.GetIdClass(domainType));
        }
        public string GetSupportViewModelBaseClass(DomainType domainType)
        {
            return !String.IsNullOrEmpty(domainType.BaseClassOverride)
                    ? templateHelper.DomainTypeFilters.GetDomainTypeByFullTypeName(domainType.BaseClassOverride).Name + "Model"
                    : domainType.IsDictionary
                        ? string.Format("DictionaryEntityModelBase<{0}>", this.GetIdClass(domainType))
                        : string.Format("AuditableModelBase<{0}>", this.GetIdClass(domainType));
        }
        public string GetSupportViewModelMetadataBaseClass(DomainType domainType)
        {
            return !String.IsNullOrEmpty(domainType.BaseClassOverride)
                    ? templateHelper.DomainTypeFilters.GetDomainTypeByFullTypeName(domainType.BaseClassOverride).Name + "ModelMetadata"
                    : domainType.IsDictionary
                        ? ""
                        : "AuditableModelBaseMetadata";
        }
        public string GetValidatorBaseClass(DomainType domainType)
        {
            return !String.IsNullOrEmpty(domainType.BaseClassOverride)
                    ? string.Format("{0}Validator<{1}>", templateHelper.DomainTypeFilters.GetDomainTypeByFullTypeName(domainType.BaseClassOverride).Name, domainType.Name)
                    : domainType.IsAbstract
                        ? string.Format("DbEntityValidatorBase<T> where T: {0}", domainType.Name)
                        : string.Format("DbEntityValidatorBase<{0}>", domainType.Name);
        }
        public string GetIdClass(DomainType domainType)
        {
            return !String.IsNullOrEmpty(domainType.IdTypeOverride)
                                    ? domainType.IdTypeOverride
                                    : domainType.IsDictionary
                                        ? "byte"
                                        : "int";
        }
    }
}
