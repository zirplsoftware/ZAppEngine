using System;

namespace Zirpl.AppEngine.VisualStudioAutomation.TextTemplating
{
    public interface IOutputFileManager : IDisposable
    {
        void UseDefaultFile(ITransform currentTransform);
        void StartFile(ITransform currentTransform, OutputInfo file);
        void EndFile();
    }
}