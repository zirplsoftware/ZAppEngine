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
using Zirpl.AppEngine.VisualStudioAutomation.TextTemplating;
using Zirpl.Collections;
using Zirpl.Reflection;

namespace Zirpl.AppEngine.VisualStudioAutomation.AppGeneration
{
    public static class AppGenerator
    {
        public static void GenerateApp(this TextTransformation callingTemplate)
        {
                GenerateApp(callingTemplate, new TemplateProvider(callingTemplate));
        }
        public static void GenerateApp(this TextTransformation callingTemplate, String templateAssemblyName)
        {
            GenerateApp(callingTemplate, new TemplateProvider(callingTemplate, new[] { templateAssemblyName }));
        }

        public static void GenerateApp(this TextTransformation callingTemplate, IEnumerable<String> templateAssemblyNames)
        {
            GenerateApp(callingTemplate, new TemplateProvider(callingTemplate, templateAssemblyNames));
        }

        public static void GenerateApp(this TextTransformation callingTemplate, Assembly templateAssembly)
        {
            GenerateApp(callingTemplate, new TemplateProvider(callingTemplate, new[] { templateAssembly }));
        }

        public static void GenerateApp(this TextTransformation callingTemplate, IEnumerable<Assembly> templateAssemblies)
        {
            GenerateApp(callingTemplate, new TemplateProvider(callingTemplate, templateAssemblies));
        }


        private static void GenerateApp(this TextTransformation callingTemplate, TemplateProvider templateProvider)
        {
            try
            {
                var app = new AppProvider(callingTemplate).GetApp();
                new TemplateRunner(app).RunTemplates(callingTemplate.Access().Property<IOutputFileManager>("FileManager"), templateProvider, new OutputFileProvider());
            }
            catch (Exception e)
            {
                try
                {
                    LogManager.GetLog().Debug(e.ToString());
                    callingTemplate.WriteLine(e.ToString());
                }
                catch (Exception)
                {
                }
                throw;
            }
        }
    }
}
    