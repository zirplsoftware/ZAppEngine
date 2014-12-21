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
        private String PlaceHolderFileName { get; set; }

        internal OutputFileManager(TextTransformationContext context)
        {
            this.CallingTemplateOriginalGenerationEnvironment = context.CallingTemplate.GenerationEnvironment;
            this.CurrentGenerationEnvironment = context.CallingTemplate.GenerationEnvironment;
            this.Context = context;
            var placeholder = context.CallingTemplateProjectItem.ProjectItems.ToEnumerable().SingleOrDefault();
            this.PlaceHolderFileName = placeholder == null ? null : placeholder.Name;
            this.CompletedFiles = new List<OutputFile>();
        }

        internal void StartFile(ITextTransformation template, OutputFile file)
        {
            this.EndFile();

            if (String.IsNullOrEmpty(file.FileNameWithoutExtension)
                || file.DestinationProject == null)
            {
                throw new ArgumentException("Cannot start a file without at least a file name and a Destination project");
            }

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
                var placeHolderItem = folder.ProjectItems.ToEnumerable().SingleOrDefault(o => o.Name == this.PlaceHolderFileName);
                if (placeHolderItem == null)
                {
                    var fullPlaceHolderPath = Path.Combine(folder.GetFullPath(), this.PlaceHolderFileName);
                    File.Create(fullPlaceHolderPath);
                    placeHolderItem = folder.ProjectItems.AddFromFile(fullPlaceHolderPath);
                }



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
                File.WriteAllText(fullFilePath, content);
                var item = this.Context.VisualStudio.Solution.GetProjectItem(fullFilePath);
                this.CurrentOutputFile.ProjectItem = item ?? placeHolderItem.ProjectItems.AddFromFile(fullFilePath);

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
                    var placeHolderList = from o in autoFolder.GetAllProjectItems()
                        where o.Name == this.PlaceHolderFileName 
                            && ((ProjectItem) o.Collection.Parent).IsPhysicalFolder()
                        select o;
                    foreach (var placeHolderItem in placeHolderList.ToList())
                    {
                        foreach (var projectItem in placeHolderItem.ProjectItems.ToEnumerable())
                        {
                            if (!this.CompletedFiles.Any(o => o.ProjectItem == projectItem))
                            {
                                this.Context.LogLineToBuildPane("Deleting stale auto-generated file: " + projectItem.GetFullPath());
                                projectItem.Delete();
                            }
                        }
                    }
                    placeHolderList = from o in autoFolder.GetAllProjectItems()
                                      where o.Name == this.PlaceHolderFileName
                                          && ((ProjectItem)o.Collection.Parent).IsPhysicalFolder()
                                      select o;
                    foreach (var placeHolderItem in placeHolderList.Where(o => o.ProjectItems.Count == 0).ToList())
                    {
                        placeHolderItem.Delete();
                    }
                    if (autoFolder.ProjectItems.Count == 0
                        || autoFolder.GetAllProjectItems().All(o => o.IsPhysicalFolder()))
                    {
                        autoFolder.Delete();
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
