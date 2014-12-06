using System;
using System.Collections.Generic;
using System.Linq;
using Microsoft.VisualStudio.OLE.Interop;
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
    public class AppGenerator : IDisposable
    {
        public AppGenerator(TextTransformation callingTemplate)
        {
            TextTransformationContext.Create(callingTemplate);
        }

        public TextTransformationContext Context { get { return TextTransformationContext.Instance; } }

        public void GenerateV1App(AppGenerationSettings settings = null, IEnumerable<ITemplateOutputFileBuilderStrategy> strategies = null)
        {
            // set all of the settings defaults
            //
            settings = settings ?? new AppGenerationSettings();
            settings.DataContextName = settings.DataContextName ?? "AppDataContext";
            settings.GeneratedContentRootFolderName = settings.GeneratedContentRootFolderName ?? @"_auto\";
            settings.FileFactory = new V1TemplateOutputFileBuilderStrategyFactory();

            var factory = new V1TemplateOutputFileBuilderStrategyFactory();
            if (strategies != null)
            {
                foreach (var strategy in strategies)
                {
                    factory.AddStrategy(strategy);
                }
            }
            settings.FileFactory = factory;

            this.GenerateApp(settings);
        }

        public void GenerateCustomApp(AppGenerationSettings settings = null, IEnumerable<ITemplateOutputFileBuilderStrategy> strategies = null)
        {
            // set all of the settings defaults
            //
            settings = settings ?? new AppGenerationSettings();
            settings.DataContextName = settings.DataContextName ?? "AppDataContext";
            settings.GeneratedContentRootFolderName = settings.GeneratedContentRootFolderName ?? @"_auto\";

            var factory = new CustomTemplateOutputFileBuilderStrategyFactory();
            if (strategies != null)
            {
                foreach (var strategy in strategies)
                {
                    factory.AddStrategy(strategy);
                }
            }
            settings.FileFactory = factory;

            GenerateApp(settings);
        }

        private void GenerateApp(AppGenerationSettings settings)
        {
            try
            {
                    settings.ProjectNamespacePrefix = settings.ProjectNamespacePrefix
                        ?? this.Context.VisualStudio.GetProjectItem(this.Context.Host.TemplateFile).ContainingProject
                                                  .GetDefaultNamespace().SubstringUntilLastInstanceOf(".");
                    // create the app
                    //
                    var app = new App()
                    {
                        Settings = settings,
                        AppGenerationConfigProject = this.Context.VisualStudio.GetProjectItem(this.Context.Host.TemplateFile).ContainingProject,
                        ModelProject = this.Context.VisualStudio.GetProject(settings.ProjectNamespacePrefix + ".Model"),
                        DataServiceProject = this.Context.VisualStudio.GetProject(settings.ProjectNamespacePrefix + ".DataService"),
                        ServiceProject = this.Context.VisualStudio.GetProject(settings.ProjectNamespacePrefix + ".Service"),
                        WebCommonProject = this.Context.VisualStudio.GetProject(settings.ProjectNamespacePrefix + ".Web.Common"),
                        WebProject = this.Context.VisualStudio.GetProject(settings.ProjectNamespacePrefix + ".Web"),
                        TestsCommonProject = this.Context.VisualStudio.GetProject(settings.ProjectNamespacePrefix + ".Tests.Common"),
                        DataServiceTestsProject = this.Context.VisualStudio.GetProject(settings.ProjectNamespacePrefix + ".Tests.DataService"),
                        ServiceTestsProject = this.Context.VisualStudio.GetProject(settings.ProjectNamespacePrefix + ".Tests.Service"),
                    };
                    
                    // create all of the domain types
                    //
                    var projectItems = app.AppGenerationConfigProject.ProjectItems.GetAllProjectItemsRecursive();
                    var paths = from p in projectItems
                        where p.GetFullPath().ToLowerInvariant().EndsWith(".domain.zae")
                        select p.GetFullPath();
                    new DomainFileParser().ParseDomainTypes(app, paths);

                    // create all of the Template output files
                    //
                    var filesToGenerate = new List<PreprocessedTextTransformationOutputFile>();
                    foreach (var strategy in app.Settings.FileFactory.GetAllStrategies())
                    {
                        filesToGenerate.AddRange(strategy.BuildOutputFiles(app));   
                    }
                    
                    //session.CallingTemplate.WriteLine(JsonConvert.SerializeObject(app, Formatting.Indented, new JsonSerializerSettings { PreserveReferencesHandling = PreserveReferencesHandling.Objects }));

                    // call the templates to build the files
                    //
                    foreach (var file in filesToGenerate)
                    {
                        foreach (var parameter in app.Settings.GlobalTemplateParameters)
                        {
                            if (file.TemplateParameters.ContainsKey(parameter.Key))
                            {
                                throw new Exception("Global GlobalTemplateParameters in Settings conflict with parameters a file to generate. Key = " + parameter.Key);
                            }
                            file.TemplateParameters.Add(parameter.Key, parameter.Value);
                        }
                        // finally the App
                        file.TemplateParameters.Add("App", app);
                        this.Context.TransformAndCreateFile(file);
                    }
            }
            catch (Exception e)
            {
                if (this.Context.CallingTemplate != null)
                {
                    try
                    {
                        this.Context.CallingTemplate.WriteLine(e.ToString());
                    }
                    catch (Exception)
                    {
                    }
                }
                throw;
            }
        }

        public void Dispose()
        {
            if (this.Context != null)
            {
                this.Context.Dispose();
            }
        }
    }
}
