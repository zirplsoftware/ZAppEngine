using System;
using Zirpl.AppEngine.CodeGeneration.TextTemplating;
using Zirpl.AppEngine.CodeGeneration.V1.ConfigModel;

namespace Zirpl.AppEngine.CodeGeneration.V1
{
    public class NamingProvider
    {
        private TransformationHelper templateHelper;

        public NamingProvider(TransformationHelper templateHelper)
        {
            this.templateHelper = templateHelper;
        }

        #region namespaces

        public string GetModelNamespace(DomainType domainType)
        {
            return templateHelper.ProjectProvider.ModelProject.GetDefaultNamespace() + (String.IsNullOrEmpty(domainType.SubNamespace) ? null : "." + domainType.SubNamespace);
        }
        public string GetDataServiceNamespace(DomainType domainType)
        {
            return templateHelper.ProjectProvider.DataServiceProject.GetDefaultNamespace() + (String.IsNullOrEmpty(domainType.SubNamespace) ? null : "." + domainType.SubNamespace);
        }
        public string GetDataServiceTestsNamespace(DomainType domainType)
        {
            return templateHelper.ProjectProvider.DataServiceTestsProject.GetDefaultNamespace() + (String.IsNullOrEmpty(domainType.SubNamespace) ? null : "." + domainType.SubNamespace);
        }
        public string GetServiceNamespace(DomainType domainType)
        {
            return templateHelper.ProjectProvider.ServiceProject.GetDefaultNamespace() + (String.IsNullOrEmpty(domainType.SubNamespace) ? null : "." + domainType.SubNamespace);
        }
        public string GetTestingNamespace(DomainType domainType)
        {
            return templateHelper.ProjectProvider.TestingProject.GetDefaultNamespace() + (String.IsNullOrEmpty(domainType.SubNamespace) ? null : "." + domainType.SubNamespace);
        }
        public string GetServiceTestsNamespace(DomainType domainType)
        {
            return templateHelper.ProjectProvider.ServiceTestsProject.GetDefaultNamespace() + (String.IsNullOrEmpty(domainType.SubNamespace) ? null : "." + domainType.SubNamespace);
        }
        public string GetSupportViewModelNamespace(DomainType domainType)
        {
            return domainType.IsDictionary
                ? templateHelper.ProjectProvider.WebProject.GetDefaultNamespace() + ".Models" // + (String.IsNullOrEmpty(domainType.SubNamespace) ? null : "." + domainType.SubNamespace)
                : templateHelper.ProjectProvider.ModelProject.GetDefaultNamespace() + ".Areas.Support.Models"; // + (String.IsNullOrEmpty(domainType.SubNamespace) ? null : "." + domainType.SubNamespace);
        }
        public string GetSupportControllerNamespace(DomainType domainType)
        {
            return templateHelper.ProjectProvider.ModelProject.GetDefaultNamespace() + ".Areas.Support.Controllers";
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
