using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using Zirpl.AppEngine.VisualStudioAutomation.AppGeneration.Config;

namespace Zirpl.AppEngine.VisualStudioAutomation.AppGeneration.TextTemplating
{
    public interface IOutputClassBuilder : IOutputFileBuilder
    {
        OutputClass BuildOutputClass(App app, DomainType domainType);
    }
}
