using System;
using System.Collections.Generic;
using System.IO;
using System.Linq;
using System.Text;
using EnvDTE;
using EnvDTE80;
using Microsoft.VisualStudio.TextTemplating;
using Zirpl.AppEngine.Logging;
using Zirpl.AppEngine.VisualStudioAutomation.VisualStudio;
using Zirpl.IO;

namespace Zirpl.AppEngine.VisualStudioAutomation.TextTemplating
{
    internal class OutputFileManager : IOutputFileManager
    {
        private readonly IList<OutputInfo> _completedFiles;
        private readonly DTE2 _visualStudio;
        private readonly IMasterTransform _masterTransform;
        private readonly String _placeHolderName;
        private readonly StringBuilder _callingTemplateOriginalGenerationEnvironment;
        private StringBuilder _currentGenerationEnvironment;
        private OutputInfo _currentOutputFile;

        internal OutputFileManager(IMasterTransform masterTransform)
        {
            if (masterTransform == null) throw new ArgumentNullException("masterTransform");
            if (!masterTransform.IsMaster) throw new ArgumentException("IsMaster returned false", "masterTransform");

            this._visualStudio = masterTransform.GetDTE();
            this._masterTransform = masterTransform;

            var callingTemplateProjectItem = masterTransform.GetProjectItem();

            this._callingTemplateOriginalGenerationEnvironment = this._masterTransform.GenerationEnvironment;
            this._currentGenerationEnvironment = this._masterTransform.GenerationEnvironment;
            var placeholder = callingTemplateProjectItem.ProjectItems.ToEnumerable().SingleOrDefault();
            this._placeHolderName = placeholder == null ? null : placeholder.Name;
            this._completedFiles = new List<OutputInfo>();
        }

        public void StartFile(ITransform currentTransform, OutputInfo file)
        {
            // we end it first, only because it will make logging easier to follow/debug
            this.EndFile();

            if (currentTransform == null) throw new ArgumentNullException("currentTransform");

            if (file == null) throw new ArgumentNullException("file");

            if (String.IsNullOrEmpty(file.FileNameWithoutExtension)
                || file.DestinationProject == null)
            {
                throw new ArgumentException("Cannot start a file without at least a file name and a Destination project");
            }

            this._currentOutputFile = file;
            this._currentGenerationEnvironment = currentTransform.GenerationEnvironment;
            this._masterTransform.GenerationEnvironment = this._currentGenerationEnvironment;
        }

        public void UseDefaultFile(ITransform currentTransform)
        {
            this.EndFile();

            if (currentTransform == null) throw new ArgumentNullException("currentTransform");

            currentTransform.GenerationEnvironment = this._callingTemplateOriginalGenerationEnvironment;
        }

        public void EndFile()
        {
            if (this._currentOutputFile != null)
            {
                var content = this._currentGenerationEnvironment.ToString();
                this._currentGenerationEnvironment = this._callingTemplateOriginalGenerationEnvironment;
                this._masterTransform.GenerationEnvironment = this._currentGenerationEnvironment;
                
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

                var fullFilePath = Path.Combine(
                    Path.GetDirectoryName(this._currentOutputFile.DestinationProject.FullName),
                    @"_auto\", 
                    this._currentOutputFile.FolderPathWithinProject ?? "",
                    this._currentOutputFile.FileName);
                PathUtilities.EnsureDirectoryExists(fullFilePath);
                this._currentOutputFile.DestinationProject.GetOrCreateFolder(@"_auto\", false);
                var folder = this._currentOutputFile.DestinationProject.GetOrCreateFolder(@"_auto\" + this._currentOutputFile.FolderPathWithinProject);
                var placeHolderItem = folder.ProjectItems.ToEnumerable().SingleOrDefault(o => o.Name == this._placeHolderName);
                if (placeHolderItem == null)
                {
                    var fullPlaceHolderPath = Path.Combine(folder.GetFullPath(), this._placeHolderName);
                    File.Create(fullPlaceHolderPath).Dispose();
                    placeHolderItem = folder.ProjectItems.AddFromFile(fullPlaceHolderPath);
                }



                if (File.Exists(fullFilePath))
                {
                    var isDifferent = File.ReadAllText(fullFilePath, this._currentOutputFile.Encoding) != content;
                    if (isDifferent
                        && this._currentOutputFile.CanOverrideExistingFile)
                    {
                        if (this._visualStudio.SourceControl != null
                            && this._visualStudio.SourceControl.IsItemUnderSCC(fullFilePath)
                            && !this._visualStudio.SourceControl.IsItemCheckedOut(fullFilePath))
                        {
                            this._visualStudio.SourceControl.CheckOutItem(fullFilePath);
                        }
                    }
                    else if (isDifferent)
                    {
                        throw new Exception("Could not overwrite file: " + fullFilePath);
                    }
                }

                this.GetLog().Debug("   Writing file: " + fullFilePath);
                //File.WriteAllText(fullFilePath, content);
                var item = this._visualStudio.Solution.GetProjectItem(fullFilePath);
                if (item == null)
                {
                    File.Create(fullFilePath).Dispose();
                    item = placeHolderItem.ProjectItems.AddFromFile(fullFilePath);
                }
                Window window = item.Open(Constants.vsext_vk_Code);
                window.Visible = false; //hide editor window
                var document = (TextDocument)window.Document.Object("TextDocument");
                var editPoint = document.CreateEditPoint();
                editPoint.Delete(document.EndPoint);
                editPoint.Insert(content);
                if (this._currentOutputFile.AutoFormat)
                {
                    editPoint.StartOfDocument();
                    editPoint.SmartFormat(document.EndPoint);
                }
                window.Document.Save();
                window.Document.Close();
                window.Close();


                this._currentOutputFile.ProjectItem = item;

                // set VS properties for the ProjectItem
                //
                if (!String.IsNullOrWhiteSpace(this._currentOutputFile.CustomTool))
                {
                    this._currentOutputFile.ProjectItem.SetPropertyValue("CustomTool", this._currentOutputFile.CustomTool);
                }
                var buildActionString = this.GetBuildActionString(this._currentOutputFile.BuildAction);
                if (!String.IsNullOrWhiteSpace(buildActionString))
                {
                    this._currentOutputFile.ProjectItem.SetPropertyValue("ItemType", buildActionString);
                }

                this._completedFiles.Add(this._currentOutputFile);
                this._currentOutputFile = null;
            }
        }

        public void Dispose()
        {
            this.EndFile();
            foreach (Project project in this._visualStudio.Solution.GetAllProjects())
            {
                var autoFolder = project.ProjectItems.GetChild("_auto");
                if (autoFolder != null)
                {
                    var placeHolderList = from o in autoFolder.GetAllProjectItems()
                        where o.Name == this._placeHolderName 
                            && ((ProjectItem) o.Collection.Parent).IsPhysicalFolder()
                        select o;
                    foreach (var placeHolderItem in placeHolderList.ToList())
                    {
                        foreach (var projectItem in placeHolderItem.ProjectItems.ToEnumerable())
                        {
                            if (!this._completedFiles.Any(o => o.ProjectItem == projectItem))
                            {
                                this.GetLog().Debug("Deleting stale auto-generated file: " + projectItem.GetFullPath());
                                projectItem.Delete();
                            }
                        }
                    }
                    placeHolderList = from o in autoFolder.GetAllProjectItems()
                                      where o.Name == this._placeHolderName
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

        private string GetBuildActionString(BuildActionTypeEnum buildAction)
        {
            switch (buildAction)
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
