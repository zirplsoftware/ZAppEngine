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
using Zirpl.AppEngine.VisualStudioAutomation.TextTemplating;
using Zirpl.AppEngine.VisualStudioAutomation.VisualStudio;

namespace Zirpl.AppEngine.VisualStudioAutomation.AppGeneration
{
    internal sealed class AppProvider
    {
        private readonly DTE2 _visualStudio;
        private readonly Project _callingTemplateProject;

        internal AppProvider(ITransform transform)
        {
            if (transform == null) throw new ArgumentNullException("transform");

            this._visualStudio = transform.GetDTE();
            this._callingTemplateProject = transform.Host.GetProjectItem().ContainingProject;
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
                ModelProject = _visualStudio.Solution.GetProject(projectNamespacePrefix + ".Model"),
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
    }
}
