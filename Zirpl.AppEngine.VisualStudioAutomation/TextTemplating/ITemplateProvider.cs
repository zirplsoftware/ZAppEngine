using System;
using System.Collections.Generic;
using Microsoft.VisualStudio.TextTemplating;

namespace Zirpl.AppEngine.VisualStudioAutomation.TextTemplating
{
    public interface ITemplateProvider
    {
        IEnumerable<Type> GetTemplates(TextTransformation textTransformation);
    }
}
