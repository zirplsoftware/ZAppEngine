using System;
using System.Text;

namespace Zirpl.AppEngine.VisualStudioAutomation.TextTemplating
{
    public class OutputInfo
    {
        public OutputInfo()
        {
            this.CanOverrideExistingFile = true;
            this.Encoding = Encoding.UTF8;
            this.BuildAction = BuildActionTypeEnum.Compile;
            this.AutoFormat = true;
        }
        public String FileNameWithoutExtension { get; set; }
        public String FileExtension { get; set; }
        public String DestinationProjectFullName { get; set; }
        public String FolderPathWithinProject { get; set; }
        public BuildActionTypeEnum BuildAction { get; set; }
        public string CustomTool { get; set; }
        public bool CanOverrideExistingFile { get; set; }
        public bool AutoFormat { get; set; }
        public Encoding Encoding { get; set; }

        public ProjectItemIndex ProjectItemIndex { get; internal set; }
        public String FileName
        {
            get { return this.FileNameWithoutExtension + this.FileExtension; }
        }

        public OutputInfo AsCSharpFile()
        {
            FileExtension = ".cs";
            BuildAction = BuildActionTypeEnum.Compile;
            return this;
        }

        public OutputInfo MatchBuildActionToFileExtension()
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
