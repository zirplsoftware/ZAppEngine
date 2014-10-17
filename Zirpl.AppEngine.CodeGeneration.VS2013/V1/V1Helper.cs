using System;
using System.Collections.Generic;
using System.IO;
using System.Linq;
using System.Runtime.Remoting.Messaging;
using System.Text;
using System.Threading.Tasks;
using System.Xml.Serialization;
using EnvDTE;
using EnvDTE80;
using Microsoft.VisualStudio.TextTemplating;
using Zirpl.AppEngine.CodeGeneration.TextTemplating;
using Zirpl.AppEngine.CodeGeneration.V1.ConfigModel;
using Property = Zirpl.AppEngine.CodeGeneration.V1.ConfigModel.Property;

namespace Zirpl.AppEngine.CodeGeneration.V1
{
    public class V1Helper : TransformationHelperBase
    {
        public AppDefinition AppDefinition { get; private set; }


        public V1Helper(TextTransformation callingTemplate)
            :base(callingTemplate)
        {
            #region loading config
            var domaintTypeConfigFilePaths = new List<string>();
            String appDefinitionConfigFilePath = null;
            var modelConfigFileProjectItems = this.CodeGenerationProject.ProjectItems.GetAllProjectItemsRecursive();
            foreach (var modelConfigProjectItem in modelConfigFileProjectItems)
            {
                var fullPath = modelConfigProjectItem.GetFullPath();
                if (fullPath.EndsWith(".model.xml"))
                {
                    domaintTypeConfigFilePaths.Add(fullPath);
                }
                else if (fullPath.EndsWith(".app.xml"))
                {
                    appDefinitionConfigFilePath = fullPath;
                }
            }

            var xmlSerializer = new XmlSerializer(typeof(AppDefinition));
            using (var fileStream = new FileStream(appDefinitionConfigFilePath, FileMode.Open, FileAccess.Read))
            {
                this.AppDefinition = (AppDefinition)xmlSerializer.Deserialize(fileStream);
            }
            foreach (var path in domaintTypeConfigFilePaths)
            {
                DomainType domainType = null;
                xmlSerializer = new XmlSerializer(typeof(DomainType));
                using (var fileStream = new FileStream(path, FileMode.Open, FileAccess.Read))
                {
                    domainType = (DomainType)xmlSerializer.Deserialize(fileStream);
                }
                this.AppDefinition.DomainTypes.Add(domainType);
            }
            #endregion
        }

        #region Solution/Project members- no virtual members
        public DTE2 VisualStudio
        {
            get
            {
                return this.FileManager.VisualStudio;
            }
        }

        public Project CodeGenerationProject
        {
            get
            {
                return this.FileManager.TemplateProjectItem.ContainingProject;
            }
        }

        public Project ModelProject
        {
            get
            {
                return this.VisualStudio.GetProject(this.AppDefinition.ModelProjectName);
            }
        }

        public Project DataServiceProject
        {
            get
            {
                return this.VisualStudio.GetProject(this.AppDefinition.DataServiceProjectName);
            }
        }

        public Project DataServiceTestsProject
        {
            get
            {
                return this.VisualStudio.GetProject(this.AppDefinition.DataServiceTestsProjectName);
            }
        }

        public Project ServiceProject
        {
            get
            {
                return this.VisualStudio.GetProject(this.AppDefinition.ServiceProjectName);
            }
        }

        public Project WebProject
        {
            get
            {
                return this.VisualStudio.GetProject(this.AppDefinition.WebProjectName);
            }
        }

        public Project WebCoreProject
        {
            get
            {
                return this.VisualStudio.GetProject(this.AppDefinition.WebCoreProjectName);
            }
        }

        public Project ServiceTestsProject
        {
            get
            {
                return this.VisualStudio.GetProject(this.AppDefinition.ServiceTestsProjectName);
            }
        }

        public Project TestsCommonProject
        {
            get
            {
                return this.VisualStudio.GetProject(this.AppDefinition.TestsCommonProjectName);
            }
        }


