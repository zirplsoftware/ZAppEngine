using System;

namespace Zirpl.AppEngine.VisualStudioAutomation.TextTemplating
{
    public interface IOutputFileManager : IDisposable
    {
        void StartFile(Object template, OutputFile file);
        void EndFile();
    }
}