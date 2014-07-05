using System;

namespace Zirpl.AppEngine.CodeGeneration.Transformation
{
    public sealed class TextBlock
    {
        public TextBlock(OutputFileProperties fileProperties = null)
        {
            this.FileProperties = fileProperties ?? new OutputFileProperties();
        }

        public String Name { get; set; }
        public int Start { get; set; } 
        public int Length { get; set; }
        public string ProjectName { get; set; }
        public string FolderName { get; set; }
        public OutputFileProperties FileProperties { get; private set; }
    }
}
