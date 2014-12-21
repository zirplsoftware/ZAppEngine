using System;
using System.Collections.Generic;
using System.IO;
using System.Linq;
using System.Runtime.InteropServices;
using System.Runtime.InteropServices.ComTypes;
using System.Text;
using EnvDTE;
using EnvDTE80;
using Zirpl.AppEngine.CodeGeneration.TextTemplating;

namespace Zirpl.AppEngine.CodeGeneration.VisualStudioAutomation
{
    public class OutputFileManagerOld
    {
        private readonly Action<string> checkOutAction;
        private readonly Action<IEnumerable<OutputFile>> projectSyncAction;
        private readonly List<string> templatePlaceholderList = new List<string>();
        private readonly List<TextBlock> files = new List<TextBlock>();
        //private readonly TextBlock footer = new TextBlock();
        //private readonly TextBlock header = new TextBlock();
        private TextBlock currentBlock;

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
        public Encoding Encoding { get; set; }


        /// <summary>
        /// Initializes an TemplateFileManager Instance  with the
        /// TextTransformation (T4 generated class) that is currently running
        /// </summary>
        public OutputFileManager()
        {
            this.CanOverrideExistingFile = true;
            this.IsAutoIndentEnabled = false;
            this.Encoding = System.Text.Encoding.UTF8;
            this.checkOutAction = fileName => TextTransformationSession.Instance.VisualStudio.SourceControl.CheckOutItem(fileName);
            this.projectSyncAction = keepFileNames => ProjectSync(TextTransformationSession.Instance.TemplateProjectItem, keepFileNames);
        }

        /// <summary>
        /// Marks the end of the last file if there was one, and starts a new
        /// and marks this point in generation as a new file.
        /// </summary>
        /// <param name="name">Filename</param>
        /// <param name="projectName">ClassName of the target project for the new file.</param>
        /// <param name="folderName">ClassName of the target folder for the new file.</param>
        /// <param name="fileProperties">File property settings in vs for the new File</param>
        public void StartNewFile(string name, string projectName = "", string folderName = "", OutputFileProperties fileProperties = null)
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

        /// <summary>
        /// Marks the end of the last file if there was one, and starts a new
        /// and marks this point in generation as a new file.
        /// </summary>
        /// <param name="name">Filename</param>
        /// <param name="projectName">ClassName of the target project for the new file.</param>
        /// <param name="folderName">ClassName of the target folder for the new file.</param>
        /// <param name="fileProperties">File property settings in vs for the new File</param>
        public void StartNewFile(string name, Project project, string folderName = "", OutputFileProperties fileProperties = null)
        {
            this.StartNewFile(name, project.Name, folderName, fileProperties);
        }

        //public void StartFooter()
        //{
        //    CurrentBlock = footer;
        //}

        //public void StartHeader()
        //{
        //    CurrentBlock = header;
        //}

        public void EndBlock()
        {
            if (CurrentBlock == null)
            {
                return;
            }

            CurrentBlock.Length = TextTransformationSession.Instance.CallingTemplate.GenerationEnvironment.Length - CurrentBlock.Start;

            //if (CurrentBlock != header && CurrentBlock != footer)
            //{
            files.Add(CurrentBlock);
            //}

            currentBlock = null;
        }

