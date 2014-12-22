using System;
using System.Collections.Generic;
using System.IO;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using EnvDTE;
using EnvDTE80;
using Microsoft.VisualStudio.PlatformUI;
using Microsoft.VisualStudio.TextTemplating;
using Zirpl.Reflection;

namespace Zirpl.AppEngine.VisualStudioAutomation.TextTemplating
{
    public static class TextTransformationExtensions
    {
        public static ITextTemplatingEngineHost GetHost(this TextTransformation textTransformation)
        {
            return new TextTransformationWrapper(textTransformation).Host;
        }

        public static DTE2 GetVisualStudio(this TextTransformation textTransformation)
        {
            return (DTE2)((IServiceProvider)textTransformation.GetHost()).GetCOMService(typeof(DTE));
        }

        public static ProjectItem GetProjectItem(this TextTransformation textTransformation)
        {
            return textTransformation.GetVisualStudio().Solution.GetProjectItem(textTransformation.GetHost().TemplateFile);
        }

        public static StringBuilder GetGenerationEnvironment(this TextTransformation textTransformation)
        {
            return new TextTransformationWrapper(textTransformation).GenerationEnvironment;
        }

        public static StringBuilder SetGenerationEnvironment(this TextTransformation textTransformation, StringBuilder generationEnvironment)
        {
            return new TextTransformationWrapper(textTransformation).GenerationEnvironment = generationEnvironment;
        }

        public static OutputFileManager GetFileManager(this TextTransformation textTransformation)
        {
            if (textTransformation.Access().HasGet<OutputFileManager>("FileManager"))
            {
                return textTransformation.Access().Property<OutputFileManager>("FileManager");
            }
            else if (textTransformation.Access().HasField<OutputFileManager>("FileManager"))
            {
                return textTransformation.Access().Field<OutputFileManager>("FileManager");
            }
            return null;
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
