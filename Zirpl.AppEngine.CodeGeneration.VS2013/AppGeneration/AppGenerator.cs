using System;
using System.Collections.Generic;
using System.Linq;
using Microsoft.VisualStudio.PlatformUI;
using Microsoft.VisualStudio.TextTemplating;
using Newtonsoft.Json;
using Zirpl.AppEngine.VisualStudioAutomation.AppGeneration.Config;
using Zirpl.AppEngine.VisualStudioAutomation.AppGeneration.Config.Parsers;
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
                    settings = settings ?? new AppGenerationSettings();
                    settings.DataContextName = settings.DataContextName ?? "AppDataContext";
                    settings.GeneratedContentRootFolderName = settings.GeneratedContentRootFolderName ?? @"_auto\";
                    settings.ProjectNamespacePrefix = settings.ProjectNamespacePrefix
                        ?? session.CallingTemplateProjectItem.ContainingProject
                                                          .GetDefaultNamespace().SubstringUntilLastInstanceOf(".");

                    var app = new App()
                    {
                        Settings = settings,
                        AppGenerationConfigProject = session.CallingTemplateProjectItem.ContainingProject,
                        ModelProject = VisualStudio.Current.GetProject(settings.ProjectNamespacePrefix + ".Model"),
                        DataServiceProject = VisualStudio.Current.GetProject(settings.ProjectNamespacePrefix + ".DataService"),
                        ServiceProject = VisualStudio.Current.GetProject(settings.ProjectNamespacePrefix + ".Service"),
                        WebCommonProject =VisualStudio.Current.GetProject(settings.ProjectNamespacePrefix + ".Web.Common"),
                        WebProject = VisualStudio.Current.GetProject(settings.ProjectNamespacePrefix + ".Web"),
                        TestsCommonProject =VisualStudio.Current.GetProject(settings.ProjectNamespacePrefix + ".Tests.Common"),
                        DataServiceTestsProject =VisualStudio.Current.GetProject(settings.ProjectNamespacePrefix + ".Tests.DataService"),
                        ServiceTestsProject =VisualStudio.Current.GetProject(settings.ProjectNamespacePrefix + ".Tests.Service"),
                    };
                    var domainFileParser = new DomainFileParser();

                    var domainFilePaths = new List<String>();
                    String appFilePath = null;

                    // get all ProjectItems for the project with the initial template
                    //
                    var projectItems = app.AppGenerationConfigProject.ProjectItems.GetAllProjectItemsRecursive();
                    foreach (var configProjectItem in projectItems)
                    {
                        var path = configProjectItem.GetFullPath();
                        if (path.EndsWith(".domain.zae"))
                        {
                            domainFilePaths.Add(path);
                            session.LogLineToBuildPane("Domain file: " + path);
                        }
                    }
                    app.DomainTypes.AddRange(domainFileParser.Parse(app, domainFilePaths));


                    var factory = new OutputFileFactory();
                    app.FilesToGenerate.AddRange(factory.CreateList(app));




                    //session.CallingTemplate.WriteLine(JsonConvert.SerializeObject(app, Formatting.Indented, new JsonSerializerSettings { PreserveReferencesHandling = PreserveReferencesHandling.Objects }));



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
                        //callingTemplate.WriteLine(e.ToString());
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
