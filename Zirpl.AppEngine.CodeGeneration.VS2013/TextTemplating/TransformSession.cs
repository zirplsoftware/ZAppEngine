using System;
using System.Collections.Generic;
using EnvDTE;
using EnvDTE80;
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
                throw new Exception("Could not obtain TemplateProjectItem from " + templateFilePath);
            }

            lock (StaticSyncRoot)
            {
                if (Instance != null)
                {
                    throw new Exception("Cannot call StartSession more than once");
                }

                var instance = new TransformSession();
                instance.CallingTemplate = callingTemplateWrapper;
                instance.Host = callingTemplateWrapper.Host;
                instance.TemplateProjectItem = templateProjectItem;
                //instance.FileManager = new OutputFileManager();
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

        public ITransformation CallingTemplate { get; private set; }
        //public OutputFileManager FileManager { get; private set; }
        public ITextTemplatingEngineHost Host { get; private set; }
        public ProjectItem TemplateProjectItem { get; private set; }

        public void TransformToFile(TransformOutputFile file, IDictionary<String, Object> additionalTemplateParameters = null)
        {
            var template = Activator.CreateInstance(file.TemplateType);

            var templateWrapper = new PreprocessedTransformWrapper(template);
            templateWrapper.Host = this.CallingTemplate.Host;
            //templateWrapper.GenerationEnvironment = this.CallingTemplate.GenerationEnvironment;

            var session = new Microsoft.VisualStudio.TextTemplating.TextTemplatingSession();
            session["FileToGenerate"] = file;
            if (additionalTemplateParameters != null)
            {
                foreach (var parameter in additionalTemplateParameters)
                {
                    session[parameter.Key] = parameter.Value;
                }
            }
            templateWrapper.Session = session;
            templateWrapper.Initialize(); // Must call this to transfer values.

            file.OutputFile.Content = templateWrapper.TransformText();

            new OutputFileManager().CreateFile(file.OutputFile);

            // TODO: write the file, add to VS and handle any filewriting responsibilities such as checking out from source control

            this.CallingTemplate.WriteLine("// File generated: " + file.OutputFile.FilePathWithinProject);
        }


        public void Dispose()
        {
            try
            {
                // TODO: reenable if we have this again
                //this.FileManager.Finish();
            }
            finally
            {
                Instance = null;
            }
        }
    }
}
