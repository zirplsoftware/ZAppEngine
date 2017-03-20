using System;
using System.Text;
using Zirpl.AppEngine.VisualStudioAutomation.VisualStudio;

namespace Zirpl.AppEngine.VisualStudioAutomation.TextTemplating
{
    public class DotNetTypeOutputInfo : OutputInfo
    {
        public string Namespace
        {
            get
            {
                var project = this.ProjectItemIndex.ProjectItem.ContainingProject;
                var namespaceBuilder = new StringBuilder();
                if (project != null)
                {
                    namespaceBuilder.Append(project.GetDefaultNamespace());
                }
                var folderPathWithinProject = FolderPathWithinProject;
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

        public string TypeName => FileNameWithoutExtension;

        public string FullTypeName
        {
            get
            {
                var nameSpace = Namespace;
                var typeName = TypeName;
                if (!string.IsNullOrEmpty(nameSpace)
                    && !string.IsNullOrEmpty(typeName))
                {
                    return nameSpace + "." + typeName;
                }
                // one is empty, so no . needed
                return nameSpace + typeName;
            }
        }
    }
}
