using System;
using EnvDTE;
using EnvDTE80;
using Microsoft.VisualStudio.TextTemplating;
using Zirpl.AppEngine.VisualStudioAutomation.TextTemplating;

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
