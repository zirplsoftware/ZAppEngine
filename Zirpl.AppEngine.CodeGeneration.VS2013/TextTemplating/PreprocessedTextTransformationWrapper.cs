using System;
using System.CodeDom.Compiler;
using System.Collections.Generic;
using System.Reflection;
using System.Text;
using Microsoft.VisualStudio.TextTemplating;
using Zirpl.Reflection;

namespace Zirpl.AppEngine.VisualStudioAutomation.TextTemplating
{
    public class PreprocessedTextTransformationWrapper : ITextTransformation
    {
        private readonly Object _textTransformation;
        private readonly IDynamicAccessor _dynamicAccessor;

        public PreprocessedTextTransformationWrapper(Object textTransformation)
        {
            this._textTransformation = textTransformation;
            this._dynamicAccessor = textTransformation.GetDynamicAccessor();
        }

        public StringBuilder GenerationEnvironment
        {
            get
            {
                return (StringBuilder)this._dynamicAccessor.GetPropertyValue(this._textTransformation, "GenerationEnvironment");
            }
            set
            {
                this._dynamicAccessor.SetPropertyValue(this._textTransformation, "GenerationEnvironment", value);
            }
        }

        public CompilerErrorCollection Errors
        {
            get
            {
                return (CompilerErrorCollection) this._dynamicAccessor.GetPropertyValue(this._textTransformation, "Errors");
            }
        }

        public ITextTemplatingEngineHost Host
        {
            get
            {
                return (ITextTemplatingEngineHost)this._dynamicAccessor.GetPropertyValue(this._textTransformation, "Host");
            }
            set
            {
                this._dynamicAccessor.SetPropertyValue(this._textTransformation, "Host", value);
            }
        }

        public string CurrentIndent
        {
            get
            {
                return (string)this._dynamicAccessor.GetPropertyValue(this._textTransformation, "CurrentIndent");
            }
        }

        public IDictionary<string, object> Session
        {
            get
            {
                return (IDictionary<string, object>)this._dynamicAccessor.GetPropertyValue(this._textTransformation, "Session");
            }
            set
            {
                this._dynamicAccessor.SetPropertyValue(this._textTransformation, "Session", value);
            }
        }

        public void ClearIndent()
        {
            this._textTransformation.GetType().GetMethod("ClearIndent", BindingFlags.Instance | BindingFlags.NonPublic | BindingFlags.Public).Invoke(this._textTransformation, null);
        }

        public void Dispose()
        {
        }

        public void Error(string message)
        {
            this._textTransformation.GetType().GetMethod("Error", BindingFlags.Instance | BindingFlags.NonPublic | BindingFlags.Public).Invoke(this._textTransformation, new Object[] { message });
        }

        public void Initialize()
        {
            this._textTransformation.GetType().GetMethod("Initialize", BindingFlags.Instance | BindingFlags.NonPublic | BindingFlags.Public).Invoke(this._textTransformation, null);
        }

        public string PopIndent()
        {
            return (String)this._textTransformation.GetType().GetMethod("PopIndent", BindingFlags.Instance | BindingFlags.NonPublic | BindingFlags.Public).Invoke(this._textTransformation, null);
        }

        public void PushIndent(string indent)
        {
            this._textTransformation.GetType().GetMethod("PushIndent", BindingFlags.Instance | BindingFlags.NonPublic | BindingFlags.Public).Invoke(this._textTransformation, new Object[] { indent });
        }

        public string TransformText()
        {
            return (String)this._textTransformation.GetType().GetMethod("TransformText", BindingFlags.Instance | BindingFlags.NonPublic | BindingFlags.Public).Invoke(this._textTransformation, null);
        }

        public void Warning(string message)
        {
            this._textTransformation.GetType().GetMethod("Warning", BindingFlags.Instance | BindingFlags.NonPublic | BindingFlags.Public).Invoke(this._textTransformation, new Object[] { message });
        }

        public void Write(string text)
        {
            this._textTransformation.GetType().GetMethod("Write", BindingFlags.Instance | BindingFlags.NonPublic | BindingFlags.Public).Invoke(this._textTransformation, new Object[] { text });
        }

        public void Write(string format, params object[] args)
        {
            this._textTransformation.GetType().GetMethod("Write", BindingFlags.Instance | BindingFlags.NonPublic | BindingFlags.Public).Invoke(this._textTransformation, new Object[] { format, args });
        }

        public void WriteLine(string text)
        {
            this._textTransformation.GetType().GetMethod("WriteLine", BindingFlags.Instance | BindingFlags.NonPublic | BindingFlags.Public).Invoke(this._textTransformation, new Object[] { text });
        }

        public void WriteLine(string format, params object[] args)
        {
            this._textTransformation.GetType().GetMethod("WriteLine", BindingFlags.Instance | BindingFlags.NonPublic | BindingFlags.Public).Invoke(this._textTransformation, new Object[] { format, args });
        }

        public override bool Equals(object obj)
        {
            return this._textTransformation.Equals(obj);
        }

        public override int GetHashCode()
        {
            return this._textTransformation.GetHashCode();
        }

        public override string ToString()
        {
            return this._textTransformation.ToString();
        }
    }
}
