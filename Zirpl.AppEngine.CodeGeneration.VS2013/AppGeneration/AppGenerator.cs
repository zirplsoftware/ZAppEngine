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
            using (TextTransformationContext.Create(callingTemplate))
            {
                GenerateApp(new TemplateProvider());
            }
        }
        public static void GenerateApp(this TextTransformation callingTemplate, String templateAssemblyName)
        {
            using (TextTransformationContext.Create(callingTemplate))
            {
                GenerateApp(new TemplateProvider(templateAssemblyName));
            }
        }

        public static void GenerateApp(this TextTransformation callingTemplate, IEnumerable<String> templateAssemblyNames)
        {
            using (TextTransformationContext.Create(callingTemplate))
            {
                GenerateApp(new TemplateProvider(templateAssemblyNames));
            }
        }

        public static void GenerateApp(this TextTransformation callingTemplate, Assembly templateAssembly)
        {
            using (TextTransformationContext.Create(callingTemplate))
            {
                GenerateApp(new TemplateProvider(templateAssembly));
            }
        }

        public static void GenerateApp(this TextTransformation callingTemplate, IEnumerable<Assembly> templateAssemblies)
        {
            using (TextTransformationContext.Create(callingTemplate))
            {
                GenerateApp(new TemplateProvider(templateAssemblies));
            }
        }


        private static void GenerateApp(TemplateProvider templateProvider)
        {
            try
            {
                var app = new AppProvider().GetApp();
                new TemplateRunner(app, templateProvider).RunTemplates();
            }
            catch (Exception e)
            {
                try
                {
                    TextTransformationContext.Instance.LogLineToBuildPane(e.ToString());
                    TextTransformationContext.Instance.CallingTemplate.WriteLine(e.ToString());
                }
                catch (Exception)
                {
                }
                throw;
            }
        }
    }
}
    