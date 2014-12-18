using System;
using System.Collections.Generic;
using System.IO;
using System.Reflection;
using System.Text;
using EnvDTE;
using EnvDTE80;
using Microsoft.VisualStudio.Shell.Interop;
using Microsoft.VisualStudio.TextTemplating;

namespace Zirpl.AppEngine.VisualStudioAutomation.TextTemplating
{
    /// <summary>
    /// Manages a TextTransformation Session
    /// </summary>
    public sealed class TextTransformationContext: IDisposable
    {
        public static TextTransformationContext Instance { get; private set; }
        private static Object STATIC_SYNC_ROOT;

        public ITextTemplatingEngineHost Host { get { return this.CallingTemplate.Host; } }
        public DTE2 VisualStudio { get; private set; }
        public ProjectItem CallingTemplateProjectItem { get; private set; }
        internal ITextTransformation CallingTemplate { get; private set; }
        private OutputFileManager FileManager { get; set; }

        public static TextTransformationContext Create(TextTransformation callingTemplate)
        {
            if (callingTemplate == null)
            {
                throw new ArgumentNullException("callingTemplate");
            }
            var callingTemplateWrapper = new TextTransformationWrapper(callingTemplate);
            if (callingTemplateWrapper.Host == null)
            {
                throw new ArgumentException("Host cannot be null. Preprocessed templates need to be passed the calling templates.", "callingTemplate");
            }
            if (String.IsNullOrEmpty(callingTemplateWrapper.Host.TemplateFile))
            {
                throw new ArgumentException("Host.TemplateFile cannot be null. Preprocessed templates need to be passed the calling templates.", "callingTemplate");
            }
            var dte2 = (DTE2)((IServiceProvider)callingTemplateWrapper.Host).GetCOMService(typeof(DTE));
            if (dte2 == null)
            {
                throw new Exception("Could not load VisualStudio DTE2");
            }

            lock (StaticSyncRoot)
            {
                if (Instance != null)
                {
                    throw new Exception("Cannot call Create more than once");
                }

                var instance = new TextTransformationContext();
                instance.CallingTemplate = callingTemplateWrapper;
                instance.VisualStudio = dte2;
                instance.FileManager = new OutputFileManager(instance);
                instance.CallingTemplateProjectItem = dte2.GetProjectItem(callingTemplateWrapper.Host.TemplateFile);
                Instance = instance;
            }
            return Instance;
        }

        private static Object StaticSyncRoot
        {
            get
            {
                if (STATIC_SYNC_ROOT == null)
                {
                    System.Threading.Interlocked.CompareExchange(ref STATIC_SYNC_ROOT, new object(), null);
                }
                return STATIC_SYNC_ROOT;
            }
        }

        internal void StartFile(ITextTransformation textTransformation, OutputFile outputFile)
        {
            this.FileManager.StartFile(textTransformation, outputFile);
        }

        internal void EndFile()
        {
            this.FileManager.EndFile();
        }

        /// <summary>
        /// Writes a line to the build pane in visual studio and activates it
        /// </summary>
        /// <param name="message">Text to output - a \n is appended</param>
        public void LogLineToBuildPane(string message)
        {
            this.LogToBuildPane(String.Format("{0}\n", message));
        }

        /// <summary>
        /// Writes a string to the build pane in visual studio and activates it
        /// </summary>
        /// <param name="message">Text to output</param>
        public void LogToBuildPane(string message)
        {
            IVsOutputWindow outWindow = (this.Host as IServiceProvider).GetService(typeof(SVsOutputWindow)) as IVsOutputWindow;
            Guid generalPaneGuid = Microsoft.VisualStudio.VSConstants.OutputWindowPaneGuid.BuildOutputPane_guid;
            // P.S. There's also the GUID_OutWindowDebugPane available.
            IVsOutputWindowPane generalPane;
            outWindow.GetPane(ref generalPaneGuid, out generalPane);
            generalPane.OutputString(message);
            generalPane.Activate(); // Brings this pane into view
        }


        public void Dispose()
        {
            try
            {
                this.FileManager.Dispose();
            }
            finally
            {
                Instance = null;
            }
        }
    }
}
