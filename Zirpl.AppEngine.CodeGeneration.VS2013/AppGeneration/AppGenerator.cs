﻿using System;
using System.Collections.Generic;
using System.IO;
using System.Linq;
using System.Reflection;
using System.Text;
using EnvDTE;
using Microsoft.VisualStudio.OLE.Interop;
using Microsoft.VisualStudio.PlatformUI;
using Microsoft.VisualStudio.TextTemplating;
using Newtonsoft.Json;
using Zirpl.AppEngine.VisualStudioAutomation.AppGeneration.Config;
using Zirpl.AppEngine.VisualStudioAutomation.AppGeneration.Config.Parsers;
using Zirpl.AppEngine.VisualStudioAutomation.TextTemplating;
using Zirpl.Collections;
using Zirpl.Reflection;

namespace Zirpl.AppEngine.VisualStudioAutomation.AppGeneration
{
    public static class AppGenerator
    {
        public static void GenerateApp(this TextTransformation callingTemplate, AppGenerationSettings settings = null)
        {
            foreach (var assembly in AppDomain.CurrentDomain.GetAssemblies().Where(o => !o.IsDynamic))
            {
                GenerateApp(callingTemplate, assembly, settings);
            }
        }
        public static void GenerateApp(this TextTransformation callingTemplate,
            String preprocessedTemplatesAssemblyFileName, AppGenerationSettings settings = null)
        {
            Assembly assembly = AppDomain.CurrentDomain.GetAssemblies().Where(o => !o.IsDynamic && o.Location.Contains(preprocessedTemplatesAssemblyFileName)).SingleOrDefault();
            GenerateApp(callingTemplate, assembly, settings);
        }

        public static void GenerateApp(this TextTransformation callingTemplate,
           IEnumerable<String> preprocessedTemplatesAssemblyFileNames, AppGenerationSettings settings = null)
        {
            foreach (var assemblyFileName in preprocessedTemplatesAssemblyFileNames)
            {
                GenerateApp(callingTemplate, assemblyFileName, settings);
            }
        }

        public static void GenerateApp(this TextTransformation callingTemplate, Assembly preprocessedTemplatesAssembly,
            AppGenerationSettings settings = null)
        {
            var listOfTemplateTypes = new List<Type>();
            //[global::System.CodeDom.Compiler.GeneratedCodeAttribute("Microsoft.VisualStudio.TextTemplating", "12.0.0.0")]
            foreach (var type in preprocessedTemplatesAssembly.GetTypes().Where(o => o.IsClass 
                && !o.IsAbstract 
                && Attribute.GetCustomAttribute(o, typeof(System.CodeDom.Compiler.GeneratedCodeAttribute)) != null))
            {       
                // ok, we have a likely candidate- let's runs some tests
                if (type.GetMethod("TransformText") != null
                    && type.GetMethod("TransformText").ReturnType.IsAssignableFrom(typeof(String))
                    && type.GetMethod("Initialize") != null)
                {
                    listOfTemplateTypes.Add(type);
                }
            }

            GenerateApp(callingTemplate, listOfTemplateTypes, settings);
        }
        public static void GenerateApp(this TextTransformation callingTemplate, IEnumerable<Assembly> preprocessedTemplatesAssemblies, AppGenerationSettings settings = null)
        {
            foreach (var assembly in preprocessedTemplatesAssemblies)
            {
                GenerateApp(callingTemplate, assembly, settings);
            }
        }

        private static void GenerateApp(this TextTransformation callingTemplate, IEnumerable<Type> preProcessedFileTemplateTypes, AppGenerationSettings settings = null)
        {
            settings = settings ?? new AppGenerationSettings();
            using (TextTransformationContext.Create(callingTemplate))
            {
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
                        AppGenerationConfigProject = TextTransformationContext.Instance.CallingTemplateProjectItem.ContainingProject,
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
                    var projectItems = app.AppGenerationConfigProject.ProjectItems.GetAllProjectItemsRecursive();
                    var paths = from p in projectItems
                                where p.GetFullPath().ToLowerInvariant().EndsWith(".domain.zae")
                                select p.GetFullPath();
                    new DomainFileParser().ParseDomainTypes(app, paths);

                    // run the preprocessed templates
                    //
                    foreach (var preProcessedFileTemplateType in preProcessedFileTemplateTypes)
                    {
                        var template = Activator.CreateInstance(preProcessedFileTemplateType);
                        if (template.GetTypeAccessor().HasPropertyGetter<DomainType>("DomainType"))
                        {
                            // once per DomainType
                            //
                            TextTransformationContext.Instance.LogLineToBuildPane(
                                "Transforming template once per domain type: " + preProcessedFileTemplateType.Name);
                            foreach (var domainType in app.DomainTypes)
                            {
                                TransformTemplate(preProcessedFileTemplateType, app, domainType);
                            }
                        }
                        else
                        {
                            // once per App
                            //
                            TextTransformationContext.Instance.LogLineToBuildPane("Transforming template once: " +
                                                                                  preProcessedFileTemplateType.Name);
                            TransformTemplate(preProcessedFileTemplateType, app);
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

            // run the template
            templateWrapper.TransformText();
        }
    }
}
