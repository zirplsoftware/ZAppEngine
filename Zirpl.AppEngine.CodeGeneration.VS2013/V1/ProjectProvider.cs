using EnvDTE;
using EnvDTE80;
using Zirpl.AppEngine.CodeGeneration.TextTemplating;
using Zirpl.AppEngine.CodeGeneration.V1.ConfigModel;

namespace Zirpl.AppEngine.CodeGeneration.V1
{
    public class ProjectProvider
    {
        private TransformationHelper templateHelper;

        public ProjectProvider(TransformationHelper templateHelper)
        {
            this.templateHelper = templateHelper;
        }


        public DTE2 Studio
        {
            get
            {
                return templateHelper.FileManager.Studio;
            }
        }

        public Project CodeGenerationProject
        {
            get
            {
                return templateHelper.FileManager.TemplateProjectItem.ContainingProject;
            }
        }

        public Project ModelProject
        {
            get
            {
                return this.Studio.GetProject(templateHelper.AppDefinition.ModelProjectName);
            }
        }

        public Project DataServiceProject
        {
            get
            {
                return this.Studio.GetProject(templateHelper.AppDefinition.DataServiceProjectName);
            }
        }

        public Project DataServiceTestsProject
        {
            get
            {
                return this.Studio.GetProject(templateHelper.AppDefinition.DataServiceTestsProjectName);
            }
        }

        public Project ServiceProject
        {
            get
            {
                return this.Studio.GetProject(templateHelper.AppDefinition.ServiceProjectName);
            }
        }

        public Project WebProject
        {
            get
            {
                return this.Studio.GetProject(templateHelper.AppDefinition.WebProjectName);
            }
        }

        public Project WebCoreProject
        {
            get
            {
                return this.Studio.GetProject(templateHelper.AppDefinition.WebCoreProjectName);
            }
        }

        public Project ServiceTestsProject
        {
            get
            {
                return this.Studio.GetProject(templateHelper.AppDefinition.ServiceTestsProjectName);
            }
        }

        public Project TestingProject
        {
            get
            {
                return this.Studio.GetProject(templateHelper.AppDefinition.TestingProjectName);
            }
        }


        public string GetGeneratedCodeFolder(DomainType domainType)
        {
            return templateHelper.AppDefinition.GeneratedCodeRootFolderName + domainType.SubNamespace.Replace(".", @"\");
        }
    }
}
