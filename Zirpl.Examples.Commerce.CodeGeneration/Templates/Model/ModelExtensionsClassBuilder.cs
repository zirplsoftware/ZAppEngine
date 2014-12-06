using System;
using System.Collections.Generic;
using System.Linq;
using Zirpl.AppEngine.VisualStudioAutomation;
using Zirpl.AppEngine.VisualStudioAutomation.AppGeneration.Config;
using Zirpl.AppEngine.VisualStudioAutomation.AppGeneration.TextTemplating;
using Zirpl.AppEngine.VisualStudioAutomation.TextTemplating;

namespace Zirpl.Examples.Commerce.CodeGeneration.Templates.Model
{
    public class ModelExtensionsClassBuilder : IOutputFileBuilder
    {
        public IEnumerable<OutputFile> BuildOutputFiles(App app)
        {
            var outputFile = new PreprocessedTextTransformationOutputFile()
            {
                FileNameWithoutExtension = "ModelExtensions",
                FileExtension = ".cs",
                FolderPathWithinProject = app.Settings.GeneratedContentRootFolderName,
                DestinationProject = app.ModelProject,
                BuildAction = BuildActionTypeEnum.Compile,
                TemplateType = typeof(ModelExtensionsClassTemplate),
            };
            return new OutputFile[] {outputFile};
        }

        public OutputFile BuildOutputFile(App app, DomainType domainType)
        {
            throw new NotImplementedException();
        }

        public string Key
        {
            get { return BuilderKeys.ModelExtensions; }
        }
    }
}
