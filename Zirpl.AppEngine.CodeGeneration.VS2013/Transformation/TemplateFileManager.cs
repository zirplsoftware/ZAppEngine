﻿using System;
using System.Collections.Generic;
using System.IO;
using System.Linq;
using System.Text;
using System.Text.RegularExpressions;
using EnvDTE;

namespace Zirpl.AppEngine.CodeGeneration.Transformation
{
    /*
    This software is supplied "AS IS". The authors disclaim all warranties, 
    expressed or implied, including, without limitation, the warranties of 
    merchantability and of fitness for any purpose. The authors assume no
    liability for direct, indirect, incidental, special, exemplary, or
    consequential damages, which may result from the use of this software,
    even if advised of the possibility of such damage.

 The TemplateFileManager is based on EntityFrameworkTemplateFileManager (EFTFM) from MS.

 Differences to EFTFM
 Version 2.1:
 - Replace Enum BuildAction with class for more flexibility
 Version 2:
 - StartHeader works with Parameter $filename$
 - StartNewFile has a new named parameter FileProperties
   - Support for:
    - BuildAction
    - CustomTool
    - user defined parameter for using in StartHeader-Block
 - Property IsAutoIndentEnabled for support Format Document (C#, VB) when set to true

 Version: 1.1
 Add method WriteLineToBuildPane, WriteToBuildPane

 Version 1:
 - StartNewFile with named parameters projectName and folderName for generating files to different locations.
 - Property CanOverrideExistingFile, to define whether existing files are can overwritten
 - Property Encoding Encode type for output files.
 */

    /// <summary>
    /// Responsible for marking the various sections of the generation,
    /// so they can be split up into separate files and projects
    /// </summary>
    /// <author>R. Leupold</author>
    public class TemplateFileManager
    {
        private EnvDTE.ProjectItem templateProjectItem;
        private Action<string> checkOutAction;
        private Action<IEnumerable<OutputFile>> projectSyncAction;
        private EnvDTE.DTE dte;
        private List<string> templatePlaceholderList = new List<string>();

        /// <summary>
        /// Creates files with VS sync
        /// </summary>
        public static TemplateFileManager Create(object textTransformation)
        {
            DynamicTextTransformation transformation = DynamicTextTransformation.Create(textTransformation);
            IDynamicHost host = transformation.Host;
            return new TemplateFileManager(transformation);
        }

        private readonly List<TextBlock> files = new List<TextBlock>();
        private readonly TextBlock footer = new TextBlock();
        private readonly TextBlock header = new TextBlock();
        private readonly DynamicTextTransformation _textTransformation;

        // reference to the GenerationEnvironment StringBuilder on the
        // TextTransformation object
        private readonly StringBuilder _generationEnvironment;

        private TextBlock currentBlock;

        /// <summary>
        /// Initializes an TemplateFileManager Instance  with the
        /// TextTransformation (T4 generated class) that is currently running
        /// </summary>
        private TemplateFileManager(object textTransformation)
        {
            if (textTransformation == null)
            {
                throw new ArgumentNullException("textTransformation");
            }

            _textTransformation = DynamicTextTransformation.Create(textTransformation);
            _generationEnvironment = _textTransformation.GenerationEnvironment;

            var hostServiceProvider = _textTransformation.Host.AsIServiceProvider();
            if (hostServiceProvider == null)
            {
                throw new ArgumentNullException("Could not obtain hostServiceProvider");
            }

            dte = (EnvDTE.DTE)hostServiceProvider.GetService(typeof(EnvDTE.DTE));
            if (dte == null)
            {
                throw new ArgumentNullException("Could not obtain DTE from host");
            }

            this.templateProjectItem = dte.Solution.FindProjectItem(_textTransformation.Host.TemplateFile);
            this.CanOverrideExistingFile = true;
            this.IsAutoIndentEnabled = false;
            this.Encoding = System.Text.Encoding.UTF8;
            checkOutAction = fileName => dte.SourceControl.CheckOutItem(fileName);
            projectSyncAction = keepFileNames => ProjectSync(templateProjectItem, keepFileNames);
        }

