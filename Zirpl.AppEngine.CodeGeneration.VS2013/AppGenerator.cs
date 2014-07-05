using System;
using System.Collections.Generic;
using System.Collections.Specialized;
using System.IO;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using System.Xml.Serialization;
using Microsoft.VisualStudio.TextTemplating;
using Zirpl.AppEngine.CodeGeneration.AppModel;
using EnvDTE;
using Zirpl.AppEngine.CodeGeneration.Transformation;

namespace Zirpl.AppEngine.CodeGeneration
{
    public class AppGenerator
    {
        public string SolutionRootNamespace { get { return "Zirpl.Commerce"; } }
        public string DataContextName { get { return "CommerceDataContext"; } }

        public string CodeGenerationRootNamespace { get { return this.SolutionRootNamespace + ".CodeGeneration"; } }
        public string ModelRootNamespace { get { return this.SolutionRootNamespace + ".Model"; } }
        public string DataServiceRootNamespace { get { return this.SolutionRootNamespace + ".DataService"; } }
        public string DataServiceTestsRootNamespace { get { return this.DataServiceRootNamespace + ".Tests"; } }
        public string ServiceRootNamespace { get { return this.SolutionRootNamespace + ".Service"; } }
        public string WebRootNamespace { get { return this.SolutionRootNamespace + ".Web"; } }
        public string WebCoreRootNamespace { get { return this.SolutionRootNamespace + ".Web.Core"; } }
        public string TestingRootNamespace { get { return this.SolutionRootNamespace + ".Testing"; } }
        public string ServiceTestsRootNamespace { get { return this.ServiceRootNamespace + ".Tests"; } }
        //public string WebTestsRootNamespace { get { return this.WebRootNamespace + ".Tests"; } }
        public const string GeneratedCodeRootFolderName = @"_auto\";
        public TextTransformation TextTransformation { get; private set; }
        //public GeneratedTextTransformation TextTransformation { get; private set; }
        public TemplateFileManager FileManager { get; private set; }
        public EnvDTE.DTE Studio { get; private set; }
        public Solution Solution { get { return this.Studio.Solution; } }
        //public ProjectItem TemplateItem { get { return this.Studio.Solution.FindProjectItem(this.TextTransformation.Host.TemplateFile); } }
        //public Project CurrentProject { get { return this.TemplateItem.ContainingProject; } }
        public Project CodeGenerationProject { get { return this.Studio.GetProject(this.CodeGenerationRootNamespace); } }
        public Project ModelProject { get { return this.Studio.GetProject(this.ModelRootNamespace); } }
        public Project DataServiceProject { get { return this.Studio.GetProject(this.DataServiceRootNamespace); } }
        public Project DataServiceTestsProject { get { return this.Studio.GetProject(this.DataServiceTestsRootNamespace); } }
        public Project ServiceProject { get { return this.Studio.GetProject(this.ServiceRootNamespace); } }
        public Project WebProject { get { return this.Studio.GetProject(this.WebRootNamespace); } }
        public Project WebCoreProject { get { return this.Studio.GetProject(this.WebCoreRootNamespace); } }
        public Project ServiceTestsProject { get { return this.Studio.GetProject(this.ServiceTestsRootNamespace); } }
        public Project TestingProject { get { return this.Studio.GetProject(this.TestingRootNamespace); } }
        public List<string> ModelConfigFilePaths { get; private set; }
        public List<DomainType> DomainTypes { get; private set; }
        public IEnumerable<DomainType> DomainTypesToGenerateModelFor
        {
            get
            {
                return from dt in this.DomainTypes
                       where dt.ModelOptions.GenerateModel
                       select dt;
            }
        }
        public IEnumerable<DomainType> DomainTypesToGenerateMetadataFor
        {
            get
            {
                return from dt in this.DomainTypes
                       where dt.ModelOptions.GenerateMetadata
                       select dt;
            }
        }
        public IEnumerable<DomainType> DomainTypesToGenerateEnumFor
        {
            get
            {
                return from dt in this.DomainTypes
                       where dt.IsDictionary
                       && dt.ModelOptions.GenerateEnum
                       select dt;
            }
        }
        public IEnumerable<DomainType> DomainTypesToGenerateDataServiceInterfaceFor
        {
            get
            {
                return from dt in this.DomainTypes
                       where dt.DataServiceOptions.GenerateDataServiceInterface
                       select dt;
            }
        }
        public IEnumerable<DomainType> DomainTypesToGenerateDataServiceFor
        {
            get
            {
                return from dt in this.DomainTypes
                       where dt.DataServiceOptions.GenerateDataService
                       select dt;
            }
        }
        public IEnumerable<DomainType> DomainTypesToGenerateDataContextPropertyFor
        {
            get
            {
                return from dt in this.DomainTypes
                       where dt.DataServiceOptions.GenerateDataContextProperty
                       select dt;
            }
        }
        public IEnumerable<DomainType> DomainTypesToGenerateEntityFrameworkMappingFor
        {
            get
            {
                return from dt in this.DomainTypes
                       where dt.DataServiceOptions.GenerateEntityFrameworkMapping
                       select dt;
            }
        }
        public IEnumerable<DomainType> DomainTypesToGenerateServiceInterfaceFor
        {
            get
            {
                return from dt in this.DomainTypes
                       where dt.ServiceOptions.GenerateServiceInterface
                       select dt;
            }
        }
        public IEnumerable<DomainType> DomainTypesToGenerateServiceFor
        {
            get
            {
                return from dt in this.DomainTypes
                       where dt.ServiceOptions.GenerateService
                       select dt;
            }
        }
        public IEnumerable<DomainType> DomainTypesToGenerateValidatorFor
        {
            get
            {
                return from dt in this.DomainTypes
                       where dt.ServiceOptions.GenerateValidator
                       && !dt.IsDictionary
                       && dt.ModelOptions.GenerateModel
                       select dt;
            }
        }
        public IEnumerable<DomainType> DomainTypesToGenerateSupportViewModelFor
        {
            get
            {
                return from dt in this.DomainTypes
                       where dt.WebOptions.GenerateSupportViewModel
                       //&& dt.IsDictionary
                       select dt;
            }
        }
        public IEnumerable<DomainType> DomainTypesToGenerateSupportControllerFor
        {
            get
            {
                return from dt in this.DomainTypes
                       where dt.WebOptions.GenerateSupportController
                       && !dt.IsDictionary
                       select dt;
            }
        }
        public IEnumerable<DomainType> DomainTypesToGenerateSupportIndexViewsFor
        {
            get
            {
                return from dt in this.DomainTypes
                       where dt.WebOptions.GenerateSupportIndexView
                       && !dt.IsDictionary
                       select dt;
            }
        }
        public IEnumerable<DomainType> DomainTypesToGenerateLookupsControllerFor
        {
            get
            {
                return from dt in this.DomainTypes
                       where dt.WebOptions.GenerateLookupsController
                       && dt.IsDictionary
                       select dt;
            }
        }


