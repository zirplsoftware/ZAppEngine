using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using Microsoft.VisualStudio.TextTemplating;

namespace Zirpl.AppEngine.CodeGeneration.TextTemplating
{
    public abstract class TransformationHelperBase :IDisposable
    {
        protected TransformationHelperBase(TextTransformation callingTemplate)
        {
            this.CallingTemplate = new TextTransformationWrapper(callingTemplate);
            this.FileManager = new TemplateFileManager(this.CallingTemplate);
        }

        public ITextTransformation CallingTemplate { get; private set; }
        public TemplateFileManager FileManager { get; private set; }
        
        public void Dispose()
        {
            this.FileManager.Finish();
        }
    }
}
