using System.Collections.Generic;
using Zirpl.AppEngine.VisualStudioAutomation.AppGeneration.Config;
using Zirpl.AppEngine.VisualStudioAutomation.TextTemplating;

namespace Zirpl.AppEngine.VisualStudioAutomation.AppGeneration
{
    public interface ITemplateOutputFileBuilderStrategy
    {
        IList<TemplateOutputFile> BuildOutputFiles(App app);
        TemplateOutputFile BuildOutputFile(App app, DomainType domainType);
    }
}
