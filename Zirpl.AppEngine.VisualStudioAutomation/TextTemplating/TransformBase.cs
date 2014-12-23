using System;
using System.CodeDom.Compiler;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using Microsoft.VisualStudio.PlatformUI;
using Microsoft.VisualStudio.TextTemplating;
using Zirpl.Reflection;

namespace Zirpl.AppEngine.VisualStudioAutomation.TextTemplating
{
    internal abstract class TransformBase : ITransform
    {
        private readonly Object _template;

        protected TransformBase(Object template)
        {
            if (template == null) throw new ArgumentNullException("template");

            this._template = template;
        }


        public abstract bool IsMaster { get; }
        public abstract IMasterTransform Master { get; }

        public object Template
        {
            get { return this._template; }
        }

        public T TemplateAs<T>()
        {
            return (T)this._template;
        }

        public StringBuilder GenerationEnvironment
        {
            get
            {
                return this._template.Access().Property<StringBuilder>("GenerationEnvironment");
            }
            set
            {
                this._template.Access().Property("GenerationEnvironment", value);
            }
        }

        public IDictionary<string, object> Session
        {
            get
            {
                var session = this._template.Access().Property<IDictionary<string, object>>("Session");
                if (session == null)
                {
                    session = new TextTemplatingSession();
                    this._template.Access().Property("Session", session);
                }
                return session;
            }
        }

        public void Initialize()
        {
            this._template.Access().Invoke("Initialize");
        }

        public string TransformText()
        {
            return this._template.Access().Invoke<String>("TransformText");
        }

        public override bool Equals(object obj)
        {
            return this._template.Equals(obj);
        }

        public override int GetHashCode()
        {
            return this._template.GetHashCode();
        }

        public override string ToString()
        {
            return this._template.ToString();
        }
        

        #region Code we might need later

        //public CompilerErrorCollection Errors
        //{
        //    get
        //    {
        //        return this._transform.Access().Property<CompilerErrorCollection>("Errors");
        //    }
        //}
        //public string CurrentIndent
        //{
        //    get
        //    {
        //        return this._transform.Access().Property<String>("CurrentIndent");
        //    }
        //}
        //public void ClearIndent()
        //{
        //    this._transform.Access().Invoke("ClearIndent");
        //}
        //public void Error(string message)
        //{
        //    this._transform.Access().Invoke("Error", message);
        //}
        //public string PopIndent()
        //{
        //    return this._transform.Access().Invoke<String>("PopIndent");
        //}
        //public void PushIndent(string indent)
        //{
        //    this._transform.Access().Invoke("PushIndent", indent);
        //}
        //public void Warning(string message)
        //{
        //    this._transform.Access().Invoke("Warning", message);
        //}
        //public void Write(string text)
        //{
        //    this._transform.Access().Invoke("Write", text);
        //}
        //public void Write(string format, params object[] args)
        //{
        //    this._transform.Access().Invoke("Write", format, args);
        //}
        //public void WriteLine(string text)
        //{
        //    this._transform.Access().Invoke("WriteLine", text);
        //}
        //public void WriteLine(string format, params object[] args)
        //{
        //    this._transform.Access().Invoke("WriteLine", format, args);
        //}
        #endregion
    }
}
