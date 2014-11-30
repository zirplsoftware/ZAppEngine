//using System;
//using System.Collections.Generic;
//using System.ComponentModel;
//using System.IO;
//using System.Linq;
//using System.Reflection;
//using System.Runtime.Remoting.Messaging;
//using System.Text;
//using System.Threading.Tasks;
//using System.Xml.Serialization;
//using EnvDTE;
//using EnvDTE80;
//using Microsoft.VisualStudio.TextTemplating;
//using Newtonsoft.Json;
//using Newtonsoft.Json.Serialization;
//using Zirpl.AppEngine.CodeGeneration.TextTemplating;
//using Zirpl.AppEngine.CodeGeneration.V2.ConfigModel;
//using Zirpl.AppEngine.CodeGeneration.V2.Parsers;
//using Zirpl.AppEngine.Model.Metadata;
//using Zirpl.IO;
//using Zirpl.Reflection;

//namespace Zirpl.AppEngine.CodeGeneration.V2
//{
//    public class TextTransformationSession2 : TextTransformationSessionBase<TextTransformationSession>
//    {
//        public AppConfig AppConfig { get; private set; }

//        public void Initialize(AppConfigParser appConfigParser, DomainClassConfigParser domainClassConfigParser)
//        {
//            appConfigParser = appConfigParser ?? new AppConfigParser();
//            domainClassConfigParser = domainClassConfigParser ?? new DomainClassConfigParser();

//            var domainClassConfigProjectItems = new List<ProjectItem>();
//            ProjectItem appConfigProjectItem = null;

//            // get all ProjectItems for the project with the initial template
//            //
//            var projectItems = this.FileManager.TemplateProjectItem.ContainingProject.ProjectItems.GetAllProjectItemsRecursive();
//            foreach (var configProjectItem in projectItems)
//            {
//                var fullPath = configProjectItem.GetFullPath();
//                if (fullPath.EndsWith(".domain.zae"))
//                {
//                    domainClassConfigProjectItems.Add(configProjectItem);
//                    this.LogLineToBuildPane(fullPath);
//                }
//                else if (fullPath.EndsWith(".app.zae"))
//                {
//                    appConfigProjectItem = configProjectItem;
//                    this.LogLineToBuildPane("App config file: " + fullPath);
//                }
//            }

//            this.AppConfig = appConfigParser.Parse(appConfigProjectItem);
//            this.AppConfig.DomainTypes.AddRange(domainClassConfigParser.Parse(this.AppConfig, domainClassConfigProjectItems));
//        }

//        //public string GetGeneratedCodeFolder(PersistableDomainType domainType, String subNamespace = null, bool includeGeneratedCodeRootFolderName = true)
//        //{
//        //    var folderHeirarchy = new List<string>();
//        //    if (includeGeneratedCodeRootFolderName)
//        //    {
//        //        folderHeirarchy.Add(this.App.GeneratedCodeRootFolderName);
//        //    }
//        //    if (subNamespace != null)
//        //    {
//        //        var tokens = subNamespace.Split('.');
//        //        foreach (var token in tokens)
//        //        {
//        //            folderHeirarchy.Add(token);
//        //        }
//        //    }
//        //    var domainNameTokens = domainType.UniqueName.Split('.');
//        //    //foreach ()
//        //    if (className != domainType.UniqueName)
//        //    {
//        //        subNamespace += ".", domainType.UniqueName.Substring(0, domainType.UniqueName.Length - className.Length - 1);
//        //    }
//        //    return (includeGeneratedCodeRootFolderName ? this.App.GeneratedCodeRootFolderName : "") + domainType.SubNamespace.Replace(".", @"\");
//        //}


//        //#region PersistableDomainType filter members- contains virtual members

//        //public PersistableDomainType GetDomainTypeByFullTypeName(string fullTypeName)
//        //{
//        //    return (from dt in this.App.PersistableTypes
//        //            where this.GetModelTypeFullName(dt) == fullTypeName
//        //            select dt).SingleOrDefault();
//        //}

//        //public PersistableDomainType GetDomainTypeByName(string name)
//        //{
//        //    return (from dt in this.App.PersistableTypes
//        //            where dt.UniqueName == name
//        //            select dt).SingleOrDefault();
//        //}
//        //public virtual IEnumerable<PersistableDomainType> DomainTypesToGenerateModelFor
//        //{
//        //    get
//        //    {
//        //        return from dt in this.App.PersistableTypes
//        //               //where dt.ModelOptions.GenerateModel
//        //               select dt;
//        //    }
//        //}
//        //public virtual IEnumerable<PersistableDomainType> DomainTypesToCustomFieldValueFor
//        //{
//        //    get
//        //    {
//        //        return from dt in this.App.PersistableTypes
//        //               where dt.IsCustomizable
//        //                    //&& !dt.IsAbstract  TODO: add in a file validation at the beginning that checks to ensure only concrete classes are extendable
//        //                    // TODO: OR should abstract classes be customizable? in some ways, it may make more sense to put it there
//        //               select dt;
//        //    }
//        //}
//        //public virtual IEnumerable<PersistableDomainType> DomainTypesToGenerateMetadataConstantsFor
//        //{
//        //    get
//        //    {
//        //        return from dt in this.App.PersistableTypes
//        //               //where dt.ModelOptions.GenerateMetadataConstants
//        //               select dt;
//        //    }
//        //}
//        //public virtual IEnumerable<PersistableDomainType> DomainTypesToGenerateModelEnumFor
//        //{
//        //    get
//        //    {
//        //        return from dt in this.App.PersistableTypes
//        //               where dt.IsPersistable
//        //                     && dt.IsDictionary
//        //                     //&& dt.ModelOptions.GenerateEnum
//        //               select dt;
//        //    }
//        //}
//        //public virtual IEnumerable<PersistableDomainType> DomainTypesToGenerateDataServiceInterfaceFor
//        //{
//        //    get
//        //    {
//        //        return from dt in this.App.PersistableTypes
//        //               where dt.IsPersistable
//        //                    //&& dt.DataServiceOptions.GenerateDataServiceInterface
//        //               select dt;
//        //    }
//        //}
//        //public virtual IEnumerable<PersistableDomainType> DomainTypesToGenerateDataServiceFor
//        //{
//        //    get
//        //    {
//        //        return from dt in this.App.PersistableTypes
//        //               where dt.IsPersistable
//        //                    //&& dt.DataServiceOptions.GenerateDataService
//        //               select dt;
//        //    }
//        //}
//        //public virtual IEnumerable<PersistableDomainType> DomainTypesToGenerateDataContextPropertyFor
//        //{
//        //    get
//        //    {
//        //        return from dt in this.App.PersistableTypes
//        //               where dt.IsPersistable
//        //                    //&& dt.DataServiceOptions.GenerateDataContextProperty
//        //               select dt;
//        //    }
//        //}
//        //public virtual IEnumerable<PersistableDomainType> DomainTypesToGenerateEntityFrameworkMappingFor
//        //{
//        //    get
//        //    {
//        //        return from dt in this.App.PersistableTypes
//        //               where dt.IsPersistable
//        //                    //&& dt.DataServiceOptions.GenerateEntityFrameworkMapping
//        //               select dt;
//        //    }
//        //}
//        //public virtual IEnumerable<PersistableDomainType> DomainTypesToGenerateServiceInterfaceFor
//        //{
//        //    get
//        //    {
//        //        return from dt in this.App.PersistableTypes
//        //               where dt.IsPersistable
//        //                    //&& dt.ServiceOptions.GenerateServiceInterface
//        //               select dt;
//        //    }
//        //}
//        //public virtual IEnumerable<PersistableDomainType> DomainTypesToGenerateServiceFor
//        //{
//        //    get
//        //    {
//        //        return from dt in this.App.PersistableTypes
//        //               where dt.IsPersistable
//        //                    //&& dt.ServiceOptions.GenerateService
//        //               select dt;
//        //    }
//        //}
//        //public virtual IEnumerable<PersistableDomainType> DomainTypesToGenerateValidatorFor
//        //{
//        //    get
//        //    {
//        //        return from dt in this.App.PersistableTypes
//        //               where !dt.IsDictionary
//        //                     //dt.ServiceOptions.GenerateValidator
//        //                     //&& !dt.IsDictionary
//        //                     //&& dt.ModelOptions.GenerateModel
//        //               select dt;
//        //    }
//        //}

