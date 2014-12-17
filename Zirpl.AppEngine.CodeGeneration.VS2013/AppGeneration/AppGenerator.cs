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

        public void TransformTemplates(IEnumerable<Type> preProcessedFileTemplateTypes)
        {
            try
            {
                foreach (var preProcessedFileTemplateType in preProcessedFileTemplateTypes)
                {
                    var template = Activator.CreateInstance(preProcessedFileTemplateType);
                    if (template.GetTypeAccessor().HasPropertyGetter<DomainType>("DomainType"))
                    {
                        // once per DomainType
                        //
                        this.Context.LogLineToBuildPane("Transforming template once per domain type: " + preProcessedFileTemplateType.Name);
                        foreach (var domainType in App.DomainTypes)
                        {
                            template = Activator.CreateInstance(preProcessedFileTemplateType);
                            var templateWrapper = new TextTransformationWrapper(template);
                            var session = new TextTemplatingSession();              
                            foreach (var globalTemplateParameter in this.App.Settings.GlobalTemplateParameters)
                            {
                                templateWrapper.Session.Add(globalTemplateParameter);
                            }
                            session.Add("AppGenerator", this);
                            session.Add("App", this.App);
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
                        this.Context.LogLineToBuildPane("Transforming template once: " + preProcessedFileTemplateType.Name);
                        template = Activator.CreateInstance(preProcessedFileTemplateType);
                        var templateWrapper = new TextTransformationWrapper(template);
                        var session = new TextTemplatingSession();
                        foreach (var globalTemplateParameter in this.App.Settings.GlobalTemplateParameters)
                        {
                            templateWrapper.Session.Add(globalTemplateParameter);
                        }
                        session.Add("AppGenerator", this);
                        session.Add("App", this.App);
                        templateWrapper.Session = session;
                        templateWrapper.Initialize();

                        // run the template
                        templateWrapper.TransformText();
                    }
                }

                this.EndFile();
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

        public void StartFile(Object textTransformation, String fileName, String folderWithinProject = null, String destinationProjectName = null, BuildActionTypeEnum? buildAction = null, String customTool = null, bool? autoFormat = null, bool? overwrite = null, Encoding encoding = null)
        {
            this.Context.StartFile(new TextTransformationWrapper(textTransformation), fileName, folderWithinProject, destinationProjectName, buildAction, customTool, autoFormat, overwrite, encoding);
        }

        public void StartFile(Object textTransformation, String fileName, String folderWithinProject = null, Project destinationProject = null, BuildActionTypeEnum? buildAction = null, String customTool = null, bool? autoFormat = null, bool? overwrite = null, Encoding encoding = null)
        {
            this.Context.StartFile(new TextTransformationWrapper(textTransformation), fileName, folderWithinProject, destinationProject, buildAction, customTool, autoFormat, overwrite, encoding);
        }

        public void StartFile(Object textTransformation, OutputFile outputFile)
        {
            this.Context.StartFile(new TextTransformationWrapper(textTransformation), outputFile);
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
