using System;
using Microsoft.VisualStudio.Shell.Interop;
using Zirpl.AppEngine.Logging;

namespace Zirpl.AppEngine.VisualStudioAutomation.VisualStudio.Logging
{
    internal sealed class LogFactory : ILogFactory
    {
        private readonly IVsOutputWindowPane _outputWindow;
        private static bool _isInitialized;

        internal static void Initialize(IServiceProvider serviceProvider)
        {
            if (!_isInitialized)
            {
                LogManager.LogFactory = new LogFactory(serviceProvider);
                _isInitialized = true;
            }
        }

        internal LogFactory(IServiceProvider serviceProvider)
        {
            var outWindow = (IVsOutputWindow)serviceProvider.GetService(typeof(SVsOutputWindow));
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
