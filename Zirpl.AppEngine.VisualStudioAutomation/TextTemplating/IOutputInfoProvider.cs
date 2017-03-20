using System;

namespace Zirpl.AppEngine.VisualStudioAutomation.TextTemplating
{
    public interface IOutputInfoProvider
    {
        OutputInfo GetOutputInfo(ITransform transform);
        OutputInfo GetOutputInfo(ITransform transform, Type templateType);
        OutputInfo GetOutputInfo<T>(ITransform transform) where T : class;
    }
}
