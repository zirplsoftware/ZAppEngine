using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using Microsoft.VisualStudio.TextTemplating;

namespace Zirpl.AppEngine.VisualStudioAutomation.TextTemplating
{
    public interface ITemplateProvider
    {
        IEnumerable<Type> GetTemplates(TextTransformation textTransformation);
    }
}
