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
                if (preprocessedTemplatesAssemblies != null)
                {
                    assemblyList.AddRange(preprocessedTemplatesAssemblies);
                }
                if (TextTransformationContext.Instance.CallingTemplateProjectItem.ContainingProject
                        .GetAllProjectItemsRecursive().Where(o => Path.GetExtension(o.GetFullPath()) == ".tt").Any())
                {
                    CompilePreProcessedTemplatesInCallingTemplateProject();
                }
                if (!assemblyList.Any())
                {
                    assemblyList.AddRange(AppDomain.CurrentDomain.GetAssemblies().Where(o => !o.IsDynamic));
                }

                //[global::System.CodeDom.Compiler.GeneratedCodeAttribute("Microsoft.VisualStudio.TextTemplating", "12.0.0.0")]

                var templateTypesList = from assembly in assemblyList
                    from o in assembly.GetTypes()
                    where o.IsClass
                          && !o.IsAbstract
                          &&
                          Attribute.GetCustomAttribute(o, typeof (System.CodeDom.Compiler.GeneratedCodeAttribute)) !=
                          null
                          && o.FullName.ToLowerInvariant().Contains("._templates.")
                          && o.GetMethod("TransformText") != null
                          && o.GetMethod("TransformText").ReturnType.IsAssignableFrom(typeof (String))
                          && o.GetMethod("Initialize") != null
                    select o;
                GenerateApp(callingTemplate, templateTypesList, settings);
            }
        }

        private static void CompilePreProcessedTemplatesInCallingTemplateProject()
        {
            var codeList = new List<String>();
            foreach (var item in TextTransformationContext.Instance.CallingTemplateProjectItem.ContainingProject.GetAllProjectItemsRecursive())
            {
                if (item.GetFullPath().EndsWith(".cs"))
                {
                    codeList.Add(File.ReadAllText(item.GetFullPath()));
                }
            }

            CSharpCodeProvider provider = new CSharpCodeProvider();
            CompilerParameters parameters = new CompilerParameters();
            // True - memory generation, false - external file generation
            parameters.GenerateInMemory = true;
            // True - exe file generation, false - dll file generation
            parameters.GenerateExecutable = false;
            parameters.ReferencedAssemblies.Clear();


            var vsproject = TextTransformationContext.Instance.CallingTemplateProjectItem.ContainingProject.Object as VSLangProj.VSProject;
            // note: you could also try casting to VsWebSite.VSWebSite

            foreach (VSLangProj.Reference reference in vsproject.References)
            {
                if (reference.Name != "mscorlib")
                {
                    parameters.ReferencedAssemblies.Add(reference.Path);
                    TextTransformationContext.Instance.LogLineToBuildPane("Adding reference: " + reference.Path);
                }
                //if (reference.SourceProject == null)
                //{
                //}
                //else
                //{
                //    // This is a project reference
                //}
            }
            CompilerResults results = provider.CompileAssemblyFromSource(parameters, codeList.ToArray());
            if (results.Errors.HasErrors)
            {
                StringBuilder sb = new StringBuilder();

                foreach (CompilerError error in results.Errors)
                {
                    sb.AppendLine(String.Format("Error ({0}): {1}", error.ErrorNumber, error.ErrorText));
                }

                throw new InvalidOperationException(sb.ToString());
            }
        }
 
        private static string GetFullName(VSLangProj.Reference reference)
        {
            return string.Format("{0}, Version={1}.{2}.{3}.{4}, Culture={5}, PublicKeyToken={6}",
                                    reference.Name,
                                    reference.MajorVersion, reference.MinorVersion, reference.BuildNumber, reference.RevisionNumber,
                                    reference.Culture.Or("neutral"),
                                    reference.PublicKeyToken.Or("null"));
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
                    var projectItems = TextTransformationContext.Instance.VisualStudio.Solution.GetAllProjectItemsRecursive();
                    var paths = from p in projectItems
                                where p.GetFullPath().ToLowerInvariant().EndsWith(".domain.zae")
                                    //&& p.GetFullPath().ToLowerInvariant().Contains("_config")
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

            if (template.GetTypeAccessor().HasPropertyGetter<bool>("ShouldTransform")
                && template.GetProperty<bool>("ShouldTransform"))
            {
                var fileName = GetFileNameFromPreProcessedTemplateType(preProcessedFileTemplateType, domainType);
                var destinationProject = GetProjectFromPreProcessedTemplateType(app, preProcessedFileTemplateType);
                var folder = GetFolderPathFromPreProcessedTemplateType(app, preProcessedFileTemplateType, domainType);

                if (destinationProject != null
                    && !String.IsNullOrEmpty(fileName))
                {
                    templateWrapper.UseNewFile(fileName, folder, destinationProject,
                        GetBuildActionFromFileName(fileName));
                }

                // run the template
                templateWrapper.TransformText();
                TextTransformationContext.Instance.EndFile();
            }
        }

        private static String GetFileNameFromPreProcessedTemplateType(Type preProcessedFileTemplateType,DomainType domainType)
        {
            // let's see if we can determine the filename, folder, and project by convention
            String fileName = null;
            var tokens = preProcessedFileTemplateType.Name.Split('_');
            if (tokens.Count() >= 2
                || tokens.Count() <= 4)
            {
                // yes, we can determine the fileName
                for (int i = 0; i < tokens.Length; i++)
                {
                    if (i != tokens.Length - 1
                        && domainType != null
                        && tokens[i].ToLowerInvariant() == "dt")
                    {
                        tokens[i] = domainType.Name;
                    }
                    if (i == tokens.Length - 1)
                    {
                        tokens[i] = tokens[i].SubstringUntilLastInstanceOf("template", StringComparison.InvariantCultureIgnoreCase);
                        tokens[i] = "." + tokens[i];
                        if (String.IsNullOrEmpty(tokens[i]))
                        {
                            // assume it is CSharp
                            tokens[i] = ".cs";
                        }
                    }
                }
                foreach (var token in tokens)
                {
                    fileName += token;
                }
            }
            return fileName;
        }

        private static Project GetProjectFromPreProcessedTemplateType(App app, Type preProcessedFileTemplateType)
        {
            var whichProject = (preProcessedFileTemplateType.Namespace + "." + preProcessedFileTemplateType.Name)
                .SubstringAfterLastInstanceOf("_templates.", StringComparison.InvariantCultureIgnoreCase)
                .SubstringUntilFirstInstanceOf("Project", StringComparison.InvariantCultureIgnoreCase);
            var whichProjectLower = whichProject.ToLower();
            if (whichProjectLower == "model")
            {
                return app.ModelProject;
            }
            else if (whichProjectLower == "dataservice")
            {
                return app.DataServiceProject;
            }
            else if (whichProjectLower == "service")
            {
                return app.ServiceProject;
            }
            else if (whichProjectLower == "webcommon")
            {
                return app.WebCommonProject;
            }
            else if (whichProjectLower == "web")
            {
                return app.WebProject;
            }
            else if (whichProjectLower == "testscommon")
            {
                return app.TestsCommonProject;
            }
            else if (whichProjectLower == "dataservicetests")
            {
                return app.DataServiceTestsProject;
            }
            else if (whichProjectLower == "servicetests")
            {
                return app.ServiceTestsProject;
            }
            else
            {
                return null;
            }
        }

        private static String GetFolderPathFromPreProcessedTemplateType(App app, Type preProcessedTemplateType, DomainType domainType)
        {
            var immediateFolder = preProcessedTemplateType.Namespace
                .SubstringAfterLastInstanceOf("_templates.", StringComparison.InvariantCultureIgnoreCase)
                .SubstringAfterFirstInstanceOf("Project.", StringComparison.InvariantCultureIgnoreCase)
                .Replace('.', '\\');
            if (domainType != null)
            {
                // combine the immediate folder of the template
                // with the subnamespace of the DomainType
                //
                immediateFolder = Path.Combine(immediateFolder, app.GetFolderPathFromNamespace(domainType.DestinationProject, domainType.Namespace).Replace('.', '\\'));
            }
            return immediateFolder;
        }

        private static BuildActionTypeEnum? GetBuildActionFromFileName(String fileName)
        {
            switch (Path.GetExtension(fileName).ToLowerInvariant())
            {
                case ".cs":
                    return BuildActionTypeEnum.Compile;
                default:
                    return BuildActionTypeEnum.None;
            }
        }
    }
}
    