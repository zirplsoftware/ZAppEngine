using EnvDTE;
using Zirpl.AppEngine.VisualStudioAutomation.TextTemplating;

namespace Zirpl.AppEngine.VisualStudioAutomation.VisualStudio
{
    public static class TransformHostExtensions
    {
        public static ProjectItem GetProjectItem(this ITransformHost host)
        {
            return host.HostTransform.GetDTE().Solution.GetProjectItem(host.Host.TemplateFile);
        }
    }
}
