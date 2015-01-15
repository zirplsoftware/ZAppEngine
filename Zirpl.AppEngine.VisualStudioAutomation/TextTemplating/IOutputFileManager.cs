using System;
using System.Text;

namespace Zirpl.AppEngine.VisualStudioAutomation.TextTemplating
{
    public interface IOutputFileManager : IDisposable
    {
        void UseDefaultFile(ITransform currentTransform);
        void StartFile(ITransform currentTransform, OutputInfo file);
        void EndFile();

        void StartCSharpFile(ITransform currentTransform, String fileName, String destinationProjectFullName = null, String folderWithinProject = null);
        void StartFile(ITransform currentTransform, String fileName, String destinationProjectFullName = null, String folderWithinProject = null, BuildActionTypeEnum? buildAction = null, String customTool = null, bool? autoFormat = null, bool? overwrite = null, Encoding encoding = null);
    }
}