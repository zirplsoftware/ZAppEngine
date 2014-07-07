using EnvDTE;
using EnvDTE80;
using Zirpl.AppEngine.CodeGeneration.TextTemplating;
using Zirpl.AppEngine.CodeGeneration.V1.ConfigModel;

namespace Zirpl.AppEngine.CodeGeneration.V1
{
    public class ProjectProvider
    {
        private TransformationHelper transformationHelper;

        public ProjectProvider(TransformationHelper transformationHelper)
        {
            this.transformationHelper = transformationHelper;
        }


        public DTE2 VisualStudio
        {
            get
            {
                return transformationHelper.FileManager.VisualStudio;
            }
        }

        public Project CodeGenerationProject
        {
            get
            {
                return transformationHelper.FileManager.TemplateProjectItem.ContainingProject;
            }
        }

        public Project ModelProject
        {
            get
            {
                return this.VisualStudio.GetProject(transformationHelper.AppDefinition.ModelProjectName);
            }
        }

        public Project DataServiceProject
        {
            get
            {
                return this.VisualStudio.GetProject(transformationHelper.AppDefinition.DataServiceProjectName);
            }
        }

        public Project DataServiceTestsProject
        {
            get
            {
                return this.VisualStudio.GetProject(transformationHelper.AppDefinition.DataServiceTestsProjectName);
            }
        }

        public Project ServiceProject
        {
            get
            {
                return this.VisualStudio.GetProject(transformationHelper.AppDefinition.ServiceProjectName);
            }
        }

        public Project WebProject
        {
            get
            {
                return this.VisualStudio.GetProject(transformationHelper.AppDefinition.WebProjectName);
            }
        }

        public Project WebCoreProject
        {
            get
            {
                return this.VisualStudio.GetProject(transformationHelper.AppDefinition.WebCoreProjectName);
            }
        }

        public Project ServiceTestsProject
        {
            get
            {
                return this.VisualStudio.GetProject(transformationHelper.AppDefinition.ServiceTestsProjectName);
            }
        }

        public Project TestingProject
        {
            get
            {
                return this.VisualStudio.GetProject(transformationHelper.AppDefinition.TestingProjectName);
            }
        }


        public string GetGeneratedCodeFolder(DomainType domainType)
        {
            return transformationHelper.AppDefinition.GeneratedCodeRootFolderName + domainType.SubNamespace.Replace(".", @"\");
        }
    }
}
