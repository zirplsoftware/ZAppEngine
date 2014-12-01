using System;
using System.Collections.Generic;
using System.Xml.Serialization;
using EnvDTE;
using EnvDTE80;
using Zirpl.AppEngine.CodeGeneration.TextTemplating;
using Zirpl.AppEngine.CodeGeneration.V2.ConfigModel.Parsers.JsonModel;
using Zirpl.Xml.Serialization;

namespace Zirpl.AppEngine.CodeGeneration.V2.ConfigModel
{
    public class AppInfo
    {
        public AppInfo()
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
        public String WebCoreProjectName { get; set; }
        public String DataServiceTestsProjectName { get; set; }
        public String ServiceTestsProjectName { get; set; }
        public String TestsCommonProjectName { get; set; }
        public String DataContextName { get; set; }
        public List<DomainTypeInfo> DomainTypes { get; set; }
        public List<FileToGenerate> FilesToGenerate { get; set; }

        #region Project references
        public DTE2 VisualStudio
        {
            get
            {
                return TextTransformationSession.Instance.FileManager.VisualStudio;
            }
        }

        public Project CodeGenerationProject
        {
            get
            {
                return TextTransformationSession.Instance.FileManager.TemplateProjectItem.ContainingProject;
            }
        }

        public Project ModelProject
        {
            get
            {
                return this.VisualStudio.GetProject(this.ModelProjectName);
            }
        }

        public Project DataServiceProject
        {
            get
            {
                return this.VisualStudio.GetProject(this.DataServiceProjectName);
            }
        }

        public Project DataServiceTestsProject
        {
            get
            {
                return this.VisualStudio.GetProject(this.DataServiceTestsProjectName);
            }
        }

        public Project ServiceProject
        {
            get
            {
                return this.VisualStudio.GetProject(this.ServiceProjectName);
            }
        }

        public Project WebProject
        {
            get
            {
                return this.VisualStudio.GetProject(this.WebProjectName);
            }
        }

        public Project WebCoreProject
        {
            get
            {
                return this.VisualStudio.GetProject(this.WebCoreProjectName);
            }
        }

        public Project ServiceTestsProject
        {
            get
            {
                return this.VisualStudio.GetProject(this.ServiceTestsProjectName);
            }
        }

        public Project TestsCommonProject
        {
            get
            {
                return this.VisualStudio.GetProject(this.TestsCommonProjectName);
            }
        }

        #endregion
    }
}
