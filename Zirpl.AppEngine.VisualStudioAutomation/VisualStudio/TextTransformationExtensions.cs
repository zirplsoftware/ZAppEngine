using System;
using EnvDTE;
using EnvDTE80;
using Microsoft.VisualStudio.TextTemplating;
using Zirpl.AppEngine.VisualStudioAutomation.TextTemplating;

namespace Zirpl.AppEngine.VisualStudioAutomation.VisualStudio
{
    public static class TextTransformationExtensions
    {
        public static DTE2 GetDTE(this ITransform textTransformation)
        {
            return (DTE2)((IServiceProvider)textTransformation.Master.Host).GetCOMService(typeof(DTE));
        }

        public static ProjectItem GetProjectItem(this IMasterTransform textTransformation)
        {
            return textTransformation.GetDTE().Solution.GetProjectItem(textTransformation.Host.TemplateFile);
        }
    }
}
