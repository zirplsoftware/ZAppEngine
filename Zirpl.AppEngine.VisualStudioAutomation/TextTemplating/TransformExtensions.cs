using System;
using EnvDTE;
using EnvDTE80;
using Microsoft.VisualStudio.TextTemplating;

namespace Zirpl.AppEngine.VisualStudioAutomation.TextTemplating
{
    public static class TransformExtensions
    {
        public static DTE2 GetDTE(this ITransform textTransformation)
        {
            return (DTE2)((IServiceProvider)textTransformation.Host.Host).GetCOMService(typeof(DTE));
        }
    }
}
