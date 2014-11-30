using System;
using EnvDTE;

namespace Zirpl.AppEngine.CodeGeneration.TextTemplating
{
    public class FileToGenerate
    {
        public String FileNameWithoutExtension { get; set; }
        public String FileExtension { get; set; }
        public Project DestinationProject { get; set; }
        public String FolderPath { get; set; }
        public OutputFileBuildActionType BuildAction { get; set; }
        public String FileName
        {
            get { return this.FileNameWithoutExtension + this.FileExtension; }
        }
        public Type TemplateType { get; set; }
    }
}
