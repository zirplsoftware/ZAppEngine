using System;
using System.Collections.Generic;
using Zirpl.AppEngine.VisualStudioAutomation.AppGeneration.Config;
using Zirpl.AppEngine.VisualStudioAutomation.TextTemplating;

namespace Zirpl.AppEngine.VisualStudioAutomation.AppGeneration.TextTemplating
{
    public interface ITemplateOutputFileBuilderStrategy
    {
        IEnumerable<TemplateOutputFile> BuildOutputFiles(App app);
        TemplateOutputFile BuildOutputFile(App app, DomainType domainType);
        String TemplateCategory { get; }
    }
}