//        //#endregion

//        //#region Model-related methods

//        //public bool HasCustomizableEntities
//        //{
//        //    get
//        //    {
//        //        return (from dt in this.App.PersistableTypes
//        //            where dt.IsCustomizable
//        //            select dt).Any();
//        //    }
//        //}

//        //public void StartModelFile(PersistableDomainType domainType)
//        //{
//        //    this.FileManager.StartNewFile(
//        //        this.GetModelTypeName(domainType) + this.App.GeneratedCSFileExtension,
//        //        this.ModelProject,
//        //        this.GetGeneratedCodeFolder(domainType),
//        //        new OutputFileProperties() { BuildAction = OutputFileBuildActionType.Compile });
//        //}
//        //public virtual string GetModelNamespace(PersistableDomainType domainType)
//        //{
//        //    return this.ModelProject.GetDefaultNamespace() + (String.IsNullOrEmpty(domainType.SubNamespace) ? null : "." + domainType.SubNamespace);
//        //}
//        //public virtual String GetModelTypeName(PersistableDomainType domainType)
//        //{
//        //    return domainType.UniqueName;
//        //}
//        //public virtual string GetModelTypeFullName(PersistableDomainType domainType)
//        //{
//        //    return String.Format("{0}.{1}", this.GetModelNamespace(domainType), this.GetModelTypeName(domainType));
//        //}
//        //public virtual string GetModelBaseDeclaration(PersistableDomainType domainType)
//        //{
//        //    String customizableInterfaceDeclaration = null;
//        //    if (domainType.IsCustomizable)
//        //    {
//        //        customizableInterfaceDeclaration =
//        //            String.Format("ICustomizable<{0}, {1}, {2}>", this.GetModelTypeName(domainType), this.GetCustomFieldValueTypeName(domainType), this.GetModelIdTypeName(domainType));
//        //    }
//        //    var baseDeclaration = !String.IsNullOrEmpty(domainType.ParentDomainClass)
//        //            ? " : " + domainType.ParentDomainClass
//        //            : domainType.IsPersistable 
//        //                ? domainType.IsDictionary
//        //                    ? string.Format(" : DictionaryEntityBase<{0}, {1}>", this.GetModelIdTypeName(domainType), this.GetModelEnumTypeName(domainType))
//        //                    : string.Format(" : EntityBase<{0}>", this.GetModelIdTypeName(domainType))
//        //                : "";
//        //    if (!String.IsNullOrEmpty(customizableInterfaceDeclaration))
//        //    {
//        //        baseDeclaration = String.IsNullOrEmpty(baseDeclaration)
//        //            ? " : " + customizableInterfaceDeclaration
//        //            : baseDeclaration + ", " + customizableInterfaceDeclaration;
//        //    }
   
//        //    return baseDeclaration;
//        //}
//        //public virtual string GetModelIdTypeName(PersistableDomainType domainType)
//        //{
//        //    return !String.IsNullOrEmpty(domainType.IdTypeOverride)
//        //                            ? domainType.IdTypeOverride
//        //                            : domainType.IsDictionary
//        //                                ? "byte"
//        //                                : "int";
//        //}

//        //#endregion

//        //#region MetadataConstants-related methods
//        //public void StartMetadataConstantsFile(PersistableDomainType domainType)
//        //{
//        //    this.FileManager.StartNewFile(
//        //        this.GetMetadataConstantsTypeName(domainType) + this.App.GeneratedCSFileExtension,
//        //        this.ModelProject,
//        //        this.App.GeneratedCodeRootFolderName + @"Metadata\Constants\" + this.GetGeneratedCodeFolder(domainType, false),
//        //        new OutputFileProperties() { BuildAction = OutputFileBuildActionType.Compile });
//        //}
//        //public virtual string GetMetadataConstantsNamespace(PersistableDomainType domainType)
//        //{
//        //    // use the same
//        //    return this.ModelProject.GetDefaultNamespace() + ".Metadata.Constants" + (String.IsNullOrEmpty(domainType.SubNamespace) ? null : "." + domainType.SubNamespace);
//        //}
//        //public virtual String GetMetadataConstantsTypeName(PersistableDomainType domainType)
//        //{
//        //    return domainType.UniqueName + "MetadataConstants";
//        //}
//        //public virtual String GetMetadataConstantsTypeFullName(PersistableDomainType domainType)
//        //{
//        //    return String.Format("{0}.{1}", this.GetMetadataConstantsNamespace(domainType), this.GetMetadataConstantsTypeName(domainType));
//        //}
//        //public virtual string GetMetadataConstantsBaseDeclaration(PersistableDomainType domainType)
//        //{
//        //    return !String.IsNullOrEmpty(domainType.ParentDomainClass)
//        //        ? " : " + this.GetMetadataConstantsTypeFullName(this.GetDomainTypeByName(domainType.ParentDomainClass))
//        //        : domainType.IsPersistable 
//        //            ? domainType.IsDictionary
//        //                ? " : DictionaryEntityBaseMetadataConstantsBase"
//        //                : " : MetadataConstantsBase"
//        //            : "";
//        //}
//        //#endregion

