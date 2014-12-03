using System;
using System.Collections.Generic;
using EnvDTE;
using EnvDTE80;
using Microsoft.VisualStudio.Shell.Interop;
using Microsoft.VisualStudio.TextTemplating;

namespace Zirpl.AppEngine.VisualStudioAutomation.TextTemplating
{
    /// <summary>
    /// Manages a TextTransformation Session
    /// </summary>
    public sealed class TransformSession: IDisposable
    {
        public static TransformSession Instance { get; private set; }
        private static Object STATIC_SYNC_ROOT;

        public ProjectItem CallingTemplateProjectItem { get; private set; }
        private ITransformation CallingTemplate { get; set; }
        private ITextTemplatingEngineHost Host { get { return this.CallingTemplate.Host; } }
        private OutputFileManager FileManager { get; set; }

        public static TransformSession StartSession(TextTransformation callingTemplate)
        {
            if (callingTemplate == null)
            {
                throw new ArgumentNullException("callingTemplate");
            }
            var callingTemplateWrapper = new TransformWrapper(callingTemplate);
            if (callingTemplateWrapper.Host == null)
            {
                throw new ArgumentException("Host cannot be null. Preprocessed templates need to be passed the calling templates.", "callingTemplate");
            }
            if (String.IsNullOrEmpty(callingTemplateWrapper.Host.TemplateFile))
            {
                throw new ArgumentException("Host.TemplateFile cannot be null. Preprocessed templates need to be passed the calling templates.", "callingTemplate");
            }
            if (VisualStudio.Current == null)
            {
                throw new Exception("Could not load VisualStudio DTE2");
            }
            var templateFilePath = callingTemplateWrapper.Host.TemplateFile;
            var templateProjectItem = VisualStudio.Current.GetProjectItem(templateFilePath);
            if (templateProjectItem == null)
            {
                throw new Exception("Could not obtain CallingTemplateProjectItem from " + templateFilePath);
            }

            lock (StaticSyncRoot)
            {
                if (Instance != null)
                {
                    throw new Exception("Cannot call StartSession more than once");
                }

                var instance = new TransformSession();
                instance.CallingTemplate = callingTemplateWrapper;
                instance.CallingTemplateProjectItem = templateProjectItem;
                instance.FileManager = new OutputFileManager();
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


        public void ExecuteTransform(TemplateOutputFile file, IDictionary<String, Object> additionalTemplateParameters = null)
        {
            var template = Activator.CreateInstance(file.TemplateType);

            var templateWrapper = new PreprocessedTransformWrapper(template);

            var session = new TextTemplatingSession();
            if (additionalTemplateParameters != null)
            {
                foreach (var parameter in additionalTemplateParameters)
                {
                    session[parameter.Key] = parameter.Value;
                }
            }
            session["TemplateOutputFile"] = file;
            templateWrapper.Session = session;
            templateWrapper.Initialize(); // Must call this to transfer values.

            file.Content = templateWrapper.TransformText();

            this.FileManager.CreateFile(file);

            this.CallingTemplate.WriteLine("//--> " + file.FilePathWithinProject);
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
                // TODO: reenable if we have this again
                this.FileManager.Finish();
            }
            finally
            {
                Instance = null;
            }
        }
    }
}
