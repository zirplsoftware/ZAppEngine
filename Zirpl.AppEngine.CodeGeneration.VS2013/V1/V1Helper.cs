﻿using System;
using System.Collections.Generic;
using System.IO;
using System.Linq;
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

        public Project TestingProject
        {
            get
            {
                return this.VisualStudio.GetProject(this.AppDefinition.TestingProjectName);
            }
        }


        public string GetGeneratedCodeFolder(DomainType domainType)
        {
            return this.AppDefinition.GeneratedCodeRootFolderName + domainType.SubNamespace.Replace(".", @"\");
        }
        #endregion

        #region StartFile members- no virtual methods

        public void StartModelFile(DomainType domainType)
        {
            this.FileManager.StartNewFile(
                this.GetModelTypeName(domainType) + this.AppDefinition.GeneratedCSFileExtension,
                this.ModelProject,
                this.GetGeneratedCodeFolder(domainType),
                new OutputFileProperties() { BuildAction = OutputFileBuildActionType.Compile });
        }
        public void StartModelMetadataFile(DomainType domainType)
        {
            this.FileManager.StartNewFile(
                this.GetModelMetadataTypeName(domainType) + this.AppDefinition.GeneratedCSFileExtension,
                this.ModelProject,
                this.GetGeneratedCodeFolder(domainType),
                new OutputFileProperties() { BuildAction = OutputFileBuildActionType.Compile });
        }
        public void StartModelEnumFile(DomainType domainType)
        {
            this.FileManager.StartNewFile(
                this.GetModelEnumTypeName(domainType) + this.AppDefinition.GeneratedCSFileExtension,
                this.ModelProject,
                this.GetGeneratedCodeFolder(domainType),
                new OutputFileProperties() { BuildAction = OutputFileBuildActionType.Compile });
        }
        public void StartDataServiceInterfaceFile(DomainType domainType)
        {
            this.FileManager.StartNewFile(
                this.GetDataServiceInterfaceTypeName(domainType) + this.AppDefinition.GeneratedCSFileExtension,
                this.DataServiceProject,
                this.GetGeneratedCodeFolder(domainType),
                new OutputFileProperties() { BuildAction = OutputFileBuildActionType.Compile });
        }
        public void StartDataServiceFile(DomainType domainType)
        {
            this.FileManager.StartNewFile(
                this.GetDataServiceTypeName(domainType) + this.AppDefinition.GeneratedCSFileExtension,
                this.DataServiceProject,
                this.GetGeneratedCodeFolder(domainType),
                new OutputFileProperties() { BuildAction = OutputFileBuildActionType.Compile });
        }
        public void StartEntityFrameworkMappingFile(DomainType domainType)
        {
            this.FileManager.StartNewFile(
                this.GetEntityFrameworkMappingTypeName(domainType) + this.AppDefinition.GeneratedCSFileExtension,
                this.DataServiceProject,
                this.AppDefinition.GeneratedCodeRootFolderName + @"Mapping",
                new OutputFileProperties() { BuildAction = OutputFileBuildActionType.Compile });
        }
        public void StartDataContextFile()
        {
            this.FileManager.StartNewFile(
                this.GetDataContextTypeName() + this.AppDefinition.GeneratedCSFileExtension,
                this.DataServiceProject,
                this.AppDefinition.GeneratedCodeRootFolderName,
                new OutputFileProperties() { BuildAction = OutputFileBuildActionType.Compile });
        }












        public void StartDataServiceTestsDataServicesProviderFile()
        {
            this.FileManager.StartNewFile("DataServicesProvider.auto.cs", this.DataServiceTestsProject, this.AppDefinition.GeneratedCodeRootFolderName, new OutputFileProperties() { BuildAction = OutputFileBuildActionType.Compile });
        }
        public void StartDataServiceTestsFile(DomainType domainType)
        {
            this.FileManager.StartNewFile(domainType.Name + "DataServiceTests.auto.cs", this.DataServiceTestsProject, this.GetGeneratedCodeFolder(domainType), new OutputFileProperties() { BuildAction = OutputFileBuildActionType.Compile });
        }
        public void StartTestsStrategyFile(DomainType domainType)
        {
            this.FileManager.StartNewFile(domainType.Name + "TestsStrategy.auto.cs", this.TestingProject, this.GetGeneratedCodeFolder(domainType), new OutputFileProperties() { BuildAction = OutputFileBuildActionType.Compile });
        }
        public void StartServiceInterfaceFile(DomainType domainType)
        {
            this.FileManager.StartNewFile("I" + domainType.Name + "Service.auto.cs", this.ServiceProject, this.GetGeneratedCodeFolder(domainType), new OutputFileProperties() { BuildAction = OutputFileBuildActionType.Compile });
        }
        public void StartServiceFile(DomainType domainType)
        {
            this.FileManager.StartNewFile(domainType.Name + "Service.auto.cs", this.ServiceProject, this.GetGeneratedCodeFolder(domainType), new OutputFileProperties() { BuildAction = OutputFileBuildActionType.Compile });
        }
        public void StartValidatorFile(DomainType domainType)
        {
            this.FileManager.StartNewFile(domainType.Name + "Validator.auto.cs", this.ServiceProject, this.AppDefinition.GeneratedCodeRootFolderName + @"Validation", new OutputFileProperties() { BuildAction = OutputFileBuildActionType.Compile });
        }
        public void StartServiceTestsServicesProviderFile()
        {
            this.FileManager.StartNewFile("ServicesProvider.auto.cs", this.ServiceTestsProject, this.AppDefinition.GeneratedCodeRootFolderName, new OutputFileProperties() { BuildAction = OutputFileBuildActionType.Compile });
        }
        public void StartServiceTestsFile(DomainType domainType)
        {
            this.FileManager.StartNewFile(domainType.Name + "ServiceTests.auto.cs", this.ServiceTestsProject, this.GetGeneratedCodeFolder(domainType), new OutputFileProperties() { BuildAction = OutputFileBuildActionType.Compile });
        }
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
        public virtual IEnumerable<DomainType> DomainTypesToGenerateMetadataFor
        {
            get
            {
                return from dt in this.AppDefinition.DomainTypes
                       where dt.ModelOptions.GenerateMetadata
                       select dt;
            }
        }
        public virtual IEnumerable<DomainType> DomainTypesToGenerateEnumFor
        {
            get
            {
                return from dt in this.AppDefinition.DomainTypes
                       where dt.IsDictionary
                             && dt.ModelOptions.GenerateEnum
                       select dt;
            }
        }
        public virtual IEnumerable<DomainType> DomainTypesToGenerateDataServiceInterfaceFor
        {
            get
            {
                return from dt in this.AppDefinition.DomainTypes
                       where dt.DataServiceOptions.GenerateDataServiceInterface
                       select dt;
            }
        }
        public virtual IEnumerable<DomainType> DomainTypesToGenerateDataServiceFor
        {
            get
            {
                return from dt in this.AppDefinition.DomainTypes
                       where dt.DataServiceOptions.GenerateDataService
                       select dt;
            }
        }
        public virtual IEnumerable<DomainType> DomainTypesToGenerateDataContextPropertyFor
        {
            get
            {
                return from dt in this.AppDefinition.DomainTypes
                       where dt.DataServiceOptions.GenerateDataContextProperty
                       select dt;
            }
        }
        public virtual IEnumerable<DomainType> DomainTypesToGenerateEntityFrameworkMappingFor
        {
            get
            {
                return from dt in this.AppDefinition.DomainTypes
                       where dt.DataServiceOptions.GenerateEntityFrameworkMapping
                       select dt;
            }
        }
        public virtual IEnumerable<DomainType> DomainTypesToGenerateServiceInterfaceFor
        {
            get
            {
                return from dt in this.AppDefinition.DomainTypes
                       where dt.ServiceOptions.GenerateServiceInterface
                       select dt;
            }
        }
        public virtual IEnumerable<DomainType> DomainTypesToGenerateServiceFor
        {
            get
            {
                return from dt in this.AppDefinition.DomainTypes
                       where dt.ServiceOptions.GenerateService
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

        #endregion

        #region Model-related methods
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
            return !String.IsNullOrEmpty(domainType.BaseClassOverride)
                    ? " : " + domainType.BaseClassOverride
                    : domainType.IsDictionary
                        ? string.Format(" : DictionaryEntityBase<{0}, {1}>", this.GetModelIdTypeName(domainType), this.GetModelEnumTypeName(domainType))
                        : string.Format(" : AuditableBase<{0}>", this.GetModelIdTypeName(domainType));
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

        #region ModelMetadata-related methods
        public virtual string GetModelMetadataNamespace(DomainType domainType)
        {
            // use the same
            return this.GetModelNamespace(domainType);
        }
        public virtual String GetModelMetadataTypeName(DomainType domainType)
        {
            return domainType.Name + "Metadata";
        }
        public virtual String GetModelMetadataTypeFullName(DomainType domainType)
        {
            return String.Format("{0}.{1}", this.GetModelMetadataNamespace(domainType), this.GetModelMetadataTypeName(domainType));
        }
        public virtual string GetModelMetadataBaseDeclaration(DomainType domainType)
        {
            return !String.IsNullOrEmpty(domainType.BaseClassOverride)
                ? " : " + domainType.BaseClassOverride + "Metadata"
                : domainType.IsDictionary
                     ? " : DictionaryEntityBaseMetadataBase"
                     : " : MetadataBase";
        }
        #endregion

        #region ModelEnum-related methods
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

        #region DataServiceInterface related methods
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






        public string GetDataServiceTestsNamespace(DomainType domainType)
        {
            return this.DataServiceTestsProject.GetDefaultNamespace() + (String.IsNullOrEmpty(domainType.SubNamespace) ? null : "." + domainType.SubNamespace);
        }
        public string GetServiceNamespace(DomainType domainType)
        {
            return this.ServiceProject.GetDefaultNamespace() + (String.IsNullOrEmpty(domainType.SubNamespace) ? null : "." + domainType.SubNamespace);
        }
        public string GetTestingNamespace(DomainType domainType)
        {
            return this.TestingProject.GetDefaultNamespace() + (String.IsNullOrEmpty(domainType.SubNamespace) ? null : "." + domainType.SubNamespace);
        }
        public string GetServiceTestsNamespace(DomainType domainType)
        {
            return this.ServiceTestsProject.GetDefaultNamespace() + (String.IsNullOrEmpty(domainType.SubNamespace) ? null : "." + domainType.SubNamespace);
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
                : string.Format("DbContextServiceBase<{2}, {0}, {1}>, I{0}Service", domainType.Name, this.GetModelIdTypeName(domainType), this.AppDefinition.DataContextName);
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
                    ? this.GetDomainTypeByFullTypeName(domainType.BaseClassOverride).Name + "Model"
                    : domainType.IsDictionary
                        ? string.Format("DictionaryEntityModelBase<{0}>", this.GetModelIdTypeName(domainType))
                        : string.Format("AuditableModelBase<{0}>", this.GetModelIdTypeName(domainType));
        }
        public string GetSupportViewModelMetadataBaseClass(DomainType domainType)
        {
            return !String.IsNullOrEmpty(domainType.BaseClassOverride)
                    ? this.GetDomainTypeByFullTypeName(domainType.BaseClassOverride).Name + "ModelMetadata"
                    : domainType.IsDictionary
                        ? ""
                        : "AuditableModelBaseMetadata";
        }
        public string GetValidatorBaseClass(DomainType domainType)
        {
            return !String.IsNullOrEmpty(domainType.BaseClassOverride)
                    ? string.Format("{0}Validator<{1}>", this.GetDomainTypeByFullTypeName(domainType.BaseClassOverride).Name, domainType.Name)
                    : domainType.IsAbstract
                        ? string.Format("DbEntityValidatorBase<T> where T: {0}", domainType.Name)
                        : string.Format("DbEntityValidatorBase<{0}>", domainType.Name);
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
