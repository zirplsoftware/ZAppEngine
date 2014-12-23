using System;
using System.Collections.Generic;
using System.IO;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using EnvDTE;
using EnvDTE80;
using Microsoft.VisualStudio.TextTemplating;
using Zirpl.AppEngine.Logging;
using Zirpl.AppEngine.VisualStudioAutomation.TextTemplating;
using Zirpl.AppEngine.VisualStudioAutomation.VisualStudio;

namespace Zirpl.AppEngine.VisualStudioAutomation.AppGeneration.VisualStudio
{
    internal sealed class SolutionBuilder
    {
        internal void GenerateProjects(ITransform transform)
        {
            if (transform == null) throw new ArgumentNullException("transform");

            var projectNamespacePrefix = transform.Host
                .GetProjectItem()
                .ContainingProject
                .GetDefaultNamespace()
                .SubstringThroughLastInstanceOf(".");

            var projectSuffixes = new String[]
            {
                "Model",
                "DataService",
                "Service",
                "Web.Common",
                "Testing",
                "Model.Tests",
                "DataService.Tests",
                "Service.Tests",
                "Web.Common.Tests"
                //"Web",
                //"Web.Tests",
            };

            foreach (var suffix in projectSuffixes)
            {
                var projectName = projectNamespacePrefix + suffix;
                if (transform.GetDTE().Solution.GetProject(projectName) == null)
                {
                    this.CreateProject(transform, projectName);
                }
            }
        }

        private Project CreateProject(ITransform transform, String projectName)
        {
            var csTemplatePath = ((Solution2)transform.GetDTE().Solution).GetProjectTemplate("ClassLibrary.zip", "CSharp"); // "vbproj"
            var folder = Path.Combine(Path.GetDirectoryName(transform.GetDTE().Solution.FullName), projectName);
            LogManager.GetLog().DebugFormat("Creating new project: {0} at {1}", projectName, folder);

            // the project returned by this line is null for some strange reason
            //
            transform.GetDTE().Solution.AddFromTemplate(csTemplatePath, Path.Combine(folder, projectName), projectName, false);

            // and this seems to return an error "Project Unavailable"
            //
            //var project = _visualStudio.Solution.GetProject(projectName);
            // 
            // so we use this instead, which works
            //
            var project = (Project)((Array)(transform.GetDTE().ActiveSolutionProjects)).GetValue(0);
            project.Save();
            //LogManager.GetLog().Debug(project.FullName);
            project.Properties.Item("TargetFrameworkMoniker").Value = transform.Host.GetProjectItem().ContainingProject.Properties.Item("TargetFrameworkMoniker");
            //project.Properties.Item("TargetFrameworkMoniker").Value = new FrameworkName(".NETFramework", new Version(4, 5, 1)).FullName;
            //textTransformation.GetProjectItem().ContainingProject.ParentProjectItem.Collection.project
            //project.Save();
            return project;
        }
    }
}
