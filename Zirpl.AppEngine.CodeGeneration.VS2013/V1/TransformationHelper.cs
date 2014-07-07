using System;
using System.Collections.Generic;
using System.IO;
using System.Xml.Serialization;
using Microsoft.VisualStudio.TextTemplating;
using Zirpl.AppEngine.CodeGeneration.TextTemplating;
using Zirpl.AppEngine.CodeGeneration.V1.ConfigModel;

namespace Zirpl.AppEngine.CodeGeneration.V1
{
    public class TransformationHelper :TransformationHelperBase
    {
        public AppDefinition AppDefinition { get; private set; }
        public NamingProvider NamingProvider { get; private set; }
        public DomainTypeFilters DomainTypeFilters { get; private set; }
        public TypeProvider TypeProvider { get; private set; }
        public ProjectProvider ProjectProvider { get; private set; }

        
        public TransformationHelper(TextTransformation callingTemplate)
            :base(callingTemplate)
        {
            this.NamingProvider = new NamingProvider(this);
            this.DomainTypeFilters = new DomainTypeFilters(this);
            this.TypeProvider = new TypeProvider(this);
            this.ProjectProvider = new ProjectProvider(this);
            
            this.LoadAppDefinition();
        }

        private void LoadAppDefinition()
        {
            var domaintTypeConfigFilePaths = new List<string>();
            String appDefinitionConfigFilePath = null;
            var modelConfigFileProjectItems = this.ProjectProvider.CodeGenerationProject.ProjectItems.GetAllProjectItemsRecursive();
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

        }

