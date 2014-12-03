using System;
using System.Collections.Generic;
using System.IO;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using Zirpl.IO;

namespace Zirpl.AppEngine.VisualStudioAutomation.TextTemplating
{
    public class OutputFileManager
    {
        public void CreateFile(OutputFile outputFile)
        {
            // apply parameters to content
            //
            // check if file exists
            //      if exists, chech if different
            //      if different, check if allowed to overwrite
            //      if allowed, check out from source control
            //
            // write the file
            //      create ProjectItem
            //      write VS properties (if each exists)
            //          CustomTool
            //          ItemType
            //      check if autoformat
            //          format
            //
            // clean up template placeholders
            PathUtilities.EnsureDirectoryExists(outputFile.FullFilePath);
            File.WriteAllText(outputFile.FullFilePath, outputFile.Content);
            var folder = outputFile.DestinationProject.GetOrCreateProjectFolder(outputFile.FolderPathWithinProject);
            outputFile.ProjectItem = folder.ProjectItems.AddFromFile(outputFile.FullFilePath);
        }
    }
}
