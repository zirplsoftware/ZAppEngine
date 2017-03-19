using EnvDTE;
using Zirpl.AppEngine.VisualStudioAutomation.VisualStudio;

namespace Zirpl.AppEngine.VisualStudioAutomation.TextTemplating
{
    public static class TransformHostExtensions
    {
        public static ProjectItem GetProjectItem(this ITransformHost host)
        {
            return host.HostTransform.GetDTE().Solution.GetProjectItem(host.Host.TemplateFile);
        }
    }
}
