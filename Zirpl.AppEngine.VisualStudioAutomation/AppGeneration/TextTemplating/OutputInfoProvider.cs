﻿using System;
using System.IO;
using Zirpl.AppEngine.VisualStudioAutomation.AppGeneration.Config;
using Zirpl.AppEngine.VisualStudioAutomation.TextTemplating;
using Zirpl.AppEngine.VisualStudioAutomation.VisualStudio;

namespace Zirpl.AppEngine.VisualStudioAutomation.AppGeneration.TextTemplating
{
    internal sealed class OutputInfoProvider : IOutputInfoProvider
    {
        public OutputInfo GetOutputInfo(ITransform transform)
        {
            if (transform == null) throw new ArgumentNullException("transform");

            App app = null;
            DomainType domainType = null;

            if (transform.Session.ContainsKey("App"))
            {
                app = (App)transform.Session["App"];
            }
            if (transform.Session.ContainsKey("DomainType"))
            {
                domainType = (DomainType)transform.Session["DomainType"];
            }

            var fileName = GetFileName(transform.Template.GetType().Name, domainType == null ? null : domainType.Name);
            var destinationProject = GetProjectIndex(app, transform.Template.GetType().Namespace, domainType);
            var folder = GetFolderPathWithinProject(transform.Template.GetType().Name, domainType);

            var outputFile = new OutputInfo()
            {
                FileNameWithoutExtension = Path.GetFileNameWithoutExtension(fileName),
                FileExtension = Path.GetExtension(fileName),
                DestinationProjectIndex = destinationProject,
                FolderPathWithinProject = folder
            };
            outputFile.MatchBuildActionToFileExtension();
            return outputFile;
        }


        private String GetFileName(String templateName, String domainTypeName)
        {
            if (domainTypeName.HasContent())
            {
                // rules for OncePerDomainType file name:
                //      - see unit tests for rules
                if (templateName.IndexOf("_dt_", 0, StringComparison.InvariantCultureIgnoreCase) < 0
                        && !templateName.StartsWith("dt_", StringComparison.InvariantCultureIgnoreCase)
                        && !templateName.EndsWith("_dt", StringComparison.InvariantCultureIgnoreCase)
                        && !templateName.ToLowerInvariant().Equals("dt"))
                {
                    throw new Exception("A Once-per-DomainType template has been named without a DomainType replacement token (_dt_, _dt, or dt_): " + templateName);
                }
                // check if there are characters after the dt
                if (templateName.ToLowerInvariant().Equals("dt"))
                {
                    return domainTypeName + ".cs";
                }
                else if (templateName.EndsWith("_dt", StringComparison.InvariantCultureIgnoreCase)
                    || templateName.EndsWith("_", StringComparison.InvariantCultureIgnoreCase))
                {
                    // we will be supplying the default extension, so just replace
                    if (templateName.StartsWith("dt_", StringComparison.InvariantCultureIgnoreCase))
                    {
                        return templateName
                            .ReplaceAtStart("dt_", domainTypeName, StringComparison.InvariantCultureIgnoreCase) 
                            + ".cs";
                    }
                    else if (templateName.IndexOf("_dt_", 0, StringComparison.InvariantCultureIgnoreCase) >= 0)
                    {
                        return
                            templateName
                            .ReplaceFirstInstanceOf("_dt_", domainTypeName,StringComparison.InvariantCultureIgnoreCase)
                            + ".cs";
                    }
                    else //type.Name.EndsWith("_dt", StringComparison.InvariantCultureIgnoreCase)
                    {
                        return templateName
                            .ReplaceAtEnd("_dt", domainTypeName, StringComparison.InvariantCultureIgnoreCase) 
                            + ".cs";
                    }
                }
                else
                {
                    // now it ALWAYS ends in some ALPHANUMERIC text OTHER than _dt, 
                    // so we have an extension too, so we need to pay attention to how we replace
                    var typeNameWithExtension = templateName.Replace(templateName.LastIndexOf("_", StringComparison.InvariantCultureIgnoreCase), 1, ".");
                    if (typeNameWithExtension.StartsWith("dt_", StringComparison.InvariantCultureIgnoreCase))
                    {
                        return typeNameWithExtension.ReplaceAtStart("dt_", domainTypeName,
                            StringComparison.InvariantCultureIgnoreCase);
                    }
                    else if (typeNameWithExtension.StartsWith("dt.", StringComparison.InvariantCultureIgnoreCase))
                    {
                        return typeNameWithExtension.ReplaceAtStart("dt.", domainTypeName + ".",
                            StringComparison.InvariantCultureIgnoreCase);
                    }
                    else if (typeNameWithExtension.IndexOf("_dt_", 0, StringComparison.InvariantCultureIgnoreCase) >= 0)
                    {
                        return typeNameWithExtension.ReplaceFirstInstanceOf("_dt_", domainTypeName,
                            StringComparison.InvariantCultureIgnoreCase);
                    }
                    else //if (typeNameWithExtension.IndexOf("_dt.", 0, StringComparison.InvariantCultureIgnoreCase) >= 0)
                    {
                        return typeNameWithExtension.ReplaceFirstInstanceOf("_dt.", domainTypeName + ".",
                            StringComparison.InvariantCultureIgnoreCase);
                    }
                }
            }
            else
            {
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
        }

        private ProjectIndex GetProjectIndex(App app, String templateTypeNamespace, DomainType domainType)
        {
            var subNamespace = (templateTypeNamespace + ".")
                .SubstringAfterLastInstanceOf("_templates.", StringComparison.InvariantCultureIgnoreCase);
            if (subNamespace.StartsWith("_"))
            {
                var whichProjectLower = subNamespace
                    .SubstringAfterFirstInstanceOf("_")
                    .SubstringUntilFirstInstanceOf(".")
                    .ToLower();
                if (whichProjectLower == "model")
                {
                    return app.ModelProjectIndex;
                }
                else if (whichProjectLower == "dataservice")
                {
                    return app.DataServiceProjectIndex;
                }
                else if (whichProjectLower == "service")
                {
                    return app.ServiceProjectIndex;
                }
                else if (whichProjectLower == "webcommon")
                {
                    return app.WebCommonProjectIndex;
                }
                else if (whichProjectLower == "web")
                {
                    return app.WebProjectIndex;
                }
                else if (whichProjectLower == "testscommon")
                {
                    return app.TestsCommonProjectIndex;
                }
                else if (whichProjectLower == "dataservicetests")
                {
                    return app.DataServiceTestsProjectIndex;
                }
                else if (whichProjectLower == "servicetests")
                {
                    return app.ServiceTestsProjectIndex;
                }
                else if (domainType != null)
                {
                    return domainType.DestinationProjectIndex;
                }
                else
                {
                    return app.CodeGenerationProjectIndex;
                }
            }
            else
            {
                if (domainType != null)
                {
                    return domainType.DestinationProjectIndex;
                }
                else
                {
                    return app.CodeGenerationProjectIndex;
                }
            }
        }

        private String GetFolderPathWithinProject(String templateTypeNamespace, DomainType domainType)
        {
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

            if (domainType != null)
            {
                // combine the immediate folder of the template
                // with the subnamespace of the DomainType
                //
                immediateFolder = Path.Combine(immediateFolder, domainType.DestinationProjectIndex.Project.GetFolderPathFromNamespace(domainType.Namespace).Replace('.', '\\'));
            }
            return immediateFolder;
        }
    }
}
