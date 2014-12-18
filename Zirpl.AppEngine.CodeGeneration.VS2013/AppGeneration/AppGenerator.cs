using System;
using System.Collections.Generic;
using System.IO;
using System.Linq;
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
        public static void GenerateApp(this TextTransformation callingTemplate, IEnumerable<Type> preProcessedFileTemplateTypes, AppGenerationSettings settings = null)
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
                                template = Activator.CreateInstance(preProcessedFileTemplateType);
                                var templateWrapper = new TextTransformationWrapper(template);
                                var session = new TextTemplatingSession();
                                foreach (var globalTemplateParameter in app.Settings.GlobalTemplateParameters)
                                {
                                    templateWrapper.Session.Add(globalTemplateParameter);
                                }
                                session.Add("Context", TextTransformationContext.Instance);
                                session.Add("App", app);
                                session.Add("DomainType", domainType);
                                templateWrapper.Session = session;
                                templateWrapper.Initialize();

                                // run the template
                                templateWrapper.TransformText();
                            }
                        }
                        else
                        {
                            // once per App
                            //
                            TextTransformationContext.Instance.LogLineToBuildPane("Transforming template once: " +
                                                                                  preProcessedFileTemplateType.Name);
                            template = Activator.CreateInstance(preProcessedFileTemplateType);
                            var templateWrapper = new TextTransformationWrapper(template);
                            var session = new TextTemplatingSession();
                            foreach (var globalTemplateParameter in app.Settings.GlobalTemplateParameters)
                            {
                                templateWrapper.Session.Add(globalTemplateParameter);
                            }
                            session.Add("Context", TextTransformationContext.Instance);
                            session.Add("App", app);
                            templateWrapper.Session = session;
                            templateWrapper.Initialize();

                            // run the template
                            templateWrapper.TransformText();
                        }
                    }
                }
                catch (Exception e)
                {
                    if (TextTransformationContext.Instance.CallingTemplate != null)
                    {
                        try
                        {
                            TextTransformationContext.Instance.CallingTemplate.WriteLine(e.ToString());
                        }
                        catch (Exception)
                        {
                        }
                        try
                        {
                            TextTransformationContext.Instance.LogLineToBuildPane(e.ToString());
                        }
                        catch (Exception)
                        {
                        }
                    }
                    throw;
                }

            }
        }
    }
}
