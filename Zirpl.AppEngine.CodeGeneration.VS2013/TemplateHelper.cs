using System;
using System.Collections.Generic;
using System.Collections.Specialized;
using System.IO;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using System.Xml.Serialization;
using Microsoft.VisualStudio.TextTemplating;
using EnvDTE;
using Zirpl.AppEngine.CodeGeneration.ConfigModel;
using Zirpl.AppEngine.CodeGeneration.Transformation;

namespace Zirpl.AppEngine.CodeGeneration
{
    public class TemplateHelper
    {
        //private static TemplateHelper instance;

        public TemplateFileManager FileManager { get; private set; }
        public AppDefinition AppDefinition { get; private set; }
        public NamingProvider NamingProvider { get; private set; }
        public DomainTypeFilters DomainTypeFilters { get; private set; }
        public TypeProvider TypeProvider { get; private set; }
        public ProjectProvider ProjectProvider { get; private set; }



        //public Solution Solution { get { return this.Studio.Solution; } }
        //public ProjectItem TemplateItem { get { return this.Studio.Solution.FindProjectItem(this.TextTransformation.Host.TemplateFile); } }
        //public Project CurrentProject { get { return this.TemplateItem.ContainingProject; } }
        //public DynamicTextTransformation TextTransformation { get; private set; }
        //public GeneratedTextTransformation TextTransformation { get; private set; }



        private TemplateHelper(TextTransformation callingTemplate)
        {
            //this.TextTransformation = textTransformation;
            this.FileManager = new TemplateFileManager(callingTemplate);
            this.NamingProvider = new NamingProvider(this);
            this.DomainTypeFilters = new DomainTypeFilters(this);
            this.TypeProvider = new TypeProvider(this);
            this.ProjectProvider = new ProjectProvider(this);
            
            //this.Studio = (this.TextTransformation.Host as IServiceProvider).GetService(typeof(EnvDTE.DTE)) as EnvDTE.DTE;

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

            //var vsProject = project.Object as VSLangProj.VSProject;
        }

        //public void GenerateApp(global::Microsoft.VisualStudio.TextTemplating.ITextTemplatingEngineHost host)
        //{
        //    var modelTransform = new Zirpl.AppEngine.CodeGeneration.ModelTransform(host, this);
        //    this.FileManager.TextTransformation.Write(modelTransform.TransformText());
        //    var modelMetadataTransform = new Zirpl.AppEngine.CodeGeneration.ModelMetadataTransform(host, this);
        //    this.FileManager.TextTransformation.Write(modelMetadataTransform.TransformText());
        //    this.End();
        //}

