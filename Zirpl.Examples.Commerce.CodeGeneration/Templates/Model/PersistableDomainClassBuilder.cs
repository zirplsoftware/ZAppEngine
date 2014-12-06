using System;
using System.Collections.Generic;
using System.Linq;
using Zirpl.AppEngine.VisualStudioAutomation.AppGeneration.Config;
using Zirpl.AppEngine.VisualStudioAutomation.AppGeneration.TextTemplating;
using Zirpl.AppEngine.VisualStudioAutomation.TextTemplating;

namespace Zirpl.Examples.Commerce.CodeGeneration.Templates.Model
{
    public class PersistableDomainClassBuilder : IOutputClassBuilder
    {
        public IEnumerable<OutputFile> BuildOutputFiles(App app)
        {
            return from d in app.DomainTypes
                where d.IsPersistable
                select this.BuildOutputFile(app, d);
        }

        public OutputFile BuildOutputFile(App app, DomainType domainType)
        {
            return this.BuildOutputClass(app, domainType).OutputFile;
        }

        public OutputClass BuildOutputClass(App app, DomainType domainType)
        {
            var outputFile = new PreprocessedTextTransformationOutputFile()
            {
                FileNameWithoutExtension = domainType.Name,
                FileExtension = ".cs",
                DestinationProject = domainType.DestinationProject,
                FolderPathWithinProject = app.GetFolderPathFromNamespace(domainType.DestinationProject, domainType.Namespace),
                BuildAction = BuildActionTypeEnum.Compile,
                TemplateType = typeof (PersistableDomainClassTemplate),
            };
            var classToGenerate = new OutputClass(outputFile)
            {
                ClassName = domainType.Name,
                Namespace = domainType.Namespace,
                BaseClass = domainType.InheritsFrom == null ? null : domainType.InheritsFrom.FullName,
                IsAbstract = domainType.IsAbstract
            };

            if (domainType.IsPersistable
                && domainType.InheritsFrom == null)
            {
                // this MUST have the ID property
                classToGenerate.InterfaceDeclarations.Add(String.Format("IPersistable<{0}>", domainType.IdProperty.DataTypeString));
            }
            if (domainType.IsAuditable
                && (domainType.InheritsFrom == null
                    || !domainType.InheritsFrom.IsAuditable))
            {
                classToGenerate.InterfaceDeclarations.Add("IAuditable");
            }
            if (domainType.IsExtensible
                && (domainType.InheritsFrom == null
                    || !domainType.InheritsFrom.IsExtensible))
            {
                classToGenerate.InterfaceDeclarations.Add(String.Format("IExtensible<{0},{1},{2}>", domainType.Name, domainType.ExtendedBy.Name, domainType.IdProperty.DataTypeString));
            }
            if (domainType.IsExtendedEntityFieldValue
                && (domainType.InheritsFrom == null
                    || !domainType.InheritsFrom.IsExtendedEntityFieldValue))
            {
                classToGenerate.InterfaceDeclarations.Add(String.Format("IExtendedEntityFieldValue<{0},{1}, {2}>", domainType.Name, domainType.Extends.Name, domainType.IdProperty.DataTypeString));
            }
            if (domainType.IsMarkDeletable
                && (domainType.InheritsFrom == null
                    || !domainType.InheritsFrom.IsMarkDeletable))
            {
                classToGenerate.InterfaceDeclarations.Add("IMarkDeletable");
            }
            if (domainType.IsStaticLookup
                && (domainType.InheritsFrom == null
                    || !domainType.InheritsFrom.IsStaticLookup))
            {
                classToGenerate.InterfaceDeclarations.Add(String.Format("IStaticLookup<{0},{1}>", domainType.IdProperty.DataTypeString, domainType.DescribedByEnum.FullName));
            }
            if (domainType.IsVersionable
                && (domainType.InheritsFrom == null
                    || !domainType.InheritsFrom.IsVersionable))
            {
                classToGenerate.InterfaceDeclarations.Add("IVersionable");
            }
            outputFile.TemplateParameters.Add("DomainType", domainType);
            outputFile.TemplateParameters.Add("OutputClass", classToGenerate);
            return classToGenerate;
        }

        public string Key
        {
            get { return BuilderKeys.PersistableDomainClass; }
        }
    }
}
