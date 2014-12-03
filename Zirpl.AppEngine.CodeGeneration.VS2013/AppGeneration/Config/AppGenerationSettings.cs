using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using Zirpl.AppEngine.VisualStudioAutomation.AppGeneration.TextTemplating;

namespace Zirpl.AppEngine.VisualStudioAutomation.AppGeneration.Config
{
    public class AppGenerationSettings
    {
        public AppGenerationSettings()
        {
            this.TemplateParameters = new Dictionary<string, object>();
            this.BuilderStrategies = new List<ITemplateOutputFileBuilderStrategy>();
        }
        public String ProjectNamespacePrefix { get; set; }
        public String GeneratedContentRootFolderName { get; set; }
        public String DataContextName { get; set; }
        public IDictionary<String, Object> TemplateParameters { get; private set; }
        public IList<ITemplateOutputFileBuilderStrategy> BuilderStrategies { get; private set; }  
    }
}