        public AppGenerator(TextTransformation textTransformation)
        {
            this.TextTransformation = textTransformation;
            this.FileManager = TemplateFileManager.Create(this.TextTransformation);
            
            // AAHHHHHHHHHHH how to do this line?
            //this.Studio = (this.TextTransformation.Host as IServiceProvider).GetService(typeof(EnvDTE.DTE)) as EnvDTE.DTE;

            this.ModelConfigFilePaths = new List<string>();
            var modelConfigFileProjectItems = this.CodeGenerationProject.ProjectItems.GetAllProjectItemsRecursive();
            foreach (var modelConfigProjectItem in modelConfigFileProjectItems)
            {
                var fullPath = modelConfigProjectItem.GetFullPath();
                if (fullPath.EndsWith(".model.xml"))
                {
                    this.ModelConfigFilePaths.Add(fullPath);
                }
            }

            this.DomainTypes = new List<DomainType>();
            foreach (var path in this.ModelConfigFilePaths)
            {
                DomainType domainType = null;
                var xmlSerializer = new XmlSerializer(typeof(DomainType));
                using (var fileStream = new FileStream(path, FileMode.Open, FileAccess.Read))
                {
                    domainType = (DomainType)xmlSerializer.Deserialize(fileStream);
                }
                this.DomainTypes.Add(domainType);
            }

            //var vsProject = project.Object as VSLangProj.VSProject;
        }

