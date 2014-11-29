using System;
using System.Collections;
using System.Collections.Generic;
using EnvDTE;

namespace Zirpl.AppEngine.CodeGeneration.V2.ConfigModel
{
    public class StaticLookupDomainTypeDefinition : DomainTypeDefinitionBase
    {
        public StaticLookupDomainTypeDefinition()
        {
            this.EnumValues = new List<StaticLookupValueDefinition>();
        }

        public IList<StaticLookupValueDefinition> EnumValues { get; set; }
    }
}
