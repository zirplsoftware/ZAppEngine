using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using Zirpl.AppEngine.CodeGeneration.ConfigModel;
using Zirpl.AppEngine.CodeGeneration.Transformation;

namespace Zirpl.AppEngine.CodeGeneration
{
    public class NamingProvider
    {
        private AppGenerator appGenerator;

        public NamingProvider(AppGenerator appGenerator)
        {
            this.appGenerator = appGenerator;
        }

        #region namespaces

        public string GetModelNamespace(DomainType domainType)
        {
            return appGenerator.ProjectProvider.ModelProject.GetDefaultNamespace() + (String.IsNullOrEmpty(domainType.SubNamespace) ? null : "." + domainType.SubNamespace);
        }
        public string GetDataServiceNamespace(DomainType domainType)
        {
            return appGenerator.ProjectProvider.DataServiceProject.GetDefaultNamespace() + (String.IsNullOrEmpty(domainType.SubNamespace) ? null : "." + domainType.SubNamespace);
        }
        public string GetDataServiceTestsNamespace(DomainType domainType)
        {
            return appGenerator.ProjectProvider.DataServiceTestsProject.GetDefaultNamespace() + (String.IsNullOrEmpty(domainType.SubNamespace) ? null : "." + domainType.SubNamespace);
        }
        public string GetServiceNamespace(DomainType domainType)
        {
            return appGenerator.ProjectProvider.ServiceProject.GetDefaultNamespace() + (String.IsNullOrEmpty(domainType.SubNamespace) ? null : "." + domainType.SubNamespace);
        }
        public string GetTestingNamespace(DomainType domainType)
        {
            return appGenerator.ProjectProvider.TestingProject.GetDefaultNamespace() + (String.IsNullOrEmpty(domainType.SubNamespace) ? null : "." + domainType.SubNamespace);
        }
        public string GetServiceTestsNamespace(DomainType domainType)
        {
            return appGenerator.ProjectProvider.ServiceTestsProject.GetDefaultNamespace() + (String.IsNullOrEmpty(domainType.SubNamespace) ? null : "." + domainType.SubNamespace);
        }
        public string GetSupportViewModelNamespace(DomainType domainType)
        {
            return domainType.IsDictionary
                ? appGenerator.ProjectProvider.WebProject.GetDefaultNamespace() + ".Models" // + (String.IsNullOrEmpty(domainType.SubNamespace) ? null : "." + domainType.SubNamespace)
                : appGenerator.ProjectProvider.ModelProject.GetDefaultNamespace() + ".Areas.Support.Models"; // + (String.IsNullOrEmpty(domainType.SubNamespace) ? null : "." + domainType.SubNamespace);
        }
        public string GetSupportControllerNamespace(DomainType domainType)
        {
            return appGenerator.ProjectProvider.ModelProject.GetDefaultNamespace() + ".Areas.Support.Controllers";
        }
        #endregion

        #region full type names

        public string GetModelFullTypeName(DomainType domainType)
        {
            return String.Format("{0}.{1}", this.GetModelNamespace(domainType), domainType.Name);
        }
        public string GetSupportViewModelFullTypeName(DomainType domainType)
        {
            return String.Format("{0}.{1}Model", this.GetSupportViewModelNamespace(domainType), domainType.Name);
        }

        #endregion



        public string GetPluralPropertyName(DomainType domainType)
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

    }
}