        /// <summary>
        /// Produce the template output files.
        /// </summary>
        public virtual IEnumerable<OutputFile> Finish(bool split = true)
        {
            var list = new List<OutputFile>();

            if (split)
            {
                EndBlock();

                //var headerText = TextTransformationSession.Instance.CallingTemplate.GenerationEnvironment.ToString(header.Start, header.Length);
                //var footerText = TextTransformationSession.Instance.CallingTemplate.GenerationEnvironment.ToString(footer.Start, footer.Length);
                files.Reverse();

                foreach (var block in files)
                {
                    var outputPath = TextTransformationSession.Instance.VisualStudio.GetOutputPath(block, Path.GetDirectoryName(TextTransformationSession.Instance.CallingTemplate.Host.TemplateFile));
                    var fileName = Path.Combine(outputPath, block.Name);
                    //var content = this.ReplaceParameter(headerText, block) + TextTransformationSession.Instance.CallingTemplate.GenerationEnvironment.ToString(block.Start, block.Length) + footerText;
                    var content = TextTransformationSession.Instance.CallingTemplate.GenerationEnvironment.ToString(block.Start, block.Length);

                    var file = new OutputFile(block.FileProperties)
                    {
                        FileName = fileName,
                        ProjectName = block.ProjectName,
                        FolderName = block.FolderName,
                        Content = content
                    };

                    CreateFile(file);
                    TextTransformationSession.Instance.CallingTemplate.GenerationEnvironment.Remove(block.Start, block.Length);

                    list.Add(file);
                }
            }

            projectSyncAction.EndInvoke(projectSyncAction.BeginInvoke(list, null, null));
            this.CleanUpTemplatePlaceholders();
            var items = TextTransformationSession.Instance.VisualStudio.GetOutputFilesAsProjectItems(list);
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
                TextTransformationSession.Instance.CallingTemplate.WriteLine(
                TextTransformationSession.Instance.VisualStudio.ExecuteVsCommand(item, "Edit.FormatDocument")); //, "Edit.RemoveAndSort"));
                TextTransformationSession.Instance.CallingTemplate.WriteLine("//-> " + item.Name);
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
            TextTransformationSession.Instance.CallingTemplate.WriteLine("// Generated helper templates");
            foreach (var item in templatePlaceholderList)
            {
                TextTransformationSession.Instance.CallingTemplate.WriteLine("// " + this.GetDirectorySolutionRelative(item));
            }

            TextTransformationSession.Instance.CallingTemplate.WriteLine("// Generated items");
            foreach (var item in list)
            {
                TextTransformationSession.Instance.CallingTemplate.WriteLine("// " + this.GetDirectorySolutionRelative(item.FileName));
            }
        }

        /// <summary>
        /// Removes old template placeholders from the solution.
        /// </summary>
        private void CleanUpTemplatePlaceholders()
        {
            string[] activeTemplateFullNames = this.templatePlaceholderList.ToArray();
            string[] allHelperTemplateFullNames = TextTransformationSession.Instance.VisualStudio.GetAllProjectItemsRecursive()
                .Where(p => p.Name == TextTransformationSession.Instance.TemplateProjectItem.GetTemplatePlaceholderName())
                .Select(p => p.GetFullPath())
                .ToArray();

            var delta = allHelperTemplateFullNames.Except(activeTemplateFullNames).ToArray();

            var dirtyHelperTemplates = TextTransformationSession.Instance.VisualStudio.GetAllProjectItemsRecursive()
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

        private string GetDirectorySolutionRelative(string fullName)
        {
            int slnPos = fullName.IndexOf(Path.GetFileNameWithoutExtension(TextTransformationSession.Instance.VisualStudio.Solution.FileName));
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
                    value.Start = TextTransformationSession.Instance.CallingTemplate.GenerationEnvironment.Length;
                }

                currentBlock = value;
            }
        }

        private void ProjectSync(ProjectItem templateProjectItem, IEnumerable<OutputFile> keepFileNames)
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
                EnvDTE.ProjectItem pi = ((DTE2)templateProjectItem.DTE).GetTemplateProjectItem(item.FirstItem, templateProjectItem);
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

        private static void ProjectSyncPart(ProjectItem templateProjectItem, IEnumerable<OutputFile> keepFileNames)
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
            if (TextTransformationSession.Instance.VisualStudio.SourceControl == null
                || !TextTransformationSession.Instance.VisualStudio.SourceControl.IsItemUnderSCC(fileName)
                    || TextTransformationSession.Instance.VisualStudio.SourceControl.IsItemCheckedOut(fileName))
            {
                return;
            }

