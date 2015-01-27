using System;
using System.Collections.Generic;
using System.IO;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using Zirpl.AppEngine.VisualStudioAutomation.TextTemplating;
using Zirpl.AppEngine.VisualStudioAutomation.VisualStudio;

namespace Zirpl.AppEngine.VisualStudioAutomation.AppGeneration.TextTemplating
{
    internal sealed class OutputInfoToClassInfoProvider : IClassInfoProvider
    {
        private readonly ITransform _transform;

        internal OutputInfoToClassInfoProvider(ITransform transform)
        {
            _transform = transform;
        }

        public string GetNamespace()
        {
            var outputInfo = new OutputInfoProvider()
                .GetOutputInfo(_transform);
            var project = _transform.GetDTE().GetAllProjects()
                .SingleOrDefault(o => o.FullName.ToLowerInvariant() == outputInfo.DestinationProjectFullName.ToLowerInvariant());
            var namespaceBuilder = new StringBuilder();
            if (project != null)
            {
                namespaceBuilder.Append(project.GetDefaultNamespace());
            }
            var folderPathWithinProject = outputInfo.FolderPathWithinProject;
            if (!String.IsNullOrEmpty(folderPathWithinProject))
            {
                if (namespaceBuilder.Length > 0)
                {
                    namespaceBuilder.Append(".");
                }
                namespaceBuilder.Append(folderPathWithinProject.Replace(@"\", "."));
            }
            return namespaceBuilder.ToString();
        }

        public string GetClassName()
        {
            return new OutputInfoProvider()
                .GetOutputInfo(_transform)
                .FileNameWithoutExtension;
        }
    }
}
