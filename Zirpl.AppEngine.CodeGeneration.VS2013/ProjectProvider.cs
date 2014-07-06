using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using EnvDTE;
using Zirpl.AppEngine.CodeGeneration.ConfigModel;
using Zirpl.AppEngine.CodeGeneration.Transformation;

namespace Zirpl.AppEngine.CodeGeneration
{
    public class ProjectProvider
    {
        public AppGenerator appGenerator;

        public ProjectProvider(AppGenerator appGenerator)
        {
            this.appGenerator = appGenerator;
        }


        public DTE Studio
        {
            get
            {
                return appGenerator.FileManager.Studio;
            }
        }

        public Project CodeGenerationProject
        {
            get
            {
                return appGenerator.FileManager.TemplateProjectItem.ContainingProject;
            }
        }

        public Project ModelProject
        {
            get
            {
                return this.Studio.GetProject(appGenerator.AppDefinition.ModelProjectName);
            }
        }

        public Project DataServiceProject
        {
            get
            {
                return this.Studio.GetProject(appGenerator.AppDefinition.DataServiceProjectName);
            }
        }

        public Project DataServiceTestsProject
        {
            get
            {
                return this.Studio.GetProject(appGenerator.AppDefinition.DataServiceTestsProjectName);
            }
        }

        public Project ServiceProject
        {
            get
            {
                return this.Studio.GetProject(appGenerator.AppDefinition.ServiceProjectName);
            }
        }

        public Project WebProject
        {
            get
            {
                return this.Studio.GetProject(appGenerator.AppDefinition.WebProjectName);
            }
        }

        public Project WebCoreProject
        {
            get
            {
                return this.Studio.GetProject(appGenerator.AppDefinition.WebCoreProjectName);
            }
        }

        public Project ServiceTestsProject
        {
            get
            {
                return this.Studio.GetProject(appGenerator.AppDefinition.ServiceTestsProjectName);
            }
        }

        public Project TestingProject
        {
            get
            {
                return this.Studio.GetProject(appGenerator.AppDefinition.TestingProjectName);
            }
        }


        public string GetGeneratedCodeFolder(DomainType domainType)
        {
            return appGenerator.AppDefinition.GeneratedCodeRootFolderName + domainType.SubNamespace.Replace(".", @"\");
        }
    }
}
