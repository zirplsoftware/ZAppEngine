using System;
using System.Collections.Concurrent;
using System.Collections.Generic;
using System.IO;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using EnvDTE;
using EnvDTE80;
using Microsoft.VisualStudio.PlatformUI;
using Microsoft.VisualStudio.TextTemplating;
using Zirpl.AppEngine.Logging;
using Zirpl.AppEngine.VisualStudioAutomation.AppGeneration;
using Zirpl.AppEngine.VisualStudioAutomation.AppGeneration.TextTemplating;
using Zirpl.AppEngine.VisualStudioAutomation.VisualStudio.Logging;
using Zirpl.Reflection;

namespace Zirpl.AppEngine.VisualStudioAutomation.TextTemplating
{
    public static class TextTransformationExtensions
    {
        public static void SetUp(this TextTransformation textTransformation)
        {
            LogFactory.Initialize((IServiceProvider)textTransformation.Wrap().Host);
        }

        public static void CleanUp(this TextTransformation textTransformation)
        {
            textTransformation.SetUp();
            textTransformation.Wrap().FileManager.EndFile();
        }

        public static ITextTransformation Wrap(this TextTransformation textTransformation)
        {
            textTransformation.SetUp();
            return new TextTransformationWrapper(textTransformation);
        }

        public static void RunTemplates(this TextTransformation textTransformation, ITemplateProvider templateProvider, IDictionary<string, object> sessionParameters = null, IOutputFileProvider outputFileProvider = null)
        {
            new Action(() =>
            {
                textTransformation.SetUp();
                textTransformation.RunTemplates(new TemplateRunner(), templateProvider, sessionParameters, outputFileProvider);
            })
            .GetRunner()
            .OnError((e) => HandleException(textTransformation, e))
            .OnComplete((passed) => textTransformation.CleanUp())
            .Run();
        }

        public static void RunTemplates(this TextTransformation textTransformation, IEnumerable<Type> templateTypes, IDictionary<string, object> sessionParameters = null, IOutputFileProvider outputFileProvider = null)
        {
            new Action(() =>
            {
                textTransformation.SetUp();
                textTransformation.RunTemplate(new TemplateRunner(), templateTypes, sessionParameters, outputFileProvider);
            })
            .GetRunner()
            .OnError((e) => HandleException(textTransformation, e))
            .OnComplete((passed) => textTransformation.CleanUp())
            .Run();
        }

        public static void RunTemplate(this TextTransformation textTransformation, Type templateType, IDictionary<string, object> sessionParameters = null, IOutputFileProvider outputFileProvider = null)
        {
            new Action(() =>
            {
                textTransformation.SetUp();
                textTransformation.RunTemplate(new TemplateRunner(), templateType, sessionParameters, outputFileProvider);
            })
            .GetRunner()
            .OnError((e) => HandleException(textTransformation, e))
            .OnComplete((passed) => textTransformation.CleanUp())
            .Run();
        }

        public static void RunTemplate<T>(this TextTransformation textTransformation, IDictionary<string, object> sessionParameters = null, IOutputFileProvider outputFileProvider = null)
        {
            new Action(() =>
            {
                textTransformation.SetUp();
                textTransformation.RunTemplate(new TemplateRunner(), typeof(T), sessionParameters, outputFileProvider);
            })
            .GetRunner()
            .OnError((e) => HandleException(textTransformation, e))
            .OnComplete((passed) => textTransformation.CleanUp())
            .Run();
        }

        public static void RunTemplates(this TextTransformation textTransformation, IEnumerable<Object> templates, IDictionary<string, object> sessionParameters = null, IOutputFileProvider outputFileProvider = null)
        {
            new Action(() =>
            {
                textTransformation.SetUp();
                textTransformation.RunTemplates(new TemplateRunner(), templates, sessionParameters, outputFileProvider);
            })
            .GetRunner()
            .OnError((e) => HandleException(textTransformation, e))
            .OnComplete((passed) => textTransformation.CleanUp())
            .Run();
        }
        
