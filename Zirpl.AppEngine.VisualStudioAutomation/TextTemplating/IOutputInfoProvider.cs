using System;
using Microsoft.VisualStudio.TextTemplating;

namespace Zirpl.AppEngine.VisualStudioAutomation.TextTemplating
{
    public interface IOutputInfoProvider
    {
        OutputInfo GetOutputInfo(ITransform transform);
    }
}
