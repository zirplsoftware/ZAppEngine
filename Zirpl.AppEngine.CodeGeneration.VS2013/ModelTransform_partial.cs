using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Zirpl.AppEngine.CodeGeneration
{
    public partial class ModelTransform
    {
        public ModelTransform(global::Microsoft.VisualStudio.TextTemplating.ITextTemplatingEngineHost host, AppGenerator appGenerator)
        {
            this.Host = host;
            this.AppGenerator = appGenerator;
        }

        public AppGenerator AppGenerator { get; set; }
    }
    public partial class ModelMetadataTransform
    {
        public ModelMetadataTransform(global::Microsoft.VisualStudio.TextTemplating.ITextTemplatingEngineHost host, AppGenerator appGenerator)
        {
            this.Host = host;
            this.AppGenerator = appGenerator;
        }

        public AppGenerator AppGenerator { get; set; }
    }
}
