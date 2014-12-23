using System;
using Microsoft.VisualStudio.TextTemplating;

namespace Zirpl.AppEngine.VisualStudioAutomation.TextTemplating
{
    public interface IOutputFileProvider
    {
        OutputFile GetOutputFile(TextTransformation textTransformation, Object template);
    }
}