//        //#region ModelEnum-related methods
//        //public void StartModelEnumFile(PersistableDomainType domainType)
//        //{
//        //    this.FileManager.StartNewFile(
//        //        this.GetModelEnumTypeName(domainType) + this.App.GeneratedCSFileExtension,
//        //        this.ModelProject,
//        //        this.GetGeneratedCodeFolder(domainType),
//        //        new OutputFileProperties() { BuildAction = OutputFileBuildActionType.Compile });
//        //}
//        //public virtual string GetModelEnumNamespace(PersistableDomainType domainType)
//        //{
//        //    // use the same
//        //    return this.GetModelNamespace(domainType);
//        //}
//        //public virtual String GetModelEnumTypeName(PersistableDomainType domainType)
//        //{
//        //    return domainType.UniqueName + "Enum";
//        //}
//        //public virtual String GetModelEnumTypeFullName(PersistableDomainType domainType)
//        //{
//        //    return String.Format("{0}.{1}", this.GetModelEnumNamespace(domainType), this.GetModelEnumTypeName(domainType));
//        //}
//        //public virtual string GetModelEnumBaseDeclaration(PersistableDomainType domainType)
//        //{
//        //    // it should match the Model's ID type
//        //    return " : " + this.GetModelIdTypeName(domainType);
//        //}
//        //#endregion

//        //#region CustomFieldValue methods

//        //public void StartCustomFieldValueFile(PersistableDomainType domainType)
//        //{
//        //    this.FileManager.StartNewFile(
//        //        this.GetCustomFieldValueTypeName(domainType) + this.App.GeneratedCSFileExtension,
//        //        this.ModelProject,
//        //        this.App.GeneratedCodeRootFolderName + @"Customization\" + this.GetGeneratedCodeFolder(domainType, false),
//        //        new OutputFileProperties() { BuildAction = OutputFileBuildActionType.Compile });
//        //}
//        //public virtual string GetCustomFieldValueNamespace(PersistableDomainType domainType)
//        //{
//        //    return this.ModelProject.GetDefaultNamespace() + ".Customization" + (String.IsNullOrEmpty(domainType.SubNamespace) ? null : "." + domainType.SubNamespace);
//        //}
//        //public virtual String GetCustomFieldValueTypeName(PersistableDomainType domainType)
//        //{
//        //    return this.GetModelTypeName(domainType) + "CustomFieldValue";
//        //}
//        //public virtual string GetCustomFieldValueTypeFullName(PersistableDomainType domainType)
//        //{
//        //    return String.Format("{0}.{1}", this.GetCustomFieldValueNamespace(domainType), this.GetCustomFieldValueTypeName(domainType));
//        //}
//        //public virtual string GetCustomFieldValueBaseDeclaration(PersistableDomainType domainType)
//        //{
//        //    return String.Format(" : CustomFieldValueBase<{0}, {1}>", this.GetModelTypeName(domainType),
//        //        this.GetModelIdTypeName(domainType));
//        //}

//        //#endregion

//        //#region DataServiceInterface related methods
//        //public void StartDataServiceInterfaceFile(PersistableDomainType domainType)
//        //{
//        //    this.FileManager.StartNewFile(
//        //        this.GetDataServiceInterfaceTypeName(domainType) + this.App.GeneratedCSFileExtension,
//        //        this.DataServiceProject,
//        //        this.GetGeneratedCodeFolder(domainType),
//        //        new OutputFileProperties() { BuildAction = OutputFileBuildActionType.Compile });
//        //}
//        //public virtual string GetDataServiceInterfaceNamespace(PersistableDomainType domainType)
//        //{
//        //    return this.DataServiceProject.GetDefaultNamespace() + (String.IsNullOrEmpty(domainType.SubNamespace) ? null : "." + domainType.SubNamespace);
//        //}
//        //public virtual String GetDataServiceInterfaceTypeName(PersistableDomainType domainType)
//        //{
//        //    return "I" + domainType.UniqueName + "DataService";
//        //}
//        //public virtual String GetDataServiceInterfaceTypeFullName(PersistableDomainType domainType)
//        //{
//        //    return String.Format("{0}.{1}", this.GetDataServiceInterfaceNamespace(domainType), this.GetDataServiceInterfaceTypeName(domainType));
//        //}
//        //public virtual string GetDataServiceInterfaceBaseDeclaration(PersistableDomainType domainType)
//        //{
//        //    return domainType.IsDictionary
//        //        ? string.Format(" : IDictionaryEntityDataService<{0}, {1}, {2}>", this.GetModelTypeName(domainType), this.GetModelIdTypeName(domainType), this.GetModelEnumTypeName(domainType))
//        //        : string.Format(" : ICompleteDataService<{0}, {1}>", this.GetModelTypeName(domainType), this.GetModelIdTypeName(domainType));
//        //}
//        //#endregion

//        //#region DataService related methods
//        //public void StartDataServiceFile(PersistableDomainType domainType)
//        //{
//        //    this.FileManager.StartNewFile(
//        //        this.GetDataServiceTypeName(domainType) + this.App.GeneratedCSFileExtension,
//        //        this.DataServiceProject,
//        //        this.GetGeneratedCodeFolder(domainType),
//        //        new OutputFileProperties() { BuildAction = OutputFileBuildActionType.Compile });
//        //}
//        //public virtual string GetDataServiceNamespace(PersistableDomainType domainType)
//        //{
//        //    return this.DataServiceProject.GetDefaultNamespace() + (String.IsNullOrEmpty(domainType.SubNamespace) ? null : "." + domainType.SubNamespace);
//        //}
//        //public virtual String GetDataServiceTypeName(PersistableDomainType domainType)
//        //{
//        //    return domainType.UniqueName + "DataService";
//        //}
//        //public virtual String GetDataServiceTypeFullName(PersistableDomainType domainType)
//        //{
//        //    return String.Format("{0}.{1}", this.GetDataServiceNamespace(domainType), this.GetDataServiceTypeName(domainType));
//        //}
//        //public virtual string GetDataServiceBaseDeclaration(PersistableDomainType domainType)
//        //{
//        //    return domainType.IsDictionary
//        //        ? string.Format(" : ReadOnlyDbContextDataServiceBase<{0}, {1}, {2}>, {3}", this.App.DataContextName, this.GetModelTypeName(domainType), this.GetModelIdTypeName(domainType), this.GetDataServiceInterfaceTypeName(domainType))
//        //        : string.Format(" : DbContextDataServiceBase<{0}, {1}, {2}>, {3}", this.App.DataContextName, this.GetModelTypeName(domainType), this.GetModelIdTypeName(domainType), this.GetDataServiceInterfaceTypeName(domainType));
//        //}
//        //#endregion

