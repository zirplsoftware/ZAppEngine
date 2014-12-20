using System;
using System.Collections.Generic;
using System.Linq;
using System.Reflection;
using System.Text;
using System.Threading.Tasks;
using Zirpl.AppEngine.Model;
using Zirpl.AppEngine.Model.Metadata;

namespace Zirpl.AppEngine.VisualStudioAutomation.AppGeneration.Config
{
    public static class DomainTypeExtensions
    {
        public static IEnumerable<DomainProperty> GetAllPropertiesIncludingInherited(this DomainType domainType)
        {
            var list = new List<DomainProperty>();
            while (domainType != null)
            {
                list.AddRange(domainType.Properties);
                domainType = domainType.InheritsFrom;
            }
            return list;
        }

        public static DomainType GetBaseMostDomainType(this DomainType domainType)
        {
            if (domainType.InheritsFrom != null)
            {
                domainType = domainType.InheritsFrom;
            }
            return domainType;
        }
    }
}