        public string GetGeneratedCodeFolder(DomainType domainType, bool includeGeneratedCodeRootFolderName = true)
        {
            return (includeGeneratedCodeRootFolderName ? this.AppDefinition.GeneratedCodeRootFolderName : "") + domainType.SubNamespace.Replace(".", @"\");
        }
        #endregion
        
        #region DomainType filter members- contains virtual members

        public DomainType GetDomainTypeByFullTypeName(string fullTypeName)
        {
            return (from dt in this.AppDefinition.DomainTypes
                    where this.GetModelTypeFullName(dt) == fullTypeName
                    select dt).SingleOrDefault();
        }
        public virtual IEnumerable<DomainType> DomainTypesToGenerateModelFor
        {
            get
            {
                return from dt in this.AppDefinition.DomainTypes
                       where dt.ModelOptions.GenerateModel
                       select dt;
            }
        }
        public virtual IEnumerable<DomainType> DomainTypesToCustomFieldValueFor
        {
            get
            {
                return from dt in this.AppDefinition.DomainTypes
                       where dt.IsCustomizable
                            //&& !dt.IsAbstract  TODO: add in a file validation at the beginning that checks to ensure only concrete classes are extendable
                            // TODO: OR should abstract classes be customizable? in some ways, it may make more sense to put it there
                       select dt;
            }
        }
        public virtual IEnumerable<DomainType> DomainTypesToGenerateMetadataConstantsFor
        {
            get
            {
                return from dt in this.AppDefinition.DomainTypes
                       where dt.ModelOptions.GenerateMetadataConstants
                       select dt;
            }
        }
        public virtual IEnumerable<DomainType> DomainTypesToGenerateModelEnumFor
        {
            get
            {
                return from dt in this.AppDefinition.DomainTypes
                       where dt.IsPersistable
                             && dt.IsDictionary
                             && dt.ModelOptions.GenerateEnum
                       select dt;
            }
        }
        public virtual IEnumerable<DomainType> DomainTypesToGenerateDataServiceInterfaceFor
        {
            get
            {
                return from dt in this.AppDefinition.DomainTypes
                       where dt.IsPersistable
                            && dt.DataServiceOptions.GenerateDataServiceInterface
                       select dt;
            }
        }
        public virtual IEnumerable<DomainType> DomainTypesToGenerateDataServiceFor
        {
            get
            {
                return from dt in this.AppDefinition.DomainTypes
                       where dt.IsPersistable
                            && dt.DataServiceOptions.GenerateDataService
                       select dt;
            }
        }
        public virtual IEnumerable<DomainType> DomainTypesToGenerateDataContextPropertyFor
        {
            get
            {
                return from dt in this.AppDefinition.DomainTypes
                       where dt.IsPersistable
                            && dt.DataServiceOptions.GenerateDataContextProperty
                       select dt;
            }
        }
        public virtual IEnumerable<DomainType> DomainTypesToGenerateEntityFrameworkMappingFor
        {
            get
            {
                return from dt in this.AppDefinition.DomainTypes
                       where dt.IsPersistable
                            && dt.DataServiceOptions.GenerateEntityFrameworkMapping
                       select dt;
            }
        }
        public virtual IEnumerable<DomainType> DomainTypesToGenerateServiceInterfaceFor
        {
            get
            {
                return from dt in this.AppDefinition.DomainTypes
                       where dt.IsPersistable
                            && dt.ServiceOptions.GenerateServiceInterface
                       select dt;
            }
        }
        public virtual IEnumerable<DomainType> DomainTypesToGenerateServiceFor
        {
            get
            {
                return from dt in this.AppDefinition.DomainTypes
                       where dt.IsPersistable
                            && dt.ServiceOptions.GenerateService
                       select dt;
            }
        }
        public virtual IEnumerable<DomainType> DomainTypesToGenerateValidatorFor
        {
            get
            {
                return from dt in this.AppDefinition.DomainTypes
                       where dt.ServiceOptions.GenerateValidator
                             && !dt.IsDictionary
                             && dt.ModelOptions.GenerateModel
                       select dt;
            }
        }

        #endregion

        #region Model-related methods

        public bool HasCustomizableEntities
        {
            get
            {
                return (from dt in this.AppDefinition.DomainTypes
                    where dt.IsCustomizable
                    select dt).Any();
            }
        }

        public void StartModelFile(DomainType domainType)
        {
            this.FileManager.StartNewFile(
                this.GetModelTypeName(domainType) + this.AppDefinition.GeneratedCSFileExtension,
                this.ModelProject,
                this.GetGeneratedCodeFolder(domainType),
                new OutputFileProperties() { BuildAction = OutputFileBuildActionType.Compile });
        }
        public virtual string GetModelNamespace(DomainType domainType)
        {
            return this.ModelProject.GetDefaultNamespace() + (String.IsNullOrEmpty(domainType.SubNamespace) ? null : "." + domainType.SubNamespace);
        }
        public virtual String GetModelTypeName(DomainType domainType)
        {
            return domainType.Name;
        }
        public virtual string GetModelTypeFullName(DomainType domainType)
        {
            return String.Format("{0}.{1}", this.GetModelNamespace(domainType), this.GetModelTypeName(domainType));
        }
        public virtual string GetModelBaseDeclaration(DomainType domainType)
        {
            String customizableInterfaceDeclaration = null;
            if (domainType.IsCustomizable)
            {
                customizableInterfaceDeclaration =
                    String.Format("ICustomizable<{0}, {1}, {2}>", this.GetModelTypeName(domainType), this.GetCustomFieldValueTypeName(domainType), this.GetModelIdTypeName(domainType));
            }
            var baseDeclaration = !String.IsNullOrEmpty(domainType.BaseClassOverride)
                    ? " : " + domainType.BaseClassOverride
                    : domainType.IsPersistable 
                        ? domainType.IsDictionary
                            ? string.Format(" : DictionaryEntityBase<{0}, {1}>", this.GetModelIdTypeName(domainType), this.GetModelEnumTypeName(domainType))
                            : string.Format(" : AuditableBase<{0}>", this.GetModelIdTypeName(domainType))
                        : "";
            if (!String.IsNullOrEmpty(customizableInterfaceDeclaration))
            {
                baseDeclaration = String.IsNullOrEmpty(baseDeclaration)
                    ? " : " + customizableInterfaceDeclaration
                    : baseDeclaration + ", " + customizableInterfaceDeclaration;
            }
   
            return baseDeclaration;
        }
        public virtual string GetModelIdTypeName(DomainType domainType)
        {
            return !String.IsNullOrEmpty(domainType.IdTypeOverride)
                                    ? domainType.IdTypeOverride
                                    : domainType.IsDictionary
                                        ? "byte"
                                        : "int";
        }
        #endregion

        #region MetadataConstants-related methods
        public void StartMetadataConstantsFile(DomainType domainType)
        {
            this.FileManager.StartNewFile(
                this.GetMetadataConstantsTypeName(domainType) + this.AppDefinition.GeneratedCSFileExtension,
                this.ModelProject,
                this.GetGeneratedCodeFolder(domainType),
                new OutputFileProperties() { BuildAction = OutputFileBuildActionType.Compile });
        }
        public virtual string GetMetadataConstantsNamespace(DomainType domainType)
        {
            // use the same
            return this.GetModelNamespace(domainType);
        }
        public virtual String GetMetadataConstantsTypeName(DomainType domainType)
        {
            return domainType.Name + "MetadataConstants";
        }
        public virtual String GetMetadataConstantsTypeFullName(DomainType domainType)
        {
            return String.Format("{0}.{1}", this.GetMetadataConstantsNamespace(domainType), this.GetMetadataConstantsTypeName(domainType));
        }
        public virtual string GetMetadataConstantsBaseDeclaration(DomainType domainType)
        {
            return !String.IsNullOrEmpty(domainType.BaseClassOverride)
                ? " : " + domainType.BaseClassOverride + "MetadataConstants"
                : domainType.IsPersistable 
                    ? domainType.IsDictionary
                        ? " : DictionaryEntityBaseMetadataConstantsBase"
                        : " : MetadataConstantsBase"
                    : "";
        }
        #endregion

        #region ModelEnum-related methods
        public void StartModelEnumFile(DomainType domainType)
        {
            this.FileManager.StartNewFile(
                this.GetModelEnumTypeName(domainType) + this.AppDefinition.GeneratedCSFileExtension,
                this.ModelProject,
                this.GetGeneratedCodeFolder(domainType),
                new OutputFileProperties() { BuildAction = OutputFileBuildActionType.Compile });
        }
        public virtual string GetModelEnumNamespace(DomainType domainType)
        {
            // use the same
            return this.GetModelNamespace(domainType);
        }
        public virtual String GetModelEnumTypeName(DomainType domainType)
        {
            return domainType.Name + "Enum";
        }
        public virtual String GetModelEnumTypeFullName(DomainType domainType)
        {
            return String.Format("{0}.{1}", this.GetModelEnumNamespace(domainType), this.GetModelEnumTypeName(domainType));
        }
        public virtual string GetModelEnumBaseDeclaration(DomainType domainType)
        {
            // it should match the Model's ID type
            return " : " + this.GetModelIdTypeName(domainType);
        }
        #endregion

        #region CustomFieldValue methods

        public void StartCustomFieldValueFile(DomainType domainType)
        {
            this.FileManager.StartNewFile(
                this.GetCustomFieldValueTypeName(domainType) + this.AppDefinition.GeneratedCSFileExtension,
                this.ModelProject,
                this.AppDefinition.GeneratedCodeRootFolderName + @"Customization\" + this.GetGeneratedCodeFolder(domainType, false),
                new OutputFileProperties() { BuildAction = OutputFileBuildActionType.Compile });
        }
        public virtual string GetCustomFieldValueNamespace(DomainType domainType)
        {
            return this.ModelProject.GetDefaultNamespace() + ".Customization" + (String.IsNullOrEmpty(domainType.SubNamespace) ? null : "." + domainType.SubNamespace);
        }
        public virtual String GetCustomFieldValueTypeName(DomainType domainType)
        {
            return this.GetModelTypeName(domainType) + "CustomFieldValue";
        }
        public virtual string GetCustomFieldValueTypeFullName(DomainType domainType)
        {
            return String.Format("{0}.{1}", this.GetCustomFieldValueNamespace(domainType), this.GetCustomFieldValueTypeName(domainType));
        }
        public virtual string GetCustomFieldValueBaseDeclaration(DomainType domainType)
        {
            return String.Format(" : CustomFieldValueBase<{0}, {1}>", this.GetModelTypeName(domainType),
                this.GetModelIdTypeName(domainType));
        }

        #endregion

        #region DataServiceInterface related methods
        public void StartDataServiceInterfaceFile(DomainType domainType)
        {
            this.FileManager.StartNewFile(
                this.GetDataServiceInterfaceTypeName(domainType) + this.AppDefinition.GeneratedCSFileExtension,
                this.DataServiceProject,
                this.GetGeneratedCodeFolder(domainType),
                new OutputFileProperties() { BuildAction = OutputFileBuildActionType.Compile });
        }
        public virtual string GetDataServiceInterfaceNamespace(DomainType domainType)
        {
            return this.DataServiceProject.GetDefaultNamespace() + (String.IsNullOrEmpty(domainType.SubNamespace) ? null : "." + domainType.SubNamespace);
        }
        public virtual String GetDataServiceInterfaceTypeName(DomainType domainType)
        {
            return "I" + domainType.Name + "DataService";
        }
        public virtual String GetDataServiceInterfaceTypeFullName(DomainType domainType)
        {
            return String.Format("{0}.{1}", this.GetDataServiceInterfaceNamespace(domainType), this.GetDataServiceInterfaceTypeName(domainType));
        }
        public virtual string GetDataServiceInterfaceBaseDeclaration(DomainType domainType)
        {
            return domainType.IsDictionary
                ? string.Format(" : IDictionaryEntityDataService<{0}, {1}, {2}>", this.GetModelTypeName(domainType), this.GetModelIdTypeName(domainType), this.GetModelEnumTypeName(domainType))
                : string.Format(" : ICompleteDataService<{0}, {1}>", this.GetModelTypeName(domainType), this.GetModelIdTypeName(domainType));
        }
        #endregion

        #region DataService related methods
        public void StartDataServiceFile(DomainType domainType)
        {
            this.FileManager.StartNewFile(
                this.GetDataServiceTypeName(domainType) + this.AppDefinition.GeneratedCSFileExtension,
                this.DataServiceProject,
                this.GetGeneratedCodeFolder(domainType),
                new OutputFileProperties() { BuildAction = OutputFileBuildActionType.Compile });
        }
        public virtual string GetDataServiceNamespace(DomainType domainType)
        {
            return this.DataServiceProject.GetDefaultNamespace() + (String.IsNullOrEmpty(domainType.SubNamespace) ? null : "." + domainType.SubNamespace);
        }
        public virtual String GetDataServiceTypeName(DomainType domainType)
        {
            return domainType.Name + "DataService";
        }
        public virtual String GetDataServiceTypeFullName(DomainType domainType)
        {
            return String.Format("{0}.{1}", this.GetDataServiceNamespace(domainType), this.GetDataServiceTypeName(domainType));
        }
        public virtual string GetDataServiceBaseDeclaration(DomainType domainType)
        {
            return domainType.IsDictionary
                ? string.Format(" : ReadOnlyDbContextDataServiceBase<{0}, {1}, {2}>, {3}", this.AppDefinition.DataContextName, this.GetModelTypeName(domainType), this.GetModelIdTypeName(domainType), this.GetDataServiceInterfaceTypeName(domainType))
                : string.Format(" : DbContextDataServiceBase<{0}, {1}, {2}>, {3}", this.AppDefinition.DataContextName, this.GetModelTypeName(domainType), this.GetModelIdTypeName(domainType), this.GetDataServiceInterfaceTypeName(domainType));
        }
        #endregion

        #region DataContext related methods
        public void StartDataContextFile()
        {
            this.FileManager.StartNewFile(
                this.GetDataContextTypeName() + this.AppDefinition.GeneratedCSFileExtension,
                this.DataServiceProject,
                this.AppDefinition.GeneratedCodeRootFolderName,
                new OutputFileProperties() { BuildAction = OutputFileBuildActionType.Compile });
        }
        public virtual string GetDataContextNamespace()
        {
            return this.DataServiceProject.GetDefaultNamespace();
        }
        public virtual String GetDataContextTypeName()
        {
            return this.AppDefinition.DataContextName;
        }
        public virtual String GetDataContextTypeFullName()
        {
            return String.Format("{0}.{1}", this.GetDataContextNamespace(), this.GetDataContextTypeName());
        }
        public virtual String GetDataContextBaseDeclaration()
        {
            return " : DbContextBase";
        }
        #endregion

        #region Entity Framework mapping related methods
        public void StartEntityFrameworkMappingFile(DomainType domainType)
        {
            this.FileManager.StartNewFile(
                this.GetEntityFrameworkMappingTypeName(domainType) + this.AppDefinition.GeneratedCSFileExtension,
                this.DataServiceProject,
                this.AppDefinition.GeneratedCodeRootFolderName + @"Mapping",
                new OutputFileProperties() { BuildAction = OutputFileBuildActionType.Compile });
        }
        public virtual string GetEntityFrameworkMappingNamespace(DomainType domainType)
        {
            return this.DataServiceProject.GetDefaultNamespace() + ".Mapping" + (String.IsNullOrEmpty(domainType.SubNamespace) ? null : "." + domainType.SubNamespace);
        }
        public virtual String GetEntityFrameworkMappingTypeName(DomainType domainType)
        {
            return domainType.Name + "Mapping";
        }
        public virtual String GetEntityFrameworkMappingTypeFullName(DomainType domainType)
        {
            return String.Format("{0}.{1}", this.GetEntityFrameworkMappingNamespace(domainType), this.GetEntityFrameworkMappingTypeName(domainType));
        }
        public virtual string GetEntityFrameworkMappingBaseDeclaration(DomainType domainType)
        {
            return domainType.IsDictionary
                ? string.Format(" : DictionaryEntityMapping<{0}, {1}, {2}>", this.GetModelTypeName(domainType), this.GetModelIdTypeName(domainType), this.GetModelEnumTypeName(domainType))
                : string.Format(" : CoreEntityMappingBase<{0}, {1}>", this.GetModelTypeName(domainType), this.GetModelIdTypeName(domainType));
        }
        #endregion

        #region ServiceInterface-related methods
        public void StartServiceInterfaceFile(DomainType domainType)
        {
            this.FileManager.StartNewFile(
                this.GetServiceInterfaceTypeName(domainType) + this.AppDefinition.GeneratedCSFileExtension,
                this.ServiceProject,
                this.GetGeneratedCodeFolder(domainType),
                new OutputFileProperties() { BuildAction = OutputFileBuildActionType.Compile });
        }
        public virtual string GetServiceInterfaceNamespace(DomainType domainType)
        {
            return this.ServiceProject.GetDefaultNamespace() + (String.IsNullOrEmpty(domainType.SubNamespace) ? null : "." + domainType.SubNamespace);
        }
        public virtual String GetServiceInterfaceTypeName(DomainType domainType)
        {
            return "I" + domainType.Name + "Service";
        }
        public virtual string GetServiceInterfaceTypeFullName(DomainType domainType)
        {
            return String.Format("{0}.{1}", this.GetServiceInterfaceNamespace(domainType), this.GetServiceInterfaceTypeName(domainType));
        }
        public virtual string GetServiceInterfaceBaseDeclaration(DomainType domainType)
        {
            return domainType.IsDictionary
                ? string.Format(" : IDictionaryEntityService<{0}, {1}, {2}>", this.GetModelTypeName(domainType), this.GetModelIdTypeName(domainType), this.GetModelEnumTypeName(domainType))
                : string.Format(" : ICompleteService<{0}, {1}>", this.GetModelTypeName(domainType), this.GetModelIdTypeName(domainType));
        }
        #endregion

        #region Service-related methods
        public void StartServiceFile(DomainType domainType)
        {
            this.FileManager.StartNewFile(
                this.GetServiceTypeName(domainType) + this.AppDefinition.GeneratedCSFileExtension,
                this.ServiceProject,
                this.GetGeneratedCodeFolder(domainType),
                new OutputFileProperties() { BuildAction = OutputFileBuildActionType.Compile });
        }
        public virtual string GetServiceNamespace(DomainType domainType)
        {
            return this.ServiceProject.GetDefaultNamespace() + (String.IsNullOrEmpty(domainType.SubNamespace) ? null : "." + domainType.SubNamespace);
        }
        public virtual String GetServiceTypeName(DomainType domainType)
        {
            return domainType.Name + "Service";
        }
        public virtual string GetServiceTypeFullName(DomainType domainType)
        {
            return String.Format("{0}.{1}", this.GetServiceNamespace(domainType), this.GetServiceTypeName(domainType));
        }
        public virtual string GetServiceBaseDeclaration(DomainType domainType)
        {
            return domainType.IsDictionary
                ? string.Format(" : DictionaryEntityService<{4}, {0}, {1}, {2}>, {3}", this.GetModelTypeName(domainType), this.GetModelIdTypeName(domainType), this.GetModelEnumTypeName(domainType), this.GetServiceInterfaceTypeName(domainType), this.GetDataContextTypeName())
                : string.Format(" : DbContextServiceBase<{2}, {0}, {1}>, {3}", this.GetModelTypeName(domainType), this.GetModelIdTypeName(domainType), this.GetDataContextTypeName(), this.GetServiceInterfaceTypeName(domainType));
        }
        #endregion

        #region Validator-related methods
        public void StartValidatorFile(DomainType domainType)
        {
            this.FileManager.StartNewFile(
                this.GetValidatorFileName(domainType),
                this.ServiceProject,
                this.AppDefinition.GeneratedCodeRootFolderName + @"Validation\" + this.GetGeneratedCodeFolder(domainType, false),
                new OutputFileProperties() { BuildAction = OutputFileBuildActionType.Compile });
        }
        public virtual string GetValidatorNamespace(DomainType domainType)
        {
            return this.ServiceProject.GetDefaultNamespace() + ".Validation" + (String.IsNullOrEmpty(domainType.SubNamespace) ? null : "." + domainType.SubNamespace);
        }
        public virtual String GetValidatorTypeName(DomainType domainType)
        {
            return domainType.IsAbstract
                ? domainType.Name + "Validator<T>"
                : domainType.Name + "Validator";
        }
        public virtual String GetValidatorContructorMemberName(DomainType domainType)
        {
            return domainType.Name + "Validator";
        }
        public virtual String GetValidatorGenericConstraintDeclaration(DomainType domainType)
        {
            return domainType.IsAbstract
                ? "where T : " + this.GetModelTypeName(domainType)
                : "";
        }
        public virtual String GetValidatorFileName(DomainType domainType)
        {
            return domainType.Name + "Validator" + this.AppDefinition.GeneratedCSFileExtension;
        }
        public virtual string GetValidatorTypeFullName(DomainType domainType)
        {
            return String.Format("{0}.{1}", this.GetValidatorNamespace(domainType), this.GetValidatorTypeName(domainType));
        }
        public virtual string GetValidatorBaseDeclaration(DomainType domainType)
        {
            return !String.IsNullOrEmpty(domainType.BaseClassOverride)
                    ? string.Format(" : {0}", this.GetValidatorTypeFullName(this.GetDomainTypeByFullTypeName(domainType.BaseClassOverride)).Replace("<T>", String.Format("<{0}>",this.GetModelTypeName(domainType))))
                    : domainType.IsPersistable 
                        ? domainType.IsAbstract
                            ? " : DbEntityValidatorBase<T>"
                            : string.Format(" : DbEntityValidatorBase<{0}>", this.GetModelTypeName(domainType))
                        : domainType.IsAbstract
                            ? " : AbstractValidator<T>"
                            : string.Format(" : AbstractValidator<{0}>", this.GetModelTypeName(domainType));
        }
        #endregion

        #region Tests Common- EntityWrapper methods

        public void StartPersistableModelTestsEntityWrapperFile(DomainType domainType)
        {
            this.FileManager.StartNewFile(
                this.GetPersistableModelTestsEntityWrapperTypeName(domainType) + this.AppDefinition.GeneratedCSFileExtension, 
                this.TestsCommonProject, 
                this.GetGeneratedCodeFolder(domainType), 
                new OutputFileProperties() { BuildAction = OutputFileBuildActionType.Compile });
        }
        public virtual string GetPersistableModelTestsEntityWrapperNamespace(DomainType domainType)
        {
            return this.TestsCommonProject.GetDefaultNamespace() + (String.IsNullOrEmpty(domainType.SubNamespace) ? null : "." + domainType.SubNamespace);
        }
        public virtual String GetPersistableModelTestsEntityWrapperTypeName(DomainType domainType)
        {
            return domainType.Name + "EntityWrapper";
        }
        public virtual string GetPersistableModelTestsEntityWrapperTypeFullName(DomainType domainType)
        {
            return String.Format("{0}.{1}", this.GetPersistableModelTestsEntityWrapperNamespace(domainType), this.GetPersistableModelTestsEntityWrapperTypeName(domainType));
        }

        #endregion

        #region Tests Common- Strategy methods

        public void StartPersistableModelTestsStrategyFile(DomainType domainType)
        {
            this.FileManager.StartNewFile(
                this.GetPersistableModelTestsStrategyTypeName(domainType) + this.AppDefinition.GeneratedCSFileExtension, 
                this.TestsCommonProject, 
                this.GetGeneratedCodeFolder(domainType), 
                new OutputFileProperties() { BuildAction = OutputFileBuildActionType.Compile });
        }
        public virtual string GetPersistableModelTestsStrategyNamespace(DomainType domainType)
        {
            return this.TestsCommonProject.GetDefaultNamespace() + (String.IsNullOrEmpty(domainType.SubNamespace) ? null : "." + domainType.SubNamespace);
        }
        public virtual String GetPersistableModelTestsStrategyTypeName(DomainType domainType)
        {
            return domainType.Name + "TestsStrategy";
        }
        public virtual string GetPersistableModelTestsStrategyTypeFullName(DomainType domainType)
        {
            return String.Format("{0}.{1}", this.GetPersistableModelTestsStrategyNamespace(domainType), this.GetPersistableModelTestsStrategyTypeName(domainType));
        }

        public string GetPersistableModelTestsStrategyBaseDeclaration(DomainType domainType)
        {
            return string.Format(" : IEntityLayerTestsStrategy<{0}, {1}, {2}>", domainType.Name, this.GetModelIdTypeName(domainType), this.GetPersistableModelTestsEntityWrapperTypeName(domainType));
        }

        
        #endregion

        #region DataService Tests- DataServiceProvider methods

        public void StartDataServiceTestsDataServicesProviderFile()
        {
            this.FileManager.StartNewFile(
                this.GetDataServiceTestsDataServicesProviderTypeName() + this.AppDefinition.GeneratedCSFileExtension,
                this.DataServiceTestsProject,
                this.AppDefinition.GeneratedCodeRootFolderName,
                new OutputFileProperties() { BuildAction = OutputFileBuildActionType.Compile });
        }

        public virtual String GetDataServiceTestsDataServicesProviderTypeName()
        {
            return "DataServicesProvider";
        }
        public virtual string GetDataServiceTestsDataServicesProviderNamespace()
        {
            return this.DataServiceTestsProject.GetDefaultNamespace();
        }

        #endregion

        #region DataServiceTests related methods

        public void StartDataServiceTestsFile(DomainType domainType)
        {
            this.FileManager.StartNewFile(
                this.GetDataServiceTestsTypeName(domainType) + this.AppDefinition.GeneratedCSFileExtension,
                this.DataServiceTestsProject,
                this.GetGeneratedCodeFolder(domainType),
                new OutputFileProperties() { BuildAction = OutputFileBuildActionType.Compile });
        }
        public virtual string GetDataServiceTestsNamespace(DomainType domainType)
        {
            return this.DataServiceTestsProject.GetDefaultNamespace() + (String.IsNullOrEmpty(domainType.SubNamespace) ? null : "." + domainType.SubNamespace);
        }
        public virtual String GetDataServiceTestsTypeName(DomainType domainType)
        {
            return this.GetDataServiceTypeName(domainType) + "Tests";
        }
        public virtual string GetDataServiceTestsTypeFullName(DomainType domainType)
        {
            return String.Format("{0}.{1}", this.GetDataServiceTestsNamespace(domainType), this.GetDataServiceTestsTypeName(domainType));
        }
        public string GetDataServiceTestsBaseTypeName(DomainType domainType)
        {
            return this.GetDataServiceTestsTypeName(domainType) + "Base";
        }
        public virtual String GetDataServiceTestsBaseTypeBaseDeclaration(DomainType domainType)
        {
            return domainType.IsDictionary
                ? string.Format(" : DictionaryEntityLayerTestFixtureBase<{0}, {1}, {2}>", this.GetModelTypeName(domainType), this.GetModelIdTypeName(domainType), this.GetModelEnumTypeName(domainType))
                : string.Format(" : EntityLayerTestFixtureBase<{0}, {1}, {2}, {3}>", this.GetModelTypeName(domainType), this.GetModelIdTypeName(domainType), this.GetPersistableModelTestsEntityWrapperTypeName(domainType), this.GetPersistableModelTestsStrategyTypeName(domainType));
        }

        #endregion

        #region Service Tests- ServiceProvider methods

        public void StartServiceTestsServicesProviderFile()
        {
            this.FileManager.StartNewFile(
                this.GetServiceTestsServicesProviderTypeName() + this.AppDefinition.GeneratedCSFileExtension,
                this.ServiceTestsProject,
                this.AppDefinition.GeneratedCodeRootFolderName,
                new OutputFileProperties() { BuildAction = OutputFileBuildActionType.Compile });
        }
        public virtual String GetServiceTestsServicesProviderTypeName()
        {
            return "ServicesProvider";
        }
        public virtual String GetServiceTestsServicesProviderTypeFullName()
        {
            return String.Format("{0}.{1}", this.GetServiceTestsServicesProviderNamespace(), this.GetServiceTestsServicesProviderTypeName());
        }
        public virtual string GetServiceTestsServicesProviderNamespace()
        {
            return this.ServiceTestsProject.GetDefaultNamespace();
        }

        #endregion

        #region ServiceTests related methods

        public void StartServiceTestsFile(DomainType domainType)
        {
            this.FileManager.StartNewFile(
                this.GetServiceTestsTypeName(domainType) + this.AppDefinition.GeneratedCSFileExtension,
                this.ServiceTestsProject,
                this.GetGeneratedCodeFolder(domainType),
                new OutputFileProperties() { BuildAction = OutputFileBuildActionType.Compile });
        }
        public virtual string GetServiceTestsNamespace(DomainType domainType)
        {
            return this.ServiceTestsProject.GetDefaultNamespace() + (String.IsNullOrEmpty(domainType.SubNamespace) ? null : "." + domainType.SubNamespace);
        }
        public virtual String GetServiceTestsTypeName(DomainType domainType)
        {
            return this.GetServiceTypeName(domainType) + "Tests";
        }
        public virtual string GetServiceTestsTypeFullName(DomainType domainType)
        {
            return String.Format("{0}.{1}", this.GetServiceTestsNamespace(domainType), this.GetServiceTestsTypeName(domainType));
        }
        public string GetServiceTestsBaseTypeName(DomainType domainType)
        {
            return this.GetDataServiceTestsTypeName(domainType) + "Base";
        }
        public virtual String GetServiceTestsBaseTypeBaseDeclaration(DomainType domainType)
        {
            return domainType.IsDictionary
                ? string.Format(" : DictionaryEntityLayerTestFixtureBase<{0}, {1}, {2}>", this.GetModelTypeName(domainType), this.GetModelIdTypeName(domainType), this.GetModelEnumTypeName(domainType))
                : string.Format(" : EntityLayerTestFixtureBase<{0}, {1}, {2}, {3}>", this.GetModelTypeName(domainType), this.GetModelIdTypeName(domainType), this.GetPersistableModelTestsEntityWrapperTypeName(domainType), this.GetPersistableModelTestsStrategyTypeName(domainType));
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
            var relationshipModel = this.GetDomainTypeByFullTypeName(property.Type);
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









        public void StartSupportViewModelFile(DomainType domainType)
        {
            this.FileManager.StartNewFile(domainType.Name + "Model.auto.cs", this.WebProject, this.AppDefinition.GeneratedCodeRootFolderName + @"Areas\Support\Models\", new OutputFileProperties() { BuildAction = OutputFileBuildActionType.Compile });
        }
        public void StartDictionaryViewModelFile(DomainType domainType)
        {
            this.FileManager.StartNewFile(domainType.Name + "Model.auto.cs", this.WebProject, this.AppDefinition.GeneratedCodeRootFolderName + @"Models\", new OutputFileProperties() { BuildAction = OutputFileBuildActionType.Compile });
        }
        public void StartLookupsControllerFile()
        {
            this.FileManager.StartNewFile("LookupsController.auto.cs", this.WebProject, this.AppDefinition.GeneratedCodeRootFolderName + @"Controllers\", new OutputFileProperties() { BuildAction = OutputFileBuildActionType.Compile });
        }
        public void StartSupportControllerFile(DomainType domainType)
        {
            this.FileManager.StartNewFile(this.GetPluralPropertyName(domainType) + "Controller.auto.cs", this.WebProject, this.AppDefinition.GeneratedCodeRootFolderName + @"Areas\Support\Controllers\", new OutputFileProperties() { BuildAction = OutputFileBuildActionType.Compile });
        }
        public void StartSupportIndexViewFile(DomainType domainType)
        {
            this.FileManager.StartNewFile("_GridColumnsAndModel.auto.cshtml", this.WebProject, this.AppDefinition.GeneratedCodeRootFolderName + @"Areas\Support\Views\" + this.GetPluralPropertyName(domainType), new OutputFileProperties() { BuildAction = OutputFileBuildActionType.Compile });
        }
        public void StartSupportAreaRegistrationFile()
        {
            this.FileManager.StartNewFile("SupportAreaRegistration.auto.cs", this.WebProject, this.AppDefinition.GeneratedCodeRootFolderName + @"Areas\Support\", new OutputFileProperties() { BuildAction = OutputFileBuildActionType.Compile });
        }
        public void StartRouteUtilsFile()
        {
            this.FileManager.StartNewFile("RouteUtils.auto.cs", this.WebProject, this.AppDefinition.GeneratedCodeRootFolderName + @"Core\", new OutputFileProperties() { BuildAction = OutputFileBuildActionType.Compile });
        }
        public void StartModelMappingFile()
        {
            this.FileManager.StartNewFile("MappingModule.auto.cs", this.WebProject, this.AppDefinition.GeneratedCodeRootFolderName + @"Models\Mapping\", new OutputFileProperties() { BuildAction = OutputFileBuildActionType.Compile });
        }
        public void StartSupportModelMappingFile()
        {
            this.FileManager.StartNewFile("MappingModule.auto.cs", this.WebProject, this.AppDefinition.GeneratedCodeRootFolderName + @"Areas\Support\Models\Mapping\", new OutputFileProperties() { BuildAction = OutputFileBuildActionType.Compile });
        }
        public void StartSupportHtmlUtilsFile()
        {
            this.FileManager.StartNewFile("HtmlUtils.auto.cs", this.WebCoreProject, this.AppDefinition.GeneratedCodeRootFolderName + @"Mvc\", new OutputFileProperties() { BuildAction = OutputFileBuildActionType.Compile });
        }






        public virtual IEnumerable<DomainType> DomainTypesToGenerateSupportViewModelFor
        {
            get
            {
                return from dt in this.AppDefinition.DomainTypes
                       where dt.WebOptions.GenerateSupportViewModel
                       //&& dt.IsDictionary
                       select dt;
            }
        }
        public virtual IEnumerable<DomainType> DomainTypesToGenerateSupportControllerFor
        {
            get
            {
                return from dt in this.AppDefinition.DomainTypes
                       where dt.WebOptions.GenerateSupportController
                             && !dt.IsDictionary
                       select dt;
            }
        }
        public virtual IEnumerable<DomainType> DomainTypesToGenerateSupportIndexViewsFor
        {
            get
            {
                return from dt in this.AppDefinition.DomainTypes
                       where dt.WebOptions.GenerateSupportIndexView
                             && !dt.IsDictionary
                       select dt;
            }
        }
        public virtual IEnumerable<DomainType> DomainTypesToGenerateLookupsControllerFor
        {
            get
            {
                return from dt in this.AppDefinition.DomainTypes
                       where dt.WebOptions.GenerateLookupsController
                             && dt.IsDictionary
                       select dt;
            }
        }








        public string GetSupportViewModelNamespace(DomainType domainType)
        {
            return domainType.IsDictionary
                ? this.WebProject.GetDefaultNamespace() + ".Models" // + (String.IsNullOrEmpty(domainType.SubNamespace) ? null : "." + domainType.SubNamespace)
                : this.ModelProject.GetDefaultNamespace() + ".Areas.Support.Models"; // + (String.IsNullOrEmpty(domainType.SubNamespace) ? null : "." + domainType.SubNamespace);
        }
        public string GetSupportViewModelFullTypeName(DomainType domainType)
        {
            return String.Format("{0}.{1}Model", this.GetSupportViewModelNamespace(domainType), domainType.Name);
        }
        public string GetSupportControllerNamespace(DomainType domainType)
        {
            return this.ModelProject.GetDefaultNamespace() + ".Areas.Support.Controllers";
        }









        public string GetSupportViewModelBaseClass(DomainType domainType)
        {
            return !String.IsNullOrEmpty(domainType.BaseClassOverride)
                    ? this.GetDomainTypeByFullTypeName(domainType.BaseClassOverride).Name + "Model"
                    : domainType.IsDictionary
                        ? string.Format("DictionaryEntityModelBase<{0}>", this.GetModelIdTypeName(domainType))
                        : string.Format("AuditableModelBase<{0}>", this.GetModelIdTypeName(domainType));
        }
        public string GetSupportViewModelMetadataConstantsBaseClass(DomainType domainType)
        {
            return !String.IsNullOrEmpty(domainType.BaseClassOverride)
                    ? this.GetDomainTypeByFullTypeName(domainType.BaseClassOverride).Name + "ModelMetadataConstants"
                    : domainType.IsDictionary
                        ? ""
                        : "AuditableModelBaseMetadataConstants";
        }



















        public string GetKendoUIModelDeclarationForProperty(Property property, string returnValue = "")
        {
            if (property.IsRelationship)
            {
                var type = this.GetDomainTypeByFullTypeName(property.Type);
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
