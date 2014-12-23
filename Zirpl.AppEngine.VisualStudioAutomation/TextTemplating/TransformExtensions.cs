using System;
using System.Collections.Generic;
using Microsoft.VisualStudio.TextTemplating;
using Zirpl.AppEngine.Logging;
using Zirpl.AppEngine.VisualStudioAutomation.VisualStudio.Logging;

namespace Zirpl.AppEngine.VisualStudioAutomation.TextTemplating
{
    public static class TransformExtensions
    {
        

        public static void RunTemplates(this ITransform transform, ITemplateProvider templateProvider, IDictionary<string, object> sessionParameters = null, IOutputInfoProvider outputFileProvider = null)
        {
            new Action(() =>
            {
                transform.RunTemplates(new TemplateRunner(), templateProvider, sessionParameters, outputFileProvider);
            })
            .GetRunner()
            .OnError((e) => HandleException(transform, e))
            .OnComplete((passed) => transform.FileManager.EndFile())
            .Run();
        }

        public static void RunTemplates(this ITransform transform, IEnumerable<Type> templateTypes, IDictionary<string, object> sessionParameters = null, IOutputInfoProvider outputFileProvider = null)
        {
            new Action(() =>
            {
                transform.RunTemplate(new TemplateRunner(), templateTypes, sessionParameters, outputFileProvider);
            })
            .GetRunner()
            .OnError((e) => HandleException(transform, e))
            .OnComplete((passed) => transform.FileManager.EndFile())
            .Run();
        }

        public static void RunTemplate(this ITransform transform, Type templateType, IDictionary<string, object> sessionParameters = null, IOutputInfoProvider outputFileProvider = null)
        {
            new Action(() =>
            {
                transform.RunTemplate(new TemplateRunner(), templateType, sessionParameters, outputFileProvider);
            })
            .GetRunner()
            .OnError((e) => HandleException(transform, e))
            .OnComplete((passed) => transform.FileManager.EndFile())
            .Run();
        }

        public static void RunTemplate<T>(this ITransform transform, IDictionary<string, object> sessionParameters = null, IOutputInfoProvider outputFileProvider = null)
        {
            new Action(() =>
            {
                transform.RunTemplate(new TemplateRunner(), typeof(T), sessionParameters, outputFileProvider);
            })
            .GetRunner()
            .OnError((e) => HandleException(transform, e))
            .OnComplete((passed) => transform.FileManager.EndFile())
            .Run();
        }

        public static void RunTemplates(this ITransform transform, IEnumerable<Object> templates, IDictionary<string, object> sessionParameters = null, IOutputInfoProvider outputFileProvider = null)
        {
            new Action(() =>
            {
                transform.RunTemplates(new TemplateRunner(), templates, sessionParameters, outputFileProvider);
            })
            .GetRunner()
            .OnError((e) => HandleException(transform, e))
            .OnComplete((passed) => transform.FileManager.EndFile())
            .Run();
        }

        public static void RunTemplate(this ITransform transform, Object template, IDictionary<string, object> sessionParameters = null, IOutputInfoProvider outputFileProvider = null)
        {
            new Action(() =>
            {
                transform.RunTemplate(new TemplateRunner(), template, sessionParameters, outputFileProvider);
            })
            .GetRunner()
            .OnError((e) => HandleException(transform, e))
            .OnComplete((passed) => transform.FileManager.EndFile())
            .Run();
        }

        public static void RunTemplates(this ITransform transform, ITemplateRunner templateRunner, ITemplateProvider templateProvider, IDictionary<string, object> sessionParameters = null, IOutputInfoProvider outputFileProvider = null)
        {
            new Action(() =>
            {
                transform.RunTemplates(
                    templateRunner,
                    templateProvider.GetTemplateTypes(),
                    sessionParameters,
                    outputFileProvider);
            })
            .GetRunner()
            .OnError((e) => HandleException(transform, e))
            .OnComplete((passed) => transform.FileManager.EndFile())
            .Run();
        }

        public static void RunTemplates(this ITransform transform, ITemplateRunner templateRunner, IEnumerable<Type> templateTypes, IDictionary<string, object> sessionParameters = null, IOutputInfoProvider outputFileProvider = null)
        {
            new Action(() =>
            {
                foreach (var templateType in templateTypes)
                {
                    transform.RunTemplate(templateRunner, templateType, sessionParameters, outputFileProvider);
                }
            })
            .GetRunner()
            .OnError((e) => HandleException(transform, e))
            .OnComplete((passed) => transform.FileManager.EndFile())
            .Run();
        }

        public static void RunTemplate(this ITransform transform, ITemplateRunner templateRunner, Type templateType, IDictionary<string, object> sessionParameters = null, IOutputInfoProvider outputFileProvider = null)
        {
            new Action(() =>
            {
                var template = Activator.CreateInstance(templateType);
                transform.RunTemplate(templateRunner, template, sessionParameters, outputFileProvider);
            })
            .GetRunner()
            .OnError((e) => HandleException(transform, e))
            .OnComplete((passed) => transform.FileManager.EndFile())
            .Run();
        }

        public static void RunTemplate<T>(this ITransform transform, ITemplateRunner templateRunner, IDictionary<string, object> sessionParameters = null, IOutputInfoProvider outputFileProvider = null)
        {
            new Action(() =>
            {
                transform.RunTemplate(templateRunner, typeof(T), sessionParameters, outputFileProvider);
            })
            .GetRunner()
            .OnError((e) => HandleException(transform, e))
            .OnComplete((passed) => transform.FileManager.EndFile())
            .Run();
        }

        public static void RunTemplates(this ITransform transform, ITemplateRunner templateRunner, IEnumerable<Object> templates, IDictionary<string, object> sessionParameters = null, IOutputInfoProvider outputFileProvider = null)
        {
            new Action(() =>
            {
                foreach (var template in templates)
                {
                    transform.RunTemplate(templateRunner, template, sessionParameters, outputFileProvider);
                }
            })
            .GetRunner()
            .OnError((e) => HandleException(transform, e))
            .OnComplete((passed) => transform.FileManager.EndFile())
            .Run();
        }

        public static void RunTemplate(this ITransform transform, ITemplateRunner templateRunner, Object template, IDictionary<string, object> sessionParameters = null, IOutputInfoProvider outputFileProvider = null)
        {
            new Action(() =>
            {
                templateRunner.RunTemplate(transform, template, sessionParameters, outputFileProvider);
            })
            .GetRunner()
            .OnError((e) => HandleException(transform, e))
            .OnComplete((passed) => transform.FileManager.EndFile())
            .Run();
        }

        private static void HandleException(ITransform transform, Exception e)
        {
            try
            {
                LogManager.GetLog().Debug(e.ToString());
                transform.Host.HostTransform.GenerationEnvironment.Append(e);
            }
            catch (Exception)
            {
            }
        }

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
