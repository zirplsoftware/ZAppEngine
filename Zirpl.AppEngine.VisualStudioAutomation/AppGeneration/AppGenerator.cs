using System;
using System.Collections.Generic;
using System.Reflection;
using Zirpl.AppEngine.VisualStudioAutomation.AppGeneration.Parsing;
using Zirpl.AppEngine.VisualStudioAutomation.AppGeneration.TextTemplating;
using Zirpl.AppEngine.VisualStudioAutomation.TextTemplating;
using Zirpl.AppEngine.VisualStudioAutomation.Utilities;
using Zirpl.AppEngine.VisualStudioAutomation.VisualStudio.Logging;

namespace Zirpl.AppEngine.VisualStudioAutomation.AppGeneration
{
    public static class AppGenerator
    {
        public static void CopyDefaultTemplates(this ITransform transform)
        {
            new Action(() => new TemplateExporter().ExportDefaultTemplates(transform))
            .GetRunner()
            .OnError((e) => HandleException(transform, e))
            .OnComplete((passed) => transform.FileManager.EndFile())
            .Run();
        }

        public static void GenerateSolutionStructure(this ITransform transform)
        {
            new Action(() => new SolutionBuilder().GenerateProjects(transform))
            .GetRunner()
            .OnError((e) => HandleException(transform, e))
            .OnComplete((passed) => transform.FileManager.EndFile())
            .Run();
        }
        public static void GenerateApp(this ITransform transform, IDictionary<string, object> sessionParameters = null)
        {
            new Action(() => transform.GenerateApp(new TemplateProvider(transform), sessionParameters))
            .GetRunner()
            .OnError((e) => HandleException(transform, e))
            .OnComplete((passed) => transform.FileManager.EndFile())
            .Run();
        }

        public static void GenerateApp(this ITransform transform, String templateAssemblyName, IDictionary<string, object> sessionParameters = null)
        {
            new Action(() => transform.GenerateApp(new TemplateProvider(transform, new[] { templateAssemblyName }), sessionParameters))
            .GetRunner()
            .OnError((e) => HandleException(transform, e))
            .OnComplete((passed) => transform.FileManager.EndFile())
            .Run();
        }

        public static void GenerateApp(this ITransform transform, IEnumerable<string> templateAssemblyNames, IDictionary<string, object> sessionParameters = null)
        {
            new Action(() => transform.GenerateApp(new TemplateProvider(transform, templateAssemblyNames), sessionParameters))
            .GetRunner()
            .OnError((e) => HandleException(transform, e))
            .OnComplete((passed) => transform.FileManager.EndFile())
            .Run();
        }

        public static void GenerateApp(this ITransform transform, Assembly templateAssembly, IDictionary<string, object> sessionParameters = null)
        {
            new Action(() => transform.GenerateApp(new TemplateProvider(transform, new[] { templateAssembly }), sessionParameters))
            .GetRunner()
            .OnError((e) => HandleException(transform, e))
            .OnComplete((passed) => transform.FileManager.EndFile())
            .Run();
        }

        public static void GenerateApp(this ITransform transform, IEnumerable<Assembly> templateAssemblies, IDictionary<string, object> sessionParameters = null)
        {
            new Action(() => transform.GenerateApp(new TemplateProvider(transform, templateAssemblies), sessionParameters))
            .GetRunner()
            .OnError((e) => HandleException(transform, e))
            .OnComplete((passed) => transform.FileManager.EndFile())
            .Run();
        }

        public static void GenerateApp(this ITransform transform, ITemplateProvider templateProvider, IDictionary<string, object> sessionParameters = null)
        {
            new Action(() =>
            {
                var app = new AppProvider(transform).GetApp();
                transform.OutputInfoProvider = new VisualStudioAutomation.AppGeneration.TextTemplating.OutputInfoProvider();
                transform.RunTemplates(new AppGeneration.TextTemplating.TemplateRunner(app), templateProvider, sessionParameters);
                transform.FileManager.EndFile();
            })
            .GetRunner()
            .OnError((e) => HandleException(transform, e))
            .OnComplete((passed) => transform.FileManager.EndFile())
            .Run();
        }


        private static Exception HandleException(ITransform transform, Exception e)
        {
            try
            {
                LogManager.GetLog().Debug(e.ToString());
                transform.Host.HostTransform.GenerationEnvironment.Append(e);
            }
            // ReSharper disable once EmptyGeneralCatchClause
            catch
            {
                // eat this
            }
            return e;
        }
    }
}
    