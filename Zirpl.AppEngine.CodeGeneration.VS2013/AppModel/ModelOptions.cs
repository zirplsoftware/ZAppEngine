using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Zirpl.AppEngine.CodeGeneration.AppModel
{
    public class ModelOptions
    {
        public ModelOptions()
        {
            this.GenerateModel = true;
            this.GenerateMetadata = true;
            this.GenerateEnum = true;
            this.GenerateSupportViewModel = true;
        }
        public bool GenerateModel { get; set; }
        public bool GenerateMetadata { get; set; }
        public bool GenerateEnum { get; set; }
        public bool GenerateSupportViewModel { get; set; }
    }
}
