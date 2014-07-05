namespace Zirpl.AppEngine.CodeGeneration.Transformation
{
    public class OutputFile
    {
        public OutputFile(OutputFileProperties fileProperties = null)
        {
            this.FileProperties = fileProperties ?? new OutputFileProperties();
        }

        public string FileName { get; set; }
        public string ProjectName { get; set; }
        public string FolderName { get; set; }
        public string Content { get; set; }
        public OutputFileProperties FileProperties { get; private set; }
    }
}
