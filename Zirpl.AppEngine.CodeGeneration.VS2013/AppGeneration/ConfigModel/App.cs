using System;
using System.Collections.Generic;
using EnvDTE;
using Zirpl.AppEngine.VisualStudioAutomation.AppGeneration.ConfigModel.Parsers.JsonModel;
using Zirpl.AppEngine.VisualStudioAutomation.TextTemplating;

namespace Zirpl.AppEngine.VisualStudioAutomation.AppGeneration.ConfigModel
{
    public class App
    {
        public App()
        {
            this.DomainTypes = new List<DomainTypeInfo>();
            this.FilesToGenerate = new List<TransformOutputFile>();
        }

        public AppJson Config { get; set; }
        public String ConfigFilePath { get; set; }
        public String GeneratedCodeRootFolderName { get; set; }
        public String GeneratedCSFileExtension { get; set; }
        public String ModelProjectName { get; set; }
        public String DataServiceProjectName { get; set; }
        public String ServiceProjectName { get; set; }
        public String WebProjectName { get; set; }
        public String WebCommonProjectName { get; set; }
        public String DataServiceTestsProjectName { get; set; }
        public String ServiceTestsProjectName { get; set; }
        public String TestsCommonProjectName { get; set; }
        public String DataContextName { get; set; }
        public List<DomainTypeInfo> DomainTypes { get; set; }
        public List<TransformOutputFile> FilesToGenerate { get; set; }

        #region Project references

        public Project CodeGenerationProject
        {
            get
            {
                return TransformSession.Instance.TemplateProjectItem.ContainingProject;
            }
        }

        public Project ModelProject
        {
            get
            {
                return VisualStudio.Current.GetProject(this.ModelProjectName);
            }
        }

        public Project DataServiceProject
        {
            get
            {
                return VisualStudio.Current.GetProject(this.DataServiceProjectName);
            }
        }

        public Project DataServiceTestsProject
        {
            get
            {
                return VisualStudio.Current.GetProject(this.DataServiceTestsProjectName);
            }
        }

        public Project ServiceProject
        {
            get
            {
                return VisualStudio.Current.GetProject(this.ServiceProjectName);
            }
        }

        public Project WebProject
        {
            get
            {
                return VisualStudio.Current.GetProject(this.WebProjectName);
            }
        }

        public Project WebCoreProject
        {
            get
            {
                return VisualStudio.Current.GetProject(this.WebCommonProjectName);
            }
        }

        public Project ServiceTestsProject
        {
            get
            {
                return VisualStudio.Current.GetProject(this.ServiceTestsProjectName);
            }
        }

        public Project TestsCommonProject
        {
            get
            {
                return VisualStudio.Current.GetProject(this.TestsCommonProjectName);
            }
        }

        #endregion
    }
}
