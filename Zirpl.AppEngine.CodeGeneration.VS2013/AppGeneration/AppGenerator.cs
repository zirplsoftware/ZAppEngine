using System;
using System.Collections.Generic;
using System.Linq;
using Microsoft.VisualStudio.PlatformUI;
using Microsoft.VisualStudio.TextTemplating;
using Newtonsoft.Json;
using Zirpl.AppEngine.VisualStudioAutomation.AppGeneration.Config;
using Zirpl.AppEngine.VisualStudioAutomation.AppGeneration.Config.Parsers;
using Zirpl.AppEngine.VisualStudioAutomation.AppGeneration.TextTemplating;
using Zirpl.AppEngine.VisualStudioAutomation.AppGeneration.TextTemplating.V1;
using Zirpl.AppEngine.VisualStudioAutomation.AppGeneration.TextTemplating.V1.Templates.Model;
using Zirpl.AppEngine.VisualStudioAutomation.TextTemplating;
using Zirpl.Collections;

namespace Zirpl.AppEngine.VisualStudioAutomation.AppGeneration
{
    public static class AppGenerator
    {
        public static void GenerateApp(this TextTransformation callingTemplate, AppGenerationSettings settings = null)
        {
            try
            {
                using (var session = TransformSession.StartSession(callingTemplate))
                {
                    // set all of the settings defaults
                    //
                    settings = settings ?? new AppGenerationSettings();
                    settings.DataContextName = settings.DataContextName ?? "AppDataContext";
                    settings.GeneratedContentRootFolderName = settings.GeneratedContentRootFolderName ?? @"_auto\";
                    settings.ProjectNamespacePrefix = settings.ProjectNamespacePrefix
                        ?? VisualStudio.Current.GetProjectItem(session.CallingTemplate.Host.TemplateFile).ContainingProject
                                                          .GetDefaultNamespace().SubstringUntilLastInstanceOf(".");

                    // default V1 builder strategies
                    //
                    if (!settings.BuilderStrategies.Where(o => o.TemplateCategory == TemplateCategories.PersistableDomainClass).Any())
                    {
                        settings.BuilderStrategies.Add(new PersistableDomainClassStrategy());
                    }

                    // create the app
                    //
                    var app = new App()
                    {
                        Settings = settings,
                        AppGenerationConfigProject = VisualStudio.Current.GetProjectItem(session.CallingTemplate.Host.TemplateFile).ContainingProject,
                        ModelProject = VisualStudio.Current.GetProject(settings.ProjectNamespacePrefix + ".Model"),
                        DataServiceProject = VisualStudio.Current.GetProject(settings.ProjectNamespacePrefix + ".DataService"),
                        ServiceProject = VisualStudio.Current.GetProject(settings.ProjectNamespacePrefix + ".Service"),
                        WebCommonProject =VisualStudio.Current.GetProject(settings.ProjectNamespacePrefix + ".Web.Common"),
                        WebProject = VisualStudio.Current.GetProject(settings.ProjectNamespacePrefix + ".Web"),
                        TestsCommonProject =VisualStudio.Current.GetProject(settings.ProjectNamespacePrefix + ".Tests.Common"),
                        DataServiceTestsProject =VisualStudio.Current.GetProject(settings.ProjectNamespacePrefix + ".Tests.DataService"),
                        ServiceTestsProject =VisualStudio.Current.GetProject(settings.ProjectNamespacePrefix + ".Tests.Service"),
                    };
                    
                    // create all of the domain types
                    //
                    var projectItems = app.AppGenerationConfigProject.ProjectItems.GetAllProjectItemsRecursive();
                    var paths = from p in projectItems
                        where p.GetFullPath().ToLowerInvariant().EndsWith(".domain.zae")
                        select p.GetFullPath();
                    app.DomainTypes.AddRange(new DomainFileParser().Parse(app, paths));

                    // create all of the Template output files
                    //
                    foreach (var strategy in app.Settings.BuilderStrategies)
                    {
                        app.FilesToGenerate.AddRange(strategy.BuildOutputFiles(app));   
                    }
                    
                    //session.CallingTemplate.WriteLine(JsonConvert.SerializeObject(app, Formatting.Indented, new JsonSerializerSettings { PreserveReferencesHandling = PreserveReferencesHandling.Objects }));

                    // call the templates to build the files
                    //
                    foreach (var file in app.FilesToGenerate)
                    {
                        var templateParameters = new Dictionary<string, object>();
                        // put in the ones from the settings file first
                        foreach (var parameter in app.Settings.TemplateParameters)
                        {
                            templateParameters.Add(parameter.Key, parameter.Value);
                        }
                        // next the ones from the file to generate (which may override from the settings)
                        foreach (var parameter in file.TemplateParameters)
                        {
                            templateParameters.Add(parameter.Key, parameter.Value);
                        }
                        // finally the App
                        templateParameters.Add("App", app);
                        session.ExecuteTransform(file, templateParameters);
                    }
                }
            }
            catch (Exception e)
            {
                if (callingTemplate != null)
                {
                    try
                    {
                        callingTemplate.WriteLine(e.ToString());
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
