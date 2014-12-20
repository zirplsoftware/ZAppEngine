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
        public static void GenerateApp(this TextTransformation callingTemplate,
            String preprocessedTemplatesAssemblyFileName, AppGenerationSettings settings = null)
        {
            using (TextTransformationContext.Create(callingTemplate))
            {
                GenerateApp(callingTemplate, new string[] {preprocessedTemplatesAssemblyFileName}, settings);
            }
        }

        public static void GenerateApp(this TextTransformation callingTemplate,
           IEnumerable<String> preprocessedTemplatesAssemblyFileNames, AppGenerationSettings settings = null)
        {
            using (TextTransformationContext.Create(callingTemplate))
            {
                var list = from fileName in preprocessedTemplatesAssemblyFileNames
                    where
                        AppDomain.CurrentDomain.GetAssemblies()
                            .Count(o => !o.IsDynamic && o.Location.Contains(fileName)) == 1
                    select
                        AppDomain.CurrentDomain.GetAssemblies()
                            .Where(o => !o.IsDynamic && o.Location.Contains(fileName))
                            .Single();

                GenerateApp(callingTemplate, list, settings);
            }
        }

        public static void GenerateApp(this TextTransformation callingTemplate, Assembly preprocessedTemplatesAssembly, AppGenerationSettings settings = null)
        {
            using (TextTransformationContext.Create(callingTemplate))
            {
                var list = new List<Assembly>();
                if (preprocessedTemplatesAssembly != null)
                {
                    list.Add(preprocessedTemplatesAssembly);
                }
                GenerateApp(callingTemplate, list, settings);
            }
        }

        public static void GenerateApp(this TextTransformation callingTemplate, IEnumerable<Assembly> preprocessedTemplatesAssemblies = null, AppGenerationSettings settings = null)
        {
            using (TextTransformationContext.Create(callingTemplate))
            {
                var assemblyList = new List<Assembly>();
                if (preprocessedTemplatesAssemblies != null
                    && preprocessedTemplatesAssemblies.Any())
                {
                    assemblyList.AddRange(preprocessedTemplatesAssemblies);
                }
                else
                {
                    if (TextTransformationContext.Instance.CallingTemplateProjectItem.ContainingProject
                        .GetAllProjectItemsRecursive().Where(o => Path.GetExtension(o.GetFullPath()) == ".tt").Any())
                    {
                        TextTransformationContext.Instance
                            .CallingTemplateProjectItem
                            .ContainingProject
                            .CompileCSharpProjectInMemory();
                    }
                    assemblyList.AddRange(AppDomain.CurrentDomain.GetAssemblies().Where(o => !o.IsDynamic));
                }

                var templateTypesList = from assembly in assemblyList
                    from o in assembly.GetTypes()
                    where IsAppTemplate(o)
                    select o;
                GenerateApp(callingTemplate, templateTypesList, settings);
            }
        }

        private static bool IsAppTemplate(Type o)
        {
            //[global::System.CodeDom.Compiler.GeneratedCodeAttribute("Microsoft.VisualStudio.TextTemplating", "12.0.0.0")]

            return o.IsClass
                && !o.IsAbstract
                //&& Attribute.GetCustomAttribute(o, typeof (GeneratedCodeAttribute)) != null
                && (typeof(OncePerAppTemplate).IsAssignableFrom(o)
                        || typeof(OncePerDomainTypeTemplate).IsAssignableFrom(o));
        }

        private static void GenerateApp(TextTransformation callingTemplate, IEnumerable<Type> preProcessedFileTemplateTypes, AppGenerationSettings settings = null)
        {
            settings = settings ?? new AppGenerationSettings();
                try
                {

                    // set all of the settings defaults
                    //
                    settings.DataContextName = settings.DataContextName ?? "AppDataContext";
                    settings.ProjectNamespacePrefix = settings.ProjectNamespacePrefix
                                                      ?? TextTransformationContext.Instance.CallingTemplateProjectItem
                                                          .ContainingProject.GetDefaultNamespace()
                                                          .SubstringUntilLastInstanceOf(".");

                    // create the app
                    //
                    var app = new App()
                    {
                        Settings = settings,
                        CodeGenerationProject = TextTransformationContext.Instance.CallingTemplateProjectItem.ContainingProject,
                        ModelProject = TextTransformationContext.Instance.VisualStudio.GetProject(settings.ProjectNamespacePrefix + ".Model"),
                        DataServiceProject = TextTransformationContext.Instance.VisualStudio.GetProject(settings.ProjectNamespacePrefix + ".DataService"),
                        ServiceProject = TextTransformationContext.Instance.VisualStudio.GetProject(settings.ProjectNamespacePrefix + ".Service"),
                        WebCommonProject = TextTransformationContext.Instance.VisualStudio.GetProject(settings.ProjectNamespacePrefix + ".Web.Common"),
                        WebProject = TextTransformationContext.Instance.VisualStudio.GetProject(settings.ProjectNamespacePrefix + ".Web"),
                        TestsCommonProject = TextTransformationContext.Instance.VisualStudio.GetProject(settings.ProjectNamespacePrefix + ".Tests.Common"),
                        DataServiceTestsProject = TextTransformationContext.Instance.VisualStudio.GetProject(settings.ProjectNamespacePrefix + ".Tests.DataService"),
                        ServiceTestsProject = TextTransformationContext.Instance.VisualStudio.GetProject(settings.ProjectNamespacePrefix + ".Tests.Service"),
                    };

                    // create all of the domain types
                    //
                    var projectItems = TextTransformationContext.Instance.VisualStudio.Solution.GetAllProjectItemsRecursive();
                    var paths = from p in projectItems
                                where p.GetFullPath().ToLowerInvariant().EndsWith(".domain.zae")
                                    && p.GetFullPath().ToLowerInvariant().Contains("_config")
                                select p.GetFullPath();
                    new DomainFileParser().ParseDomainTypes(app, paths);

                    // run the preprocessed templates
                    //
                    foreach (var templateType in preProcessedFileTemplateTypes)
                    {
                        TextTransformationContext.Instance.LogLineToBuildPane(
                            "Found preprocessed template: " + templateType.FullName);
                    }
                    foreach (var templateType in preProcessedFileTemplateTypes)
                    {
                        if (typeof(OncePerDomainTypeTemplate).IsAssignableFrom(templateType))
                        {
                            // once per DomainType
                            //
                            TextTransformationContext.Instance.LogLineToBuildPane("Transforming template once per domain type: " + templateType.Name);
                            foreach (var domainType in app.DomainTypes)
                            {
                                TransformTemplate(templateType, app, domainType);
                            }
                        }
                        else if (typeof(OncePerAppTemplate).IsAssignableFrom(templateType))
                        {
                            // once per App
                            //
                            TextTransformationContext.Instance.LogLineToBuildPane("Transforming template once: " + templateType.Name);
                            TransformTemplate(templateType, app);
                        }
                    }
                }
                catch (Exception e)
                {
                    if (TextTransformationContext.Instance.CallingTemplate != null)
                    {
                        try
                        {
                            TextTransformationContext.Instance.LogLineToBuildPane(e.ToString());
                            TextTransformationContext.Instance.CallingTemplate.WriteLine(e.ToString());
                        }
                        catch (Exception)
                        {
                        }
                    }
                    throw;
                }
        }

        private static void TransformTemplate(Type preProcessedFileTemplateType, App app, DomainType domainType = null)
        {
            var template = Activator.CreateInstance(preProcessedFileTemplateType);
            var templateWrapper = new TextTransformationWrapper(template);
            var session = new TextTemplatingSession();
            foreach (var globalTemplateParameter in app.Settings.GlobalTemplateParameters)
            {
                templateWrapper.Session.Add(globalTemplateParameter);
            }
            session.Add("Context", TextTransformationContext.Instance);
            session.Add("App", app);
            if (domainType != null)
            {
                session.Add("DomainType", domainType);
            }
            templateWrapper.Session = session;
            templateWrapper.Initialize();

            var templateBase = template as TemplateBase;
            if (templateBase != null)
            {
                if (!templateBase.ShouldTransform)
                {
                    return;
                }
                else
                {
                    var outputFile = templateBase.OutputFile;
                    if (outputFile != null)
                    {
                        TextTransformationContext.Instance.StartFile(templateWrapper, outputFile);
                    }
                }
            }
            // run the template
            templateWrapper.TransformText();
            TextTransformationContext.Instance.EndFile();
        }

    }
}
    