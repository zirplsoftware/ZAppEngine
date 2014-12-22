using System;
using System.Collections.Concurrent;
using System.Collections.Generic;
using System.IO;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using EnvDTE;
using EnvDTE80;
using Microsoft.VisualStudio.PlatformUI;
using Microsoft.VisualStudio.TextTemplating;
using Zirpl.AppEngine.Logging;
using Zirpl.AppEngine.VisualStudioAutomation.AppGeneration;
using Zirpl.AppEngine.VisualStudioAutomation.AppGeneration.TextTemplating;
using Zirpl.AppEngine.VisualStudioAutomation.TextTemplating;
using Zirpl.Reflection;

namespace Zirpl.AppEngine.VisualStudioAutomation.VisualStudio
{
    public static class TextTransformationExtensions
    {
        public static DTE2 GetDTE(this TextTransformation textTransformation)
        {
            textTransformation.SetUp();
            return (DTE2)((IServiceProvider)textTransformation.Wrap().Host).GetCOMService(typeof(DTE));
        }

        public static ProjectItem GetProjectItem(this TextTransformation textTransformation)
        {
            textTransformation.SetUp();
            return textTransformation.GetDTE().Solution.GetProjectItem(textTransformation.Wrap().Host.TemplateFile);
        }
    }
}
