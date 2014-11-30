using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using EnvDTE;
using Microsoft.VisualStudio.TextTemplating;
using Zirpl.AppEngine.CodeGeneration.V2;
using Zirpl.AppEngine.CodeGeneration.V2.ConfigModel;
using Zirpl.AppEngine.CodeGeneration.V2.Parsers;
using Zirpl.Reflection;

namespace Zirpl.AppEngine.CodeGeneration.TextTemplating
{
    /// <summary>
    /// Manages a TextTransformation Session
    /// </summary>
    public class TextTransformationSession: IDisposable
    {
        public static TextTransformationSession Instance { get; private set; }
        private static Object STATIC_SYNC_ROOT;

        public static TextTransformationSession StartSession(TextTransformation callingTemplate)
        {
            lock (StaticSyncRoot)
            {
                if (Instance != null)
                {
                    Instance.Dispose();
                    Instance = null;
//                    throw new Exception("Cannot call StartSession more than once");
                }
                Instance = new TextTransformationSession();
                Instance.CallingTemplate = new TextTransformationWrapper(callingTemplate);
                Instance.FileManager = new TemplateFileManager(Instance.CallingTemplate);
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
            this.FileManager.Finish();
        }
    }
}
