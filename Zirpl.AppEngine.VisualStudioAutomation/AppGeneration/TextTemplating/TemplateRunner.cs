using System;
using System.Collections.Generic;
using Microsoft.VisualStudio.TextTemplating;
using Zirpl.AppEngine.Logging;
using Zirpl.AppEngine.VisualStudioAutomation.AppGeneration.Config;
using Zirpl.AppEngine.VisualStudioAutomation.TextTemplating;
using Zirpl.Reflection;

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

            if (template.Access().HasGet<DomainType>("DomainType"))
            {
                // once per DomainType
                //
                this.GetLog().Debug("Calling template once per domain type: " + template.GetType().FullName);
                foreach (var domainType in _app.DomainTypes)
                {
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
    }
}
