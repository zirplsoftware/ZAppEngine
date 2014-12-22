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
            this.BuildAction = BuildActionTypeEnum.Compile;
            this.AutoFormat = true;
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
        public String FileName
        {
            get { return this.FileNameWithoutExtension + this.FileExtension; }
        }

        public OutputFile AsCSharpFile()
        {
            FileExtension = ".cs";
            BuildAction = BuildActionTypeEnum.Compile;
            return this;
        }

        public OutputFile MatchBuildActionToFileExtension()
        {
            var extension = FileExtension.OrEmpty().ToLowerInvariant();
            switch (extension)
            {
                case ".cs":
                    BuildAction = BuildActionTypeEnum.Compile;
                    break;
                default:
                    BuildAction = BuildActionTypeEnum.None;
                    break;
            }
            return this;
        }
    }
}
