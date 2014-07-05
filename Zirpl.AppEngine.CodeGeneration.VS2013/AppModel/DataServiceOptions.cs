using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Zirpl.AppEngine.CodeGeneration.AppModel
{
    public class DataServiceOptions
    {
        public DataServiceOptions()
        {
            this.GenerateDataService = true;
            this.GenerateDataServiceInterface = true;
            this.GenerateEntityFrameworkMapping = true;
            this.GenerateDataContextProperty = true;
        }
        public bool GenerateDataService { get; set; }
        public bool GenerateDataServiceInterface { get; set; }
        public bool GenerateEntityFrameworkMapping { get; set; }
        public bool GenerateDataContextProperty { get; set; }
    }
}
