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
        internal ITextTransformation CallingTemplate { get; private set; }
        private StringBuilder CallingTemplateOriginalGenerationEnvironment { get; set; }
        private StringBuilder CurrentGenerationEnvironment { get; set; }
        private OutputFileManager FileManager { get; set; }
        private OutputFile CurrentOutputFile { get; set; }

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
                instance.CallingTemplateOriginalGenerationEnvironment = instance.CallingTemplate.GenerationEnvironment;
                instance.CurrentGenerationEnvironment = instance.CallingTemplateOriginalGenerationEnvironment;
                instance.VisualStudio = dte2;
                instance.FileManager = new OutputFileManager(instance);
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


        public void WriteFile(PreprocessedTextTransformationOutputFile file)
        {
            if (file == this.CurrentOutputFile)
            {
                this.EndFile();
            }
            else
            {
                this.EndFile();

                var template = Activator.CreateInstance(file.TemplateType);

                var templateWrapper = new PreprocessedTextTransformationWrapper(template);

                var session = new TextTemplatingSession();
                foreach (var parameter in file.TemplateParameters)
                {
                    session[parameter.Key] = parameter.Value;
                }
                session["TemplateOutputFile"] = file;
                templateWrapper.Session = session;
                templateWrapper.Initialize(); // Must call this to transfer values.

                file.Content = templateWrapper.TransformText();

                this.FileManager.CreateFile(file);
            }
        }

        public void WriteFile(OutputFile file)
        {
            if (file == this.CurrentOutputFile)
            {
                this.EndFile();
            }
            else
            {
                this.EndFile();

                this.FileManager.CreateFile(file);   
            }
        }

        public void StartFile(String fileName, String folderWithinProject = null, String destinationProjectName = null, BuildActionTypeEnum? buildAction = null, String customTool = null, bool? autoFormat = null, bool? overwrite = null, Encoding encoding = null)
        {
            var project = String.IsNullOrEmpty(destinationProjectName)
                ? null
                : VisualStudio.GetProject(destinationProjectName);

            this.StartFile(fileName, folderWithinProject, project, buildAction, customTool, autoFormat, overwrite, encoding);
        }

        public void StartFile(String fileName, String folderWithinProject = null, Project destinationProject = null, BuildActionTypeEnum? buildAction = null, String customTool = null, bool? autoFormat = null, bool? overwrite = null, Encoding encoding = null)
        {
            //this.EndFile();

            var outputFile = new OutputFile()
            {
                FileNameWithoutExtension = Path.GetFileNameWithoutExtension(fileName),
                FileExtension = Path.GetExtension(fileName),
                DestinationProject = destinationProject ?? VisualStudio.GetProjectItem(this.CallingTemplate.Host.TemplateFile).ContainingProject,
                FolderPathWithinProject = folderWithinProject,
                CustomTool = customTool
            };
            outputFile.BuildAction = buildAction ?? this.CurrentOutputFile.BuildAction;
            outputFile.CanOverrideExistingFile = overwrite ?? this.CurrentOutputFile.CanOverrideExistingFile;
            outputFile.AutoFormat = autoFormat ?? this.CurrentOutputFile.AutoFormat;
            outputFile.Encoding = encoding ?? this.CurrentOutputFile.Encoding;

            this.StartFile(outputFile);
        }

        public void StartFile(OutputFile file)
        {
            this.EndFile();

            this.CurrentOutputFile = file;
            this.CurrentGenerationEnvironment = new StringBuilder();
            this.CallingTemplate.GenerationEnvironment = this.CurrentGenerationEnvironment;
        }

        public void EndFile()
        {
            if (this.CurrentOutputFile != null)
            {
                this.CurrentOutputFile.Content += this.CurrentGenerationEnvironment.ToString();
                this.CurrentGenerationEnvironment = this.CallingTemplateOriginalGenerationEnvironment;
                this.CallingTemplate.GenerationEnvironment = this.CurrentGenerationEnvironment;
                this.FileManager.CreateFile(this.CurrentOutputFile);
                this.CurrentOutputFile = null;
            }
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
            IVsOutputWindow outWindow = (this.CallingTemplate.Host as IServiceProvider).GetService(typeof(SVsOutputWindow)) as IVsOutputWindow;
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
                this.EndFile();
                this.FileManager.Finish();
            }
            finally
            {
                Instance = null;
            }
        }
    }
}
