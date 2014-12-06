using System;
using System.Collections.Generic;
using System.IO;
using System.Linq;
using System.Runtime.InteropServices;
using System.Runtime.InteropServices.ComTypes;
using EnvDTE;
using EnvDTE80;

namespace Zirpl.AppEngine.VisualStudioAutomation
{
    public static class VisualStudioExtensions
    {
        #region ProjectItem extension methods

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

        #endregion

        #region ProjectItems extension methods

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

        private static ProjectItem GetOrCreateProjectItem(this ProjectItems items, string fullName) //, bool canCreateIfNotExists)
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

        private static ProjectItem GetOrCreateProjectFolder(this ProjectItems projectItems, string projectPath)
        {
            if (String.IsNullOrEmpty(projectPath))
            {
                projectPath = @"\";
            }
            projectPath = projectPath.Replace("/", @"\");
            projectPath = projectPath.Replace("//", @"\");
            projectPath = projectPath.Replace(@"\\", @"\");
            projectPath = projectPath.IndexOf(@"\") == 0 ? projectPath.Substring(1) : projectPath;
            projectPath = projectPath.IndexOf(@"\") == projectPath.Length - 1
                ? projectPath.Substring(0, projectPath.Length - 1)
                : projectPath;

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
                return GetOrCreateProjectFolder(folderProjectItem.ProjectItems, projectPath);
            }
            else
            {
                return folderProjectItem;
            }
        }

        #endregion

        #region Project extension methods

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

        public static String GetDefaultNamespace(this Project project)
        {
            return project.Properties.Item("DefaultNamespace").Value.ToString();
        }

        public static ProjectItem GetOrCreateProjectFolder(this Project project, String folderPath)
        {
            return GetOrCreateProjectFolder(project.ProjectItems, folderPath);
        }

        public static Guid? GetUniqueGuid(this Project proj)
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

        #endregion

        #region DTE2 extension methods

        public static ProjectItem GetProjectItem(this DTE2 dte, string fullName)
        {
            ProjectItem item = (from i in dte.GetAllProjectItemsRecursive()
                                where i.Name == Path.GetFileName(fullName)
                                select i).FirstOrDefault();
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

        #endregion






        public static object GetService(object serviceProvider, Type type)
        {
            return GetService(serviceProvider, type.GUID);
        }

        public static object GetService(object serviceProviderObject, Guid guid)
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
