using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Zirpl.AppEngine.CodeGeneration.AppModel
{
    public class WebOptions
    {
        public WebOptions()
        {
            this.GenerateSupportViewModel = true;
            this.GenerateLookupsController = true;
            this.GenerateSupportController = true;
            this.GenerateSupportIndexView = true;
        }
        public bool GenerateSupportViewModel { get; set; }
        public bool GenerateSupportIndexView { get; set; }
        public bool GenerateSupportController { get; set; }
        public bool GenerateLookupsController { get; set; }
        public DomainTypeProperty[] AdditionalProperties { get; set; }
    }
}
