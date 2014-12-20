using System;
using System.Collections.Generic;
using System.IO;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using EnvDTE;
using EnvDTE80;
using Microsoft.VisualStudio.TextTemplating;
using Zirpl.IO;

namespace Zirpl.AppEngine.VisualStudioAutomation.TextTemplating
{
    internal class OutputFileManager : IDisposable
    {
        private IList<OutputFile> CompletedFiles { get; set; }
        private TextTransformationContext Context { get; set; }
        private StringBuilder CallingTemplateOriginalGenerationEnvironment { get; set; }
        private StringBuilder CurrentGenerationEnvironment { get; set; }
        private OutputFile CurrentOutputFile { get; set; }

        internal OutputFileManager(TextTransformationContext context)
        {
            this.CallingTemplateOriginalGenerationEnvironment = context.CallingTemplate.GenerationEnvironment;
            this.CurrentGenerationEnvironment = context.CallingTemplate.GenerationEnvironment;
            this.Context = context;
            this.CompletedFiles = new List<OutputFile>();
        }

        internal void StartFile(ITextTransformation template, OutputFile file)
        {
            this.EndFile();

            this.CurrentOutputFile = file;
            this.CurrentGenerationEnvironment = template.GenerationEnvironment;
            this.Context.CallingTemplate.GenerationEnvironment = this.CurrentGenerationEnvironment;
        }

        internal void EndFile()
        {
            if (this.CurrentOutputFile != null)
            {
                var content = this.CurrentGenerationEnvironment.ToString();
                this.CurrentGenerationEnvironment = this.CallingTemplateOriginalGenerationEnvironment;
                this.Context.CallingTemplate.GenerationEnvironment = this.CurrentGenerationEnvironment;
                
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
                var fullFilePath = Path.Combine(
                    Path.GetDirectoryName(this.CurrentOutputFile.DestinationProject.FullName),
                    @"_auto\", 
                    this.CurrentOutputFile.FolderPathWithinProject ?? "",
                    this.CurrentOutputFile.FileName);
                PathUtilities.EnsureDirectoryExists(fullFilePath);
                this.CurrentOutputFile.DestinationProject.GetOrCreateFolder(@"_auto\", false);
                var folder = this.CurrentOutputFile.DestinationProject.GetOrCreateFolder(@"_auto\" + this.CurrentOutputFile.FolderPathWithinProject);

                if (File.Exists(fullFilePath))
                {
                    var isDifferent = File.ReadAllText(fullFilePath, this.CurrentOutputFile.Encoding) != content;
                    if (isDifferent
                        && this.CurrentOutputFile.CanOverrideExistingFile)
                    {
                        if (this.Context.VisualStudio.SourceControl != null
                            && this.Context.VisualStudio.SourceControl.IsItemUnderSCC(fullFilePath)
                            && !this.Context.VisualStudio.SourceControl.IsItemCheckedOut(fullFilePath))
                        {
                            this.Context.VisualStudio.SourceControl.CheckOutItem(fullFilePath);
                        }
                    }
                    else if (isDifferent)
                    {
                        throw new Exception("Could not overwrite file: " + fullFilePath);
                    }
                }

                this.Context.LogLineToBuildPane("   Writing file: " + fullFilePath);
                var item = this.Context.VisualStudio.Solution.GetProjectItem(fullFilePath);
                if (item != null)
                {
                    item.Remove();
                    //this.CurrentOutputFile.ProjectItem = item;
                    //item.Open();
                    //var td = (TextDocument) item.Document.Object();
                    //td.
                }
                //else
                {
                    File.WriteAllText(fullFilePath, content);
                    this.CurrentOutputFile.ProjectItem = folder.ProjectItems.AddFromFile(fullFilePath);
                }

                // set VS properties for the ProjectItem
                //
                if (!String.IsNullOrWhiteSpace(this.CurrentOutputFile.CustomTool))
                {
                    this.CurrentOutputFile.ProjectItem.SetPropertyValue("CustomTool", this.CurrentOutputFile.CustomTool);
                }
                var buildActionString = this.GetBuildActionString(this.CurrentOutputFile.BuildAction);
                if (!String.IsNullOrWhiteSpace(buildActionString))
                {
                    this.CurrentOutputFile.ProjectItem.SetPropertyValue("ItemType", buildActionString);
                }

                // autoformat 
                //
                if (this.CurrentOutputFile.AutoFormat)
                {
                    this.Context.VisualStudio.ExecuteVsCommand(this.CurrentOutputFile.ProjectItem, "Edit.FormatDocument"); //, "Edit.RemoveAndSort"));
                }

                this.CompletedFiles.Add(this.CurrentOutputFile);
                this.CurrentOutputFile = null;
            }
        }

        public void Dispose()
        {
            this.EndFile();
            foreach (Project project in this.Context.VisualStudio.Solution.GetAllProjects())
            {
                var autoFolder = project.ProjectItems.GetChild("_auto");
                if (autoFolder != null)
                {
                    foreach (var projectItem in autoFolder.GetAllProjectItems())
                    {
                        if (projectItem.IsPhysicalFile()
                            && !this.CompletedFiles.Any(o => o.ProjectItem == projectItem))
                        {
                            // this seemns to work ok the first time, then removed EVERYTHING after that
                            // additionally, this way of handling stale items makes it so that only 1 master t4 template can be used in the solution at a time, which is not ok.
                            // TODO: need to figure out a better way to handle, probably with subitems of the templates name
                            //
                            //projectItem.Delete();
                        }
                    }
                }
            }
        }

        private string GetBuildActionString(BuildActionTypeEnum buidlAction)
        {
            switch (buidlAction)
            {
                case BuildActionTypeEnum.Compile:
                    return "Compile";
                case BuildActionTypeEnum.Content:
                    return "Content";
                case BuildActionTypeEnum.EmbeddedResource:
                    return "EmbeddedResource";
                case BuildActionTypeEnum.EntityDeploy:
                    return "EntityDeploy";
                case BuildActionTypeEnum.None:
                default:
                    return "None";
            }
        }
    }
}
