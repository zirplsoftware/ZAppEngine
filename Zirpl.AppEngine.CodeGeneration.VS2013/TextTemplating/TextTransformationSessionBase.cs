using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using Microsoft.VisualStudio.TextTemplating;

namespace Zirpl.AppEngine.CodeGeneration.TextTemplating
{
    /// <summary>
    /// Manages a TextTransformation Session
    /// </summary>
    public abstract class TextTransformationSessionBase<TConcrete>: IDisposable
        where TConcrete : TextTransformationSessionBase<TConcrete>, new()
    {
        protected TextTransformationSessionBase()
        {
        }

        public static TConcrete Instance { get; private set; }
        private static Object STATIC_SYNC_ROOT;

        public static TConcrete StartSession(TextTransformation callingTemplate)
        {
            lock (StaticSyncRoot)
            {
                if (Instance != null)
                {
                    Instance.Dispose();
                    Instance = null;
//                    throw new Exception("Cannot call StartSession more than once");
                }
                Instance = new TConcrete();
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
        
        public void Dispose()
        {
            FileManager.Finish();
        }
    }
}
