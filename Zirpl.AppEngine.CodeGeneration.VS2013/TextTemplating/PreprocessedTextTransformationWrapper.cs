//using System.Collections.Generic;
//using System.Reflection;
//using System.Text;
//using Microsoft.VisualStudio.TextTemplating;

//namespace Zirpl.AppEngine.CodeGeneration.TextTemplating
//{
//    public class PreprocessedTextTransformationWrapper : ITextTransformationWrapper
//    {
//        private readonly IPreprocessedTextTransformation _textTransformation;

//        public PreprocessedTextTransformationWrapper(IPreprocessedTextTransformation textTransformation)
//        {
//            this._textTransformation = textTransformation;
//        }



//        public StringBuilder GenerationEnvironment
//        {
//            get
//            {
//                return (StringBuilder)this._textTransformation.GetType()
//                    .GetProperty("GenerationEnvironment",
//                  BindingFlags.Instance | BindingFlags.NonPublic | BindingFlags.FlattenHierarchy)
//                    .GetValue(this._textTransformation);
//            }
//            set
//            {
//                this._textTransformation.GetType()
//                    .GetProperty("GenerationEnvironment",
//                  BindingFlags.Instance | BindingFlags.NonPublic | BindingFlags.FlattenHierarchy)
//                    .SetValue(this._textTransformation, value);
//            }
//        }

//        public System.CodeDom.Compiler.CompilerErrorCollection Errors
//        {
//            get { return this._textTransformation.Errors; }
//        }

//        public ITextTemplatingEngineHost Host
//        {
//            get { return this._textTransformation.Host; }
//            set
//            {
//                this._textTransformation.Host = value;
//            }
//        }

//        public string CurrentIndent
//        {
//            get { return this._textTransformation.CurrentIndent; }
//        }

//        public IDictionary<string, object> Session
//        {
//            get { return this._textTransformation.Session; }
//            set { this._textTransformation.Session = value; }
//        }

//        public void ClearIndent()
//        {
//            this._textTransformation.ClearIndent();
//        }

//        public void Dispose()
//        {
//        }

//        public void Error(string message)
//        {
//            this._textTransformation.Error(message);
//        }

//        public void Initialize()
//        {
//        }

//        public string PopIndent()
//        {
//            return this._textTransformation.PopIndent();
//        }

//        public void PushIndent(string indent)
//        {
//            this._textTransformation.PushIndent(indent);
//        }

//        public string TransformText()
//        {
//            return this._textTransformation.TransformText();
//        }

//        public void Warning(string message)
//        {
//            this._textTransformation.Warning(message);
//        }

//        public void Write(string text)
//        {
//            this._textTransformation.Write(text);
//        }

//        public void Write(string format, params object[] args)
//        {
//            this._textTransformation.Write(format, args);
//        }

//        public void WriteLine(string text)
//        {
//            this._textTransformation.WriteLine(text);
//        }

//        public void WriteLine(string format, params object[] args)
//        {
//            this._textTransformation.WriteLine(format, args);
//        }

//        public override bool Equals(object obj)
//        {
//            return this._textTransformation.Equals(obj);
//        }

//        public override int GetHashCode()
//        {
//            return this._textTransformation.GetHashCode();
//        }

//        public override string ToString()
//        {
//            return this._textTransformation.ToString();
//        }
//    }
//}
