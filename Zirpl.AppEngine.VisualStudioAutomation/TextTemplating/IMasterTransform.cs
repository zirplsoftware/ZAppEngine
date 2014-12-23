using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using Microsoft.VisualStudio.TextTemplating;

namespace Zirpl.AppEngine.VisualStudioAutomation.TextTemplating
{
    public interface IMasterTransform : ITransform
    {
        ITextTemplatingEngineHost Host { get; }
        IOutputFileManager FileManager { get; }
        ITransform GetChild(Object childTemplate);
    }
}
