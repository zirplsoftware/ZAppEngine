using System;
using Microsoft.VisualStudio.Shell.Interop;
using Microsoft.VisualStudio.TextTemplating;
using Zirpl.AppEngine.Logging;
using Zirpl.AppEngine.VisualStudioAutomation.VisualStudio.Logging;

namespace Zirpl.AppEngine.VisualStudioAutomation.TextTemplating.Logging
{
    internal sealed class LogFactory : ILogFactory
    {
        private readonly IVsOutputWindowPane _outputWindow;
        private static bool _isInitialized;

        internal static void Initialize(TextTransformation textTransformation)
        {
            if (!_isInitialized)
            {
                LogManager.LogFactory = new LogFactory(textTransformation.Wrap().Host);
                _isInitialized = true;
            }
        }

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
