using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Zirpl.AppEngine.CodeGeneration.AppModel
{
    public class DomainType
    {
        public DomainType()
        {
            this.ModelOptions = new ModelOptions();
            this.DataServiceOptions = new DataServiceOptions();
            this.ServiceOptions = new ServiceOptions();
            this.WebOptions = new WebOptions();
        }

        public String Name { get; set; }
        public String BaseClassOverride { get; set; }
        public String PluralNameOverride { get; set; }
        public String SubNamespace { get; set; }
        public bool IsDictionary { get; set; }
        public bool IsAbstract { get; set; }
        public string IdTypeOverride { get; set; }
        public ModelOptions ModelOptions { get; set; }
        public DataServiceOptions DataServiceOptions { get; set; }
        public ServiceOptions ServiceOptions { get; set; }
        public WebOptions WebOptions { get; set; }
        public DomainTypeProperty[] Properties { get; set; }
        public EnumValueEntry[] EnumValueEntries { get; set; }
    }
}
