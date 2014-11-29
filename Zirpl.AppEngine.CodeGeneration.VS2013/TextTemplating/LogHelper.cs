using System;
using Microsoft.VisualStudio.Shell.Interop;
using Microsoft.VisualStudio.TextTemplating;

namespace Zirpl.AppEngine.CodeGeneration.TextTemplating
{
    public static class LogHelper
    {
        /// <summary>
        /// Writes a line to the build pane in visual studio and activates it
        /// </summary>
        /// <param name="message">Text to output - a \n is appended</param>
        public static void LogLineToBuildPane(this TransformationHelperBase helper, string message)
        {
            helper.CallingTemplate.Host.LogToBuildPane(String.Format("{0}\n", message));
        }

        /// <summary>
        /// Writes a line to the build pane in visual studio and activates it
        /// </summary>
        /// <param name="message">Text to output - a \n is appended</param>
        public static void LogLineToBuildPane(this ITextTemplatingEngineHost host, string message)
        {
            host.LogToBuildPane(String.Format("{0}\n", message));
        }

        /// <summary>
        /// Writes a string to the build pane in visual studio and activates it
        /// </summary>
        /// <param name="message">Text to output</param>
        public static void LogToBuildPane(this ITextTemplatingEngineHost host, string message)
        {
            IVsOutputWindow outWindow = (host as IServiceProvider).GetService(typeof(SVsOutputWindow)) as IVsOutputWindow;
            Guid generalPaneGuid = Microsoft.VisualStudio.VSConstants.OutputWindowPaneGuid.BuildOutputPane_guid;
            // P.S. There's also the GUID_OutWindowDebugPane available.
            IVsOutputWindowPane generalPane;
            outWindow.GetPane(ref generalPaneGuid, out generalPane);
            generalPane.OutputString(message);
            generalPane.Activate(); // Brings this pane into view
        }
    }
}