            // run on worker thread to prevent T4 calling back into VS
            checkOutAction.EndInvoke(checkOutAction.BeginInvoke(fileName, null, null));
        }








        public string GetTemplatePlaceholderName(ProjectItem item)
        {
            return String.Format("{0}.txt4", Path.GetFileNameWithoutExtension(item.Name));
        }


        public static IEnumerable<ProjectItem> GetOutputFilesAsProjectItems(this DTE2 dte, IEnumerable<OutputFile> outputFiles)
        {
            var fileNames = (from o in outputFiles
                select Path.GetFileName(o.FileName)).ToArray();

            return dte.GetAllProjectItemsRecursive().Where(f => fileNames.Contains(f.Name));
        }


        public static ProjectItem GetTemplateProjectItem(this DTE2 dte, OutputFile file, ProjectItem defaultItem)
        {
            if (String.IsNullOrEmpty(file.ProjectName) == true && String.IsNullOrEmpty(file.FolderName) == true)
            {
                return defaultItem;
            }

            string templatePlaceholder = defaultItem.GetTemplatePlaceholderName();
            string itemPath = Path.GetDirectoryName(file.FileName);
            string fullName = Path.Combine(itemPath, templatePlaceholder);
            Project prj = null;
            ProjectItem item = null;

            if (String.IsNullOrEmpty(file.ProjectName) == false)
            {
                prj = dte.GetProject(file.ProjectName);
            }

            if (String.IsNullOrEmpty(file.FolderName) == true && prj != null)
            {
                return prj.ProjectItems.GetProjectItem(fullName);
            }
            else if (prj != null && String.IsNullOrEmpty(file.FolderName) == false)
            {
                //item = GetAllProjectItemsRecursive(prj.ProjectItems).Where(i=>i.ClassName == file.FolderName).First();
                item = EnsureProjectFolderExists(file.FolderName, prj.ProjectItems);
            }
            else if (String.IsNullOrEmpty(file.FolderName) == false)
            {
                //item = GetAllProjectItemsRecursive(
                //	VisualStudio.ActiveDocument.ProjectItem.ContainingProject.ProjectItems).
                //	Where(i=>i.ClassName == file.FolderName).First();
                item = EnsureProjectFolderExists(file.FolderName, dte.ActiveDocument.ProjectItem.ContainingProject.ProjectItems);
            }

            if (item != null)
            {
                return item.ProjectItems.GetProjectItem(fullName);
            }

            return defaultItem;
        }


        public static string GetOutputPath(this DTE2 dte, TextBlock block, string defaultPath)
        {
            if (String.IsNullOrEmpty(block.ProjectName) == true && String.IsNullOrEmpty(block.FolderName) == true)
            {
                return defaultPath;
            }

            Project prj = null;
            ProjectItem item = null;

            if (String.IsNullOrEmpty(block.ProjectName) == false)
            {
                prj = dte.GetProject(block.ProjectName);
            }

            if (String.IsNullOrEmpty(block.FolderName) == true && prj != null)
            {
                return Path.GetDirectoryName(prj.FullName);
            }
            else if (prj != null && String.IsNullOrEmpty(block.FolderName) == false)
            {
                //item = GetAllProjectItemsRecursive(prj.ProjectItems).Where(i=>i.ClassName == block.FolderName).First();
                item = EnsureProjectFolderExists(block.FolderName, prj.ProjectItems);
            }
            else if (String.IsNullOrEmpty(block.FolderName) == false)
            {
                //var items =GetAllProjectItemsRecursive(
                //	VisualStudio.ActiveDocument.ProjectItem.ContainingProject.ProjectItems).ToList();
                //item = items.Where(i=>i.ClassName == block.FolderName).First();
                item = EnsureProjectFolderExists(block.FolderName, dte.ActiveDocument.ProjectItem.ContainingProject.ProjectItems);
            }

            if (item != null)
            {
                return item.GetFullPath();
            }

            return defaultPath;
        }
    }
}
