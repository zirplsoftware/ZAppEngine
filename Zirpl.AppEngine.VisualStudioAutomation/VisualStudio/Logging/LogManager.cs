using System;
using Microsoft.VisualStudio.Shell.Interop;

namespace Zirpl.AppEngine.VisualStudioAutomation.VisualStudio.Logging
{
    internal static class LogManager
    {
        private static IVsOutputWindowPane _outputWindow;
        private static bool _isInitialized;

        internal static void Initialize(IServiceProvider serviceProvider)
        {
            if (!_isInitialized)
            {
                var outWindow = (IVsOutputWindow)serviceProvider.GetService(typeof(SVsOutputWindow));
                var generalPaneGuid = Microsoft.VisualStudio.VSConstants.OutputWindowPaneGuid.BuildOutputPane_guid;
                // P.S. There's also the GUID_OutWindowDebugPane available.
                outWindow.GetPane(ref generalPaneGuid, out _outputWindow);
                _outputWindow.OutputString("\n");

                _isInitialized = true;
            }
        }

        public static IVsOutputWindowLog GetLog(string logName = null)
        {
            return new VsOutputPaneLog(_outputWindow, logName);
        }

        public static IVsOutputWindowLog GetLog(this object logContext)
        {
            return new VsOutputPaneLog(_outputWindow, logContext.GetType().FullName);
        }

        public static IVsOutputWindowLog GetLog<T>()
        {
            return new VsOutputPaneLog(_outputWindow, typeof(T).FullName);
        }

        public static IVsOutputWindowLog GetLog(Type typeOfLogContext)
        {
            return new VsOutputPaneLog(_outputWindow, typeOfLogContext.FullName);
        }
    }

    public interface IVsOutputWindowLog
    {
        void Trace(string message, Exception exception = null);
        void Debug(string message, Exception exception = null);
        void Info(string message, Exception exception = null);
        void Warn(string message, Exception exception = null);
        void Error(string message, Exception exception = null);
        void Fatal(string message, Exception exception = null);
    }
}