        public void StartModelFile(DomainType domainType)
        {
            this.FileManager.StartNewFile(domainType.Name + ".auto.cs", this.ProjectProvider.ModelProject, this.ProjectProvider.GetGeneratedCodeFolder(domainType));
        }
        public void StartMetadataFile(DomainType domainType)
        {
            this.FileManager.StartNewFile(domainType.Name + "Metadata.auto.cs", this.ProjectProvider.ModelProject, this.ProjectProvider.GetGeneratedCodeFolder(domainType));
        }
        public void StartEnumFile(DomainType domainType)
        {
            this.FileManager.StartNewFile(domainType.Name + "Enum.auto.cs", this.ProjectProvider.ModelProject, this.ProjectProvider.GetGeneratedCodeFolder(domainType));
        }
        public void StartDataServiceInterfaceFile(DomainType domainType)
        {
            this.FileManager.StartNewFile("I" + domainType.Name + "DataService.auto.cs", this.ProjectProvider.DataServiceProject, this.ProjectProvider.GetGeneratedCodeFolder(domainType));
        }
        public void StartDataServiceFile(DomainType domainType)
        {
            this.FileManager.StartNewFile(domainType.Name + "DataService.auto.cs", this.ProjectProvider.DataServiceProject, this.ProjectProvider.GetGeneratedCodeFolder(domainType));
        }
        public void StartEntityFrameworkFile(DomainType domainType)
        {
            this.FileManager.StartNewFile(domainType.Name + "Mapping.auto.cs", this.ProjectProvider.DataServiceProject, this.AppDefinition.GeneratedCodeRootFolderName + @"Mapping");
        }
        public void StartDataContextFile()
        {
            this.FileManager.StartNewFile(this.AppDefinition.DataContextName + ".auto.cs", this.ProjectProvider.DataServiceProject, this.AppDefinition.GeneratedCodeRootFolderName);
        }
        public void StartDataServiceTestsDataServicesProviderFile()
        {
            this.FileManager.StartNewFile("DataServicesProvider.auto.cs", this.ProjectProvider.DataServiceTestsProject, this.AppDefinition.GeneratedCodeRootFolderName);
        }
        public void StartDataServiceTestsFile(DomainType domainType)
        {
            this.FileManager.StartNewFile(domainType.Name + "DataServiceTests.auto.cs", this.ProjectProvider.DataServiceTestsProject, this.ProjectProvider.GetGeneratedCodeFolder(domainType));
        }
        public void StartTestsStrategyFile(DomainType domainType)
        {
            this.FileManager.StartNewFile(domainType.Name + "TestsStrategy.auto.cs", this.ProjectProvider.TestingProject, this.ProjectProvider.GetGeneratedCodeFolder(domainType));
        }
        public void StartServiceInterfaceFile(DomainType domainType)
        {
            this.FileManager.StartNewFile("I" + domainType.Name + "Service.auto.cs", this.ProjectProvider.ServiceProject, this.ProjectProvider.GetGeneratedCodeFolder(domainType));
        }
        public void StartServiceFile(DomainType domainType)
        {
            this.FileManager.StartNewFile(domainType.Name + "Service.auto.cs", this.ProjectProvider.ServiceProject, this.ProjectProvider.GetGeneratedCodeFolder(domainType));
        }
        public void StartValidatorFile(DomainType domainType)
        {
            this.FileManager.StartNewFile(domainType.Name + "Validator.auto.cs", this.ProjectProvider.ServiceProject, this.AppDefinition.GeneratedCodeRootFolderName + @"Validation");
        }
        public void StartServiceTestsServicesProviderFile()
        {
            this.FileManager.StartNewFile("ServicesProvider.auto.cs", this.ProjectProvider.ServiceTestsProject, this.AppDefinition.GeneratedCodeRootFolderName);
        }
        public void StartServiceTestsFile(DomainType domainType)
        {
            this.FileManager.StartNewFile(domainType.Name + "ServiceTests.auto.cs", this.ProjectProvider.ServiceTestsProject, this.ProjectProvider.GetGeneratedCodeFolder(domainType));
        }
        public void StartSupportViewModelFile(DomainType domainType)
        {
            this.FileManager.StartNewFile(domainType.Name + "Model.auto.cs", this.ProjectProvider.WebProject, this.AppDefinition.GeneratedCodeRootFolderName + @"Areas\Support\Models\");
        }
        public void StartDictionaryViewModelFile(DomainType domainType)
        {
            this.FileManager.StartNewFile(domainType.Name + "Model.auto.cs", this.ProjectProvider.WebProject, this.AppDefinition.GeneratedCodeRootFolderName + @"Models\");
        }
        public void StartLookupsControllerFile()
        {
            this.FileManager.StartNewFile("LookupsController.auto.cs", this.ProjectProvider.WebProject, this.AppDefinition.GeneratedCodeRootFolderName + @"Controllers\");
        }
        public void StartSupportControllerFile(DomainType domainType)
        {
            this.FileManager.StartNewFile(this.NamingProvider.GetPluralPropertyName(domainType) + "Controller.auto.cs", this.ProjectProvider.WebProject, this.AppDefinition.GeneratedCodeRootFolderName + @"Areas\Support\Controllers\");
        }
        public void StartSupportIndexViewFile(DomainType domainType)
        {
            this.FileManager.StartNewFile("_GridColumnsAndModel.auto.cshtml", this.ProjectProvider.WebProject, this.AppDefinition.GeneratedCodeRootFolderName + @"Areas\Support\Views\" + this.NamingProvider.GetPluralPropertyName(domainType));
        }
        public void StartSupportAreaRegistrationFile()
        {
            this.FileManager.StartNewFile("SupportAreaRegistration.auto.cs", this.ProjectProvider.WebProject, this.AppDefinition.GeneratedCodeRootFolderName + @"Areas\Support\");
        }
        public void StartRouteUtilsFile()
        {
            this.FileManager.StartNewFile("RouteUtils.auto.cs", this.ProjectProvider.WebProject, this.AppDefinition.GeneratedCodeRootFolderName + @"Core\");
        }
        public void StartModelMappingFile()
        {
            this.FileManager.StartNewFile("MappingModule.auto.cs", this.ProjectProvider.WebProject, this.AppDefinition.GeneratedCodeRootFolderName + @"Models\Mapping\");
        }
        public void StartSupportModelMappingFile()
        {
            this.FileManager.StartNewFile("MappingModule.auto.cs", this.ProjectProvider.WebProject, this.AppDefinition.GeneratedCodeRootFolderName + @"Areas\Support\Models\Mapping\");
        }
        public void StartSupportHtmlUtilsFile()
        {
            this.FileManager.StartNewFile("HtmlUtils.auto.cs", this.ProjectProvider.WebCoreProject, this.AppDefinition.GeneratedCodeRootFolderName + @"Mvc\");
        }

        public void End()
        {
            this.FileManager.Process();
        }

    }
}