        /// <summary>
        /// If set to false, existing files are not overwritten
        /// </summary>
        /// <returns></returns>
        public bool CanOverrideExistingFile { get; set; }

        /// <summary>
        /// If set to true, output files (c#, vb) are formatted based on the vs settings.
        /// </summary>
        /// <returns></returns>
        public bool IsAutoIndentEnabled { get; set; }

        /// <summary>
        /// Defines Encoding format for generated output file. (Default UTF8)
        /// </summary>
        /// <returns></returns>
        public System.Text.Encoding Encoding { get; set; }

        /// <summary>
        /// Marks the end of the last file if there was one, and starts a new
        /// and marks this point in generation as a new file.
        /// </summary>
        /// <param name="name">Filename</param>
        /// <param name="projectName">Name of the target project for the new file.</param>
        /// <param name="folderName">Name of the target folder for the new file.</param>
        /// <param name="fileProperties">File property settings in vs for the new File</param>
        public void StartNewFile(string name
            , string projectName = "", string folderName = "", OutputFileProperties fileProperties = null)
        {
            if (String.IsNullOrWhiteSpace(name) == true)
            {
                throw new ArgumentException("name");
            }

            CurrentBlock = new TextBlock(fileProperties)
            {
                Name = name,
                ProjectName = projectName,
                FolderName = folderName
            };
        }

        public void StartFooter()
        {
            CurrentBlock = footer;
        }

        public void StartHeader()
        {
            CurrentBlock = header;
        }

        public void EndBlock()
        {
            if (CurrentBlock == null)
            {
                return;
            }

            CurrentBlock.Length = _generationEnvironment.Length - CurrentBlock.Start;

            if (CurrentBlock != header && CurrentBlock != footer)
            {
                files.Add(CurrentBlock);
            }

            currentBlock = null;
        }

        /// <summary>
        /// Produce the template output files.
        /// </summary>
        public virtual IEnumerable<OutputFile> Process(bool split = true)
        {
            var list = new List<OutputFile>();

            if (split)
            {
                EndBlock();

                var headerText = _generationEnvironment.ToString(header.Start, header.Length);
                var footerText = _generationEnvironment.ToString(footer.Start, footer.Length);
                files.Reverse();

                foreach (var block in files)
                {
                    var outputPath = dte.GetOutputPath(block, Path.GetDirectoryName(_textTransformation.Host.TemplateFile));
                    var fileName = Path.Combine(outputPath, block.Name);
                    var content = this.ReplaceParameter(headerText, block) +
                    _generationEnvironment.ToString(block.Start, block.Length) +
                    footerText;

                    var file = new OutputFile(block.FileProperties)
                    {
                        FileName = fileName,
                        ProjectName = block.ProjectName,
                        FolderName = block.FolderName,
                        Content = content
                    };

                    CreateFile(file);
                    _generationEnvironment.Remove(block.Start, block.Length);

                    list.Add(file);
                }
            }

            projectSyncAction.EndInvoke(projectSyncAction.BeginInvoke(list, null, null));
            this.CleanUpTemplatePlaceholders();
            var items = this.dte.GetOutputFilesAsProjectItems(list);
            this.WriteVsProperties(items, list);

            if (this.IsAutoIndentEnabled == true && split == true)
            {
                this.FormatProjectItems(items);
            }

            this.WriteLog(list);

            return list;
        }

        private void FormatProjectItems(IEnumerable<EnvDTE.ProjectItem> items)
        {
            foreach (var item in items)
            {
                this._textTransformation.WriteLine(
                this.dte.ExecuteVsCommand(item, "Edit.FormatDocument")); //, "Edit.RemoveAndSort"));
                this._textTransformation.WriteLine("//-> " + item.Name);
            }
        }

