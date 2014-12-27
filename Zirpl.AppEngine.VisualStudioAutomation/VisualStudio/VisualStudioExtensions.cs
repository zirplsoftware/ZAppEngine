using System;
using System.CodeDom.Compiler;
using System.Collections.Generic;
using System.IO;
using System.Linq;
using System.Reflection;
using System.Text;
using EnvDTE;
using EnvDTE80;
using Microsoft.CSharp;
using Zirpl.AppEngine.Logging;
using Zirpl.AppEngine.VisualStudioAutomation.TextTemplating;
using Zirpl.IO;
using Zirpl.Logging;

namespace Zirpl.AppEngine.VisualStudioAutomation.VisualStudio
{
    public static class VisualStudioExtensions
    {
        #region ProjectItem extension methods

        public static IEnumerable<ProjectItem> GetAllProjectItems(this ProjectItem projectItem)
        {
            var list = new List<ProjectItem>();
            GetAllProjectItems(list, projectItem.ProjectItems);
            return list;
        }

        public static ProjectItem GetProjectItem(this ProjectItem projectItem, string fullPath)
        {
            ProjectItem item = (from i in projectItem.GetAllProjectItems()
                                where PathUtilities.NormalizePath(i.GetFullPath()) == PathUtilities.NormalizePath(fullPath)
                                select i).FirstOrDefault();
            return item;
        }

        public static void SetPropertyValue(this ProjectItem projectItem, string propertyName, object value)
        {
            Property property = projectItem.Properties.Item(propertyName);

            if (property == null)
            {
                throw new ArgumentException(String.Format("The property {0} was not found.", propertyName));
            }
            else
            { 
                property.Value = value;
            }
        }

        public static T GetPropertyValue<T>(this ProjectItem projectItem, string propertyName)
        {
            Property property = projectItem.Properties.Item(propertyName);

            if (property == null)
            {
                throw new ArgumentException(String.Format("The property {0} was not found.", propertyName));
            }
            else
            {
                return (T)property.Value;
            }
        }

        public static string GetFullPath(this ProjectItem projectItem)
        {
            if (projectItem != null
                && projectItem.Properties != null
                && projectItem.Properties.Item("FullPath") != null
                && projectItem.Properties.Item("FullPath").Value != null)
            {
                return projectItem.Properties.Item("FullPath").Value.ToString();
            }
            return "";
        }

        public static bool IsPhysicalFile(this ProjectItem projectItem)
        {
            return projectItem.Kind == "{6BB5F8EE-4483-11D3-8BCF-00C04F8EC28C}";
        }

        public static bool IsPhysicalFolder(this ProjectItem projectItem)
        {
            return projectItem.Kind == "{6BB5F8EF-4483-11D3-8BCF-00C04F8EC28C}";
        }

        public static bool IsVirtualFolder(this ProjectItem projectItem)
        {
            return projectItem.Kind == "{6BB5F8F0-4483-11D3-8BCF-00C04F8EC28C}";
        }

        public static bool IsSubProject(this ProjectItem projectItem)
        {
            return projectItem.Kind == "{EA6618E8-6E24-4528-94BE-6889FE16485C}";
        }

        public static ProjectItemIndex GetIndex(this ProjectItem projectItem)
        {
            return new ProjectItemIndex(projectItem);
        }

        #endregion


        #region ProjectItems extension methods

        public static IEnumerable<ProjectItem> GetAllProjectItems(this ProjectItems projectItems)
        {
            var list = new List<ProjectItem>();
            GetAllProjectItems(list, projectItems);
            return list;
        }

        public static ProjectItem GetProjectItem(this ProjectItems projectItems, string fullPath)
        {
            ProjectItem item = (from i in projectItems.GetAllProjectItems()
                                where PathUtilities.NormalizePath(i.GetFullPath()) == PathUtilities.NormalizePath(fullPath)
                                select i).FirstOrDefault();
            return item;
        }

        public static ProjectItem GetChild(this ProjectItems projectItems, string name)
        {
            foreach (ProjectItem projectItem in projectItems)
            {
                if (projectItem.Name == name)
                {
                    return projectItem;
                }
            }
            return null;
        }

