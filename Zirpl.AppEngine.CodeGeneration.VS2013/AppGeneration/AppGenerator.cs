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
using Zirpl.AppEngine.VisualStudioAutomation.AppGeneration.TextTemplating;
using Zirpl.AppEngine.VisualStudioAutomation.TextTemplating;
using Zirpl.Collections;

namespace Zirpl.AppEngine.VisualStudioAutomation.AppGeneration
{
    public class AppGenerator : IDisposable
    {
        public TextTransformationContext Context { get { return TextTransformationContext.Instance; } }
        public App App { get; private set; }

        public AppGenerator(TextTransformation callingTemplate, AppGenerationSettings settings = null)
        {
            TextTransformationContext.Create(callingTemplate);
            
            // set all of the settings defaults
            //
            settings = settings ?? new AppGenerationSettings();
            settings.DataContextName = settings.DataContextName ?? "AppDataContext";
            settings.GeneratedContentRootFolderName = settings.GeneratedContentRootFolderName ?? @"_auto\";
            settings.ProjectNamespacePrefix = settings.ProjectNamespacePrefix
                ?? this.Context.VisualStudio.GetProjectItem(this.Context.Host.TemplateFile).ContainingProject
                                          .GetDefaultNamespace().SubstringUntilLastInstanceOf(".");

            // create the app
            //
            this.App = new App()
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
            var projectItems = this.App.AppGenerationConfigProject.ProjectItems.GetAllProjectItemsRecursive();
            var paths = from p in projectItems
                        where p.GetFullPath().ToLowerInvariant().EndsWith(".domain.zae")
                        select p.GetFullPath();
            new DomainFileParser().ParseDomainTypes(this.App, paths);
        }


        public void GenerateFiles(IEnumerable<IOutputFileBuilder> builders)
        {
            var factory = new DefaultOutputFileBuilderFactory();
            factory.AddBuilders(builders);

            this.GenerateFiles(factory);
        }

        public void GenerateFiles(IOutputFileBuilderFactory factory)
        {
            try
            {
                // create all of the Template output files
                //
                var filesToGenerate = new List<OutputFile>();
                foreach (var strategy in factory.GetAllBuilders())
                {
                    filesToGenerate.AddRange(strategy.BuildOutputFiles(this.App));
                }

                //session.CallingTemplate.WriteLine(JsonConvert.SerializeObject(app, Formatting.Indented, new JsonSerializerSettings { PreserveReferencesHandling = PreserveReferencesHandling.Objects }));

                // call the templates to build the files
                //
                foreach (var file in filesToGenerate)
                {
                    var preprocessedFile = file as PreprocessedTextTransformationOutputFile;
                    if (preprocessedFile != null)
                    {
                        foreach (var parameter in this.App.Settings.GlobalTemplateParameters)
                        {
                            if (preprocessedFile.TemplateParameters.ContainsKey(parameter.Key))
                            {
                                throw new Exception(
                                    "Global GlobalTemplateParameters in Settings conflict with parameters a file to generate. Key = " +
                                    parameter.Key);
                            }
                            preprocessedFile.TemplateParameters.Add(parameter.Key, parameter.Value);
                        }
                        // finally the App
                        preprocessedFile.TemplateParameters.Add("App", this.App);
                        this.Context.WriteFile(file);
                    }
                    else
                    {
                        this.Context.WriteFile(file);
                    }
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

        public void WriteFile(OutputFile outputFile)
        {
            this.Context.WriteFile(outputFile);
        }

        public void WriteFile(PreprocessedTextTransformationOutputFile outputFile)
        {
            this.Context.WriteFile(outputFile);
        }

        public void StartFile(String fileName, String folderWithinProject = null, String destinationProjectName = null, BuildActionTypeEnum? buildAction = null, String customTool = null, bool? autoFormat = null, bool? overwrite = null, Encoding encoding = null)
        {
            this.Context.StartFile(fileName, folderWithinProject, destinationProjectName, buildAction, customTool, autoFormat, overwrite, encoding);
        }

        public void StartFile(String fileName, String folderWithinProject = null, Project destinationProject = null, BuildActionTypeEnum? buildAction = null, String customTool = null, bool? autoFormat = null, bool? overwrite = null, Encoding encoding = null)
        {
            this.Context.StartFile(fileName, folderWithinProject, destinationProject, buildAction, customTool, autoFormat, overwrite, encoding);
        }

        public void StartFile(OutputFile outputFile)
        {
            this.Context.StartFile(outputFile);
        }

        public void EndFile()
        {
            this.Context.EndFile();
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