        public DomainType GetDomainTypeByFullTypeName(string fullTypeName)
        {
            return (from dt in this.DomainTypes
                    where this.GetModelFullTypeName(dt) == fullTypeName
                    select dt).SingleOrDefault();
        }
        public string GetIdType(DomainType domainType)
        {
            return !String.IsNullOrEmpty(domainType.IdTypeOverride)
                                    ? domainType.IdTypeOverride
                                    : domainType.IsDictionary
                                        ? "byte"
                                        : "int";
        }
        public string GetPluralName(DomainType domainType)
        {
            if (!String.IsNullOrEmpty(domainType.PluralNameOverride))
            {
                return domainType.PluralNameOverride;
            }
            else if (domainType.Name.EndsWith("s"))
            {
                return domainType.Name + "es";
            }
            else if (domainType.Name.EndsWith("y"))
            {
                return domainType.Name.Substring(0, domainType.Name.Length - 1) + "ies";
            }
            else
            {
                return domainType.Name + "s";
            }
        }


        public void StartModelFile(DomainType domainType)
        {
            this.FileManager.StartNewFile(domainType.Name + ".auto.cs", this.ModelProject.Name, this.GetGeneratedCodeFolder(domainType));
        }
        public void StartMetadataFile(DomainType domainType)
        {
            this.FileManager.StartNewFile(domainType.Name + "Metadata.auto.cs", this.ModelProject.Name, this.GetGeneratedCodeFolder(domainType));
        }
        public void StartEnumFile(DomainType domainType)
        {
            this.FileManager.StartNewFile(domainType.Name + "Enum.auto.cs", this.ModelProject.Name, this.GetGeneratedCodeFolder(domainType));
        }
        public void StartDataServiceInterfaceFile(DomainType domainType)
        {
            this.FileManager.StartNewFile("I" + domainType.Name + "DataService.auto.cs", this.DataServiceProject.Name, this.GetGeneratedCodeFolder(domainType));
        }
        public void StartDataServiceFile(DomainType domainType)
        {
            this.FileManager.StartNewFile(domainType.Name + "DataService.auto.cs", this.DataServiceProject.Name, this.GetGeneratedCodeFolder(domainType));
        }
        public void StartEntityFrameworkFile(DomainType domainType)
        {
            this.FileManager.StartNewFile(domainType.Name + "Mapping.auto.cs", this.DataServiceProject.Name, GeneratedCodeRootFolderName + @"Mapping");
        }
        public void StartDataContextFile()
        {
            this.FileManager.StartNewFile(this.DataContextName + ".auto.cs", this.DataServiceProject.Name, GeneratedCodeRootFolderName);
        }
        public void StartDataServiceTestsDataServicesProviderFile()
        {
            this.FileManager.StartNewFile("DataServicesProvider.auto.cs", this.DataServiceTestsProject.Name, GeneratedCodeRootFolderName);
        }
        public void StartDataServiceTestsFile(DomainType domainType)
        {
            this.FileManager.StartNewFile(domainType.Name + "DataServiceTests.auto.cs", this.DataServiceTestsProject.Name, this.GetGeneratedCodeFolder(domainType));
        }
        public void StartTestsStrategyFile(DomainType domainType)
        {
            this.FileManager.StartNewFile(domainType.Name + "TestsStrategy.auto.cs", this.TestingProject.Name, this.GetGeneratedCodeFolder(domainType));
        }
        public void StartServiceInterfaceFile(DomainType domainType)
        {
            this.FileManager.StartNewFile("I" + domainType.Name + "Service.auto.cs", this.ServiceProject.Name, this.GetGeneratedCodeFolder(domainType));
        }
        public void StartServiceFile(DomainType domainType)
        {
            this.FileManager.StartNewFile(domainType.Name + "Service.auto.cs", this.ServiceProject.Name, this.GetGeneratedCodeFolder(domainType));
        }
        public void StartServiceTestsServicesProviderFile()
        {
            this.FileManager.StartNewFile("ServicesProvider.auto.cs", this.ServiceTestsProject.Name, GeneratedCodeRootFolderName);
        }
        public void StartServiceTestsFile(DomainType domainType)
        {
            this.FileManager.StartNewFile(domainType.Name + "ServiceTests.auto.cs", this.ServiceTestsProject.Name, this.GetGeneratedCodeFolder(domainType));
        }
        public void StartValidatorFile(DomainType domainType)
        {
            this.FileManager.StartNewFile(domainType.Name + "Validator.auto.cs", this.ServiceProject.Name, GeneratedCodeRootFolderName + @"Validation");
        }
        public void StartSupportViewModelFile(DomainType domainType)
        {
            this.FileManager.StartNewFile(domainType.Name + "Model.auto.cs", this.WebProject.Name, GeneratedCodeRootFolderName + @"Areas\Support\Models\");
        }
        public void StartDictionaryViewModelFile(DomainType domainType)
        {
            this.FileManager.StartNewFile(domainType.Name + "Model.auto.cs", this.WebProject.Name, GeneratedCodeRootFolderName + @"Models\");
        }
        public void StartLookupsControllerFile()
        {
            this.FileManager.StartNewFile("LookupsController.auto.cs", this.WebProject.Name, GeneratedCodeRootFolderName + @"Controllers\");
        }
        public void StartSupportControllerFile(DomainType domainType)
        {
            this.FileManager.StartNewFile(this.GetPluralName(domainType) + "Controller.auto.cs", this.WebProject.Name, GeneratedCodeRootFolderName + @"Areas\Support\Controllers\");
        }
        public void StartSupportIndexViewFile(DomainType domainType)
        {
            this.FileManager.StartNewFile("_GridColumnsAndModel.auto.cshtml", this.WebProject.Name, GeneratedCodeRootFolderName + @"Areas\Support\Views\" + this.GetPluralName(domainType));
        }
        public void StartSupportHtmlUtilsFile()
        {
            this.FileManager.StartNewFile("HtmlUtils.auto.cs", this.WebCoreProject.Name, GeneratedCodeRootFolderName + @"Mvc\");
        }
        public void StartSupportAreaRegistrationFile()
        {
            this.FileManager.StartNewFile("SupportAreaRegistration.auto.cs", this.WebProject.Name, GeneratedCodeRootFolderName + @"Areas\Support\");
        }
        public void StartRouteUtilsFile()
        {
            this.FileManager.StartNewFile("RouteUtils.auto.cs", this.WebProject.Name, GeneratedCodeRootFolderName + @"Core\");
        }
        public void StartModelMappingFile()
        {
            this.FileManager.StartNewFile("MappingModule.auto.cs", this.WebProject.Name, GeneratedCodeRootFolderName + @"Models\Mapping\");
        }
        public void StartSupportModelMappingFile()
        {
            this.FileManager.StartNewFile("MappingModule.auto.cs", this.WebProject.Name, GeneratedCodeRootFolderName + @"Areas\Support\Models\Mapping\");
        }



        private string GetGeneratedCodeFolder(DomainType domainType)
        {
            return GeneratedCodeRootFolderName + domainType.SubNamespace.Replace(".", @"\");
        }




        public string GetModelBaseClass(DomainType domainType)
        {
            return !String.IsNullOrEmpty(domainType.BaseClassOverride)
                    ? domainType.BaseClassOverride
                    : domainType.IsDictionary
                        ? string.Format("DictionaryEntityBase<{0}, {1}Enum>", this.GetIdType(domainType), domainType.Name)
                        : string.Format("AuditableBase<{0}>", this.GetIdType(domainType));
        }
        public string GetMetadataBaseClass(DomainType domainType)
        {
            return !String.IsNullOrEmpty(domainType.BaseClassOverride)
                ? domainType.BaseClassOverride + "Metadata"
                : domainType.IsDictionary
                    ? "DictionaryEntityBaseMetadataBase"
                    : "MetadataBase";
        }
        public string GetDataServiceInterfaceBaseInterface(DomainType domainType)
        {
            return domainType.IsDictionary
                ? string.Format("IDictionaryEntityDataService<{0}, {1}, {0}Enum>", domainType.Name, this.GetIdType(domainType))
                : string.Format("ICompleteDataService<{0}, {1}>", domainType.Name, this.GetIdType(domainType));
        }
        public string GetDataServiceBaseClass(DomainType domainType)
        {
            return domainType.IsDictionary
                ? string.Format("DictionaryEntityDataService<{0}, {1}, {0}Enum>, I{0}DataService", domainType.Name, this.GetIdType(domainType))
                : string.Format("DbContextDataServiceBase<{2}, {0}, {1}>, I{0}DataService", domainType.Name, this.GetIdType(domainType), this.DataContextName);
        }
        public string GetDataServiceTestsBaseClass(DomainType domainType)
        {
            return domainType.IsDictionary
                ? string.Format("DictionaryEntityLayerTestFixtureBase<{0}, {1}, {0}Enum>", domainType.Name, this.GetIdType(domainType))
                : string.Format("EntityLayerTestFixtureBase<{0}, {1}, {0}EntityWrapper, {0}TestsStrategy>", domainType.Name, this.GetIdType(domainType));
        }
        public string GetDataServiceTestsStrategyBaseClass(DomainType domainType)
        {
            return string.Format("IEntityLayerTestsStrategy<{0}, {1}, {0}EntityWrapper>", domainType.Name, this.GetIdType(domainType));
        }
        //IEntityLayerTestFixtureStrategy<TEntity, TId, TEntityWrapper>
        public string GetEntityFrameworkMappingBaseClass(DomainType domainType)
        {
            return domainType.IsDictionary
                ? string.Format("DictionaryEntityMapping<{0}, {1}, {0}Enum>", domainType.Name, this.GetIdType(domainType))
                : string.Format("CoreEntityMappingBase<{0}, {1}>", domainType.Name, this.GetIdType(domainType));
        }
        public string GetServiceInterfaceBaseInterface(DomainType domainType)
        {
            return domainType.IsDictionary
                ? string.Format("IDictionaryEntityService<{0}, {1}, {0}Enum>", domainType.Name, this.GetIdType(domainType))
                : string.Format("ICompleteService<{0}, {1}>", domainType.Name, this.GetIdType(domainType));
        }
        public string GetServiceBaseClass(DomainType domainType)
        {
            return domainType.IsDictionary
                ? string.Format("DictionaryEntityService<{0}, {1}, {0}Enum>, I{0}Service", domainType.Name, this.GetIdType(domainType))
                : string.Format("DbContextServiceBase<{2}, {0}, {1}>, I{0}Service", domainType.Name, this.GetIdType(domainType), this.DataContextName);
        }
        public string GetServiceTestsBaseClass(DomainType domainType)
        {
            return domainType.IsDictionary
                ? string.Format("DictionaryEntityLayerTestFixtureBase<{0}, {1}, {0}Enum>", domainType.Name, this.GetIdType(domainType))
                : string.Format("EntityLayerTestFixtureBase<{0}, {1}, {0}EntityWrapper, {0}TestsStrategy>", domainType.Name, this.GetIdType(domainType));
        }
        public string GetSupportViewModelBaseClass(DomainType domainType)
        {
            return !String.IsNullOrEmpty(domainType.BaseClassOverride)
                    ? this.GetDomainTypeByFullTypeName(domainType.BaseClassOverride).Name + "Model"
                    : domainType.IsDictionary
                        ? string.Format("DictionaryEntityModelBase<{0}>", this.GetIdType(domainType))
                        : string.Format("AuditableModelBase<{0}>", this.GetIdType(domainType));
        }
        public string GetSupportViewModelMetadataBaseClass(DomainType domainType)
        {
            return !String.IsNullOrEmpty(domainType.BaseClassOverride)
                    ? this.GetDomainTypeByFullTypeName(domainType.BaseClassOverride).Name + "ModelMetadata"
                    : domainType.IsDictionary
                        ? ""
                        : "AuditableModelBaseMetadata";
        }
        public string GetValidatorBaseClass(DomainType domainType)
        {
            return !String.IsNullOrEmpty(domainType.BaseClassOverride)
                    ? string.Format("{0}Validator<{1}>", this.GetDomainTypeByFullTypeName(domainType.BaseClassOverride).Name, domainType.Name)
                    : domainType.IsAbstract
                        ? string.Format("DbEntityValidatorBase<T> where T: {0}", domainType.Name)
                        : string.Format("DbEntityValidatorBase<{0}>", domainType.Name);
        }
        public string GetKendoUIModelDeclarationForProperty(DomainTypeProperty property, string returnValue = "")
        {
            if (property.IsRelationship)
            {
                var type = this.GetDomainTypeByFullTypeName(property.Type);
                if (type.WebOptions.GenerateSupportIndexView)
                {
                    returnValue = String.Format("{0}{1}: {{   }}", returnValue, property.Name);
                    foreach (var prop in type.Properties)
                    {
                        //returnValue = this.GetKendoUIModelDeclarationForProperty(prop, returnValue) + ",";
                    }
                }
            }
            else
            {
                var propertyType = this.GetKendoUIModelPropertyType(property.Type);
                returnValue = String.Format("{0}{1}:  {{ type: {3}\"{2}{3}\", editable: false }}", returnValue, property.Name, propertyType, @"\");
            }
            return returnValue;
        }
        public string GetKendoUIModelPropertyType(string propertyTypeName)
        {
            var propertyType = propertyTypeName.ToLowerInvariant();
            switch (propertyType)
            {
                case "currency":
                case "decimal":
                case "int":
                case "double":
                case "byte":
                    propertyType = "number";
                    break;
                case "datetime":
                    propertyType = "date";
                    break;
                case "bool":
                    propertyType = "boolean";
                    break;
                case "guid":
                    propertyType = "string";
                    break;
            }
            return propertyType;
        }

        public string ToLowerCamelCaseString(String source)
        {
            return source.First().ToString().ToLower() + String.Join("", source.Skip(1));
        }

        /// <summary>
        /// Parses a camel cased or pascal cased string and returns an array of the words within the string.
        /// </summary>
        /// <example>
        /// The string "PascalCasing" will return an array with two elements, "Pascal" and "Casing".
        /// </example>
        /// <param name="source">The string that is camel cased that needs to be split</param>
        /// <returns>An arry of each word part</returns>
        public string[] SplitCamelCase(string source)
        {
            if (source == null)
                return new string[] { }; //Return empty array.

            if (source.Length == 0)
                return new string[] { "" };

            StringCollection words = new StringCollection();
            int wordStartIndex = 0;

            char[] letters = source.ToCharArray();
            // Skip the first letter. we don't care what case it is.
            for (int i = 1; i < letters.Length; i++)
            {
                if (char.IsUpper(letters[i]))
                {
                    //Grab everything before the current index.
                    words.Add(new String(letters, wordStartIndex, i - wordStartIndex));
                    wordStartIndex = i;
                }
            }

            //We need to have the last word.
            words.Add(new String(letters, wordStartIndex, letters.Length - wordStartIndex));

            //Copy to a string array.
            string[] wordArray = new string[words.Count];
            words.CopyTo(wordArray, 0);
            return wordArray;
        }//SplitUpperCase

        /// <summary>
        /// Parses a camel cased or pascal cased string and returns a new string with spaces between the words in the string.
        /// </summary>
        /// <example>
        /// The string "PascalCasing" will return an array with two elements, "Pascal" and "Casing".
        /// </example>
        /// <param name="source">The string that is camel cased that needs to be split</param>
        /// <returns>A string with spaces between each word part</returns>
        public string ToSplitCamelCaseString(string source)
        {
            return string.Join(" ", SplitCamelCase(source));
        }



        public string GetModelNamespace(DomainType domainType)
        {
            return this.ModelRootNamespace + (String.IsNullOrEmpty(domainType.SubNamespace) ? null : "." + domainType.SubNamespace);
        }
        public string GetDataServiceNamespace(DomainType domainType)
        {
            return this.DataServiceRootNamespace + (String.IsNullOrEmpty(domainType.SubNamespace) ? null : "." + domainType.SubNamespace);
        }
        public string GetDataServiceTestsNamespace(DomainType domainType)
        {
            return this.DataServiceTestsRootNamespace + (String.IsNullOrEmpty(domainType.SubNamespace) ? null : "." + domainType.SubNamespace);
        }
        public string GetServiceNamespace(DomainType domainType)
        {
            return this.ServiceRootNamespace + (String.IsNullOrEmpty(domainType.SubNamespace) ? null : "." + domainType.SubNamespace);
        }
        public string GetTestingNamespace(DomainType domainType)
        {
            return this.TestingRootNamespace + (String.IsNullOrEmpty(domainType.SubNamespace) ? null : "." + domainType.SubNamespace);
        }
        public string GetServiceTestsNamespace(DomainType domainType)
        {
            return this.ServiceTestsRootNamespace + (String.IsNullOrEmpty(domainType.SubNamespace) ? null : "." + domainType.SubNamespace);
        }
        public string GetSupportViewModelNamespace(DomainType domainType)
        {
            return domainType.IsDictionary
                ? this.WebRootNamespace + ".Models" // + (String.IsNullOrEmpty(domainType.SubNamespace) ? null : "." + domainType.SubNamespace)
                : this.WebRootNamespace + ".Areas.Support.Models"; // + (String.IsNullOrEmpty(domainType.SubNamespace) ? null : "." + domainType.SubNamespace);
        }
        public string GetSupportControllerNamespace(DomainType domainType)
        {
            return this.WebRootNamespace + ".Areas.Support.Controllers";
        }

        public string GetModelFullTypeName(DomainType domainType)
        {
            return String.Format("{0}.{1}", this.GetModelNamespace(domainType), domainType.Name);
        }

        public string GetSupportViewModelFullTypeName(DomainType domainType)
        {
            return String.Format("{0}.{1}Model", this.GetSupportViewModelNamespace(domainType), domainType.Name);
        }

        public void End()
        {
            this.FileManager.Process();
        }
    }
}
