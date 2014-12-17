using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;

namespace Zirpl.AppEngine.VisualStudioAutomation.AppGeneration.Config
{
    public class AppGenerationSettings
    {
        public AppGenerationSettings()
        {
            this.GlobalTemplateParameters = new Dictionary<string, object>();
        }
        public String ProjectNamespacePrefix { get; set; }
        public String GeneratedContentRootFolderName { get; set; }
        public String DataContextName { get; set; }
        public IDictionary<String, Object> GlobalTemplateParameters { get; private set; }
    }
}
