using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using EnvDTE;
using Zirpl.AppEngine.CodeGeneration.TextTemplating;

namespace Zirpl.AppEngine.CodeGeneration.V2.ConfigModel
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
