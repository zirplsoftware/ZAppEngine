using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Zirpl.AppEngine.VisualStudioAutomation.AppGeneration.Config
{
    public class AppGenerationSettings
    {
        public AppGenerationSettings()
        {
            this.TemplateParameters = new Dictionary<string, object>();
        }
        public String ProjectNamespacePrefix { get; set; }
        public String GeneratedContentRootFolderName { get; set; }
        public String DataContextName { get; set; }
        public IDictionary<String, Object> TemplateParameters { get; private set; }
    }
}
