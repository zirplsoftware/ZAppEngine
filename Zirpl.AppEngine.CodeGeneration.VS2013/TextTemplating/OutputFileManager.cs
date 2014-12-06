using System;
using System.Collections.Generic;
using System.IO;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using EnvDTE80;
using Microsoft.VisualStudio.TextTemplating;
using Zirpl.IO;

namespace Zirpl.AppEngine.VisualStudioAutomation.TextTemplating
{
    public class OutputFileManager
    {
        private IList<OutputFile> Files { get; set; }
        private TextTransformationContext Context { get; set; }

        public OutputFileManager(TextTransformationContext context)
        {
            this.Context = context;
            this.Files = new List<OutputFile>();
        }

        public void CreateFile(OutputFile outputFile)
        {
            // apply parameters to content
            //
            // 1) ensure directory exists       [DONE]
            //      first within file system    [DONE]
            //      then within Solution        [DONE]
            //
            // 2) check out from source control
            //      check if file exists                        [DONE]
            //      if exists, check if different               [DONE]
            //      if different, check if allowed to overwrite [DONE]
            //      if allowed, check out from source control   [DONE]
            //
            // 3) create the file
            //      write the file to disk                  [DONE]
            //      add file to folder project item         [DONE]
            //      write VS properties (if each exists)    [DONE]
            //          CustomTool                          [DONE]
            //          ItemType                            [DONE]
            //      check if autoformat                     [DONE]
            //          format                              [DONE]
            //
            // clean up template placeholders

            // TODO: use template placeholders if should

            PathUtilities.EnsureDirectoryExists(outputFile.FullFilePath);
            var folder = outputFile.DestinationProject.GetOrCreateProjectFolder(outputFile.FolderPathWithinProject);

            if (File.Exists(outputFile.FullFilePath))
            {
                if (File.ReadAllText(outputFile.FullFilePath, outputFile.Encoding) != outputFile.Content
                    && outputFile.CanOverrideExistingFile)
                {
                    if (this.Context.VisualStudio.SourceControl != null
                        && this.Context.VisualStudio.SourceControl.IsItemUnderSCC(outputFile.FullFilePath)
                        && !this.Context.VisualStudio.SourceControl.IsItemCheckedOut(outputFile.FullFilePath))
                    {
                        this.Context.VisualStudio.SourceControl.CheckOutItem(outputFile.FullFilePath);
                    }
                }
            }

            File.WriteAllText(outputFile.FullFilePath, outputFile.Content);
            outputFile.ProjectItem = folder.ProjectItems.AddFromFile(outputFile.FullFilePath);

            // set VS properties for the ProjectItem
            //
            if (!String.IsNullOrWhiteSpace(outputFile.CustomTool))
            {
                outputFile.ProjectItem.SetPropertyValue("CustomTool", outputFile.CustomTool);
            }
            if (!String.IsNullOrWhiteSpace(outputFile.BuildActionString))
            {
                outputFile.ProjectItem.SetPropertyValue("ItemType", outputFile.BuildActionString);
            }

            // autoformat 
            //
            if (outputFile.AutoFormat)
            {
                this.Context.VisualStudio.ExecuteVsCommand(outputFile.ProjectItem, "Edit.FormatDocument"); //, "Edit.RemoveAndSort"));
            }
        }

        public void Finish()
        {
            
        }
    }
}
