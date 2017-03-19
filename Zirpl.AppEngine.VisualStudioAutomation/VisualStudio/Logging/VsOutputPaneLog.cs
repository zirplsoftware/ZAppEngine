using System;
using Microsoft.VisualStudio.Shell.Interop;

namespace Zirpl.AppEngine.VisualStudioAutomation.VisualStudio.Logging
{
    internal sealed class VsOutputPaneLog : IVsOutputWindowLog
    {
        private readonly IVsOutputWindowPane _outputWindow;
        private readonly string _name;

        internal VsOutputPaneLog(IVsOutputWindowPane outputWindow, string name)
        {
            _outputWindow = outputWindow;
            _name = name;
        }

        private void WriteMessageToLog(string logLevel, object message, Exception exception = null)
        {
            this._outputWindow.OutputString($"ZAE: {logLevel.ToUpperInvariant()} {_name ?? string.Empty}: {message ?? string.Empty} {exception?.ToString() ?? string.Empty}\n");
            this._outputWindow.Activate(); // Brings this pane into view
        }

        public void Trace(string message, Exception exception = null)
        {
            WriteMessageToLog("Trace", message, exception);
        }

        public void Debug(string message, Exception exception = null)
        {
            WriteMessageToLog("Debug", message, exception);
        }

        public void Info(string message, Exception exception = null)
        {
            WriteMessageToLog("Info", message, exception);
        }

        public void Warn(string message, Exception exception = null)
        {
            WriteMessageToLog("Warn", message, exception);
        }

        public void Error(string message, Exception exception = null)
        {
            WriteMessageToLog("Error", message, exception);
        }

        public void Fatal(string message, Exception exception = null)
        {
            WriteMessageToLog("Fatal", message, exception);
        }
    }
}
