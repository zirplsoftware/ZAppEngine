using System;
using System.Collections.Generic;
using System.Linq;
using System.Runtime.Serialization.Formatters;
using System.Text;
using System.Threading.Tasks;
using Microsoft.VisualStudio.Shell.Interop;
using Microsoft.VisualStudio.TextTemplating;
using Zirpl.AppEngine.Logging;

namespace Zirpl.AppEngine.VisualStudioAutomation.Logging
{
    internal class OutputPaneLogWrapper : LogBase
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
