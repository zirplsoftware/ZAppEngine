using System;
using System.Collections.Generic;

namespace Zirpl.AppEngine.VisualStudioAutomation.TextTemplating
{
    public interface ITemplateProvider
    {
        IEnumerable<Type> GetTemplateTypes();
    }
}
