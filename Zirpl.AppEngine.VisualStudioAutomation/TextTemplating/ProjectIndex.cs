using EnvDTE;

namespace Zirpl.AppEngine.VisualStudioAutomation.TextTemplating
{
    public sealed class ProjectIndex
    {
        internal ProjectIndex(Project project)
        {
            this.Project = project;
        }

        internal Project Project { get; private set; }
    }
}