//        //#region DataContext related methods
//        //public void StartDataContextFile()
//        //{
//        //    this.FileManager.StartNewFile(
//        //        this.GetDataContextTypeName() + this.App.GeneratedCSFileExtension,
//        //        this.DataServiceProject,
//        //        this.App.GeneratedCodeRootFolderName,
//        //        new OutputFileProperties() { BuildAction = OutputFileBuildActionType.Compile });
//        //}
//        //public virtual string GetDataContextNamespace()
//        //{
//        //    return this.DataServiceProject.GetDefaultNamespace();
//        //}
//        //public virtual String GetDataContextTypeName()
//        //{
//        //    return this.App.DataContextName;
//        //}
//        //public virtual String GetDataContextTypeFullName()
//        //{
//        //    return String.Format("{0}.{1}", this.GetDataContextNamespace(), this.GetDataContextTypeName());
//        //}
//        //public virtual String GetDataContextBaseDeclaration()
//        //{
//        //    return " : DbContextBase";
//        //}
//        //#endregion

//        //#region Entity Framework mapping related methods
//        //public void StartEntityFrameworkMappingFile(PersistableDomainType domainType)
//        //{
//        //    this.FileManager.StartNewFile(
//        //        this.GetEntityFrameworkMappingTypeName(domainType) + this.App.GeneratedCSFileExtension,
//        //        this.DataServiceProject,
//        //        this.App.GeneratedCodeRootFolderName + @"Mapping",
//        //        new OutputFileProperties() { BuildAction = OutputFileBuildActionType.Compile });
//        //}
//        //public virtual string GetEntityFrameworkMappingNamespace(PersistableDomainType domainType)
//        //{
//        //    return this.DataServiceProject.GetDefaultNamespace() + ".Mapping" + (String.IsNullOrEmpty(domainType.SubNamespace) ? null : "." + domainType.SubNamespace);
//        //}
//        //public virtual String GetEntityFrameworkMappingTypeName(PersistableDomainType domainType)
//        //{
//        //    return domainType.UniqueName + "Mapping";
//        //}
//        //public virtual String GetEntityFrameworkMappingTypeFullName(PersistableDomainType domainType)
//        //{
//        //    return String.Format("{0}.{1}", this.GetEntityFrameworkMappingNamespace(domainType), this.GetEntityFrameworkMappingTypeName(domainType));
//        //}
//        //public virtual string GetEntityFrameworkMappingBaseDeclaration(PersistableDomainType domainType)
//        //{
//        //    return domainType.IsDictionary
//        //        ? string.Format(" : DictionaryEntityMapping<{0}, {1}, {2}>", this.GetModelTypeName(domainType), this.GetModelIdTypeName(domainType), this.GetModelEnumTypeName(domainType))
//        //        : string.Format(" : EntityMappingBase<{0}, {1}>", this.GetModelTypeName(domainType), this.GetModelIdTypeName(domainType));
//        //}
//        //#endregion

//        //#region ServiceInterface-related methods
//        //public void StartServiceInterfaceFile(PersistableDomainType domainType)
//        //{
//        //    this.FileManager.StartNewFile(
//        //        this.GetServiceInterfaceTypeName(domainType) + this.App.GeneratedCSFileExtension,
//        //        this.ServiceProject,
//        //        this.GetGeneratedCodeFolder(domainType),
//        //        new OutputFileProperties() { BuildAction = OutputFileBuildActionType.Compile });
//        //}
//        //public virtual string GetServiceInterfaceNamespace(PersistableDomainType domainType)
//        //{
//        //    return this.ServiceProject.GetDefaultNamespace() + (String.IsNullOrEmpty(domainType.SubNamespace) ? null : "." + domainType.SubNamespace);
//        //}
//        //public virtual String GetServiceInterfaceTypeName(PersistableDomainType domainType)
//        //{
//        //    return "I" + domainType.UniqueName + "Service";
//        //}
//        //public virtual string GetServiceInterfaceTypeFullName(PersistableDomainType domainType)
//        //{
//        //    return String.Format("{0}.{1}", this.GetServiceInterfaceNamespace(domainType), this.GetServiceInterfaceTypeName(domainType));
//        //}
//        //public virtual string GetServiceInterfaceBaseDeclaration(PersistableDomainType domainType)
//        //{
//        //    return domainType.IsDictionary
//        //        ? string.Format(" : IDictionaryEntityService<{0}, {1}, {2}>", this.GetModelTypeName(domainType), this.GetModelIdTypeName(domainType), this.GetModelEnumTypeName(domainType))
//        //        : string.Format(" : ICompleteService<{0}, {1}>", this.GetModelTypeName(domainType), this.GetModelIdTypeName(domainType));
//        //}
//        //#endregion

//        //#region Service-related methods
//        //public void StartServiceFile(PersistableDomainType domainType)
//        //{
//        //    this.FileManager.StartNewFile(
//        //        this.GetServiceTypeName(domainType) + this.App.GeneratedCSFileExtension,
//        //        this.ServiceProject,
//        //        this.GetGeneratedCodeFolder(domainType),
//        //        new OutputFileProperties() { BuildAction = OutputFileBuildActionType.Compile });
//        //}
//        //public virtual string GetServiceNamespace(PersistableDomainType domainType)
//        //{
//        //    return this.ServiceProject.GetDefaultNamespace() + (String.IsNullOrEmpty(domainType.SubNamespace) ? null : "." + domainType.SubNamespace);
//        //}
//        //public virtual String GetServiceTypeName(PersistableDomainType domainType)
//        //{
//        //    return domainType.UniqueName + "Service";
//        //}
//        //public virtual string GetServiceTypeFullName(PersistableDomainType domainType)
//        //{
//        //    return String.Format("{0}.{1}", this.GetServiceNamespace(domainType), this.GetServiceTypeName(domainType));
//        //}
//        //public virtual string GetServiceBaseDeclaration(PersistableDomainType domainType)
//        //{
//        //    return domainType.IsDictionary
//        //        ? string.Format(" : DictionaryEntityService<{4}, {0}, {1}, {2}>, {3}", this.GetModelTypeName(domainType), this.GetModelIdTypeName(domainType), this.GetModelEnumTypeName(domainType), this.GetServiceInterfaceTypeName(domainType), this.GetDataContextTypeName())
//        //        : string.Format(" : DbContextServiceBase<{2}, {0}, {1}>, {3}", this.GetModelTypeName(domainType), this.GetModelIdTypeName(domainType), this.GetDataContextTypeName(), this.GetServiceInterfaceTypeName(domainType));
//        //}
//        //#endregion