        public static void RunTemplate(this TextTransformation textTransformation, Object template, IDictionary<string, object> sessionParameters = null, IOutputFileProvider outputFileProvider = null)
        {
            new Action(() =>
            {
                textTransformation.SetUp();
                textTransformation.RunTemplate(new TemplateRunner(), template, sessionParameters, outputFileProvider);
            })
            .GetRunner()
            .OnError((e) => HandleException(textTransformation, e))
            .OnComplete((passed) => textTransformation.CleanUp())
            .Run();
        }

        public static void RunTemplates(this TextTransformation textTransformation, ITemplateRunner templateRunner, ITemplateProvider templateProvider, IDictionary<string, object> sessionParameters = null, IOutputFileProvider outputFileProvider = null)
        {
            new Action(() =>
            {
                textTransformation.SetUp();
                textTransformation.RunTemplates(
                    templateRunner,
                    templateProvider.GetTemplates(textTransformation),
                    sessionParameters,
                    outputFileProvider);
            })
            .GetRunner()
            .OnError((e) => HandleException(textTransformation, e))
            .OnComplete((passed) => textTransformation.CleanUp())
            .Run();
        }

        public static void RunTemplates(this TextTransformation textTransformation, ITemplateRunner templateRunner, IEnumerable<Type> templateTypes, IDictionary<string, object> sessionParameters = null, IOutputFileProvider outputFileProvider = null)
        {
            new Action(() =>
            {
                textTransformation.SetUp();
                foreach (var templateType in templateTypes)
                {
                    textTransformation.RunTemplate(templateRunner, templateType, sessionParameters, outputFileProvider);
                }
            })
            .GetRunner()
            .OnError((e) => HandleException(textTransformation, e))
            .OnComplete((passed) => textTransformation.CleanUp())
            .Run();
        }

        public static void RunTemplate(this TextTransformation textTransformation, ITemplateRunner templateRunner, Type templateType, IDictionary<string, object> sessionParameters = null, IOutputFileProvider outputFileProvider = null)
        {
            new Action(() =>
            {
                textTransformation.SetUp();
                var template = Activator.CreateInstance(templateType);
                textTransformation.RunTemplate(templateRunner, template, sessionParameters, outputFileProvider);
            })
            .GetRunner()
            .OnError((e) => HandleException(textTransformation, e))
            .OnComplete((passed) => textTransformation.CleanUp())
            .Run();
        }

        public static void RunTemplate<T>(this TextTransformation textTransformation, ITemplateRunner templateRunner, IDictionary<string, object> sessionParameters = null, IOutputFileProvider outputFileProvider = null)
        {
            new Action(() =>
            {
                textTransformation.SetUp();
                textTransformation.RunTemplate(templateRunner, typeof(T), sessionParameters, outputFileProvider);
            })
            .GetRunner()
            .OnError((e) => HandleException(textTransformation, e))
            .OnComplete((passed) => textTransformation.CleanUp())
            .Run();
        }

        public static void RunTemplates(this TextTransformation textTransformation, ITemplateRunner templateRunner, IEnumerable<Object> templates, IDictionary<string, object> sessionParameters = null, IOutputFileProvider outputFileProvider = null)
        {
            new Action(() =>
            {
                textTransformation.SetUp();
                foreach (var template in templates)
                {
                    textTransformation.RunTemplate(templateRunner, template, sessionParameters, outputFileProvider);
                }
            })
            .GetRunner()
            .OnError((e) => HandleException(textTransformation, e))
            .OnComplete((passed) => textTransformation.CleanUp())
            .Run();
        }

        public static void RunTemplate(this TextTransformation textTransformation, ITemplateRunner templateRunner, Object template, IDictionary<string, object> sessionParameters = null, IOutputFileProvider outputFileProvider = null)
        {
            new Action(() =>
            {
                textTransformation.SetUp();
                templateRunner.RunTemplate(textTransformation, template, sessionParameters, outputFileProvider);
            })
            .GetRunner()
            .OnError((e) => HandleException(textTransformation, e))
            .OnComplete((passed) => textTransformation.CleanUp())
            .Run();
        }

