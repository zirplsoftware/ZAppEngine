using System;
using System.Collections.Generic;
using EnvDTE;
using EnvDTE80;
using Microsoft.VisualStudio.TextTemplating;

namespace Zirpl.AppEngine.CodeGeneration.TextTemplating
{
    /// <summary>
    /// Manages a TextTransformation Session
    /// </summary>
    public sealed class TextTransformationSession: IDisposable
    {
        public static TextTransformationSession Instance { get; private set; }
        private static Object STATIC_SYNC_ROOT;

        public static TextTransformationSession StartSession(TextTransformation callingTemplate)
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
            var visualStudio = VisualStudioExtensions.GetCurrentVisualStudioInstance();
            if (visualStudio == null)
            {
                throw new Exception("Could not obtain DTE2");
            }
            var templateFilePath = callingTemplateWrapper.Host.TemplateFile;
            var templateProjectItem = visualStudio.FindProjectItem(templateFilePath);
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

                var instance = new TextTransformationSession();
                instance.CallingTemplate = callingTemplateWrapper;
                instance.Host = callingTemplateWrapper.Host;
                instance.VisualStudio = visualStudio;
                instance.TemplateProjectItem = templateProjectItem;
                instance.FileManager = new TemplateFileManager();
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

        public ITextTransformation CallingTemplate { get; private set; }
        public TemplateFileManager FileManager { get; private set; }
        public ITextTemplatingEngineHost Host { get; private set; }
        public DTE2 VisualStudio { get; private set; }
        public ProjectItem TemplateProjectItem { get; private set; }

        public void CreateFile(FileToGenerate fileToGenerate, IDictionary<String, Object> additionalTemplateParameters = null)
        {
            var template = Activator.CreateInstance(fileToGenerate.TemplateType);

            var templateWrapper = new PreprocessedTextTransformationWrapper(template);
            templateWrapper.Host = this.CallingTemplate.Host;
            templateWrapper.GenerationEnvironment = this.CallingTemplate.GenerationEnvironment;


            var session = new Microsoft.VisualStudio.TextTemplating.TextTemplatingSession();
            session["FileToGenerate"] = fileToGenerate;
            if (additionalTemplateParameters != null)
            {
                foreach (var parameter in additionalTemplateParameters)
                {
                    session[parameter.Key] = parameter.Value;
                }
            }
            templateWrapper.Session = session;
            templateWrapper.Initialize(); // Must call this to transfer values.

            this.FileManager.StartNewFile(
                fileToGenerate.FileName,
                fileToGenerate.DestinationProject,
                fileToGenerate.FolderPath,
                new OutputFileProperties() { BuildAction = fileToGenerate.BuildAction });

            templateWrapper.TransformText();
        }


        public void Dispose()
        {
            try
            {
                this.FileManager.Finish();
            }
            finally
            {
                Instance = null;
            }
        }
    }
}
