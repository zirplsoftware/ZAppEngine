using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using Microsoft.VisualStudio.TextTemplating;
using Zirpl.AppEngine.Logging;
using Zirpl.AppEngine.VisualStudioAutomation.Logging;

namespace Zirpl.AppEngine.VisualStudioAutomation.TextTemplating
{
    public abstract class TemplateBase : TextTransformation
    {
        private readonly IOutputFileManager _outputFileManager;

        public IOutputFileManager FileManager { get { return this._outputFileManager; } }

        protected TemplateBase()
            : base()
        {
            if (this.GetHost() == null)
            {
                throw new InvalidOperationException("Host cannot be null. Preprocessed templates need to be passed the calling templates.");
            }
            if (this.GetVisualStudio() == null)
            {
                throw new InvalidOperationException("Could not load VisualStudio DTE2");
            }
            //if (this.GetProjectItem() == null)
            //{
            //    throw new InvalidOperationException("Host.TemplateFile cannot be null. Preprocessed templates need to be passed the calling templates.");
            //}

            // initialize logging
            LogManager.LogFactory = new LogFactory(this.GetHost());
            // initialize the FileManager
            this._outputFileManager = new OutputFileManager(this);

        }

        protected override void Dispose(bool disposing)
        {
            this.FileManager.Dispose();

            base.Dispose(disposing);
        }
    }
}
