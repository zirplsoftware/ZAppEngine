using System;
using System.Collections.Generic;
using Zirpl.AppEngine.VisualStudioAutomation.AppGeneration.Config;
using Zirpl.AppEngine.VisualStudioAutomation.TextTemplating;

namespace Zirpl.AppEngine.VisualStudioAutomation.AppGeneration.TextTemplating
{
    public interface IOutputFileBuilder
    {
        IEnumerable<OutputFile> BuildOutputFiles(App app);
        OutputFile BuildOutputFile(App app, DomainType domainType);
        String Key { get; }
    }
}
