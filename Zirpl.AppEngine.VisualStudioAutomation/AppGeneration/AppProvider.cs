using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using Zirpl.AppEngine.VisualStudioAutomation.AppGeneration.Config;
using Zirpl.AppEngine.VisualStudioAutomation.AppGeneration.Config.Parsers;
using Zirpl.AppEngine.VisualStudioAutomation.TextTemplating;

namespace Zirpl.AppEngine.VisualStudioAutomation.AppGeneration
{
    internal class AppProvider
    {
        internal App GetApp()
        {
            // set all of the settings defaults
            //
            var projectNamespacePrefix = TextTransformationContext.Instance.CallingTemplateProjectItem
                                                    .ContainingProject.GetDefaultNamespace()
                                                    .SubstringUntilLastInstanceOf(".");

            // create the app
            //
            var app = new App()
            {
                CodeGenerationProject = TextTransformationContext.Instance.CallingTemplateProjectItem.ContainingProject,
                ModelProject = TextTransformationContext.Instance.VisualStudio.Solution.GetProject(projectNamespacePrefix + ".Model"),
                DataServiceProject = TextTransformationContext.Instance.VisualStudio.Solution.GetProject(projectNamespacePrefix + ".DataService"),
                ServiceProject = TextTransformationContext.Instance.VisualStudio.Solution.GetProject(projectNamespacePrefix + ".Service"),
                WebCommonProject = TextTransformationContext.Instance.VisualStudio.Solution.GetProject(projectNamespacePrefix + ".Web.Common"),
                WebProject = TextTransformationContext.Instance.VisualStudio.Solution.GetProject(projectNamespacePrefix + ".Web"),
                TestsCommonProject = TextTransformationContext.Instance.VisualStudio.Solution.GetProject(projectNamespacePrefix + ".Tests.Common"),
                DataServiceTestsProject = TextTransformationContext.Instance.VisualStudio.Solution.GetProject(projectNamespacePrefix + ".Tests.DataService"),
                ServiceTestsProject = TextTransformationContext.Instance.VisualStudio.Solution.GetProject(projectNamespacePrefix + ".Tests.Service"),
            };

            // create all of the domain types
            //
            var projectItems = TextTransformationContext.Instance.VisualStudio.Solution.GetAllProjectItems();
            var paths = from p in projectItems
                        where p.GetFullPath().ToLowerInvariant().EndsWith(".domain.zae")
                            && p.GetFullPath().ToLowerInvariant().Contains("_config")
                        select p.GetFullPath();
            new DomainFileParser().ParseDomainTypes(app, paths);

            foreach (var domainType in app.DomainTypes.OrderBy(o => o.FullName))
            {
                TextTransformationContext.Instance.LogLineToBuildPane("Resulting DomainType: " + domainType.FullName);
            }
            return app;
        }
    }
}
