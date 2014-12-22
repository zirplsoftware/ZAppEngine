using System;
using System.CodeDom.Compiler;
using System.Collections.Generic;
using System.Reflection;
using System.Text;
using Microsoft.VisualStudio.TextTemplating;
using Zirpl.Reflection;

namespace Zirpl.AppEngine.VisualStudioAutomation.TextTemplating
{
    internal class TextTransformationWrapper : ITextTransformation
    {
        private readonly Object _preProcessedTextTransformation;
        private readonly TextTransformation _textTransformation;

        internal TextTransformationWrapper(Object textTransformation)
        {
            if (textTransformation == null)
            {
                throw new ArgumentNullException("textTransformation");
            }
            if (textTransformation is TextTransformation)
            {
                this._textTransformation = (TextTransformation)textTransformation;
            }
            else
            {
                this._preProcessedTextTransformation = textTransformation;
            }
        }

        public bool IsPreProcessed
        {
            get { return this._preProcessedTextTransformation != null; }
        }

        public StringBuilder GenerationEnvironment
        {
            get
            {
                return (this._textTransformation ?? this._preProcessedTextTransformation).Access().Property<StringBuilder>("GenerationEnvironment");
            }
            set
            {
                (this._textTransformation ?? this._preProcessedTextTransformation).Access().Property("GenerationEnvironment", value);
            }
        }

        public CompilerErrorCollection Errors
        {
            get
            {
                return this._textTransformation != null ? this._textTransformation.Errors : this._preProcessedTextTransformation.Access().Property<CompilerErrorCollection>("Errors");
            }
        }

        public ITextTemplatingEngineHost Host
        {
            get
            {
                if (this._preProcessedTextTransformation != null
                    && !this._preProcessedTextTransformation.Access().HasGet<ITextTemplatingEngineHost>("Host"))
                {
                    return (this._textTransformation ?? this._preProcessedTextTransformation).Access().Property<ITextTemplatingEngineHost>("Host");
                }
                return null;
            }
            set
            {
                if (value != null
                    && this._preProcessedTextTransformation != null
                    && !this._preProcessedTextTransformation.Access().HasSet<ITextTemplatingEngineHost>("Host"))
                {
                    throw new InvalidOperationException("Template is not Host-specific");
                }
                (this._textTransformation ?? this._preProcessedTextTransformation).Access().Property("Host", value);
            }
        }

        public string CurrentIndent
        {
            get
            {
                return (this._textTransformation ?? this._preProcessedTextTransformation).Access().Property<String>("CurrentIndent");
            }
        }

        public IDictionary<string, object> Session
        {
            get
            {
                return (this._textTransformation ?? this._preProcessedTextTransformation).Access().Property<IDictionary<string, object>>("Session");
            }
            set
            {
                (this._textTransformation ?? this._preProcessedTextTransformation).Access().Property("Session", value);
            }
        }

        public void ClearIndent()
        {
            if (this._textTransformation != null)
            {
                this._textTransformation.ClearIndent();
            }
            else
            {
                this._preProcessedTextTransformation.Access().Invoke("ClearIndent");
            }
        }

        public void Dispose()
        {
        }

        public void Error(string message)
        {
            if (this._textTransformation != null)
            {
                this._textTransformation.Error(message);
            }
            else
            {
                this._preProcessedTextTransformation.Access().Invoke("Error", message);
            }
        }

        public void Initialize()
        {
            if (this._textTransformation != null)
            {
                this._textTransformation.Initialize();
            }
            else
            {
                this._preProcessedTextTransformation.Access().Invoke("Initialize");
            }
        }

        public string PopIndent()
        {
            if (this._textTransformation != null)
            {
                return this._textTransformation.PopIndent();
            }
            else
            {
                return this._preProcessedTextTransformation.Access().Invoke<String>("PopIndent");
            }
        }

        public void PushIndent(string indent)
        {
            if (this._textTransformation != null)
            {
                this._textTransformation.PushIndent(indent);
            }
            else
            {
                this._preProcessedTextTransformation.Access().Invoke("PushIndent", indent);
            }
        }

        public string TransformText()
        {
            if (this._textTransformation != null)
            {
                return this._textTransformation.TransformText();
            }
            else
            {
                return this._preProcessedTextTransformation.Access().Invoke<String>("TransformText");
            }
        }

        public void Warning(string message)
        {
            if (this._textTransformation != null)
            {
                this._textTransformation.Warning(message);
            }
            else
            {
                this._preProcessedTextTransformation.Access().Invoke("Warning", message);
            }
        }

        public void Write(string text)
        {
            if (this._textTransformation != null)
            {
                this._textTransformation.Write(text);
            }
            else
            {
                this._preProcessedTextTransformation.Access().Invoke("Write", text);
            }
        }

        public void Write(string format, params object[] args)
        {
            if (this._textTransformation != null)
            {
                this._textTransformation.Write(format, args);
            }
            else
            {
                this._preProcessedTextTransformation.Access().Invoke("Write", format, args);
            }
        }

        public void WriteLine(string text)
        {
            if (this._textTransformation != null)
            {
                this._textTransformation.WriteLine(text);
            }
            else
            {
                this._preProcessedTextTransformation.Access().Invoke("WriteLine", text);
            }
        }

        public void WriteLine(string format, params object[] args)
        {
            if (this._textTransformation != null)
            {
                this._textTransformation.WriteLine(format, args);
            }
            else
            {
                this._preProcessedTextTransformation.Access().Invoke("WriteLine", format, args);
            }
        }

        public override bool Equals(object obj)
        {
            return (this._textTransformation ?? this._preProcessedTextTransformation).Equals(obj);
        }

        public override int GetHashCode()
        {
            return (this._textTransformation ?? this._preProcessedTextTransformation).GetHashCode();
        }

        public override string ToString()
        {
            return (this._textTransformation ?? this._preProcessedTextTransformation).ToString();
        }


        public string FileExtension
        {
            get
            {
                if (this.Host != null)
                {
                    return this.Host.Access().Property<String>("FileExtension");
                }
                return null;
            }
        }
    }
}
