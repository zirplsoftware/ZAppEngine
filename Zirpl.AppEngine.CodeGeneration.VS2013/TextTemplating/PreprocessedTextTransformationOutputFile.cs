using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Zirpl.AppEngine.VisualStudioAutomation.TextTemplating
{
    public class PreprocessedTextTransformationOutputFile : OutputFile
    {
        public PreprocessedTextTransformationOutputFile()
        {
            this.TemplateParameters = new Dictionary<string, object>();
        }

        public Type TemplateType { get; set; }
        public IDictionary<string, object> TemplateParameters { get; set; }
    }
}
