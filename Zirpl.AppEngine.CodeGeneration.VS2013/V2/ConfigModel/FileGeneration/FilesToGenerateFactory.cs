using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using EnvDTE;
using Zirpl.AppEngine.CodeGeneration.TextTemplating;
using Zirpl.AppEngine.CodeGeneration.V2.Templates.Model;
using Zirpl.AppEngine.Model.Metadata;

namespace Zirpl.AppEngine.CodeGeneration.V2.ConfigModel.FileGeneration
{
    public class FilesToGenerateFactory
    {
        public IEnumerable<FileToGenerate> CreateList(AppInfo app)
        {
            var list = new List<FileToGenerate>();

            #region Model for persistable domain classes
            // generate the Model class for persistable domain classes
            foreach (var domainType in app.DomainTypes.Where(o => o.IsPersistable))
            {
                var classToGenerate = new ClassFileToGenerate()
                {
                    FileNameWithoutExtension = domainType.Name,
                    FileExtension = app.GeneratedCSFileExtension,
                    DestinationProject = domainType.DestinationProject,
                    FolderPath = this.GetFolderPathFromNamespace(app, domainType.DestinationProject, domainType.Namespace),
                    BuildAction = OutputFileBuildActionType.Compile,
                    TemplateType = typeof (PersistableDomainClassTemplate),
                    ClassName = domainType.Name,
                    ClassFullName = domainType.FullName,
                    Namespace = domainType.Namespace,
                    BaseClassDeclaration = domainType.InheritsFrom == null ? null : domainType.InheritsFrom.FullName,
                    IsAbstract = domainType.IsAbstract
                };
                if (domainType.IsPersistable
                    && domainType.InheritsFrom == null)
                {
                    // this MUST have the ID property
                    classToGenerate.InterfaceDeclarations.Add(String.Format("Zirpl.AppEngine.Model.IPersistable<{0}>", domainType.IdProperty.DataTypeString));
                }
                if (domainType.IsAuditable
                    && (domainType.InheritsFrom == null
                        || !domainType.InheritsFrom.IsAuditable))
                {
                    classToGenerate.InterfaceDeclarations.Add("Zirpl.AppEngine.Model.IAuditable");
                }
                if (domainType.IsExtensible
                    && (domainType.InheritsFrom == null
                        || !domainType.InheritsFrom.IsExtensible))
                {
                    classToGenerate.InterfaceDeclarations.Add(String.Format("Zirpl.AppEngine.Model.Extensibility.IExtensible<{0},{1},{2}>", domainType.FullName, domainType.ExtendedBy.FullName, domainType.IdProperty.DataTypeString));
                }
                if (domainType.IsExtendedEntityFieldValue
                    && (domainType.InheritsFrom == null
                        || !domainType.InheritsFrom.IsExtendedEntityFieldValue))
                {
                    classToGenerate.InterfaceDeclarations.Add(String.Format("Zirpl.AppEngine.Model.Extensibility.IExtendedEntityFieldValue<{0},{1}>", domainType.Extends.FullName, domainType.IdProperty.DataTypeString));
                }
                if (domainType.IsMarkDeletable
                    && (domainType.InheritsFrom == null
                        || !domainType.InheritsFrom.IsMarkDeletable))
                {
                    classToGenerate.InterfaceDeclarations.Add("Zirpl.AppEngine.Model.IMarkDeletable");
                }
                if (domainType.IsStaticLookup
                    && (domainType.InheritsFrom == null
                        || !domainType.InheritsFrom.IsStaticLookup))
                {
                    classToGenerate.InterfaceDeclarations.Add(String.Format("Zirpl.AppEngine.Model.IStaticLookup<{0},{1}>", domainType.IdProperty.DataTypeString, domainType.DescribedByEnum.FullName));
                }
                if (domainType.IsVersionable
                    && (domainType.InheritsFrom == null
                        || !domainType.InheritsFrom.IsVersionable))
                {
                    classToGenerate.InterfaceDeclarations.Add("Zirpl.AppEngine.Model.IVersionable");
                }
                classToGenerate.TemplateParameters.Add("DomainType", domainType);


                list.Add(classToGenerate);
            }

            #endregion

            return list;
        }

        private String GetFolderPathFromNamespace(AppInfo app, Project project, String nameSpace)
        {
            String folderPath = nameSpace;
            folderPath = folderPath.SubstringAfterFirstInstanceOf(project.GetDefaultNamespace() + ".");
            folderPath = folderPath.Replace('.', '\\');
            folderPath = app.GeneratedCodeRootFolderName + folderPath;
            return folderPath;
        }

        
    }
}
