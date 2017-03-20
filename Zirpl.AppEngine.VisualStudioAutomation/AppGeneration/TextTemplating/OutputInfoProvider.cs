using System;
using System.IO;
using System.Linq;
using Zirpl.AppEngine.VisualStudioAutomation.AppGeneration.Model;
using Zirpl.AppEngine.VisualStudioAutomation.TextTemplating;
using Zirpl.AppEngine.VisualStudioAutomation.Utilities;
using Zirpl.AppEngine.VisualStudioAutomation.VisualStudio;
using Zirpl.FluentReflection;

namespace Zirpl.AppEngine.VisualStudioAutomation.AppGeneration.TextTemplating
{
    public sealed class OutputInfoProvider : VisualStudioAutomation.TextTemplating.OutputInfoProvider
    {
        protected override string GetFileName(ITransform transform)
        {
            DomainType domainType = null;
            if (transform.Template.Property<DomainType>("DomainType").Exists)
            {
                domainType = (DomainType)transform.Session["DomainType"];
            }
            
            if (domainType != null)
            {
                var templateName = transform.Template.GetType().Name;
                var domainTypeName = domainType.Name;
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
                return base.GetFileName(transform);
            }
        }

        protected override string GetProjectFullName(ITransform transform)
        {
            DomainType domainType = null;
            if (transform.Template.Property<DomainType>("DomainType").Exists)
            {
                domainType = (DomainType)transform.Session["DomainType"];
            }
            if (domainType != null
                && !string.IsNullOrEmpty(domainType.DestinationProjectFullName))
            {
                return domainType.DestinationProjectFullName;
            }
            return base.GetProjectFullName(transform);
        }

        protected override string GetFolderPathWithinProject(ITransform transform)
        {
            string immediateFolder = base.GetFolderPathWithinProject(transform);

            DomainType domainType = null;
            if (transform.Template.Property<DomainType>("DomainType").Exists)
            {
                domainType = (DomainType)transform.Session["DomainType"];
            }
            if (domainType != null)
            {
                // combine the immediate folder of the template
                // with the subnamespace of the DomainType
                //
                var project = transform.GetDTE().GetAllProjects().Single(o => o.FullName.ToLowerInvariant() == domainType.DestinationProjectFullName.ToLowerInvariant());
                immediateFolder = Path.Combine(immediateFolder, project.GetFolderPathFromNamespace(domainType.Namespace).Replace('.', '\\'));
            }
            return immediateFolder;
        }
    }
}
