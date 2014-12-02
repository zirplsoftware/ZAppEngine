using System;
using System.Collections.Generic;
using EnvDTE;
using Microsoft.VisualStudio.Shell.Interop;

namespace Zirpl.AppEngine.CodeGeneration.TextTemplating
{
    public class FileToGenerate
    {
        public FileToGenerate()
        {
            this.TemplateParameters = new Dictionary<string, object>();
        }
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
        public IDictionary<string, object> TemplateParameters { get; set; }
    }
}
