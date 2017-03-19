using System;
using System.Collections.Generic;
using Zirpl.AppEngine.VisualStudioAutomation.AppGeneration.Model;
using Zirpl.AppEngine.VisualStudioAutomation.TextTemplating;
using Zirpl.AppEngine.VisualStudioAutomation.VisualStudio.Logging;
using Zirpl.FluentReflection;

namespace Zirpl.AppEngine.VisualStudioAutomation.AppGeneration.TextTemplating
{
    internal sealed class TemplateRunner : Zirpl.AppEngine.VisualStudioAutomation.TextTemplating.TemplateRunner
    {
        private readonly App _app;

        internal TemplateRunner(App app)
        {
            if (app == null) throw new ArgumentNullException("app");

            this._app = app;
        }

        public override void RunTemplate(ITransform transform, object template, IDictionary<string, object> sessionParameters = null, IOutputInfoProvider outputFileProvider = null)
        {
            if (transform == null) throw new ArgumentNullException("transform");
            if (template == null) throw new ArgumentNullException("template");

            if (template.Property<DomainType>("DomainType").Exists)
            {
                // once per DomainType
                //
                this.GetLog().Debug("Calling template once per domain type: " + template.GetType().FullName);
                foreach (var domainType in _app.DomainTypes)
                {
                    transform.GetChild(template).Session.Clear();
                    transform.GetChild(template).Initialize();
                    var session = new Dictionary<String, Object>();
                    if (sessionParameters != null)
                    {
                        foreach (var pair in sessionParameters)
                        {
                            session.Add(pair.Key, pair.Value);
                        }
                    }
                    session.Add("App", _app);
                    session.Add("DomainType", domainType);
                    base.RunTemplate(transform, template, session, outputFileProvider);
                }
            }
            else
            {
                // once per App
                //
                this.GetLog().Debug("Calling template once: " + template.GetType().FullName);
                transform.GetChild(template).Session.Clear();
                transform.GetChild(template).Initialize();
                var session = new Dictionary<String, Object>();
                if (sessionParameters != null)
                {
                    foreach (var pair in sessionParameters)
                    {
                        session.Add(pair.Key, pair.Value);
                    }
                }
                session.Add("App", _app);
                base.RunTemplate(transform, template, session, outputFileProvider);
            }
        }

        protected override void LogShouldTransformAsFalse(ITransform childTransform)
        {
            if (childTransform.Template.Property<DomainType>("DomainType").Exists)
            {
                this.GetLog().Debug($"   Not Transforming for {((DomainType)childTransform.Session["DomainType"]).FullName}");
            }
            else
            {
                base.LogShouldTransformAsFalse(childTransform);
            }
        }

        protected override void LogTransforming(ITransform childTransform)
        {
            if (childTransform.Template.Property<DomainType>("DomainType").Exists)
            {
                this.GetLog().Debug($"   Transforming for {((DomainType)childTransform.Session["DomainType"]).FullName}");
            }
            else
            {
                base.LogShouldTransformAsFalse(childTransform);
            }
        }
    }
}
