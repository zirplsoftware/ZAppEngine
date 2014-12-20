using System;
using System.Collections.Generic;
using System.IO;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using EnvDTE80;

namespace Zirpl.AppEngine.VisualStudioAutomation.TextTemplating
{
    public static class OutputFileExtensions
    {
        public static OutputFile MakeCSharpFile(this OutputFile outputFile)
        {
            outputFile.FileExtension = ".cs";
            outputFile.BuildAction = BuildActionTypeEnum.Compile;
            return outputFile;
        }

        public static OutputFile MatchBuildActionToFileExtension(this OutputFile outputFile)
        {
            switch (outputFile.FileExtension.ToLowerInvariant())
            {
                case ".cs":
                    outputFile.BuildAction = BuildActionTypeEnum.Compile;
                    break;
                default:
                    outputFile.BuildAction = BuildActionTypeEnum.None;
                    break;
            }
            return outputFile;
        }
    }
}