//        //#region Validator-related methods
//        //public void StartValidatorFile(PersistableDomainType domainType)
//        //{
//        //    this.FileManager.StartNewFile(
//        //        this.GetValidatorFileName(domainType),
//        //        this.ServiceProject,
//        //        this.App.GeneratedCodeRootFolderName + @"Validation\" + this.GetGeneratedCodeFolder(domainType, false),
//        //        new OutputFileProperties() { BuildAction = OutputFileBuildActionType.Compile });
//        //}
//        //public virtual string GetValidatorNamespace(PersistableDomainType domainType)
//        //{
//        //    return this.ServiceProject.GetDefaultNamespace() + ".Validation" + (String.IsNullOrEmpty(domainType.SubNamespace) ? null : "." + domainType.SubNamespace);
//        //}
//        //public virtual String GetValidatorTypeName(PersistableDomainType domainType)
//        //{
//        //    return domainType.IsAbstract
//        //        ? domainType.UniqueName + "Validator<T>"
//        //        : domainType.UniqueName + "Validator";
//        //}
//        //public virtual String GetValidatorContructorMemberName(PersistableDomainType domainType)
//        //{
//        //    return domainType.UniqueName + "Validator";
//        //}
//        //public virtual String GetValidatorGenericConstraintDeclaration(PersistableDomainType domainType)
//        //{
//        //    return domainType.IsAbstract
//        //        ? "where T : " + this.GetModelTypeName(domainType)
//        //        : "";
//        //}
//        //public virtual String GetValidatorFileName(PersistableDomainType domainType)
//        //{
//        //    return domainType.UniqueName + "Validator" + this.App.GeneratedCSFileExtension;
//        //}
//        //public virtual string GetValidatorTypeFullName(PersistableDomainType domainType)
//        //{
//        //    return String.Format("{0}.{1}", this.GetValidatorNamespace(domainType), this.GetValidatorTypeName(domainType));
//        //}
//        //public virtual string GetValidatorBaseDeclaration(PersistableDomainType domainType)
//        //{
//        //    return !String.IsNullOrEmpty(domainType.ParentDomainClass)
//        //            ? string.Format(" : {0}", this.GetValidatorTypeFullName(this.GetDomainTypeByName(domainType.ParentDomainClass)).Replace("<T>", String.Format("<{0}>", this.GetModelTypeName(domainType))))
//        //            : domainType.IsPersistable 
//        //                ? domainType.IsAbstract
//        //                    ? " : DbEntityValidatorBase<T>"
//        //                    : string.Format(" : DbEntityValidatorBase<{0}>", this.GetModelTypeName(domainType))
//        //                : domainType.IsAbstract
//        //                    ? " : AbstractValidator<T>"
//        //                    : string.Format(" : AbstractValidator<{0}>", this.GetModelTypeName(domainType));
//        //}
//        //#endregion

//        //#region Tests Common- EntityWrapper methods

//        //public void StartPersistableModelTestsEntityWrapperFile(PersistableDomainType domainType)
//        //{
//        //    this.FileManager.StartNewFile(
//        //        this.GetPersistableModelTestsEntityWrapperTypeName(domainType) + this.App.GeneratedCSFileExtension, 
//        //        this.TestsCommonProject, 
//        //        this.GetGeneratedCodeFolder(domainType), 
//        //        new OutputFileProperties() { BuildAction = OutputFileBuildActionType.Compile });
//        //}
//        //public virtual string GetPersistableModelTestsEntityWrapperNamespace(PersistableDomainType domainType)
//        //{
//        //    return this.TestsCommonProject.GetDefaultNamespace() + (String.IsNullOrEmpty(domainType.SubNamespace) ? null : "." + domainType.SubNamespace);
//        //}
//        //public virtual String GetPersistableModelTestsEntityWrapperTypeName(PersistableDomainType domainType)
//        //{
//        //    return domainType.UniqueName + "EntityWrapper";
//        //}
//        //public virtual string GetPersistableModelTestsEntityWrapperTypeFullName(PersistableDomainType domainType)
//        //{
//        //    return String.Format("{0}.{1}", this.GetPersistableModelTestsEntityWrapperNamespace(domainType), this.GetPersistableModelTestsEntityWrapperTypeName(domainType));
//        //}

//        //#endregion

//        //#region Tests Common- Strategy methods

//        //public void StartPersistableModelTestsStrategyFile(PersistableDomainType domainType)
//        //{
//        //    this.FileManager.StartNewFile(
//        //        this.GetPersistableModelTestsStrategyTypeName(domainType) + this.App.GeneratedCSFileExtension, 
//        //        this.TestsCommonProject, 
//        //        this.GetGeneratedCodeFolder(domainType), 
//        //        new OutputFileProperties() { BuildAction = OutputFileBuildActionType.Compile });
//        //}
//        //public virtual string GetPersistableModelTestsStrategyNamespace(PersistableDomainType domainType)
//        //{
//        //    return this.TestsCommonProject.GetDefaultNamespace() + (String.IsNullOrEmpty(domainType.SubNamespace) ? null : "." + domainType.SubNamespace);
//        //}
//        //public virtual String GetPersistableModelTestsStrategyTypeName(PersistableDomainType domainType)
//        //{
//        //    return domainType.UniqueName + "TestsStrategy";
//        //}
//        //public virtual string GetPersistableModelTestsStrategyTypeFullName(PersistableDomainType domainType)
//        //{
//        //    return String.Format("{0}.{1}", this.GetPersistableModelTestsStrategyNamespace(domainType), this.GetPersistableModelTestsStrategyTypeName(domainType));
//        //}

//        //public string GetPersistableModelTestsStrategyBaseDeclaration(PersistableDomainType domainType)
//        //{
//        //    return string.Format(" : IEntityLayerTestsStrategy<{0}, {1}, {2}>", domainType.UniqueName, this.GetModelIdTypeName(domainType), this.GetPersistableModelTestsEntityWrapperTypeName(domainType));
//        //}

        
//        //#endregion

