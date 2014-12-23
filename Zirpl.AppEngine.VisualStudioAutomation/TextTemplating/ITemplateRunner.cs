using System;
using System.Collections.Generic;
using Microsoft.VisualStudio.TextTemplating;

namespace Zirpl.AppEngine.VisualStudioAutomation.TextTemplating
{
    public interface ITemplateRunner
    {
        void RunTemplate(TextTransformation textTransformation, Object template, IDictionary<string, object> sessionParameters = null, IOutputFileProvider outputFileProvider = null);
    }
}
