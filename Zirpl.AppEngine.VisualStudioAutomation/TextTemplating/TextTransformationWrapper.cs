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
                return (this._textTransformation ?? this._preProcessedTextTransformation).GetAccessor().GetProperty<StringBuilder>("GenerationEnvironment");
            }
            set
            {
                (this._textTransformation ?? this._preProcessedTextTransformation).GetAccessor().SetProperty("GenerationEnvironment", value);
            }
        }

        public CompilerErrorCollection Errors
        {
            get
            {
                return this._textTransformation != null ? this._textTransformation.Errors : this._preProcessedTextTransformation.GetAccessor().GetProperty<CompilerErrorCollection>("Errors");
            }
        }

        public ITextTemplatingEngineHost Host
        {
            get
            {
                if (this._preProcessedTextTransformation != null
                    && !this._preProcessedTextTransformation.GetTypeAccessor().HasPropertyGetter<ITextTemplatingEngineHost>("Host"))
                {
                    throw new InvalidOperationException("Template is not Host-specific");
                }

                return (this._textTransformation ?? this._preProcessedTextTransformation).GetAccessor().GetProperty<ITextTemplatingEngineHost>("Host");
            }
            set
            {
                if (this._preProcessedTextTransformation != null
                    && !this._preProcessedTextTransformation.GetTypeAccessor().HasPropertySetter<ITextTemplatingEngineHost>("Host"))
                {
                    throw new InvalidOperationException("Template is not Host-specific");
                }
                (this._textTransformation ?? this._preProcessedTextTransformation).GetAccessor().SetProperty("Host", value);
            }
        }

        public string CurrentIndent
        {
            get
            {
                return (this._textTransformation ?? this._preProcessedTextTransformation).GetAccessor().GetProperty<String>("CurrentIndent");
            }
        }

        public IDictionary<string, object> Session
        {
            get
            {
                return (this._textTransformation ?? this._preProcessedTextTransformation).GetAccessor().GetProperty<IDictionary<string, object>>("Session");
            }
            set
            {
                (this._textTransformation ?? this._preProcessedTextTransformation).GetAccessor().SetProperty("Session", value);
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
                this._preProcessedTextTransformation.GetAccessor().InvokeMethod("ClearIndent");
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
                this._preProcessedTextTransformation.GetAccessor().InvokeMethod("Error", message);
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
                this._preProcessedTextTransformation.GetAccessor().InvokeMethod("Initialize");
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
                return this._preProcessedTextTransformation.GetAccessor().InvokeMethod<String>("PopIndent");
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
                this._preProcessedTextTransformation.GetAccessor().InvokeMethod("PushIndent", indent);
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
                return this._preProcessedTextTransformation.GetAccessor().InvokeMethod<String>("TransformText");
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
                this._preProcessedTextTransformation.GetAccessor().InvokeMethod("Warning", message);
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
                this._preProcessedTextTransformation.GetAccessor().InvokeMethod("Write", text);
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
                this._preProcessedTextTransformation.GetAccessor().InvokeMethod("Write", format, args);
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
                this._preProcessedTextTransformation.GetAccessor().InvokeMethod("WriteLine", text);
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
                this._preProcessedTextTransformation.GetAccessor().InvokeMethod("WriteLine", format, args);
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
                    return this.Host.GetAccessor().GetProperty<String>("FileExtension");
                }
                return null;
            }
        }
    }
}
