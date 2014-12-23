using System;
using System.IO;
using System.Linq;
using System.Runtime.Versioning;
using EnvDTE;
using EnvDTE80;
using Microsoft.VisualStudio.TextTemplating;
using Zirpl.AppEngine.Logging;
using Zirpl.AppEngine.VisualStudioAutomation.AppGeneration.Config;
using Zirpl.AppEngine.VisualStudioAutomation.AppGeneration.Config.Parsers;
using Zirpl.AppEngine.VisualStudioAutomation.VisualStudio;

namespace Zirpl.AppEngine.VisualStudioAutomation.AppGeneration
{
    internal class AppProvider
    {
        private readonly DTE2 _visualStudio;
        private readonly Project _callingTemplateProject;

        internal AppProvider(TextTransformation callingTemplate)
        {
            this._visualStudio = callingTemplate.GetDTE();
            this._callingTemplateProject = callingTemplate.GetProjectItem().ContainingProject;
        }

        internal App GetApp()
        {
            // set all of the settings defaults
            //
            var projectNamespacePrefix = this._callingTemplateProject.GetDefaultNamespace()
                                                    .SubstringUntilLastInstanceOf(".");

            // create the app
            //
            var app = new App()
            {
                CodeGenerationProject = this._callingTemplateProject,
                ModelProject = _visualStudio.Solution.GetProject(projectNamespacePrefix + ".Model") ?? this.CreateProject(projectNamespacePrefix + ".Model"),
                DataServiceProject = _visualStudio.Solution.GetProject(projectNamespacePrefix + ".DataService"),
                ServiceProject = _visualStudio.Solution.GetProject(projectNamespacePrefix + ".Service"),
                WebCommonProject = _visualStudio.Solution.GetProject(projectNamespacePrefix + ".Web.Common"),
                WebProject = _visualStudio.Solution.GetProject(projectNamespacePrefix + ".Web"),
                TestsCommonProject = _visualStudio.Solution.GetProject(projectNamespacePrefix + ".Tests.Common"),
                DataServiceTestsProject = _visualStudio.Solution.GetProject(projectNamespacePrefix + ".Tests.DataService"),
                ServiceTestsProject = _visualStudio.Solution.GetProject(projectNamespacePrefix + ".Tests.Service"),
            };

            // create all of the domain types
            //
            var projectItems = _visualStudio.Solution.GetAllProjectItems();
            var paths = from p in projectItems
                        where p.GetFullPath().ToLowerInvariant().EndsWith(".domain.zae")
                            && p.GetFullPath().ToLowerInvariant().Contains("_config")
                        select p.GetFullPath();
            new DomainFileParser().ParseDomainTypes(app, paths);

            foreach (var domainType in app.DomainTypes.OrderBy(o => o.FullName))
            {
                this.GetLog().Debug("Resulting DomainType: " + domainType.FullName);
            }
            return app;
        }

        private Project CreateProject(String projectName)
        {
            var csTemplatePath = ((Solution2)_visualStudio.Solution).GetProjectTemplate("ClassLibrary.zip", "CSharp"); // "vbproj"
            //LogManager.GetLog().Debug("C# template path: " + csTemplatePath);
            var folder = Path.GetDirectoryName(_visualStudio.Solution.FullName);
            LogManager.GetLog().Debug("Solution folder: " + folder);
            //_visualStudio.Solution.AddFromTemplate(csTemplatePath, Path.Combine(folder, projectName), projectName, false);
            //var project = (Project)((Array)(_visualStudio.ActiveSolutionProjects)).GetValue(0);
            var project = _visualStudio.Solution.GetProject(projectName);
            //project.Properties.Item("TargetFrameworkMoniker").Value = _callingTemplateProject.Properties.Item("TargetFrameworkMoniker");
            project.LogAllProperties();
            project.Properties.Item("TargetFrameworkMoniker").Value = new FrameworkName(".NETFramework", new Version(4, 5, 1)).FullName;
            return project;
        }
    }
}