        private void WriteVsProperties(IEnumerable<EnvDTE.ProjectItem> items, IEnumerable<OutputFile> outputFiles)
        {
            foreach (var file in outputFiles)
            {
                var item = items.Where(p => p.Name == Path.GetFileName(file.FileName)).FirstOrDefault();
                if (item == null) continue;

                if (String.IsNullOrEmpty(file.FileProperties.CustomTool) == false)
                {
                    item.SetPropertyValue("CustomTool", file.FileProperties.CustomTool);
                }

                if (String.IsNullOrEmpty(file.FileProperties.BuildActionString) == false)
                {
                    item.SetPropertyValue("ItemType", file.FileProperties.BuildActionString);
                }
            }
        }

        private string ReplaceParameter(string text, TextBlock block)
        {
            if (String.IsNullOrEmpty(text) == false)
            {
                text = text.Replace("$filename$", block.Name);
            }


            foreach (var item in block.FileProperties.TemplateParameter.AsEnumerable())
            {
                text = text.Replace(item.Key, item.Value);
            }

            return text;
        }

        /// <summary>
        /// Write log to the default output file.
        /// </summary>
        /// <param name="list"></param>
        private void WriteLog(IEnumerable<OutputFile> list)
        {
            this._textTransformation.WriteLine("// Generated helper templates");
            foreach (var item in templatePlaceholderList)
            {
                this._textTransformation.WriteLine("// " + this.GetDirectorySolutionRelative(item));
            }

            this._textTransformation.WriteLine("// Generated items");
            foreach (var item in list)
            {
                this._textTransformation.WriteLine("// " + this.GetDirectorySolutionRelative(item.FileName));
            }
        }

        /// <summary>
        /// Removes old template placeholders from the solution.
        /// </summary>
        private void CleanUpTemplatePlaceholders()
        {
            string[] activeTemplateFullNames = this.templatePlaceholderList.ToArray();
            string[] allHelperTemplateFullNames = this.dte.GetAllProjectItemsRecursive()
                .Where(p => p.Name == this.templateProjectItem.GetTemplatePlaceholderName())
                .Select(p => p.GetFullPath())
                .ToArray();

            var delta = allHelperTemplateFullNames.Except(activeTemplateFullNames).ToArray();

            var dirtyHelperTemplates = this.dte.GetAllProjectItemsRecursive()
                .Where(p => delta.Contains(p.GetFullPath()));

            foreach (ProjectItem item in dirtyHelperTemplates)
            {
                if (item.ProjectItems != null)
                {
                    foreach (ProjectItem subItem in item.ProjectItems)
                    {
                        subItem.Remove();
                    }
                }

                item.Remove();
            }
        }

        /// <summary>
        /// Gets a list of helper templates from the log.
        /// </summary>
        /// <returns>List of generated helper templates.</returns>
        private string[] GetPreviousTemplatePlaceholdersFromLog()
        {
            string path = Path.GetDirectoryName(this._textTransformation.Host.ResolvePath(this._textTransformation.Host.TemplateFile));
            string file1 = Path.GetFileNameWithoutExtension(this._textTransformation.Host.TemplateFile) + ".txt";
            string contentPrevious = File.ReadAllText(Path.Combine(path, file1));

            var result = contentPrevious
                  .Split(new[] { Environment.NewLine }, StringSplitOptions.RemoveEmptyEntries)
                  .Select(x => x.Split(new[] { "=>" }, StringSplitOptions.RemoveEmptyEntries).First())
                  .Select(x => Regex.Replace(x, "//", String.Empty).Trim())
                  .Where(x => x.EndsWith(this.templateProjectItem.GetTemplatePlaceholderName()))
                  .ToArray();

            return result;
        }

        private string GetDirectorySolutionRelative(string fullName)
        {
            int slnPos = fullName.IndexOf(Path.GetFileNameWithoutExtension(this.dte.Solution.FileName));
            if (slnPos < 0)
            {
                slnPos = 0;
            }

            return fullName.Substring(slnPos);
        }

