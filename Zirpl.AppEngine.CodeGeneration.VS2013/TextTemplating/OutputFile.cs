using System;
using System.Collections.Generic;
using System.IO;
using System.Text;
using EnvDTE;

namespace Zirpl.AppEngine.VisualStudioAutomation.TextTemplating
{
    public class OutputFile
    {
        public OutputFile()
        {
            //this.ContentParameters = new Dictionary<string, string>();
            this.CanOverrideExistingFile = true;
            this.Encoding = Encoding.UTF8;
        }
        public String FileNameWithoutExtension { get; set; }
        public String FileExtension { get; set; }
        public Project DestinationProject { get; set; }
        public String FolderPathWithinProject { get; set; }
        public BuildActionTypeEnum BuildAction { get; set; }
        public string CustomTool { get; set; }
        public bool CanOverrideExistingFile { get; set; }
        public bool AutoFormat { get; set; }
        public Encoding Encoding { get; set; }
        //public Dictionary<string, string> ContentParameters { get; set; }

        public ProjectItem ProjectItem { get; internal set; }
        public string Content { get; internal set; }
        public String FileName
        {
            get { return this.FileNameWithoutExtension + this.FileExtension; }
        }
        public String FullFilePath
        {
            get
            {
                return Path.Combine(
                    Path.GetDirectoryName(this.DestinationProject.FullName),
                    this.FolderPathWithinProject,
                    this.FileName);
            }
        }
        public String FilePathWithinProject
        {
            get
            {
                return Path.Combine(
                    this.FolderPathWithinProject,
                    this.FileName);
            }
        }
        internal string BuildActionString
        {
            get
            {
                switch (BuildAction)
                {
                    case BuildActionTypeEnum.Compile:
                        return "Compile";
                    case BuildActionTypeEnum.Content:
                        return "Content";
                    case BuildActionTypeEnum.EmbeddedResource:
                        return "EmbeddedResource";
                    case BuildActionTypeEnum.EntityDeploy:
                        return "EntityDeploy";
                    case BuildActionTypeEnum.None:
                    default:
                        return "None";
                }
            }
        }
    }
}
