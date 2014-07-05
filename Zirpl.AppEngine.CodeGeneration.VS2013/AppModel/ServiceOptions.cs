using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Zirpl.AppEngine.CodeGeneration.AppModel
{
    public class ServiceOptions
    {
        public ServiceOptions()
        {
            this.GenerateService = true;
            this.GenerateServiceInterface = true;
            this.GenerateValidator = true;
        }
        public bool GenerateService { get; set; }
        public bool GenerateServiceInterface { get; set; }
        public bool GenerateValidator { get; set; }
    }
}