        public static ProjectItem GetOrCreateFolder(this ProjectItems projectItems, string relativeFolderPath, bool isNamespaceProvider = true)
        {
            if (String.IsNullOrEmpty(relativeFolderPath))
            {
                relativeFolderPath = @"\";
            }
            relativeFolderPath = relativeFolderPath.Replace("/", @"\");
            relativeFolderPath = relativeFolderPath.Replace("//", @"\");
            relativeFolderPath = relativeFolderPath.Replace(@"\\", @"\");
            relativeFolderPath = relativeFolderPath.IndexOf(@"\") == 0 ? relativeFolderPath.Substring(1) : relativeFolderPath;
            relativeFolderPath = relativeFolderPath.IndexOf(@"\") == relativeFolderPath.Length - 1
                ? relativeFolderPath.Substring(0, relativeFolderPath.Length - 1)
                : relativeFolderPath;

            var folderName = relativeFolderPath;
            if (relativeFolderPath.Contains(@"\"))
            {
                folderName = relativeFolderPath.Substring(0, relativeFolderPath.IndexOf(@"\"));
                relativeFolderPath = relativeFolderPath.Substring(folderName.Length);
            }
            else
            {
                relativeFolderPath = null;
            }
            var folderProjectItem = projectItems.GetChild(folderName);
            if (folderProjectItem == null)
            {
                folderProjectItem = projectItems.AddFolder(folderName);
                if (!isNamespaceProvider)
                {
                    // this functionality is ONLY specific to Resharper, but lots of people use it (including me)
                    // and it is REALLY nice to turn off namespace providers for these auto folders

                    List<String> lines = null;
                    if (File.Exists(folderProjectItem.GetFullPath() + ".DotSettings"))
                    {
                        lines = File.ReadLines(folderProjectItem.ContainingProject.FullName + ".DotSettings").ToList();
                    }
                    else
                    {
                        lines = new List<string>();
                        lines.Add("<wpf:ResourceDictionary xml:space=\"preserve\" xmlns:x=\"http://schemas.microsoft.com/winfx/2006/xaml\" xmlns:s=\"clr-namespace:System;assembly=mscorlib\" xmlns:ss=\"urn:shemas-jetbrains-com:settings-storage-xaml\" xmlns:wpf=\"http://schemas.microsoft.com/winfx/2006/xaml/presentation\">");
                        lines.Add("</wpf:ResourceDictionary>");
                    }
                    // TODO: this could add infinite lines... see if there is a way to do this through Resharper, IF it is installed
                    lines.Insert(1, String.Format("<s:Boolean x:Key=\"/Default/CodeInspection/NamespaceProvider/NamespaceFoldersToSkip/={0}/@EntryIndexedValue\">True</s:Boolean>",
                            folderName.Replace("_", "_005F")));
                    File.WriteAllLines(folderProjectItem.ContainingProject.FullName + ".DotSettings", lines);
                }
            }
            if (relativeFolderPath != null)
            {
                return GetOrCreateFolder(folderProjectItem.ProjectItems, relativeFolderPath, isNamespaceProvider);
            }
            else
            {
                return folderProjectItem;
            }
        }

        public static IEnumerable<ProjectItem> ToEnumerable(this ProjectItems projectItems)
        {
            var list = new List<ProjectItem>();
            if (projectItems != null)
            {
                foreach (ProjectItem item in projectItems)
                {
                    list.Add(item);
                }
            }
            return list;
        }
        
        #endregion


        #region Project extension methods

        public static void TryLogAllProperties(this Project project)
        {
            if (project.Properties != null)
            {
                foreach (Property property in project.Properties)
                {
                    try
                    {
                        LogManager.GetLog().DebugFormat("Property {0}: {1}", property.Name, property.Value);
                    }
                    catch (Exception ex)
                    {
                        LogManager.GetLog().TryError(ex);
                    }
                }
            }
        }

        public static String GetFolderPathFromNamespace(this Project project, String nameSpace)
        {
            String folderPath = nameSpace;
            folderPath = folderPath.SubstringAfterFirstInstanceOf(project.GetDefaultNamespace() + ".");
            folderPath = folderPath.Replace('.', '\\');
            return folderPath;
        }

        public static IEnumerable<ProjectItem> GetAllProjectItems(this Project project)
        {
            var list = new List<ProjectItem>();
            GetAllProjectItems(list, project.ProjectItems);
            return list;
        }

        public static ProjectItem GetProjectItem(this Project project, string fullPath)
        {
            ProjectItem item = (from i in project.GetAllProjectItems()
                                where PathUtilities.NormalizePath(i.GetFullPath()) == PathUtilities.NormalizePath(fullPath)
                                select i).FirstOrDefault();
            return item;
        }

        public static String GetDefaultNamespace(this Project project)
        {
            return project.Properties.Item("DefaultNamespace").Value.ToString();
        }

        public static ProjectItem GetOrCreateFolder(this Project project, String relativeFolderPath, bool isNamespaceProvider = true)
        {
            return GetOrCreateFolder(project.ProjectItems, relativeFolderPath, isNamespaceProvider);
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

        public static Assembly CompileCSharpProjectInMemory(this Project project, bool suppressCompilerErrors = false)
        {
            var codeList = new List<String>();
            foreach (var item in project.GetAllProjectItems())
            {
                if (item.GetFullPath().EndsWith(".cs"))
                {
                    codeList.Add(File.ReadAllText(item.GetFullPath()));
                }
            }

            CSharpCodeProvider provider = new CSharpCodeProvider();
            CompilerParameters parameters = new CompilerParameters();
            // True - memory generation, false - external file generation
            parameters.GenerateInMemory = true;
            // True - exe file generation, false - dll file generation
            parameters.GenerateExecutable = false;
            parameters.ReferencedAssemblies.Clear();

            var vsproject = (VSLangProj.VSProject)project.Object;
            // note: you could also try casting to VsWebSite.VSWebSite

            foreach (VSLangProj.Reference reference in vsproject.References)
            {
                if (reference.Name != "mscorlib")
                {
                    parameters.ReferencedAssemblies.Add(reference.Path);
                }
                //string.Format("{0}, Version={1}.{2}.{3}.{4}, Culture={5}, PublicKeyToken={6}",
                //                    reference.Name,
                //                    reference.MajorVersion, reference.MinorVersion, reference.BuildNumber, reference.RevisionNumber,
                //                    reference.Culture.Or("neutral"),
                //                    reference.PublicKeyToken.Or("null"));
                //if (reference.SourceProject == null)
                //{
                //}
                //else
                //{
                //    // This is a project reference
                //}
            }

            CompilerResults results = provider.CompileAssemblyFromSource(parameters, codeList.ToArray());
            if (results.Errors.HasErrors
                && !suppressCompilerErrors)
            {
                StringBuilder sb = new StringBuilder();

                foreach (CompilerError error in results.Errors)
                {
                    sb.AppendLine(String.Format("Error ({0}): {1}", error.ErrorNumber, error.ErrorText));
                }

                throw new InvalidOperationException(sb.ToString());
            }
            return results.CompiledAssembly;
        }

        public static ProjectIndex GetIndex(this Project project)
        {
            return new ProjectIndex(project);
        }

        #endregion


        #region Solution methods

        public static IEnumerable<ProjectItem> GetAllProjectItems(this Solution solution)
        {
            var list = new List<ProjectItem>();
            foreach (Project project in solution.GetAllProjects())
            {
                GetAllProjectItems(list, project.ProjectItems);
            }
            return list;
        }

        public static ProjectItem GetProjectItem(this Solution solution, string fullPath)
        {
            ProjectItem item = (from i in solution.GetAllProjectItems()
                                where PathUtilities.NormalizePath(i.GetFullPath()) == PathUtilities.NormalizePath(fullPath)
                                select i).FirstOrDefault();
            return item;
        }
        public static ProjectItem GetProjectItem(this ProjectItemIndex index)
        {
            return index.ProjectItem;
        }

        public static IEnumerable<Project> GetAllProjects(this Solution solution)
        {
            List<Project> list = new List<Project>();
            var item = solution.Projects.GetEnumerator();
            while (item.MoveNext())
            {
                var project = item.Current as Project;
                if (project == null)
                {
                    continue;
                }

                if (project.Kind == ProjectKinds.vsProjectKindSolutionFolder)
                {
                    list.AddRange(GetSolutionFolderProjects(project));
                }
                else
                {
                    list.Add(project);
                }
            }

            return list;
        }

        public static Project GetProject(this Solution solution, string projectName)
        {
            return solution.GetAllProjects().FirstOrDefault(p => p.Name == projectName);
        }
        public static Project GetProject(this ProjectIndex index)
        {
            return index.Project;
        }

        #endregion


        #region DTE2 extension methods



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
                window.Visible = false;
                
                //window.Activate();

                foreach (var cmd in command)
                {
                    if (String.IsNullOrWhiteSpace(cmd))
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


        #region Helper methods
        private static IEnumerable<Project> GetSolutionFolderProjects(Project solutionFolder)
        {
            var list = new List<Project>();
            for (var i = 1; i <= solutionFolder.ProjectItems.Count; i++)
            {
                var subProject = solutionFolder.ProjectItems.Item(i).SubProject;
                if (subProject == null)
                {
                    continue;
                }

                // If this is another solution folder, do a recursive call, otherwise add
                if (subProject.Kind == ProjectKinds.vsProjectKindSolutionFolder)
                {
                    list.AddRange(GetSolutionFolderProjects(subProject));
                }
                else
                {
                    list.Add(subProject);
                }
            }
            return list;
        }

        private static void GetAllProjectItems(IList<ProjectItem> list, ProjectItems projectItems)
        {
            foreach (ProjectItem projectItem in projectItems)
            {
                list.Add(projectItem);
                if (projectItem.ProjectItems != null)
                {
                    GetAllProjectItems(list, projectItem.ProjectItems);
                }
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
