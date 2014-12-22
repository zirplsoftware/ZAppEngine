using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using Microsoft.VisualStudio.Shell.Interop;
using Microsoft.VisualStudio.TextTemplating;
using Zirpl.AppEngine.Logging;

namespace Zirpl.AppEngine.VisualStudioAutomation.Logging
{
    internal class LogFactory : ILogFactory
    {
        private readonly IVsOutputWindowPane _outputWindow;

        internal LogFactory(ITextTemplatingEngineHost host)
        {
            var outWindow = (IVsOutputWindow) (host as IServiceProvider).GetService(typeof (SVsOutputWindow));
            var generalPaneGuid = Microsoft.VisualStudio.VSConstants.OutputWindowPaneGuid.BuildOutputPane_guid;
            // P.S. There's also the GUID_OutWindowDebugPane available.
            outWindow.GetPane(ref generalPaneGuid, out _outputWindow);
            this._outputWindow.OutputString("\n");
        }

        public ILog GetLog(string name)
        {
            return new OutputPaneLogWrapper(this._outputWindow);
        }
    }
}
