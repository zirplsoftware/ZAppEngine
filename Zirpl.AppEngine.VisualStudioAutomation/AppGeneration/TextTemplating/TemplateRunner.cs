using System;
using Microsoft.VisualStudio.TextTemplating;
using Zirpl.AppEngine.Logging;
using Zirpl.AppEngine.VisualStudioAutomation.AppGeneration.Config;
using Zirpl.AppEngine.VisualStudioAutomation.TextTemplating;
using Zirpl.Reflection;

namespace Zirpl.AppEngine.VisualStudioAutomation.AppGeneration.TextTemplating
{
    internal class TemplateRunner : ITemplateRunner
    {
        private readonly App _app;

        internal TemplateRunner(App app)
        {
            this._app = app;
        }

        public void RunTemplates(IOutputFileManager outputFileManager, ITemplateProvider templateProvider, IOutputFileProvider outputFileProvider)
        {
            foreach (var templateType in templateProvider.GetTemplates())
            {
                if (templateType.GetTypeAccessor().HasPropertyGetter<DomainType>("DomainType"))
                {
                    // once per DomainType
                    //
                    this.GetLog().Debug("Calling template once per domain type: " + templateType.FullName);
                    foreach (var domainType in _app.DomainTypes)
                    {
                        TransformTemplate(outputFileManager, outputFileProvider, templateType, domainType);
                    }
                }
                else
                {
                    // once per App
                    //
                    this.GetLog().Debug("Calling template once: " + templateType.FullName);
                    TransformTemplate(outputFileManager, outputFileProvider, templateType);
                }
            }
        }

        private void TransformTemplate(IOutputFileManager outputFileManager, IOutputFileProvider outputFileProvider, Type templateType, DomainType domainType = null)
        {
            var template = Activator.CreateInstance(templateType);
            var templateWrapper = new TextTransformationWrapper(template);
            var session = new TextTemplatingSession();
            //foreach (var globalTemplateParameter in app.GlobalTemplateParameters)
            //{
            //    templateWrapper.Session.Add(globalTemplateParameter);
            //}
            session.Add("App", _app);
            if (domainType != null)
            {
                session.Add("DomainType", domainType);
            }
            templateWrapper.Session = session;
            templateWrapper.Initialize();

            if (!template.Access().HasGet<bool>("ShouldTransform")
                || template.Access().Property<bool>("ShouldTransform"))
            {
                OutputFile outputFile = null;
                if (template.Access().HasGet<OutputFile>("OutputFile"))
                {
                    outputFile = template.Access().Property<OutputFile>("OutputFile");
                }
                if (outputFile == null)
                {
                    outputFile = outputFileProvider.GetOutputFile(template);
                }
                if (outputFile != null)
                {
                    outputFileManager.StartFile(template, outputFile);
                    // run the template
                    templateWrapper.TransformText();
                    outputFileManager.EndFile();
                }
                else
                {
                    // we are counting on the template itself to manage it, otherwise it will be going directly to the calling template
                    templateWrapper.TransformText();
                }
            }
            else
            {
                if (domainType != null)
                {
                    this.GetLog().Debug(String.Format("      ShouldTransform == false. Not transforming for {0}", domainType.FullName));
                }
                else
                {
                    this.GetLog().Debug(String.Format("      ShouldTransform == false. Not transforming"));
                }
            }
        }
    }
}
