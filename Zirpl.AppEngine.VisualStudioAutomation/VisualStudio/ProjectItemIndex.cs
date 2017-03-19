using System;
using EnvDTE;

namespace Zirpl.AppEngine.VisualStudioAutomation.VisualStudio
{
    public sealed class ProjectItemIndex
    {
        internal ProjectItemIndex(ProjectItem projectItem)
        {
            if (projectItem == null) throw new ArgumentNullException("projectItem");

            this.ProjectItem = projectItem;
        }

        internal ProjectItem ProjectItem { get; private set; }
    }
}