//        //#region DataService Tests- DataServiceProvider methods

//        //public void StartDataServiceTestsDataServicesProviderFile()
//        //{
//        //    this.FileManager.StartNewFile(
//        //        this.GetDataServiceTestsDataServicesProviderTypeName() + this.App.GeneratedCSFileExtension,
//        //        this.DataServiceTestsProject,
//        //        this.App.GeneratedCodeRootFolderName,
//        //        new OutputFileProperties() { BuildAction = OutputFileBuildActionType.Compile });
//        //}

//        //public virtual String GetDataServiceTestsDataServicesProviderTypeName()
//        //{
//        //    return "DataServicesProvider";
//        //}
//        //public virtual string GetDataServiceTestsDataServicesProviderNamespace()
//        //{
//        //    return this.DataServiceTestsProject.GetDefaultNamespace();
//        //}

//        //#endregion

//        //#region DataServiceTests related methods

//        //public void StartDataServiceTestsFile(PersistableDomainType domainType)
//        //{
//        //    this.FileManager.StartNewFile(
//        //        this.GetDataServiceTestsTypeName(domainType) + this.App.GeneratedCSFileExtension,
//        //        this.DataServiceTestsProject,
//        //        this.GetGeneratedCodeFolder(domainType),
//        //        new OutputFileProperties() { BuildAction = OutputFileBuildActionType.Compile });
//        //}
//        //public virtual string GetDataServiceTestsNamespace(PersistableDomainType domainType)
//        //{
//        //    return this.DataServiceTestsProject.GetDefaultNamespace() + (String.IsNullOrEmpty(domainType.SubNamespace) ? null : "." + domainType.SubNamespace);
//        //}
//        //public virtual String GetDataServiceTestsTypeName(PersistableDomainType domainType)
//        //{
//        //    return this.GetDataServiceTypeName(domainType) + "Tests";
//        //}
//        //public virtual string GetDataServiceTestsTypeFullName(PersistableDomainType domainType)
//        //{
//        //    return String.Format("{0}.{1}", this.GetDataServiceTestsNamespace(domainType), this.GetDataServiceTestsTypeName(domainType));
//        //}
//        //public string GetDataServiceTestsBaseTypeName(PersistableDomainType domainType)
//        //{
//        //    return this.GetDataServiceTestsTypeName(domainType) + "Base";
//        //}
//        //public virtual String GetDataServiceTestsBaseTypeBaseDeclaration(PersistableDomainType domainType)
//        //{
//        //    return domainType.IsDictionary
//        //        ? string.Format(" : DictionaryEntityLayerTestFixtureBase<{0}, {1}, {2}>", this.GetModelTypeName(domainType), this.GetModelIdTypeName(domainType), this.GetModelEnumTypeName(domainType))
//        //        : string.Format(" : EntityLayerTestFixtureBase<{0}, {1}, {2}, {3}>", this.GetModelTypeName(domainType), this.GetModelIdTypeName(domainType), this.GetPersistableModelTestsEntityWrapperTypeName(domainType), this.GetPersistableModelTestsStrategyTypeName(domainType));
//        //}

//        //#endregion

//        //#region Service Tests- ServiceProvider methods

//        //public void StartServiceTestsServicesProviderFile()
//        //{
//        //    this.FileManager.StartNewFile(
//        //        this.GetServiceTestsServicesProviderTypeName() + this.App.GeneratedCSFileExtension,
//        //        this.ServiceTestsProject,
//        //        this.App.GeneratedCodeRootFolderName,
//        //        new OutputFileProperties() { BuildAction = OutputFileBuildActionType.Compile });
//        //}
//        //public virtual String GetServiceTestsServicesProviderTypeName()
//        //{
//        //    return "ServicesProvider";
//        //}
//        //public virtual String GetServiceTestsServicesProviderTypeFullName()
//        //{
//        //    return String.Format("{0}.{1}", this.GetServiceTestsServicesProviderNamespace(), this.GetServiceTestsServicesProviderTypeName());
//        //}
//        //public virtual string GetServiceTestsServicesProviderNamespace()
//        //{
//        //    return this.ServiceTestsProject.GetDefaultNamespace();
//        //}

//        //#endregion

//        //#region ServiceTests related methods

//        //public void StartServiceTestsFile(PersistableDomainType domainType)
//        //{
//        //    this.FileManager.StartNewFile(
//        //        this.GetServiceTestsTypeName(domainType) + this.App.GeneratedCSFileExtension,
//        //        this.ServiceTestsProject,
//        //        this.GetGeneratedCodeFolder(domainType),
//        //        new OutputFileProperties() { BuildAction = OutputFileBuildActionType.Compile });
//        //}
//        //public virtual string GetServiceTestsNamespace(PersistableDomainType domainType)
//        //{
//        //    return this.ServiceTestsProject.GetDefaultNamespace() + (String.IsNullOrEmpty(domainType.SubNamespace) ? null : "." + domainType.SubNamespace);
//        //}
//        //public virtual String GetServiceTestsTypeName(PersistableDomainType domainType)
//        //{
//        //    return this.GetServiceTypeName(domainType) + "Tests";
//        //}
//        //public virtual string GetServiceTestsTypeFullName(PersistableDomainType domainType)
//        //{
//        //    return String.Format("{0}.{1}", this.GetServiceTestsNamespace(domainType), this.GetServiceTestsTypeName(domainType));
//        //}
//        //public string GetServiceTestsBaseTypeName(PersistableDomainType domainType)
//        //{
//        //    return this.GetDataServiceTestsTypeName(domainType) + "Base";
//        //}
//        //public virtual String GetServiceTestsBaseTypeBaseDeclaration(PersistableDomainType domainType)
//        //{
//        //    return domainType.IsDictionary
//        //        ? string.Format(" : DictionaryEntityLayerTestFixtureBase<{0}, {1}, {2}>", this.GetModelTypeName(domainType), this.GetModelIdTypeName(domainType), this.GetModelEnumTypeName(domainType))
//        //        : string.Format(" : EntityLayerTestFixtureBase<{0}, {1}, {2}, {3}>", this.GetModelTypeName(domainType), this.GetModelIdTypeName(domainType), this.GetPersistableModelTestsEntityWrapperTypeName(domainType), this.GetPersistableModelTestsStrategyTypeName(domainType));
//        //}

//        //#endregion

