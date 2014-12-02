using System;
using System.Collections.Generic;
using System.IO;
using System.Linq;
using System.Runtime.InteropServices;
using System.Runtime.InteropServices.ComTypes;
using EnvDTE;
using EnvDTE80;

namespace Zirpl.AppEngine.CodeGeneration.TextTemplating
{
    public static class VisualStudioExtensions
    {
        [DllImport("ole32.dll")]
        private static extern void CreateBindCtx(int reserved, out IBindCtx ppbc);
        [DllImport("ole32.dll")]
        private static extern void GetRunningObjectTable(int reserved, out IRunningObjectTable prot);
        public static DTE2 GetCurrentVisualStudioInstance()
        {
            // TODO: this method needs to handle case of multiple VS's being open
            // TODO: this method should  really be in another place
            // TODO: this method needs to handle other VS versions

            //rot entry for visual studio running under current process.

            var rotEntry = System.Diagnostics.Debugger.IsAttached 
                ? "!VisualStudio.DTE.12.0"
                : String.Format("!VisualStudio.DTE.12.0:{0}", System.Diagnostics.Process.GetCurrentProcess().Id);

            IRunningObjectTable rot;
            GetRunningObjectTable(0, out rot);
            IEnumMoniker enumMoniker;
            rot.EnumRunning(out enumMoniker);
            enumMoniker.Reset();
            IntPtr fetched = IntPtr.Zero;
            IMoniker[] moniker = new IMoniker[1];
            while (enumMoniker.Next(1, moniker, fetched) == 0)
            {
                IBindCtx bindCtx;
                CreateBindCtx(0, out bindCtx);
                string displayName;
                moniker[0].GetDisplayName(bindCtx, null, out displayName);

                var match = System.Diagnostics.Debugger.IsAttached
                    ? displayName.StartsWith(rotEntry)
                    : displayName == rotEntry;
                if (match)
                {
                    object comObject;
                    rot.GetObject(moniker[0], out comObject);
                    return (DTE2)comObject;
                }
            }
            return null;
        }

        /// <summary>
        /// Execute Visual VisualStudio commands against the project item.
        /// </summary>
        /// <param name="item">The current project item.</param>
        /// <param name="command">The vs command as string.</param>
        /// <returns>An error message if the command fails.</returns>
        public static string ExecuteVsCommand(this DTE2 dte, ProjectItem item, params string[] command)
        {
            if (item == null)
            {
                throw new ArgumentNullException("item");
            }

            string error = String.Empty;

            try
            {
                Window window = item.Open();
                window.Activate();

                foreach (var cmd in command)
                {
                    if (String.IsNullOrWhiteSpace(cmd) == true)
                    {
                        continue;
                    }

                    //EnvDTE80.DTE2 dte2 = dte as EnvDTE80.DTE2;
                    dte.ExecuteCommand(cmd, String.Empty);
                }

                item.Save();
                window.Visible = false;
                // window.Close(); // Ends VS, but not the tab :(
            }
            catch (Exception ex)
            {
                error = String.Format("Error processing file {0} {1}", item.Name, ex.Message);
            }

            return error;
        }

        public static IEnumerable<ProjectItem> GetOutputFilesAsProjectItems(this DTE2 dte, IEnumerable<OutputFile> outputFiles)
        {
            var fileNames = (from o in outputFiles
                             select Path.GetFileName(o.FileName)).ToArray();

            return dte.GetAllProjectItemsRecursive().Where(f => fileNames.Contains(f.Name));
        }

        /// <summary>
        /// Sets a property value for the vs project item.
        /// </summary>
        public static void SetPropertyValue(this ProjectItem item, string propertyName, object value)
        {
            Property property = item.Properties.Item(propertyName);
            if (property == null)
            {
                throw new ArgumentException(String.Format("The property {0} was not found.", propertyName));
            }
            else
            {
                property.Value = value;
            }
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
                //item = GetAllProjectItemsRecursive(prj.ProjectItems).Where(i=>i.Name == block.FolderName).First();
                item = EnsureProjectFolderExists(block.FolderName, prj.ProjectItems);
            }
            else if (String.IsNullOrEmpty(block.FolderName) == false)
            {
                //var items =GetAllProjectItemsRecursive(
                //	VisualStudio.ActiveDocument.ProjectItem.ContainingProject.ProjectItems).ToList();
                //item = items.Where(i=>i.Name == block.FolderName).First();
                item = EnsureProjectFolderExists(block.FolderName, dte.ActiveDocument.ProjectItem.ContainingProject.ProjectItems);
            }

