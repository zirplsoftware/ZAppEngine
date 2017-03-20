using System;
using System.IO;
using System.Linq;
using System.Text;
using Zirpl.AppEngine.VisualStudioAutomation.Utilities;
using Zirpl.AppEngine.VisualStudioAutomation.VisualStudio;

namespace Zirpl.AppEngine.VisualStudioAutomation.TextTemplating
{
    public class OutputInfoProvider : IOutputInfoProvider
    {
        public OutputInfo GetOutputInfo(ITransform transform)
        {
            if (transform == null) throw new ArgumentNullException("transform");
            var outputFile = CreateOutputInfo(transform);

            var fileName = GetFileName(transform) ?? transform.Template.GetType().Name + ".txt";
            var destinationProject = GetProjectFullName(transform) ?? transform.Host.GetProjectItem().ContainingProject.FullName;
            var folder = GetFolderPathWithinProject(transform);

            outputFile.FileNameWithoutExtension = Path.GetFileNameWithoutExtension(fileName);
            outputFile.FileExtension = Path.GetExtension(fileName);
            outputFile.DestinationProjectFullName = destinationProject;
            outputFile.FolderPathWithinProject = folder;

            if (outputFile is DotNetTypeOutputInfo)
            {
                SetNamespace(transform, (DotNetTypeOutputInfo) outputFile);
            }

            outputFile.MatchBuildActionToFileExtension();
            return outputFile;
        }

        private void SetNamespace(ITransform transform, DotNetTypeOutputInfo outputInfo)
        {
            var project = transform.GetDTE()
                    .GetAllProjects()
                    .SingleOrDefault(
                        o => o.FullName.EndsWith(outputInfo.DestinationProjectFullName + ".csproj", StringComparison.InvariantCultureIgnoreCase));
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
            outputInfo.Namespace = namespaceBuilder.ToString();
        }

        private OutputInfo CreateOutputInfo(ITransform transform)
        {
            var templateName = transform.Template.GetType().Name;
            if (templateName.EndsWith("_cs"))
            {
                return new DotNetTypeOutputInfo();
            }
            else
            {
                return new OutputInfo();
            }
        }

        protected virtual string GetFileName(ITransform transform)
        {
            var templateName = transform.Template.GetType().Name;
            // rules for OncePerApp file name (taken from the unit tests)
            //     check unit tests for rules
            if (templateName.Contains("_"))
            {
                if (templateName.EndsWith("_"))
                {
                    return templateName + ".cs";
                }
                else
                {
                    // get the index of the last _
                    // and make sure there is at least 1 alphanumeric char before that
                    var index = templateName.LastIndexOf("_", StringComparison.InvariantCultureIgnoreCase);
                    var foundOneAlphaNumericCharBeforeIt = false;
                    while (index >= 0
                        && !foundOneAlphaNumericCharBeforeIt)
                    {
                        if (Char.IsLetterOrDigit(templateName[index]))
                        {
                            foundOneAlphaNumericCharBeforeIt = true;
                        }
                        index--;
                    }
                    if (foundOneAlphaNumericCharBeforeIt)
                    {
                        // okay, we have a valid . substitution
                        return templateName.Replace(templateName.LastIndexOf("_", StringComparison.InvariantCultureIgnoreCase), 1,
                                ".");
                    }
                    else
                    {
                        return templateName + ".cs";
                    }
                }
            }
            else
            {
                return templateName + ".cs";
            }
        }

        protected virtual string GetProjectFullName(ITransform transform) //, String templateTypeNamespace, DomainType domainType)
        {
            var templateTypeNamespace = transform.Template.GetType().Namespace;
            var subNamespace = (templateTypeNamespace + ".")
                .SubstringAfterLastInstanceOf("_templates.", StringComparison.InvariantCultureIgnoreCase);
            if (subNamespace.StartsWith("_"))
            { 
                subNamespace = subNamespace
                    .SubstringAfterFirstInstanceOf("_")
                    .SubstringUntilFirstInstanceOf(".");
                var project = transform.GetDTE()
                    .GetAllProjects()
                    .SingleOrDefault(
                        o => o.FullName.EndsWith(subNamespace + ".csproj", StringComparison.InvariantCultureIgnoreCase));
                return project == null ? null : project.FullName;
            }
            else
            {
                return null;
            }
        }

        protected virtual string GetFolderPathWithinProject(ITransform transform)
        {
            var templateTypeNamespace = transform.Template.GetType().Name;
            String immediateFolder;
            var subNamespace = (templateTypeNamespace + ".")
                .SubstringAfterLastInstanceOf("_templates.", StringComparison.InvariantCultureIgnoreCase);
            if (subNamespace.StartsWith("_"))
            {
                immediateFolder = subNamespace
                    .SubstringAfterFirstInstanceOf(".")
                    .ReplaceAtEnd(".", null)
                    .Replace('.', '\\');
            }
            else
            {
                immediateFolder = subNamespace
                    .ReplaceAtEnd(".", null)
                    .Replace('.', '\\');
            }

            return immediateFolder;
        }

        public OutputInfo GetOutputInfo(ITransform transform, Type templateType)
        {
            var childTransform = transform.Host.HostTransform.GetChild(Activator.CreateInstance(templateType));
            foreach (var pair in transform.Session)
            {
                childTransform.Session[pair.Key] = pair.Value;
            }
            childTransform.Initialize();
            return GetOutputInfo(childTransform);
        }

        public OutputInfo GetOutputInfo<T>(ITransform transform) where T : class
        {
            return GetOutputInfo(transform, typeof(T));
        }
    }
}