//        //#region Property-related methods
//        //public String GetPropertyTypeName(Property property)
//        //{
//        //    string propertyType = null;
//        //    switch (property.DataType)
//        //    {
//        //        case DataTypeEnum.Boolean:
//        //            propertyType = String.Format("bool{0}", property.IsRequired ? "" : "?");
//        //            break;
//        //        case DataTypeEnum.Byte:
//        //            propertyType = String.Format("byte{0}", property.IsRequired ? "" : "?");
//        //            break;
//        //        case DataTypeEnum.Char:
//        //            propertyType = String.Format("char{0}", property.IsRequired ? "" : "?");
//        //            break;
//        //        case DataTypeEnum.Date:
//        //        case DataTypeEnum.DateTime:
//        //        case DataTypeEnum.Time:
//        //            propertyType = String.Format("DateTime{0}", property.IsRequired ? "" : "?");
//        //            break;
//        //        case DataTypeEnum.Decimal:
//        //        case DataTypeEnum.Percentage:
//        //            propertyType = String.Format("decimal{0}", property.IsRequired ? "" : "?");
//        //            break;
//        //        case DataTypeEnum.Currency:
//        //        case DataTypeEnum.Double:
//        //            propertyType = String.Format("double{0}", property.IsRequired ? "" : "?");
//        //            break;
//        //        case DataTypeEnum.Float:
//        //            propertyType = String.Format("float{0}", property.IsRequired ? "" : "?");
//        //            break;
//        //        case DataTypeEnum.Guid:
//        //            propertyType = String.Format("Guid{0}", property.IsRequired ? "" : "?");
//        //            break;
//        //        case DataTypeEnum.Image:
//        //            propertyType = "byte[]";
//        //            break;
//        //        case DataTypeEnum.Int:
//        //            propertyType = String.Format("int{0}", property.IsRequired ? "" : "?");
//        //            break;
//        //        case DataTypeEnum.Long:
//        //            propertyType = String.Format("long{0}", property.IsRequired ? "" : "?");
//        //            break;
//        //        case DataTypeEnum.Object:
//        //            propertyType = "object";
//        //            break;
//        //        case DataTypeEnum.Relationship:
//        //            propertyType = this.GetModelTypeFullName(this.GetDomainTypeByName(property.RelationshipEntityName));
//        //            break;
//        //        case DataTypeEnum.EmailAddress:
//        //        case DataTypeEnum.String:
//        //        case DataTypeEnum.Url:
//        //            propertyType = "string";
//        //            break;
//        //        default:
//        //            throw new UnexpectedCaseException("Unknown DataTypeEnum: " + property.DataType);
//        //    }
//        //    return propertyType;
//        //}
//        //public String GetRelationshipIdPropertyTypeName(Property property)
//        //{
//        //    var relationshipModel = this.GetDomainTypeByName(property.RelationshipEntityName);
//        //    string relationshipIdTypeString = this.GetModelIdTypeName(relationshipModel);
//        //    relationshipIdTypeString += (property.IsRequired ? "" : "?");
//        //    return relationshipIdTypeString;
//        //}
//        //public virtual string GetPluralPropertyName(PersistableDomainType domainType)
//        //{
//        //    if (!String.IsNullOrEmpty(domainType.PluralNameOverride))
//        //    {
//        //        return domainType.PluralNameOverride;
//        //    }
//        //    else if (domainType.UniqueName.EndsWith("s"))
//        //    {
//        //        return domainType.UniqueName + "es";
//        //    }
//        //    else if (domainType.UniqueName.EndsWith("y"))
//        //    {
//        //        return domainType.UniqueName.Substring(0, domainType.UniqueName.Length - 1) + "ies";
//        //    }
//        //    else
//        //    {
//        //        return domainType.UniqueName + "s";
//        //    }
//        //}
//        //public IEnumerable<Property> GetOneToManyRelationshipProperties(PersistableDomainType domainType)
//        //{
//        //    return from p in domainType.Properties
//        //           where this.IsPropertyOneToManyRelationship(p)
//        //           select p;
//        //}
//        //public bool IsPropertyOneToManyRelationship(Property property)
//        //{
//        //    return property.DataType == DataTypeEnum.Relationship
//        //           && property.RelationshipType == RelationshipTypeEnum.OneToMany;
//        //}
//        //#endregion









//        #region pieces not used yet



//        //public void StartSupportViewModelFile(PersistableDomainType domainType)
//        //{
//        //    this.FileManager.StartNewFile(domainType.UniqueName + "Model.auto.cs", this.WebProject, this.App.GeneratedCodeRootFolderName + @"Areas\Support\Models\", new OutputFileProperties() { BuildAction = OutputFileBuildActionType.Compile });
//        //}
//        //public void StartDictionaryViewModelFile(PersistableDomainType domainType)
//        //{
//        //    this.FileManager.StartNewFile(domainType.UniqueName + "Model.auto.cs", this.WebProject, this.App.GeneratedCodeRootFolderName + @"Models\", new OutputFileProperties() { BuildAction = OutputFileBuildActionType.Compile });
//        //}
//        //public void StartLookupsControllerFile()
//        //{
//        //    this.FileManager.StartNewFile("LookupsController.auto.cs", this.WebProject, this.App.GeneratedCodeRootFolderName + @"Controllers\", new OutputFileProperties() { BuildAction = OutputFileBuildActionType.Compile });
//        //}
//        //public void StartSupportControllerFile(PersistableDomainType domainType)
//        //{
//        //    this.FileManager.StartNewFile(this.GetPluralPropertyName(domainType) + "Controller.auto.cs", this.WebProject, this.App.GeneratedCodeRootFolderName + @"Areas\Support\Controllers\", new OutputFileProperties() { BuildAction = OutputFileBuildActionType.Compile });
//        //}
//        //public void StartSupportIndexViewFile(PersistableDomainType domainType)
//        //{
//        //    this.FileManager.StartNewFile("_GridColumnsAndModel.auto.cshtml", this.WebProject, this.App.GeneratedCodeRootFolderName + @"Areas\Support\Views\" + this.GetPluralPropertyName(domainType), new OutputFileProperties() { BuildAction = OutputFileBuildActionType.Compile });
//        //}
//        //public void StartSupportAreaRegistrationFile()
//        //{
//        //    this.FileManager.StartNewFile("SupportAreaRegistration.auto.cs", this.WebProject, this.App.GeneratedCodeRootFolderName + @"Areas\Support\", new OutputFileProperties() { BuildAction = OutputFileBuildActionType.Compile });
//        //}
//        //public void StartRouteUtilsFile()
//        //{
//        //    this.FileManager.StartNewFile("RouteUtils.auto.cs", this.WebProject, this.App.GeneratedCodeRootFolderName + @"Core\", new OutputFileProperties() { BuildAction = OutputFileBuildActionType.Compile });
//        //}
//        //public void StartModelMappingFile()
//        //{
//        //    this.FileManager.StartNewFile("MappingModule.auto.cs", this.WebProject, this.App.GeneratedCodeRootFolderName + @"Models\Mapping\", new OutputFileProperties() { BuildAction = OutputFileBuildActionType.Compile });
//        //}
//        //public void StartSupportModelMappingFile()
//        //{
//        //    this.FileManager.StartNewFile("MappingModule.auto.cs", this.WebProject, this.App.GeneratedCodeRootFolderName + @"Areas\Support\Models\Mapping\", new OutputFileProperties() { BuildAction = OutputFileBuildActionType.Compile });
//        //}
//        //public void StartSupportHtmlUtilsFile()
//        //{
//        //    this.FileManager.StartNewFile("HtmlUtils.auto.cs", this.WebCoreProject, this.App.GeneratedCodeRootFolderName + @"Mvc\", new OutputFileProperties() { BuildAction = OutputFileBuildActionType.Compile });
//        //}






