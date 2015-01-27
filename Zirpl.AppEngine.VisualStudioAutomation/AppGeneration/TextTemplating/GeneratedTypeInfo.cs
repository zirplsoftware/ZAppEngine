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
    internal sealed class GeneratedTypeInfo : IGeneratedTypeInfo
    {
        private readonly ITransform _transform;

        internal GeneratedTypeInfo(ITransform transform)
        {
            _transform = transform;
        }

        public string Namespace
        {
            get
            {
                var outputInfo = new OutputInfoProvider()
                    .GetOutputInfo(_transform);
                var project = _transform.GetDTE().GetAllProjects()
                    .SingleOrDefault(
                        o => o.FullName.ToLowerInvariant() == outputInfo.DestinationProjectFullName.ToLowerInvariant());
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
        }

        public string TypeName
        {
            get
            {
                return new OutputInfoProvider()
                    .GetOutputInfo(_transform)
                    .FileNameWithoutExtension;
            }
        }

        public String FullTypeName
        {
            get
            {
                var nameSpace = this.Namespace;
                var typeName = this.TypeName;
                if (!nameSpace.IsNullOrEmpty()
                    && !typeName.IsNullOrEmpty())
                {
                    return nameSpace + "." + typeName;
                }
                // one is empty, so no . needed
                return nameSpace + typeName;
            }
        }
    }
}