        private static void HandleException(TextTransformation textTransformation, Exception e)
        {
            try
            {
                LogManager.GetLog().Debug(e.ToString());
                textTransformation.WriteLine(e.ToString());
            }
            catch (Exception)
            {
            }
        }

        //private static void AssertContext(Object textTransformation)
        //{
        //    if (TextTransformationContext.Instance == null)
        //    {
        //        if (textTransformation is TextTransformation)
        //        {
        //            ((TextTransformation)textTransformation).CreateContext();
        //        }
        //        else
        //        {
        //            throw new InvalidOperationException("No Context exists. Use 'using (var context = this.CreateContext())' within a normal template.");
        //        }
        //    }
        //}

        //public static void UseNewCSharpFile(this Object textTransformation, String fileName, Project destinationProject = null)
        //{
        //    textTransformation.UseNewCSharpFile(fileName, null, destinationProject);
        //}

        //public static void UseNewCSharpFile(this Object textTransformation, String fileName, String folderWithinProject = null, Project destinationProject = null)
        //{
        //    AssertContext(textTransformation);
        //    if (!Path.HasExtension(fileName))
        //    {
        //        fileName += ".cs";
        //    }
        //    textTransformation.UseNewFile(fileName, folderWithinProject, destinationProject, BuildActionTypeEnum.Compile);
        //}

        //public static void UseNewCSharpFile(this Object textTransformation, String fileName, String folderWithinProject = null, String destinationProjectName = null)
        //{
        //    AssertContext(textTransformation);
        //    if (!Path.HasExtension(fileName))
        //    {
        //        fileName += ".cs";
        //    }
        //    textTransformation.UseNewFile(fileName, folderWithinProject, destinationProjectName, BuildActionTypeEnum.Compile);
        //}

        //public static void UseNewFile(this Object textTransformation, String fileName, String folderWithinProject = null, String destinationProjectName = null, BuildActionTypeEnum? buildAction = null, String customTool = null, bool? autoFormat = null, bool? overwrite = null, Encoding encoding = null)
        //{
        //    AssertContext(textTransformation);
        //    var project = String.IsNullOrEmpty(destinationProjectName)
        //        ? null
        //        : TextTransformationContext.Instance.VisualStudio.GetProject(destinationProjectName);

        //    textTransformation.UseNewFile(fileName, folderWithinProject, project, buildAction, customTool, autoFormat, overwrite, encoding);
        //}

        //public static void UseNewFile(this Object textTransformation, String fileName, String folderWithinProject = null, Project destinationProject = null, BuildActionTypeEnum? buildAction = null, String customTool = null, bool? autoFormat = null, bool? overwrite = null, Encoding encoding = null)
        //{
        //    AssertContext(textTransformation);
        //    var outputFile = new OutputFile()
        //    {
        //        FileNameWithoutExtension = Path.GetFileNameWithoutExtension(fileName),
        //        FileExtension = Path.GetExtension(fileName),
        //        DestinationProject = destinationProject ?? TextTransformationContext.Instance.CallingTemplateProjectItem.ContainingProject,
        //        FolderPathWithinProject = folderWithinProject,
        //        CustomTool = customTool
        //    };
        //    outputFile.BuildAction = buildAction ?? outputFile.BuildAction;
        //    outputFile.CanOverrideExistingFile = overwrite ?? outputFile.CanOverrideExistingFile;
        //    outputFile.AutoFormat = autoFormat ?? outputFile.AutoFormat;
        //    outputFile.Encoding = encoding ?? outputFile.Encoding;

        //    textTransformation.UseNewFile(outputFile);
        //}

        //public static void UseNewFile(this Object textTransformation, OutputFile outputFile)
        //{
        //    AssertContext(textTransformation);
        //    TextTransformationContext.Instance.StartFile(new TextTransformationWrapper(textTransformation), outputFile);
        //}

        //public static void UseDefaultFile(this Object textTransformation)
        //{
        //    AssertContext(textTransformation);
        //    TextTransformationContext.Instance.EndFile();
        //}
    }
}