            if (item != null)
            {
                return item.GetFullPath();
            }

            return defaultPath;
        }

        public static ProjectItem GetByName(this ProjectItems projectItems, string name)
        {
            foreach (ProjectItem innerProjectItem in projectItems)
            {
                if (innerProjectItem.Name == name)
                {
                    return innerProjectItem;
                }
            }
            return null;
        }

        public static String GetDefaultNamespace(this Project project)
        {
            return project.Properties.Item("DefaultNamespace").Value.ToString();
        }

        private static ProjectItem EnsureProjectFolderExists(string projectPath, ProjectItems projectItems, bool isFullPath = true)
        {
            projectPath = projectPath.Replace("/", @"\");
            projectPath = projectPath.Replace("//", @"\");
            projectPath = projectPath.Replace(@"\\", @"\");
            projectPath = projectPath.IndexOf(@"\") == 0 ? projectPath.Substring(1) : projectPath;
            projectPath = projectPath.IndexOf(@"\") == projectPath.Length - 1 ? projectPath.Substring(0, projectPath.Length - 1) : projectPath;

            if (isFullPath)
            {
                //WriteLineToBuildPane("Ensuring project path: " + projectPath);
            }

            var folderName = projectPath;
            if (projectPath.Contains(@"\"))
            {
                folderName = projectPath.Substring(0, projectPath.IndexOf(@"\"));
                projectPath = projectPath.Substring(folderName.Length);
            }
            else
            {
                projectPath = null;
            }
            var folderProjectItem = projectItems.GetByName(folderName);
            if (folderProjectItem == null)
            {
                //WriteLineToBuildPane("Creating folder: " + folderName);
                folderProjectItem = projectItems.AddFolder(folderName);
            }
            if (projectPath != null)
            {
                return EnsureProjectFolderExists(projectPath, folderProjectItem.ProjectItems, false);
            }
            else
            {
                return folderProjectItem;
            }
        }

        public static string GetTemplatePlaceholderName(this ProjectItem item)
        {
            return String.Format("{0}.txt4", Path.GetFileNameWithoutExtension(item.Name));
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
                return prj.ProjectItems.FindProjectItem(fullName);
            }
            else if (prj != null && String.IsNullOrEmpty(file.FolderName) == false)
            {
                //item = GetAllProjectItemsRecursive(prj.ProjectItems).Where(i=>i.Name == file.FolderName).First();
                item = EnsureProjectFolderExists(file.FolderName, prj.ProjectItems);
            }
            else if (String.IsNullOrEmpty(file.FolderName) == false)
            {
                //item = GetAllProjectItemsRecursive(
                //	VisualStudio.ActiveDocument.ProjectItem.ContainingProject.ProjectItems).
                //	Where(i=>i.Name == file.FolderName).First();
                item = EnsureProjectFolderExists(file.FolderName, dte.ActiveDocument.ProjectItem.ContainingProject.ProjectItems);
            }

            if (item != null)
            {
                return item.ProjectItems.FindProjectItem(fullName);
            }

            return defaultItem;
        }

        public static ProjectItem FindProjectItem(this DTE2 dte, string fullName)
        {
            ProjectItem item = (from i in dte.GetAllProjectItemsRecursive()
                                where i.Name == Path.GetFileName(fullName)
                                select i).FirstOrDefault();
            return item;
        }


        private static ProjectItem FindProjectItem(this ProjectItems items, string fullName) //, bool canCreateIfNotExists)
        {
            ProjectItem item = (from i in items.Cast<ProjectItem>()
                                       where i.Name == Path.GetFileName(fullName)
                                       select i).FirstOrDefault();
            if (item == null)
            {
                File.CreateText(fullName);
                item = items.AddFromFile(fullName);
            }

            return item;
        }

        public static Project GetProject(this DTE2 dte, string projectName)
        {
            return dte.GetAllProjects().FirstOrDefault(p => p.Name == projectName);
        }

        public static IEnumerable<Project> GetAllProjects(this DTE2 dte)
        {
            List<Project> projectList = new List<Project>();

            var folders = dte.Solution.Projects.Cast<Project>().Where(p => p.Kind == EnvDTE80.ProjectKinds.vsProjectKindSolutionFolder);

            foreach (Project folder in folders)
            {
                if (folder.ProjectItems == null) continue;

                foreach (ProjectItem item in folder.ProjectItems)
                {
                    if (item.Object is Project)
                        projectList.Add(item.Object as Project);
                }
            }

            var projects = dte.Solution.Projects.Cast<Project>().Where(p => p.Kind != EnvDTE80.ProjectKinds.vsProjectKindSolutionFolder);

            if (projects.Count() > 0)
                projectList.AddRange(projects);

            return projectList;
        }

        //public static ProjectItem GetProjectItemWithName(this ProjectItems items, string itemName)
        //{
        //    return GetAllProjectItemsRecursive(items).Cast<ProjectItem>().Where(i => i.Name == itemName).First();
        //}

        public static string GetFullPath(this ProjectItem item)
        {
            if (item != null
                && item.Properties != null
                && item.Properties.Item("FullPath") != null
                && item.Properties.Item("FullPath").Value != null)
            {
                return item.Properties.Item("FullPath").Value.ToString();
            }
            return "";
        }

        public static IEnumerable<ProjectItem> GetAllProjectItemsRecursive(this DTE2 dte)
        {
            List<ProjectItem> itemList = new List<ProjectItem>();

            foreach (Project item in dte.GetAllProjects())
            {
                if (item == null || item.ProjectItems == null) continue;

                itemList.AddRange(item.ProjectItems.GetAllProjectItemsRecursive());
            }

            return itemList;
        }

        public static IEnumerable<ProjectItem> GetAllProjectItemsRecursive(this ProjectItems projectItems)
        {
            foreach (ProjectItem projectItem in projectItems)
            {
                if (projectItem.ProjectItems == null) continue;

                foreach (ProjectItem subItem in projectItem.ProjectItems.GetAllProjectItemsRecursive())
                {
                    yield return subItem;
                }

                yield return projectItem;
            }
        }

        public static Guid? GetUniqueGuid(this EnvDTE.Project proj)
        {
            Guid? guid = null;
            object service = null;
            Microsoft.VisualStudio.Shell.Interop.IVsSolution solution = null;
            Microsoft.VisualStudio.Shell.Interop.IVsHierarchy hierarchy = null;
            Microsoft.VisualStudio.Shell.Interop.IVsAggregatableProject aggregatableProject = null;
            int result = 0;
            service = GetService(proj.DTE, typeof(Microsoft.VisualStudio.Shell.Interop.IVsSolution));
            solution = (Microsoft.VisualStudio.Shell.Interop.IVsSolution)service;

            result = solution.GetProjectOfUniqueName(proj.UniqueName, out hierarchy);

            if (result == 0)
            {
                Guid theGuid = default(Guid);
                solution.GetGuidOfProject(hierarchy, out theGuid);
                guid = (theGuid == Guid.Empty) ? null : new Nullable<Guid>(theGuid);
                //aggregatableProject = (Microsoft.VisualStudio.Shell.Interop.IVsAggregatableProject)hierarchy;
                //result = aggregatableProject.GetAggregateProjectTypeGuids(out projectTypeGuids);
                // return projectTypeGuids; (string)
            }

            return guid;
        }

        public static object GetService(object serviceProvider, System.Type type)
        {
            return GetService(serviceProvider, type.GUID);
        }

        public static object GetService(object serviceProviderObject, System.Guid guid)
        {
            object service = null;
            Microsoft.VisualStudio.OLE.Interop.IServiceProvider serviceProvider = null;
            IntPtr serviceIntPtr;
            int hr = 0;
            Guid SIDGuid;
            Guid IIDGuid;

            SIDGuid = guid;
            IIDGuid = SIDGuid;
            serviceProvider = (Microsoft.VisualStudio.OLE.Interop.IServiceProvider)serviceProviderObject;
            hr = serviceProvider.QueryService(ref SIDGuid, ref IIDGuid, out serviceIntPtr);

            if (hr != 0)
            {
                System.Runtime.InteropServices.Marshal.ThrowExceptionForHR(hr);
            }
            else if (!serviceIntPtr.Equals(IntPtr.Zero))
            {
                service = System.Runtime.InteropServices.Marshal.GetObjectForIUnknown(serviceIntPtr);
                System.Runtime.InteropServices.Marshal.Release(serviceIntPtr);
            }

            return service;
        }
    }
}
