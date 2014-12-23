using System;
using System.Collections.Generic;

namespace Zirpl.AppEngine.VisualStudioAutomation.TextTemplating
{
    public interface ITemplateRunner
    {
        void RunTemplate(ITransform transform, Object template, IDictionary<string, object> sessionParameters = null, IOutputInfoProvider outputFileProvider = null);
    }
}
