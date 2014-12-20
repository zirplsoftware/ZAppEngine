using System;
using System.IO;
using System.Linq;
using EnvDTE;
using Zirpl.AppEngine.VisualStudioAutomation.AppGeneration.Config;
using Zirpl.AppEngine.VisualStudioAutomation.TextTemplating;

namespace Zirpl.AppEngine.VisualStudioAutomation.AppGeneration.TextTemplating
{
    internal class OutputFileProvider
    {
        private Type type;
        private App app;
        private DomainType domainType;

        internal OutputFileProvider(TemplateBase template)
        {
            this.type = template.GetType();
            this.app = template.App;
            if (template is OncePerDomainTypeTemplate)
            {
                this.domainType = ((OncePerDomainTypeTemplate) template).DomainType;
            }
        }

        internal OutputFile GetOutputFile()
        {
            var fileName = GetFileName();
            var destinationProject = GetProject();
            var folder = GetFolderPathWithinProject();

            var outputFile = new OutputFile()
            {
                FileNameWithoutExtension = Path.GetFileNameWithoutExtension(fileName),
                FileExtension = Path.GetExtension(fileName),
                DestinationProject = destinationProject,
                FolderPathWithinProject = folder
            };
            outputFile.MatchBuildActionToFileExtension();
            return outputFile;
        }


        private String GetFileName()
        {
            // let's see if we can determine the filename, folder, and project by convention
            String fileName = null;
            if (type.Name.Contains("_"))
            {
                var tokens = type.Name.Split('_');
                if (tokens.Count() >= 2
                    || tokens.Count() <= 4)
                {
                    // yes, we can determine the fileName
                    for (int i = 0; i < tokens.Length; i++)
                    {
                        if (i != tokens.Length - 1
                            && tokens[i].ToLowerInvariant() == "dt")
                        {
                            tokens[i] = domainType != null ? domainType.Name : null;
                        }
                        if (i == tokens.Length - 1)
                        {
                            // remove "template" at the end if it was included in the name
                            tokens[i] = tokens[i].SubstringUntilLastInstanceOf("template",
                                StringComparison.InvariantCultureIgnoreCase);
                            tokens[i] = "." + tokens[i];
                            if (tokens[i] == ".")
                            {
                                // assume it is CSharp
                                tokens[i] = ".cs";
                            }
                        }
                    }
                    foreach (var token in tokens)
                    {
                        fileName += token;
                    }
                }
            }
            return fileName;
        }

        private Project GetProject()
        {
            var whichProject = (type.Namespace + "." + type.Name)
                .SubstringAfterLastInstanceOf("_templates.", StringComparison.InvariantCultureIgnoreCase)
                .SubstringUntilFirstInstanceOf("Project", StringComparison.InvariantCultureIgnoreCase);
            var whichProjectLower = whichProject.ToLower();
            if (whichProjectLower == "model")
            {
                return app.ModelProject;
            }
            else if (whichProjectLower == "dataservice")
            {
                return app.DataServiceProject;
            }
            else if (whichProjectLower == "service")
            {
                return app.ServiceProject;
            }
            else if (whichProjectLower == "webcommon")
            {
                return app.WebCommonProject;
            }
            else if (whichProjectLower == "web")
            {
                return app.WebProject;
            }
            else if (whichProjectLower == "testscommon")
            {
                return app.TestsCommonProject;
            }
            else if (whichProjectLower == "dataservicetests")
            {
                return app.DataServiceTestsProject;
            }
            else if (whichProjectLower == "servicetests")
            {
                return app.ServiceTestsProject;
            }
            else if (domainType != null)
            {
                return domainType.DestinationProject;
            }
            else
            {
                return app.CodeGenerationProject;
            }
        }

        private String GetFolderPathWithinProject()
        {
            var immediateFolder = type.Namespace
                .SubstringAfterLastInstanceOf("_templates.", StringComparison.InvariantCultureIgnoreCase)
                .SubstringAfterFirstInstanceOf("Project.", StringComparison.InvariantCultureIgnoreCase)
                .Replace('.', '\\');
            if (domainType != null)
            {
                // combine the immediate folder of the template
                // with the subnamespace of the DomainType
                //
                immediateFolder = Path.Combine(immediateFolder, domainType.DestinationProject.GetFolderPathFromNamespace(domainType.Namespace).Replace('.', '\\'));
            }
            return immediateFolder;
        }
    }
}
