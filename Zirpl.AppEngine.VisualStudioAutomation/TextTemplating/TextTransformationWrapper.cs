using System;
using System.CodeDom.Compiler;
using System.Collections.Concurrent;
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
                return this._textTransformation != null ? this._textTransformation.CurrentIndent : this._preProcessedTextTransformation.Access().Property<String>("CurrentIndent");
            }
        }

        public IDictionary<string, object> Session
        {
            get
            {
                return this._textTransformation != null ? this._textTransformation.Session : this._preProcessedTextTransformation.Access().Property<IDictionary<string, object>>("Session");
            }
            set
            {
                if (this._textTransformation != null)
                {
                    this._textTransformation.Session = value;
                }
                else
                {
                    this._preProcessedTextTransformation.Access().Property("Session", value);
                }
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

        public IOutputFileManager FileManager
        {
            get
            {
                var outputFileManager =  GetFileManager();
                if (outputFileManager == null)
                {
                    throw new Exception("A PreProcessedTemplate MUST be passed the IOutputFileManager either in the Session under key 'FileManager' or to a Field or Property named 'FileManager'");
                }
                return outputFileManager;
            }
            set
            {
                if (value == null)
                {
                    throw new ArgumentNullException("value");
                }
                if (this._textTransformation != null)
                {
                    throw new InvalidOperationException("Cannot set on a non-preprocessed template. Getter will auto-instantiate.");
                }
                var outputFileManager = this.GetFileManager();
                if (outputFileManager != null
                    && value != outputFileManager)
                {
                    throw new InvalidOperationException("Already has a different instance of an IOutputFileManager");
                }

                if (_preProcessedTextTransformation.Access().HasSet<IOutputFileManager>("FileManager")
                    && _preProcessedTextTransformation.Access().HasGet<IOutputFileManager>("FileManager"))
                {
                    _preProcessedTextTransformation.Access().Property("FileManager", value);
                }
                else if (_preProcessedTextTransformation.Access().HasField<IOutputFileManager>("FileManager"))
                {
                    _preProcessedTextTransformation.Access().Field("FileManager", value);
                }
                else // okay, we're going to use the session
                {
                    this.EnsureFileManagerKey();
                    Session[(String)Session["___FileManagerKey"]] = value;
                }
            }
        }

        private IOutputFileManager GetFileManager()
        {
            IOutputFileManager outputFileManager = null;
            if (_textTransformation != null)
            {
                if (_textTransformation.Access().HasGet<IOutputFileManager>("FileManager"))
                {
                    outputFileManager = _textTransformation.Access().Property<IOutputFileManager>("FileManager");
                    if (outputFileManager == null)
                    {
                        if (!_textTransformation.Access().HasSet<IOutputFileManager>("FileManager"))
                        {
                            throw new Exception("Has a FileManager property without a setter that returns null: " + _textTransformation.GetType());
                        }
                        outputFileManager = new OutputFileManager(_textTransformation);
                        _textTransformation.Access().Property("FileManager", outputFileManager);
                    }
                }
                else if (_textTransformation.Access().HasField<IOutputFileManager>("FileManager"))
                {
                    outputFileManager = _textTransformation.Access().Field<IOutputFileManager>("FileManager");
                    if (outputFileManager == null)
                    {
                        outputFileManager = new OutputFileManager(_textTransformation);
                        _textTransformation.Access().Field("FileManager", outputFileManager);
                    }
                }
                else // okay, we're going to use the session
                {
                    this.EnsureFileManagerKey();
                    outputFileManager = (IOutputFileManager)Session[(String)Session["___FileManagerKey"]];
                    if (outputFileManager == null)
                    {
                        outputFileManager = new OutputFileManager(_textTransformation);
                        Session[(String)Session["___FileManagerKey"]] = outputFileManager;
                    }
                }
            }
            else // preprocessed template- in this case we can ONLY get, not create
            {
                if (_preProcessedTextTransformation.Access().HasGet<IOutputFileManager>("FileManager"))
                {
                    outputFileManager = _preProcessedTextTransformation.Access().Property<IOutputFileManager>("FileManager");
                }
                else if (_preProcessedTextTransformation.Access().HasField<IOutputFileManager>("FileManager"))
                {
                    outputFileManager = _textTransformation.Access().Field<IOutputFileManager>("FileManager");
                }
                else // okay, we're going to use the session
                {
                    this.EnsureFileManagerKey();
                    outputFileManager = (IOutputFileManager)Session[(String)Session["___FileManagerKey"]];
                }
            }
            return outputFileManager;
        }

        private void EnsureFileManagerKey()
        {
            Session = Session ?? new Dictionary<string, object>();
            if (!Session.ContainsKey("___FileManagerKey"))
            {
                Session["___FileManagerKey"] =
                    (!Session.ContainsKey("FileManager")
                     || Session["FileManager"] == null
                     || Session["FileManager"] is IOutputFileManager)
                        ? "FileManager"
                        : "___FileManager";
            }
        }
    }
}
