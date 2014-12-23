using System;
using System.CodeDom.Compiler;
using System.Collections.Generic;
using System.Data.Odbc;
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
using Zirpl.AppEngine.VisualStudioAutomation.TextTemplating;
using Zirpl.Collections;
using Zirpl.Reflection;

namespace Zirpl.AppEngine.VisualStudioAutomation.AppGeneration
{
    public static class AppGenerator
    {
        public static void GenerateApp(this TextTransformation callingTemplate, IDictionary<string, object> sessionParameters = null, IOutputFileProvider outputFileProvider = null)
        {
            new Action(() =>
            {
                callingTemplate.SetUp();
                callingTemplate.GenerateApp(new TemplateProvider(callingTemplate));
            })
            .GetRunner()
            .OnError((e) => HandleException(callingTemplate, e))
            .OnComplete((passed) => callingTemplate.CleanUp())
            .Run();
        }

        public static void GenerateApp(this TextTransformation callingTemplate, String templateAssemblyName, IDictionary<string, object> sessionParameters = null, IOutputFileProvider outputFileProvider = null)
        {
            new Action(() =>
            {
                callingTemplate.SetUp();
                callingTemplate.GenerateApp(new TemplateProvider(callingTemplate, new[] { templateAssemblyName }));
            })
            .GetRunner()
            .OnError((e) => HandleException(callingTemplate, e))
            .OnComplete((passed) => callingTemplate.CleanUp())
            .Run();
        }

        public static void GenerateApp(this TextTransformation callingTemplate, IEnumerable<string> templateAssemblyNames, IDictionary<string, object> sessionParameters = null, IOutputFileProvider outputFileProvider = null)
        {
            new Action(() =>
            {
                callingTemplate.SetUp();
                callingTemplate.GenerateApp(new TemplateProvider(callingTemplate, templateAssemblyNames));
            })
            .GetRunner()
            .OnError((e) => HandleException(callingTemplate, e))
            .OnComplete((passed) => callingTemplate.CleanUp())
            .Run();
        }

        public static void GenerateApp(this TextTransformation callingTemplate, Assembly templateAssembly, IDictionary<string, object> sessionParameters = null, IOutputFileProvider outputFileProvider = null)
        {
            new Action(() =>
            {
                callingTemplate.SetUp();
                callingTemplate.GenerateApp(new TemplateProvider(callingTemplate, new[] { templateAssembly }));
            })
            .GetRunner()
            .OnError((e) => HandleException(callingTemplate, e))
            .OnComplete((passed) => callingTemplate.CleanUp())
            .Run();
        }

        public static void GenerateApp(this TextTransformation callingTemplate, IEnumerable<Assembly> templateAssemblies, IDictionary<string, object> sessionParameters = null, IOutputFileProvider outputFileProvider = null)
        {
            new Action(() =>
            {
                callingTemplate.SetUp();
                callingTemplate.GenerateApp(new TemplateProvider(callingTemplate, templateAssemblies));
            })
            .GetRunner()
            .OnError((e) => HandleException(callingTemplate, e))
            .OnComplete((passed) => callingTemplate.CleanUp())
            .Run();
        }

        public static void GenerateApp(this TextTransformation callingTemplate, ITemplateProvider templateProvider, IDictionary<string, object> sessionParameters = null, IOutputFileProvider outputFileProvider = null)
        {
            var app = new AppProvider(callingTemplate).GetApp();
            outputFileProvider = outputFileProvider ?? new OutputFileProvider();
            callingTemplate.RunTemplates(new AppGeneration.TextTemplating.TemplateRunner(app), templateProvider, sessionParameters, outputFileProvider);
        }


        private static void HandleException(TextTransformation textTransformation, Exception e)
        {
            try
            {
                LogManager.GetLog().Debug(e.ToString());
                textTransformation.WriteLine(e.ToString());
            }
            catch (Exception)
            {
            }
        }
    }
}
    