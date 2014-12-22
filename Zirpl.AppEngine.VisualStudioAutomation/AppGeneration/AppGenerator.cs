using System;
using System.CodeDom.Compiler;
using System.Collections.Generic;
using System.IO;
using System.Linq;
using System.Reflection;
using System.Text;
using EnvDTE;
using Microsoft.CSharp;
using Microsoft.VisualStudio.OLE.Interop;
using Microsoft.VisualStudio.PlatformUI;
using Microsoft.VisualStudio.TextTemplating;
using Newtonsoft.Json;
using Zirpl.AppEngine.Logging;
using Zirpl.AppEngine.VisualStudioAutomation.AppGeneration.Config;
using Zirpl.AppEngine.VisualStudioAutomation.AppGeneration.Config.Parsers;
using Zirpl.AppEngine.VisualStudioAutomation.AppGeneration.TextTemplating;
using Zirpl.AppEngine.VisualStudioAutomation.Logging;
using Zirpl.AppEngine.VisualStudioAutomation.TextTemplating;
using Zirpl.Collections;
using Zirpl.Reflection;

namespace Zirpl.AppEngine.VisualStudioAutomation.AppGeneration
{
    public static class AppGenerator
    {
        public static void GenerateApp(this TextTransformation callingTemplate)
        {
            new Action(() =>
            {
                callingTemplate.SetUp();
                GenerateApp(callingTemplate, new TemplateProvider(callingTemplate));
            })
            .GetRunner()
            .OnError(callingTemplate.LogException)
            .OnComplete((passed) => callingTemplate.CleanUp())
            .Run();
        }

        public static void GenerateApp(this TextTransformation callingTemplate, String templateAssemblyName)
        {
            new Action(() =>
            {
                callingTemplate.SetUp();
                GenerateApp(callingTemplate, new TemplateProvider(callingTemplate, new[] { templateAssemblyName }));
            })
            .GetRunner()
            .OnError(callingTemplate.LogException)
            .OnComplete((passed) => callingTemplate.CleanUp())
            .Run();
        }

        public static void GenerateApp(this TextTransformation callingTemplate, IEnumerable<String> templateAssemblyNames)
        {
            new Action(() =>
            {
                callingTemplate.SetUp();
                GenerateApp(callingTemplate, new TemplateProvider(callingTemplate, templateAssemblyNames));
            })
            .GetRunner()
            .OnError(callingTemplate.LogException)
            .OnComplete((passed) => callingTemplate.CleanUp())
            .Run();
        }

        public static void GenerateApp(this TextTransformation callingTemplate, Assembly templateAssembly)
        {
            new Action(() =>
            {
                callingTemplate.SetUp();
                GenerateApp(callingTemplate, new TemplateProvider(callingTemplate, new[] { templateAssembly }));
            })
            .GetRunner()
            .OnError(callingTemplate.LogException)
            .OnComplete((passed) => callingTemplate.CleanUp())
            .Run();
        }

        public static void GenerateApp(this TextTransformation callingTemplate, IEnumerable<Assembly> templateAssemblies)
        {
            new Action(() =>
            {
                callingTemplate.SetUp();
                GenerateApp(callingTemplate, new TemplateProvider(callingTemplate, templateAssemblies));
            })
            .GetRunner()
            .OnError(callingTemplate.LogException)
            .OnComplete((passed) => callingTemplate.CleanUp())
            .Run();
        }

        private static void GenerateApp(TextTransformation callingTemplate, ITemplateProvider templateProvider)
        {
            var app = new AppProvider(callingTemplate).GetApp();
            var outputFileProvider = new OutputFileProvider();
            var templateRunner = new TemplateRunner(app);
            callingTemplate.RunTemplates(templateRunner, templateProvider, outputFileProvider);
        }
    }
}
    