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
using Zirpl.AppEngine.VisualStudioAutomation.VisualStudio;

namespace Zirpl.AppEngine.VisualStudioAutomation.AppGeneration.VisualStudio
{
    public class SolutionBuilder
    {
        public void GenerateProjects(TextTransformation textTransformation)
        {
            
            var projectNamespacePrefix = textTransformation
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
                if (textTransformation.GetDTE().Solution.GetProject(projectName) == null)
                {
                    this.CreateProject(textTransformation, projectName, projectName.EndsWith(".Web"));
                }
            }
        }

        private Project CreateProject(TextTransformation textTransformation, String projectName, bool isWeb)
        {
            var csTemplatePath = ((Solution2)textTransformation.GetDTE().Solution).GetProjectTemplate("ClassLibrary.zip", "CSharp"); // "vbproj"
            var folder = Path.Combine(Path.GetDirectoryName(textTransformation.GetDTE().Solution.FullName), projectName);
            LogManager.GetLog().DebugFormat("Creating new project: {0} at {1}", projectName, folder);

            // the project returned by this line is null for some strange reason
            //
            textTransformation.GetDTE().Solution.AddFromTemplate(csTemplatePath, Path.Combine(folder, projectName), projectName, false);

            // and this seems to return an error "Project Unavailable"
            //
            //var project = _visualStudio.Solution.GetProject(projectName);
            // 
            // so we use this instead, which works
            //
            var project = (Project)((Array)(textTransformation.GetDTE().ActiveSolutionProjects)).GetValue(0);
            project.Save();
            //LogManager.GetLog().Debug(project.FullName);
            project.Properties.Item("TargetFrameworkMoniker").Value = textTransformation.GetProjectItem().ContainingProject.Properties.Item("TargetFrameworkMoniker");
            //project.Properties.Item("TargetFrameworkMoniker").Value = new FrameworkName(".NETFramework", new Version(4, 5, 1)).FullName;
            //textTransformation.GetProjectItem().ContainingProject.ParentProjectItem.Collection.project
            //project.Save();
            return project;
        }
    }
}
