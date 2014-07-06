using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using Zirpl.AppEngine.CodeGeneration.ConfigModel;

namespace Zirpl.AppEngine.CodeGeneration
{
    public class DomainTypeFilters
    {
        private AppGenerator appGenerator;

        public DomainTypeFilters(AppGenerator appGenerator)
        {
            this.appGenerator = appGenerator;
        }

        public IEnumerable<DomainType> DomainTypesToGenerateModelFor
        {
            get
            {
                return from dt in appGenerator.AppDefinition.DomainTypes
                    where dt.ModelOptions.GenerateModel
                    select dt;
            }
        }

        public IEnumerable<DomainType> DomainTypesToGenerateMetadataFor
        {
            get
            {
                return from dt in appGenerator.AppDefinition.DomainTypes
                    where dt.ModelOptions.GenerateMetadata
                    select dt;
            }
        }

        public IEnumerable<DomainType> DomainTypesToGenerateEnumFor
        {
            get
            {
                return from dt in appGenerator.AppDefinition.DomainTypes
                    where dt.IsDictionary
                          && dt.ModelOptions.GenerateEnum
                    select dt;
            }
        }

        public IEnumerable<DomainType> DomainTypesToGenerateDataServiceInterfaceFor
        {
            get
            {
                return from dt in appGenerator.AppDefinition.DomainTypes
                    where dt.DataServiceOptions.GenerateDataServiceInterface
                    select dt;
            }
        }

        public IEnumerable<DomainType> DomainTypesToGenerateDataServiceFor
        {
            get
            {
                return from dt in appGenerator.AppDefinition.DomainTypes
                    where dt.DataServiceOptions.GenerateDataService
                    select dt;
            }
        }

        public IEnumerable<DomainType> DomainTypesToGenerateDataContextPropertyFor
        {
            get
            {
                return from dt in appGenerator.AppDefinition.DomainTypes
                    where dt.DataServiceOptions.GenerateDataContextProperty
                    select dt;
            }
        }

        public IEnumerable<DomainType> DomainTypesToGenerateEntityFrameworkMappingFor
        {
            get
            {
                return from dt in appGenerator.AppDefinition.DomainTypes
                    where dt.DataServiceOptions.GenerateEntityFrameworkMapping
                    select dt;
            }
        }

        public IEnumerable<DomainType> DomainTypesToGenerateServiceInterfaceFor
        {
            get
            {
                return from dt in appGenerator.AppDefinition.DomainTypes
                    where dt.ServiceOptions.GenerateServiceInterface
                    select dt;
            }
        }

        public IEnumerable<DomainType> DomainTypesToGenerateServiceFor
        {
            get
            {
                return from dt in appGenerator.AppDefinition.DomainTypes
                    where dt.ServiceOptions.GenerateService
                    select dt;
            }
        }

        public IEnumerable<DomainType> DomainTypesToGenerateValidatorFor
        {
            get
            {
                return from dt in appGenerator.AppDefinition.DomainTypes
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
                return from dt in appGenerator.AppDefinition.DomainTypes
                    where dt.WebOptions.GenerateSupportViewModel
                    //&& dt.IsDictionary
                    select dt;
            }
        }

        public IEnumerable<DomainType> DomainTypesToGenerateSupportControllerFor
        {
            get
            {
                return from dt in appGenerator.AppDefinition.DomainTypes
                    where dt.WebOptions.GenerateSupportController
                          && !dt.IsDictionary
                    select dt;
            }
        }

        public IEnumerable<DomainType> DomainTypesToGenerateSupportIndexViewsFor
        {
            get
            {
                return from dt in appGenerator.AppDefinition.DomainTypes
                    where dt.WebOptions.GenerateSupportIndexView
                          && !dt.IsDictionary
                    select dt;
            }
        }

        public IEnumerable<DomainType> DomainTypesToGenerateLookupsControllerFor
        {
            get
            {
                return from dt in appGenerator.AppDefinition.DomainTypes
                    where dt.WebOptions.GenerateLookupsController
                          && dt.IsDictionary
                    select dt;
            }
        }


        public DomainType GetDomainTypeByFullTypeName(string fullTypeName)
        {
            return (from dt in appGenerator.AppDefinition.DomainTypes
                    where appGenerator.NamingProvider.GetModelFullTypeName(dt) == fullTypeName
                    select dt).SingleOrDefault();
        }

    }
}
