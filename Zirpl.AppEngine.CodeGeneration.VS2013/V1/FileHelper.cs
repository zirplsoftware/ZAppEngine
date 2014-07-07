using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using Zirpl.AppEngine.CodeGeneration.TextTemplating;
using Zirpl.AppEngine.CodeGeneration.V1.ConfigModel;

namespace Zirpl.AppEngine.CodeGeneration.V1
{
    public class FileHelper
    {
        private TransformationHelper transformationHelper;

        public FileHelper(TransformationHelper transformationHelper)
        {
            this.transformationHelper = transformationHelper;
        }

        public void StartModelFile(DomainType domainType)
        {
            this.transformationHelper.FileManager.StartNewFile(this.transformationHelper.CodeHelper.GetModelClassName(domainType) + ".auto.cs", this.transformationHelper.ProjectProvider.ModelProject, this.transformationHelper.ProjectProvider.GetGeneratedCodeFolder(domainType), new OutputFileProperties() { BuildAction = OutputFileBuildActionType.Compile });
        }
        public void StartMetadataFile(DomainType domainType)
        {
            this.transformationHelper.FileManager.StartNewFile(this.transformationHelper.CodeHelper.GetModelMetadataClassName(domainType) + ".auto.cs", this.transformationHelper.ProjectProvider.ModelProject, this.transformationHelper.ProjectProvider.GetGeneratedCodeFolder(domainType), new OutputFileProperties() { BuildAction = OutputFileBuildActionType.Compile });
        }
        public void StartEnumFile(DomainType domainType)
        {
            this.transformationHelper.FileManager.StartNewFile(domainType.Name + "Enum.auto.cs", this.transformationHelper.ProjectProvider.ModelProject, this.transformationHelper.ProjectProvider.GetGeneratedCodeFolder(domainType), new OutputFileProperties() { BuildAction = OutputFileBuildActionType.Compile });
        }
        public void StartDataServiceInterfaceFile(DomainType domainType)
        {
            this.transformationHelper.FileManager.StartNewFile("I" + domainType.Name + "DataService.auto.cs", this.transformationHelper.ProjectProvider.DataServiceProject, this.transformationHelper.ProjectProvider.GetGeneratedCodeFolder(domainType), new OutputFileProperties() { BuildAction = OutputFileBuildActionType.Compile });
        }
        public void StartDataServiceFile(DomainType domainType)
        {
            this.transformationHelper.FileManager.StartNewFile(domainType.Name + "DataService.auto.cs", this.transformationHelper.ProjectProvider.DataServiceProject, this.transformationHelper.ProjectProvider.GetGeneratedCodeFolder(domainType), new OutputFileProperties() { BuildAction = OutputFileBuildActionType.Compile });
        }
        public void StartEntityFrameworkFile(DomainType domainType)
        {
            this.transformationHelper.FileManager.StartNewFile(domainType.Name + "Mapping.auto.cs", this.transformationHelper.ProjectProvider.DataServiceProject, this.transformationHelper.AppDefinition.GeneratedCodeRootFolderName + @"Mapping", new OutputFileProperties() { BuildAction = OutputFileBuildActionType.Compile });
        }
        public void StartDataContextFile()
        {
            this.transformationHelper.FileManager.StartNewFile(this.transformationHelper.AppDefinition.DataContextName + ".auto.cs", this.transformationHelper.ProjectProvider.DataServiceProject, this.transformationHelper.AppDefinition.GeneratedCodeRootFolderName, new OutputFileProperties() { BuildAction = OutputFileBuildActionType.Compile });
        }
        public void StartDataServiceTestsDataServicesProviderFile()
        {
            this.transformationHelper.FileManager.StartNewFile("DataServicesProvider.auto.cs", this.transformationHelper.ProjectProvider.DataServiceTestsProject, this.transformationHelper.AppDefinition.GeneratedCodeRootFolderName, new OutputFileProperties() { BuildAction = OutputFileBuildActionType.Compile });
        }
        public void StartDataServiceTestsFile(DomainType domainType)
        {
            this.transformationHelper.FileManager.StartNewFile(domainType.Name + "DataServiceTests.auto.cs", this.transformationHelper.ProjectProvider.DataServiceTestsProject, this.transformationHelper.ProjectProvider.GetGeneratedCodeFolder(domainType), new OutputFileProperties() { BuildAction = OutputFileBuildActionType.Compile });
        }
        public void StartTestsStrategyFile(DomainType domainType)
        {
            this.transformationHelper.FileManager.StartNewFile(domainType.Name + "TestsStrategy.auto.cs", this.transformationHelper.ProjectProvider.TestingProject, this.transformationHelper.ProjectProvider.GetGeneratedCodeFolder(domainType), new OutputFileProperties() { BuildAction = OutputFileBuildActionType.Compile });
        }
        public void StartServiceInterfaceFile(DomainType domainType)
        {
            this.transformationHelper.FileManager.StartNewFile("I" + domainType.Name + "Service.auto.cs", this.transformationHelper.ProjectProvider.ServiceProject, this.transformationHelper.ProjectProvider.GetGeneratedCodeFolder(domainType), new OutputFileProperties() { BuildAction = OutputFileBuildActionType.Compile });
        }
        public void StartServiceFile(DomainType domainType)
        {
            this.transformationHelper.FileManager.StartNewFile(domainType.Name + "Service.auto.cs", this.transformationHelper.ProjectProvider.ServiceProject, this.transformationHelper.ProjectProvider.GetGeneratedCodeFolder(domainType), new OutputFileProperties() { BuildAction = OutputFileBuildActionType.Compile });
        }
        public void StartValidatorFile(DomainType domainType)
        {
            this.transformationHelper.FileManager.StartNewFile(domainType.Name + "Validator.auto.cs", this.transformationHelper.ProjectProvider.ServiceProject, this.transformationHelper.AppDefinition.GeneratedCodeRootFolderName + @"Validation", new OutputFileProperties() { BuildAction = OutputFileBuildActionType.Compile });
        }
        public void StartServiceTestsServicesProviderFile()
        {
            this.transformationHelper.FileManager.StartNewFile("ServicesProvider.auto.cs", this.transformationHelper.ProjectProvider.ServiceTestsProject, this.transformationHelper.AppDefinition.GeneratedCodeRootFolderName, new OutputFileProperties() { BuildAction = OutputFileBuildActionType.Compile });
        }
        public void StartServiceTestsFile(DomainType domainType)
        {
            this.transformationHelper.FileManager.StartNewFile(domainType.Name + "ServiceTests.auto.cs", this.transformationHelper.ProjectProvider.ServiceTestsProject, this.transformationHelper.ProjectProvider.GetGeneratedCodeFolder(domainType), new OutputFileProperties() { BuildAction = OutputFileBuildActionType.Compile });
        }
        public void StartSupportViewModelFile(DomainType domainType)
        {
            this.transformationHelper.FileManager.StartNewFile(domainType.Name + "Model.auto.cs", this.transformationHelper.ProjectProvider.WebProject, this.transformationHelper.AppDefinition.GeneratedCodeRootFolderName + @"Areas\Support\Models\", new OutputFileProperties() { BuildAction = OutputFileBuildActionType.Compile });
        }
        public void StartDictionaryViewModelFile(DomainType domainType)
        {
            this.transformationHelper.FileManager.StartNewFile(domainType.Name + "Model.auto.cs", this.transformationHelper.ProjectProvider.WebProject, this.transformationHelper.AppDefinition.GeneratedCodeRootFolderName + @"Models\", new OutputFileProperties() { BuildAction = OutputFileBuildActionType.Compile });
        }
        public void StartLookupsControllerFile()
        {
            this.transformationHelper.FileManager.StartNewFile("LookupsController.auto.cs", this.transformationHelper.ProjectProvider.WebProject, this.transformationHelper.AppDefinition.GeneratedCodeRootFolderName + @"Controllers\", new OutputFileProperties() { BuildAction = OutputFileBuildActionType.Compile });
        }
        public void StartSupportControllerFile(DomainType domainType)
        {
            this.transformationHelper.FileManager.StartNewFile(this.transformationHelper.CodeHelper.GetPluralPropertyName(domainType) + "Controller.auto.cs", this.transformationHelper.ProjectProvider.WebProject, this.transformationHelper.AppDefinition.GeneratedCodeRootFolderName + @"Areas\Support\Controllers\", new OutputFileProperties() { BuildAction = OutputFileBuildActionType.Compile });
        }
        public void StartSupportIndexViewFile(DomainType domainType)
        {
            this.transformationHelper.FileManager.StartNewFile("_GridColumnsAndModel.auto.cshtml", this.transformationHelper.ProjectProvider.WebProject, this.transformationHelper.AppDefinition.GeneratedCodeRootFolderName + @"Areas\Support\Views\" + this.transformationHelper.CodeHelper.GetPluralPropertyName(domainType), new OutputFileProperties() { BuildAction = OutputFileBuildActionType.Compile });
        }
        public void StartSupportAreaRegistrationFile()
        {
            this.transformationHelper.FileManager.StartNewFile("SupportAreaRegistration.auto.cs", this.transformationHelper.ProjectProvider.WebProject, this.transformationHelper.AppDefinition.GeneratedCodeRootFolderName + @"Areas\Support\", new OutputFileProperties() { BuildAction = OutputFileBuildActionType.Compile });
        }
        public void StartRouteUtilsFile()
        {
            this.transformationHelper.FileManager.StartNewFile("RouteUtils.auto.cs", this.transformationHelper.ProjectProvider.WebProject, this.transformationHelper.AppDefinition.GeneratedCodeRootFolderName + @"Core\", new OutputFileProperties() { BuildAction = OutputFileBuildActionType.Compile });
        }
        public void StartModelMappingFile()
        {
            this.transformationHelper.FileManager.StartNewFile("MappingModule.auto.cs", this.transformationHelper.ProjectProvider.WebProject, this.transformationHelper.AppDefinition.GeneratedCodeRootFolderName + @"Models\Mapping\", new OutputFileProperties() { BuildAction = OutputFileBuildActionType.Compile });
        }
        public void StartSupportModelMappingFile()
        {
            this.transformationHelper.FileManager.StartNewFile("MappingModule.auto.cs", this.transformationHelper.ProjectProvider.WebProject, this.transformationHelper.AppDefinition.GeneratedCodeRootFolderName + @"Areas\Support\Models\Mapping\", new OutputFileProperties() { BuildAction = OutputFileBuildActionType.Compile });
        }
        public void StartSupportHtmlUtilsFile()
        {
            this.transformationHelper.FileManager.StartNewFile("HtmlUtils.auto.cs", this.transformationHelper.ProjectProvider.WebCoreProject, this.transformationHelper.AppDefinition.GeneratedCodeRootFolderName + @"Mvc\", new OutputFileProperties() { BuildAction = OutputFileBuildActionType.Compile });
        }
    }
}
