using Microsoft.VisualStudio.TextTemplating;

namespace Zirpl.AppEngine.VisualStudioAutomation.TextTemplating
{
    public interface ITransformHost
    {
        ITextTemplatingEngineHost Host { get; }
        ITransform HostTransform { get; }
    }
}
