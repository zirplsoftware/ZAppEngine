using System;
using System.Collections.Generic;
using System.Text;
using Microsoft.VisualStudio.TextTemplating;
using Zirpl.AppEngine.Logging;
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

        public object Template
        {
            get { return this._template; }
        }

        public abstract ITransformHost Host { get; }
        public abstract IOutputFileManager FileManager { get; }

        public ITransform GetChild(Object childTemplate)
        {
            if (childTemplate == null) throw new ArgumentNullException("childTemplate");

            return new ChildTransform(this.Host, childTemplate);
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

        public void RunTemplates(ITemplateProvider templateProvider, IDictionary<string, object> sessionParameters = null, IOutputInfoProvider outputInfoProvider = null)
        {
            new Action(() =>
            {
                RunTemplates(new TemplateRunner(), templateProvider, sessionParameters, outputInfoProvider);
            })
            .GetRunner()
            .OnError(HandleException)
            .OnComplete((passed) => FileManager.EndFile())
            .Run();
        }

        public void RunTemplates(IEnumerable<Type> templateTypes, IDictionary<string, object> sessionParameters = null, IOutputInfoProvider outputInfoProvider = null)
        {
            new Action(() =>
            {
                RunTemplate(new TemplateRunner(), templateTypes, sessionParameters, outputInfoProvider);
            })
            .GetRunner()
            .OnError(HandleException)
            .OnComplete((passed) => FileManager.EndFile())
            .Run();
        }

        public void RunTemplate(Type templateType, IDictionary<string, object> sessionParameters = null, IOutputInfoProvider outputInfoProvider = null)
        {
            new Action(() =>
            {
                RunTemplate(new TemplateRunner(), templateType, sessionParameters, outputInfoProvider);
            })
            .GetRunner()
            .OnError(HandleException)
            .OnComplete((passed) => FileManager.EndFile())
            .Run();
        }

        public void RunTemplate<T>(IDictionary<string, object> sessionParameters = null, IOutputInfoProvider outputInfoProvider = null)
        {
            new Action(() =>
            {
                RunTemplate(new TemplateRunner(), typeof(T), sessionParameters, outputInfoProvider);
            })
            .GetRunner()
            .OnError(HandleException)
            .OnComplete((passed) => FileManager.EndFile())
            .Run();
        }

        public void RunTemplates(IEnumerable<Object> templates, IDictionary<string, object> sessionParameters = null, IOutputInfoProvider outputInfoProvider = null)
        {
            new Action(() =>
            {
                RunTemplates(new TemplateRunner(), templates, sessionParameters, outputInfoProvider);
            })
            .GetRunner()
            .OnError(HandleException)
            .OnComplete((passed) => FileManager.EndFile())
            .Run();
        }

        public void RunTemplate(Object template, IDictionary<string, object> sessionParameters = null, IOutputInfoProvider outputInfoProvider = null)
        {
            new Action(() =>
            {
                RunTemplate(new TemplateRunner(), template, sessionParameters, outputInfoProvider);
            })
            .GetRunner()
            .OnError(HandleException)
            .OnComplete((passed) => FileManager.EndFile())
            .Run();
        }

        public void RunTemplates(ITemplateRunner templateRunner, ITemplateProvider templateProvider, IDictionary<string, object> sessionParameters = null, IOutputInfoProvider outputInfoProvider = null)
        {
            new Action(() =>
            {
                RunTemplates(
                    templateRunner,
                    templateProvider.GetTemplateTypes(),
                    sessionParameters,
                    outputInfoProvider);
            })
            .GetRunner()
            .OnError(HandleException)
            .OnComplete((passed) => FileManager.EndFile())
            .Run();
        }

        public void RunTemplates(ITemplateRunner templateRunner, IEnumerable<Type> templateTypes, IDictionary<string, object> sessionParameters = null, IOutputInfoProvider outputInfoProvider = null)
        {
            new Action(() =>
            {
                foreach (var templateType in templateTypes)
                {
                    RunTemplate(templateRunner, templateType, sessionParameters, outputInfoProvider);
                }
            })
            .GetRunner()
            .OnError(HandleException)
            .OnComplete((passed) => FileManager.EndFile())
            .Run();
        }

        public void RunTemplate(ITemplateRunner templateRunner, Type templateType, IDictionary<string, object> sessionParameters = null, IOutputInfoProvider outputInfoProvider = null)
        {
            new Action(() =>
            {
                var template = Activator.CreateInstance(templateType);
                RunTemplate(templateRunner, template, sessionParameters, outputInfoProvider);
            })
            .GetRunner()
            .OnError(HandleException)
            .OnComplete((passed) => FileManager.EndFile())
            .Run();
        }

        public void RunTemplate<T>(ITemplateRunner templateRunner, IDictionary<string, object> sessionParameters = null, IOutputInfoProvider outputInfoProvider = null)
        {
            new Action(() =>
            {
                RunTemplate(templateRunner, typeof(T), sessionParameters, outputInfoProvider);
            })
            .GetRunner()
            .OnError(HandleException)
            .OnComplete((passed) => FileManager.EndFile())
            .Run();
        }

        public void RunTemplates(ITemplateRunner templateRunner, IEnumerable<Object> templates, IDictionary<string, object> sessionParameters = null, IOutputInfoProvider outputInfoProvider = null)
        {
            new Action(() =>
            {
                foreach (var template in templates)
                {
                    RunTemplate(templateRunner, template, sessionParameters, outputInfoProvider);
                }
            })
            .GetRunner()
            .OnError(HandleException)
            .OnComplete((passed) => FileManager.EndFile())
            .Run();
        }

        public void RunTemplate(ITemplateRunner templateRunner, Object template, IDictionary<string, object> sessionParameters = null, IOutputInfoProvider outputInfoProvider = null)
        {
            new Action(() =>
            {
                templateRunner.RunTemplate(this, template, sessionParameters, outputInfoProvider);
            })
            .GetRunner()
            .OnError(HandleException)
            .OnComplete((passed) => FileManager.EndFile())
            .Run();
        }

        private Exception HandleException(Exception e)
        {
            try
            {
                LogManager.GetLog().Debug(e.ToString());
                Host.HostTransform.GenerationEnvironment.Append(e);
            }
            catch (Exception)
            {
            }
            return e;
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
