using System;
using System.Collections.Generic;
using System.Linq;
using Zirpl.AppEngine.VisualStudioAutomation;
using Zirpl.AppEngine.VisualStudioAutomation.AppGeneration.Config;
using Zirpl.AppEngine.VisualStudioAutomation.AppGeneration.TextTemplating;
using Zirpl.AppEngine.VisualStudioAutomation.TextTemplating;

namespace Zirpl.Examples.Commerce.CodeGeneration.Templates.Model
{
    public class EnumBuilder : IOutputFileBuilder
    {
        public IEnumerable<OutputFile> BuildOutputFiles(App app)
        {
            return from o in app.DomainTypes
                where o.IsEnum
                select this.BuildOutputFile(app, o);
        }

        public OutputFile BuildOutputFile(App app, DomainType domainType)
        {
            if (domainType.IsEnum)
            {
                var outputFile = new PreprocessedTextTransformationOutputFile()
                {
                    FileNameWithoutExtension = domainType.Name,
                    FileExtension = ".cs",
                    DestinationProject = domainType.DestinationProject,
                    FolderPathWithinProject = app.GetFolderPathFromNamespace(domainType.DestinationProject, domainType.Namespace),
                    BuildAction = BuildActionTypeEnum.Compile,
                    TemplateType = typeof(EnumTemplate),
                };

                outputFile.TemplateParameters.Add("DomainType", domainType);
                return outputFile;
            }
            return null;
        }

        public string Key
        {
            get { return BuilderKeys.Enum; }
        }
    }
}
