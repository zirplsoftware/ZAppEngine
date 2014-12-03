using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Zirpl.AppEngine.VisualStudioAutomation.TextTemplating
{
    public class TransformOutputFile
    {
        public TransformOutputFile(OutputFile outputFile)
        {
            if (outputFile == null)
            {
                throw new ArgumentNullException("outputFile");
            }
            this.OutputFile = outputFile;
            this.TemplateParameters = new Dictionary<string, object>();
        }

        public Type TemplateType { get; set; }
        public IDictionary<string, object> TemplateParameters { get; set; }
        public OutputFile OutputFile { get; set; }
    }
}
