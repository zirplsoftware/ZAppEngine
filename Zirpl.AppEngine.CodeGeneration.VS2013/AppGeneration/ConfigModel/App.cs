using System;
using System.Collections.Generic;
using EnvDTE;
using Zirpl.AppEngine.CodeGeneration.AppGeneration.ConfigModel.Parsers.JsonModel;
using Zirpl.AppEngine.CodeGeneration.TextTemplating;

namespace Zirpl.AppEngine.CodeGeneration.AppGeneration.ConfigModel
{
    public class App
    {
        public App()
        {
            this.DomainTypes = new List<DomainTypeInfo>();
            this.FilesToGenerate = new List<FileToGenerate>();
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
        public List<FileToGenerate> FilesToGenerate { get; set; }

        #region Project references

        public Project CodeGenerationProject
        {
            get
            {
                return TextTransformationSession.Instance.TemplateProjectItem.ContainingProject;
            }
        }

        public Project ModelProject
        {
            get
            {
                return TextTransformationSession.Instance.VisualStudio.GetProject(this.ModelProjectName);
            }
        }

        public Project DataServiceProject
        {
            get
            {
                return TextTransformationSession.Instance.VisualStudio.GetProject(this.DataServiceProjectName);
            }
        }

        public Project DataServiceTestsProject
        {
            get
            {
                return TextTransformationSession.Instance.VisualStudio.GetProject(this.DataServiceTestsProjectName);
            }
        }

        public Project ServiceProject
        {
            get
            {
                return TextTransformationSession.Instance.VisualStudio.GetProject(this.ServiceProjectName);
            }
        }

        public Project WebProject
        {
            get
            {
                return TextTransformationSession.Instance.VisualStudio.GetProject(this.WebProjectName);
            }
        }

        public Project WebCoreProject
        {
            get
            {
                return TextTransformationSession.Instance.VisualStudio.GetProject(this.WebCommonProjectName);
            }
        }

        public Project ServiceTestsProject
        {
            get
            {
                return TextTransformationSession.Instance.VisualStudio.GetProject(this.ServiceTestsProjectName);
            }
        }

        public Project TestsCommonProject
        {
            get
            {
                return TextTransformationSession.Instance.VisualStudio.GetProject(this.TestsCommonProjectName);
            }
        }

        #endregion
    }
}