        public void StartModelFile(DomainType domainType)
        {
            this.FileManager.StartNewFile(domainType.Name + ".auto.cs", this.ProjectProvider.ModelProject, this.ProjectProvider.GetGeneratedCodeFolder(domainType), new OutputFileProperties() {BuildAction = OutputFileBuildActionType.Compile});
        }
        public void StartMetadataFile(DomainType domainType)
        {
            this.FileManager.StartNewFile(domainType.Name + "Metadata.auto.cs", this.ProjectProvider.ModelProject, this.ProjectProvider.GetGeneratedCodeFolder(domainType), new OutputFileProperties() { BuildAction = OutputFileBuildActionType.Compile });
        }
        public void StartEnumFile(DomainType domainType)
        {
            this.FileManager.StartNewFile(domainType.Name + "Enum.auto.cs", this.ProjectProvider.ModelProject, this.ProjectProvider.GetGeneratedCodeFolder(domainType), new OutputFileProperties() { BuildAction = OutputFileBuildActionType.Compile });
        }
        public void StartDataServiceInterfaceFile(DomainType domainType)
        {
            this.FileManager.StartNewFile("I" + domainType.Name + "DataService.auto.cs", this.ProjectProvider.DataServiceProject, this.ProjectProvider.GetGeneratedCodeFolder(domainType), new OutputFileProperties() { BuildAction = OutputFileBuildActionType.Compile });
        }
        public void StartDataServiceFile(DomainType domainType)
        {
            this.FileManager.StartNewFile(domainType.Name + "DataService.auto.cs", this.ProjectProvider.DataServiceProject, this.ProjectProvider.GetGeneratedCodeFolder(domainType), new OutputFileProperties() { BuildAction = OutputFileBuildActionType.Compile });
        }
        public void StartEntityFrameworkFile(DomainType domainType)
        {
            this.FileManager.StartNewFile(domainType.Name + "Mapping.auto.cs", this.ProjectProvider.DataServiceProject, this.AppDefinition.GeneratedCodeRootFolderName + @"Mapping", new OutputFileProperties() { BuildAction = OutputFileBuildActionType.Compile });
        }
        public void StartDataContextFile()
        {
            this.FileManager.StartNewFile(this.AppDefinition.DataContextName + ".auto.cs", this.ProjectProvider.DataServiceProject, this.AppDefinition.GeneratedCodeRootFolderName, new OutputFileProperties() { BuildAction = OutputFileBuildActionType.Compile });
        }
        public void StartDataServiceTestsDataServicesProviderFile()
        {
            this.FileManager.StartNewFile("DataServicesProvider.auto.cs", this.ProjectProvider.DataServiceTestsProject, this.AppDefinition.GeneratedCodeRootFolderName, new OutputFileProperties() { BuildAction = OutputFileBuildActionType.Compile });
        }
        public void StartDataServiceTestsFile(DomainType domainType)
        {
            this.FileManager.StartNewFile(domainType.Name + "DataServiceTests.auto.cs", this.ProjectProvider.DataServiceTestsProject, this.ProjectProvider.GetGeneratedCodeFolder(domainType), new OutputFileProperties() { BuildAction = OutputFileBuildActionType.Compile });
        }
        public void StartTestsStrategyFile(DomainType domainType)
        {
            this.FileManager.StartNewFile(domainType.Name + "TestsStrategy.auto.cs", this.ProjectProvider.TestingProject, this.ProjectProvider.GetGeneratedCodeFolder(domainType), new OutputFileProperties() { BuildAction = OutputFileBuildActionType.Compile });
        }
        public void StartServiceInterfaceFile(DomainType domainType)
        {
            this.FileManager.StartNewFile("I" + domainType.Name + "Service.auto.cs", this.ProjectProvider.ServiceProject, this.ProjectProvider.GetGeneratedCodeFolder(domainType), new OutputFileProperties() { BuildAction = OutputFileBuildActionType.Compile });
        }
        public void StartServiceFile(DomainType domainType)
        {
            this.FileManager.StartNewFile(domainType.Name + "Service.auto.cs", this.ProjectProvider.ServiceProject, this.ProjectProvider.GetGeneratedCodeFolder(domainType), new OutputFileProperties() { BuildAction = OutputFileBuildActionType.Compile });
        }
        public void StartValidatorFile(DomainType domainType)
        {
            this.FileManager.StartNewFile(domainType.Name + "Validator.auto.cs", this.ProjectProvider.ServiceProject, this.AppDefinition.GeneratedCodeRootFolderName + @"Validation", new OutputFileProperties() { BuildAction = OutputFileBuildActionType.Compile });
        }
        public void StartServiceTestsServicesProviderFile()
        {
            this.FileManager.StartNewFile("ServicesProvider.auto.cs", this.ProjectProvider.ServiceTestsProject, this.AppDefinition.GeneratedCodeRootFolderName, new OutputFileProperties() { BuildAction = OutputFileBuildActionType.Compile });
        }
        public void StartServiceTestsFile(DomainType domainType)
        {
            this.FileManager.StartNewFile(domainType.Name + "ServiceTests.auto.cs", this.ProjectProvider.ServiceTestsProject, this.ProjectProvider.GetGeneratedCodeFolder(domainType), new OutputFileProperties() { BuildAction = OutputFileBuildActionType.Compile });
        }
        public void StartSupportViewModelFile(DomainType domainType)
        {
            this.FileManager.StartNewFile(domainType.Name + "Model.auto.cs", this.ProjectProvider.WebProject, this.AppDefinition.GeneratedCodeRootFolderName + @"Areas\Support\Models\", new OutputFileProperties() { BuildAction = OutputFileBuildActionType.Compile });
        }
        public void StartDictionaryViewModelFile(DomainType domainType)
        {
            this.FileManager.StartNewFile(domainType.Name + "Model.auto.cs", this.ProjectProvider.WebProject, this.AppDefinition.GeneratedCodeRootFolderName + @"Models\", new OutputFileProperties() { BuildAction = OutputFileBuildActionType.Compile });
        }
        public void StartLookupsControllerFile()
        {
            this.FileManager.StartNewFile("LookupsController.auto.cs", this.ProjectProvider.WebProject, this.AppDefinition.GeneratedCodeRootFolderName + @"Controllers\", new OutputFileProperties() { BuildAction = OutputFileBuildActionType.Compile });
        }
        public void StartSupportControllerFile(DomainType domainType)
        {
            this.FileManager.StartNewFile(this.NamingProvider.GetPluralPropertyName(domainType) + "Controller.auto.cs", this.ProjectProvider.WebProject, this.AppDefinition.GeneratedCodeRootFolderName + @"Areas\Support\Controllers\", new OutputFileProperties() { BuildAction = OutputFileBuildActionType.Compile });
        }
        public void StartSupportIndexViewFile(DomainType domainType)
        {
            this.FileManager.StartNewFile("_GridColumnsAndModel.auto.cshtml", this.ProjectProvider.WebProject, this.AppDefinition.GeneratedCodeRootFolderName + @"Areas\Support\Views\" + this.NamingProvider.GetPluralPropertyName(domainType), new OutputFileProperties() { BuildAction = OutputFileBuildActionType.Compile });
        }
        public void StartSupportAreaRegistrationFile()
        {
            this.FileManager.StartNewFile("SupportAreaRegistration.auto.cs", this.ProjectProvider.WebProject, this.AppDefinition.GeneratedCodeRootFolderName + @"Areas\Support\", new OutputFileProperties() { BuildAction = OutputFileBuildActionType.Compile });
        }
        public void StartRouteUtilsFile()
        {
            this.FileManager.StartNewFile("RouteUtils.auto.cs", this.ProjectProvider.WebProject, this.AppDefinition.GeneratedCodeRootFolderName + @"Core\", new OutputFileProperties() { BuildAction = OutputFileBuildActionType.Compile });
        }
        public void StartModelMappingFile()
        {
            this.FileManager.StartNewFile("MappingModule.auto.cs", this.ProjectProvider.WebProject, this.AppDefinition.GeneratedCodeRootFolderName + @"Models\Mapping\", new OutputFileProperties() { BuildAction = OutputFileBuildActionType.Compile });
        }
        public void StartSupportModelMappingFile()
        {
            this.FileManager.StartNewFile("MappingModule.auto.cs", this.ProjectProvider.WebProject, this.AppDefinition.GeneratedCodeRootFolderName + @"Areas\Support\Models\Mapping\", new OutputFileProperties() { BuildAction = OutputFileBuildActionType.Compile });
        }
        public void StartSupportHtmlUtilsFile()
        {
            this.FileManager.StartNewFile("HtmlUtils.auto.cs", this.ProjectProvider.WebCoreProject, this.AppDefinition.GeneratedCodeRootFolderName + @"Mvc\", new OutputFileProperties() { BuildAction = OutputFileBuildActionType.Compile });
        }
    }
}