        protected virtual void CreateFile(OutputFile file)
        {
            if (this.CanOverrideExistingFile == false && File.Exists(file.FileName) == true)
            {
                return;
            }

            if (IsFileContentDifferent(file))
            {
                CheckoutFileIfRequired(file.FileName);
                File.WriteAllText(file.FileName, file.Content, this.Encoding);
            }
        }

        protected bool IsFileContentDifferent(OutputFile file)
        {
            return !(File.Exists(file.FileName) && File.ReadAllText(file.FileName) == file.Content);
        }

        private TextBlock CurrentBlock
        {
            get { return currentBlock; }
            set
            {
                if (CurrentBlock != null)
                {
                    EndBlock();
                }

                if (value != null)
                {
                    value.Start = _generationEnvironment.Length;
                }

                currentBlock = value;
            }
        }

        private void ProjectSync(EnvDTE.ProjectItem templateProjectItem, IEnumerable<OutputFile> keepFileNames)
        {
            var groupedFileNames = from f in keepFileNames
                                   group f by new { f.ProjectName, f.FolderName }
                                       into l
                                       select new
                                       {
                                           ProjectName = l.Key.ProjectName,
                                           FolderName = l.Key.FolderName,
                                           FirstItem = l.First(),
                                           OutputFiles = l
                                       };

            this.templatePlaceholderList.Clear();

            foreach (var item in groupedFileNames)
            {
                EnvDTE.ProjectItem pi = templateProjectItem.DTE.GetTemplateProjectItem(item.FirstItem, templateProjectItem);
                ProjectSyncPart(pi, item.OutputFiles);

                if (pi.Name.EndsWith("txt4"))
                    this.templatePlaceholderList.Add(pi.GetFullPath());
            }

            // clean up
            bool hasDefaultItems = groupedFileNames.Where(f => String.IsNullOrEmpty(f.ProjectName) && String.IsNullOrEmpty(f.FolderName)).Count() > 0;
            if (hasDefaultItems == false)
            {
                ProjectSyncPart(templateProjectItem, new List<OutputFile>());
            }
        }

        private static void ProjectSyncPart(EnvDTE.ProjectItem templateProjectItem, IEnumerable<OutputFile> keepFileNames)
        {
            var keepFileNameSet = new HashSet<OutputFile>(keepFileNames);
            var projectFiles = new Dictionary<string, EnvDTE.ProjectItem>();
            var originalOutput = Path.GetFileNameWithoutExtension(templateProjectItem.FileNames[0]);

            foreach (EnvDTE.ProjectItem projectItem in templateProjectItem.ProjectItems)
            {
                projectFiles.Add(projectItem.FileNames[0], projectItem);
            }

            // Remove unused items from the project
            foreach (var pair in projectFiles)
            {
                bool isNotFound = keepFileNames.Where(f => f.FileName == pair.Key).Count() == 0;
                if (isNotFound == true
                    && !(Path.GetFileNameWithoutExtension(pair.Key) + ".").StartsWith(originalOutput + "."))
                {
                    pair.Value.Delete();
                }
            }

            // Add missing files to the project
            foreach (var fileName in keepFileNameSet)
            {
                if (!projectFiles.ContainsKey(fileName.FileName))
                {
                    templateProjectItem.ProjectItems.AddFromFile(fileName.FileName);
                }
            }
        }

        private void CheckoutFileIfRequired(string fileName)
        {
            if (dte.SourceControl == null
                || !dte.SourceControl.IsItemUnderSCC(fileName)
                    || dte.SourceControl.IsItemCheckedOut(fileName))
            {
                return;
            }

            // run on worker thread to prevent T4 calling back into VS
            checkOutAction.EndInvoke(checkOutAction.BeginInvoke(fileName, null, null));
        }
    }
}