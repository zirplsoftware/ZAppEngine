using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Zirpl.AppEngine.VisualStudioAutomation.TextTemplating
{
    public interface ITemplateRunner
    {
        void RunTemplates(IOutputFileManager fileManager, ITemplateProvider templateProvider, IOutputFileProvider outputFileProvider);
    }
}