//        //public virtual IEnumerable<PersistableDomainType> DomainTypesToGenerateSupportViewModelFor
//        //{
//        //    get
//        //    {
//        //        return from dt in this.App.PersistableTypes
//        //               //where dt.WebOptions.GenerateSupportViewModel
//        //               //&& dt.IsDictionary
//        //               select dt;
//        //    }
//        //}
//        //public virtual IEnumerable<PersistableDomainType> DomainTypesToGenerateSupportControllerFor
//        //{
//        //    get
//        //    {
//        //        return from dt in this.App.PersistableTypes
//        //              // where dt.WebOptions.GenerateSupportController
//        //              where!dt.IsDictionary
//        //                     //&& !dt.IsDictionary
//        //               select dt;
//        //    }
//        //}
//        //public virtual IEnumerable<PersistableDomainType> DomainTypesToGenerateSupportIndexViewsFor
//        //{
//        //    get
//        //    {
//        //        return from dt in this.App.PersistableTypes
//        //               //where dt.WebOptions.GenerateSupportIndexView
//        //               //      && !dt.IsDictionary
//        //               where !dt.IsDictionary
//        //               select dt;
//        //    }
//        //}
//        //public virtual IEnumerable<PersistableDomainType> DomainTypesToGenerateLookupsControllerFor
//        //{
//        //    get
//        //    {
//        //        return from dt in this.App.PersistableTypes
//        //               //where dt.WebOptions.GenerateLookupsController
//        //               //      && dt.IsDictionary
//        //               where !dt.IsDictionary
//        //               select dt;
//        //    }
//        //}








//        //public string GetSupportViewModelNamespace(PersistableDomainType domainType)
//        //{
//        //    return domainType.IsDictionary
//        //        ? this.WebProject.GetDefaultNamespace() + ".Models" // + (String.IsNullOrEmpty(domainType.SubNamespace) ? null : "." + domainType.SubNamespace)
//        //        : this.ModelProject.GetDefaultNamespace() + ".Areas.Support.Models"; // + (String.IsNullOrEmpty(domainType.SubNamespace) ? null : "." + domainType.SubNamespace);
//        //}
//        //public string GetSupportViewModelFullTypeName(PersistableDomainType domainType)
//        //{
//        //    return String.Format("{0}.{1}Model", this.GetSupportViewModelNamespace(domainType), domainType.UniqueName);
//        //}
//        //public string GetSupportControllerNamespace(PersistableDomainType domainType)
//        //{
//        //    return this.ModelProject.GetDefaultNamespace() + ".Areas.Support.Controllers";
//        //}









//        //public string GetSupportViewModelBaseClass(PersistableDomainType domainType)
//        //{
//        //    return !String.IsNullOrEmpty(domainType.ParentDomainClass)
//        //            ? this.GetDomainTypeByName(domainType.ParentDomainClass).UniqueName + "Model"
//        //            : domainType.IsDictionary
//        //                ? string.Format("DictionaryEntityModelBase<{0}>", this.GetModelIdTypeName(domainType))
//        //                : string.Format("AuditableModelBase<{0}>", this.GetModelIdTypeName(domainType));
//        //}
//        //public string GetSupportViewModelMetadataConstantsBaseClass(PersistableDomainType domainType)
//        //{
//        //    return !String.IsNullOrEmpty(domainType.ParentDomainClass)
//        //            ? this.GetDomainTypeByName(domainType.ParentDomainClass).UniqueName + "ModelMetadataConstants"
//        //            : domainType.IsDictionary
//        //                ? ""
//        //                : "AuditableModelBaseMetadataConstants";
//        //}



















//        //public string GetKendoUIModelDeclarationForProperty(Property property, string returnValue = "")
//        //{
//        //    if (property.IsRelationship)
//        //    {
//        //        var type = this.GetDomainTypeByFullTypeName(property.Type);
//        //        if (type.WebOptions.GenerateSupportIndexView)
//        //        {
//        //            returnValue = String.Format("{0}{1}: {{   }}", returnValue, property.Name);
//        //            foreach (var prop in type.Properties)
//        //            {
//        //                //returnValue = this.GetKendoUIModelDeclarationForProperty(prop, returnValue) + ",";
//        //            }
//        //        }
//        //    }
//        //    else
//        //    {
//        //        var propertyType = this.GetKendoUIModelPropertyType(property.Type);
//        //        returnValue = String.Format("{0}{1}:  {{ type: {3}\"{2}{3}\", editable: false }}", returnValue, property.Name, propertyType, @"\");
//        //    }
//        //    return returnValue;
//        //}
//        //private string GetKendoUIModelPropertyType(string propertyTypeName)
//        //{
//        //    var propertyType = propertyTypeName.ToLowerInvariant();
//        //    switch (propertyType)
//        //    {
//        //        case "currency":
//        //        case "decimal":
//        //        case "int":
//        //        case "double":
//        //        case "byte":
//        //            propertyType = "number";
//        //            break;
//        //        case "datetime":
//        //            propertyType = "date";
//        //            break;
//        //        case "bool":
//        //            propertyType = "boolean";
//        //            break;
//        //        case "guid":
//        //            propertyType = "string";
//        //            break;
//        //    }
//        //    return propertyType;
//        //}

//        #endregion
//    }
//}
