using System;
using System.Collections.Generic;
using System.IO;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using Microsoft.VisualStudio.TextTemplating;
using Zirpl.IO;

namespace Zirpl.AppEngine.VisualStudioAutomation.TextTemplating
{
    public class OutputFileManager
    {
        public void CreateFile(OutputFile outputFile)
        {
            // apply parameters to content
            //
            // check out from source control
            //      check if file exists
            //      if exists, check if different
            //      if different, check if allowed to overwrite
            //      if allowed, check out from source control
            //
            // create the file
            //      create the folder on system
            //      write the file to disk
            //      create the folder in project
            //      add file to folder project item
            //      write VS properties (if each exists)
            //          CustomTool
            //          ItemType
            //      check if autoformat
            //          format
            //
            // clean up template placeholders

            // TODO: implement all of the above

            PathUtilities.EnsureDirectoryExists(outputFile.FullFilePath);
            File.WriteAllText(outputFile.FullFilePath, outputFile.Content);
            var folder = outputFile.DestinationProject.GetOrCreateProjectFolder(outputFile.FolderPathWithinProject);
            outputFile.ProjectItem = folder.ProjectItems.AddFromFile(outputFile.FullFilePath);
        }

        public void Finish()
        {
            
        }
    }
}
