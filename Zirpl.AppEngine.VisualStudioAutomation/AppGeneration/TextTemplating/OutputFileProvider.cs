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
            if (this.domainType != null)
            {
                // rules for OncePerDomainType file name:
                //      - see unit tests for rules
                if (type.Name.IndexOf("_dt_", 0, StringComparison.InvariantCultureIgnoreCase) < 0
                        && !type.Name.StartsWith("dt_", StringComparison.InvariantCultureIgnoreCase)
                        && !type.Name.EndsWith("_dt", StringComparison.InvariantCultureIgnoreCase)
                        && !type.Name.ToLowerInvariant().Equals("dt"))
                {
                    throw new Exception("A OncePerDomainTypeTemplate has been named without a DomainType replacement token (_dt_, _dt, or dt_): " + type.FullName);
                }
                // check if there are characters after the dt
                if (type.Name.ToLowerInvariant().Equals("dt"))
                {
                    return this.domainType.Name + ".cs";
                }
                else if (type.Name.EndsWith("_dt", StringComparison.InvariantCultureIgnoreCase)
                    || type.Name.EndsWith("_", StringComparison.InvariantCultureIgnoreCase))
                {
                    // we will be supplying the default extension, so just replace
                    if (type.Name.StartsWith("dt_", StringComparison.InvariantCultureIgnoreCase))
                    {
                        return type.Name
                            .ReplaceAtStart("dt_", domainType.Name, StringComparison.InvariantCultureIgnoreCase) 
                            + ".cs";
                    }
                    else if (type.Name.IndexOf("_dt_", 0, StringComparison.InvariantCultureIgnoreCase) >= 0)
                    {
                        return
                            type.Name
                            .ReplaceFirstInstanceOf("_dt_", domainType.Name,StringComparison.InvariantCultureIgnoreCase)
                            + ".cs";
                    }
                    else //type.Name.EndsWith("_dt", StringComparison.InvariantCultureIgnoreCase)
                    {
                        return type.Name
                            .ReplaceAtEnd("_dt", domainType.Name, StringComparison.InvariantCultureIgnoreCase) 
                            + ".cs";
                    }
                }
                else
                {
                    // now it ALWAYS ends in some ALPHANUMERIC text OTHER than _dt, 
                    // so we have an extension too, so we need to pay attention to how we replace
                    var typeNameWithExtension = type.Name.Replace(type.Name.LastIndexOf("_", StringComparison.InvariantCultureIgnoreCase), 1, ".");
                    if (typeNameWithExtension.StartsWith("dt_", StringComparison.InvariantCultureIgnoreCase))
                    {
                        return typeNameWithExtension.ReplaceAtStart("dt_", domainType.Name,
                            StringComparison.InvariantCultureIgnoreCase);
                    }
                    else if (typeNameWithExtension.StartsWith("dt.", StringComparison.InvariantCultureIgnoreCase))
                    {
                        return typeNameWithExtension.ReplaceAtStart("dt.", domainType.Name + ".",
                            StringComparison.InvariantCultureIgnoreCase);
                    }
                    else if (typeNameWithExtension.IndexOf("_dt_", 0, StringComparison.InvariantCultureIgnoreCase) >= 0)
                    {
                        return typeNameWithExtension.ReplaceFirstInstanceOf("_dt_", domainType.Name,
                            StringComparison.InvariantCultureIgnoreCase);
                    }
                    else //if (typeNameWithExtension.IndexOf("_dt.", 0, StringComparison.InvariantCultureIgnoreCase) >= 0)
                    {
                        return typeNameWithExtension.ReplaceFirstInstanceOf("_dt.", domainType.Name + ".",
                            StringComparison.InvariantCultureIgnoreCase);
                    }
                }
            }
            else
            {
                // rules for OncePerApp file name (taken from the unit tests)
                //new OutputFileProvider(new OncePerAppTypes._()).InvokeMethod<String>("GetFileName").Should().Be("_.cs");
                //new OutputFileProvider(new OncePerAppTypes.__()).InvokeMethod<String>("GetFileName").Should().Be("__.cs");
                //new OutputFileProvider(new OncePerAppTypes.___()).InvokeMethod<String>("GetFileName").Should().Be("___.cs");
                //new OutputFileProvider(new OncePerAppTypes.Service()).InvokeMethod<String>("GetFileName").Should().Be("Service.cs");
                //new OutputFileProvider(new OncePerAppTypes._Service()).InvokeMethod<String>("GetFileName").Should().Be("_Service.cs");
                //new OutputFileProvider(new OncePerAppTypes.__Service()).InvokeMethod<String>("GetFileName").Should().Be("__Service.cs");
                //new OutputFileProvider(new OncePerAppTypes._Service_()).InvokeMethod<String>("GetFileName").Should().Be("_Service_.cs");
                //new OutputFileProvider(new OncePerAppTypes.Service_()).InvokeMethod<String>("GetFileName").Should().Be("Service_.cs");
                //new OutputFileProvider(new OncePerAppTypes.Service__()).InvokeMethod<String>("GetFileName").Should().Be("Service__.cs");
                //new OutputFileProvider(new OncePerAppTypes.Service_txt()).InvokeMethod<String>("GetFileName").Should().Be("Service.txt");
                //new OutputFileProvider(new OncePerAppTypes.Service__txt()).InvokeMethod<String>("GetFileName").Should().Be("Service_.txt");
                //new OutputFileProvider(new OncePerAppTypes.Service_txt_()).InvokeMethod<String>("GetFileName").Should().Be("Service_txt_.cs");
                //new OutputFileProvider(new OncePerAppTypes.dt()).InvokeMethod<String>("GetFileName").Should().Be("dt.cs");
                //new OutputFileProvider(new OncePerAppTypes.dt_ext()).InvokeMethod<String>("GetFileName").Should().Be("dt.ext");
                //new OutputFileProvider(new OncePerAppTypes.Service_dt_ext()).InvokeMethod<String>("GetFileName").Should().Be("Service_dt.ext");
                //new OutputFileProvider(new OncePerAppTypes.Service_dt()).InvokeMethod<String>("GetFileName").Should().Be("Service.dt");
                if (type.Name.Contains("_"))
                {
                    if (type.Name.EndsWith("_"))
                    {
                        return type.Name + ".cs";
                    }
                    else
                    {
                        // get the index of the last _
                        // and make sure there is at least 1 alphanumeric char before that
                        var index = type.Name.LastIndexOf("_", StringComparison.InvariantCultureIgnoreCase);
                        var foundOneAlphaNumericCharBeforeIt = false;
                        while (index >= 0
                            && !foundOneAlphaNumericCharBeforeIt)
                        {
                            if (Char.IsLetterOrDigit(type.Name[index]))
                            {
                                foundOneAlphaNumericCharBeforeIt = true;
                            }
                            index--;
                        }
                        if (foundOneAlphaNumericCharBeforeIt)
                        {
                            // okay, we have a valid . substitution
                            return type.Name.Replace(type.Name.LastIndexOf("_", StringComparison.InvariantCultureIgnoreCase), 1,
                                    ".");
                        }
                        else
                        {
                            return type.Name + ".cs";
                        }
                    }
                }
                else
                {
                    return type.Name + ".cs";
                }
            }
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
