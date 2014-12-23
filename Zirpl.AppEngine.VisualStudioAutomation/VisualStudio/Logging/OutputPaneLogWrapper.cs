using System;
using Microsoft.VisualStudio.Shell.Interop;
using Zirpl.AppEngine.Logging;

namespace Zirpl.AppEngine.VisualStudioAutomation.VisualStudio.Logging
{
    internal sealed class OutputPaneLogWrapper : LogBase
    {
        private readonly IVsOutputWindowPane _outputWindow;
        internal OutputPaneLogWrapper(IVsOutputWindowPane outputWindow)
        {
            this._outputWindow = outputWindow;
        }

        protected override void WriteMessageToLog(LogBase.MessageType messageType, object message, Exception exception = null)
        {
            if (exception != null)
            {
                this._outputWindow.OutputString("ZAE: " + message + " " + exception.ToString() + "\n");
            }
            else
            {
                this._outputWindow.OutputString("ZAE: " + message + "\n");
            }
            this._outputWindow.Activate(); // Brings this pane into view
        }

        protected override bool IsLogEnabled(LogBase.MessageType messageType)
        {
            return true;
        }
    }
}
