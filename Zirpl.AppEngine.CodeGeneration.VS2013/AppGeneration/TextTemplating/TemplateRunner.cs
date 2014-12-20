using System;
using Microsoft.VisualStudio.TextTemplating;
using Zirpl.AppEngine.VisualStudioAutomation.AppGeneration.Config;
using Zirpl.AppEngine.VisualStudioAutomation.TextTemplating;

namespace Zirpl.AppEngine.VisualStudioAutomation.AppGeneration.TextTemplating
{
    internal class TemplateRunner
    {
        private App app;
        private TemplateProvider templateProvider;

        internal TemplateRunner(App app, TemplateProvider templateProvider)
        {
            this.app = app;
            this.templateProvider = templateProvider;
        }

        internal void RunTemplates()
        {
            foreach (var templateType in templateProvider.GetTemplates())
            {
                if (typeof(OncePerDomainTypeTemplate).IsAssignableFrom(templateType))
                {
                    // once per DomainType
                    //
                    TextTransformationContext.Instance.LogLineToBuildPane("Calling template once per domain type: " + templateType.FullName);
                    foreach (var domainType in app.DomainTypes)
                    {
                        TransformTemplate(templateType, domainType);
                    }
                }
                else if (typeof(OncePerAppTemplate).IsAssignableFrom(templateType))
                {
                    // once per App
                    //
                    TextTransformationContext.Instance.LogLineToBuildPane("Calling template once: " + templateType.FullName);
                    TransformTemplate(templateType);
                }
                else
                {
                    throw new Exception("Could not determine how to run template. Ensure all templates inherit from OncePerDomainTypeTemplate or OncePerAppTemplate");
                }
            }
        }

        private void TransformTemplate(Type templateType, DomainType domainType = null)
        {
            var template = Activator.CreateInstance(templateType);
            var templateWrapper = new TextTransformationWrapper(template);
            var session = new TextTemplatingSession();
            //foreach (var globalTemplateParameter in app.GlobalTemplateParameters)
            //{
            //    templateWrapper.Session.Add(globalTemplateParameter);
            //}
            session.Add("Context", TextTransformationContext.Instance);
            session.Add("App", app);
            if (domainType != null)
            {
                session.Add("DomainType", domainType);
            }
            templateWrapper.Session = session;
            templateWrapper.Initialize();

            var templateBase = template as TemplateBase;
            if (templateBase != null)
            {
                if (!templateBase.ShouldTransform)
                {
                    if (domainType != null)
                    {
                        TextTransformationContext.Instance.LogLineToBuildPane(String.Format("      ShouldTransform == false. Not transforming for {0}", domainType.FullName));
                    }
                    else
                    {
                        TextTransformationContext.Instance.LogLineToBuildPane(String.Format("      ShouldTransform == false. Not transforming"));
                    }
                    return;
                }
                else
                {
                    var outputFile = templateBase.OutputFile;
                    if (outputFile != null)
                    {
                        TextTransformationContext.Instance.StartFile(templateWrapper, outputFile);
                    }
                }
            }
            // run the template
            templateWrapper.TransformText();
            TextTransformationContext.Instance.EndFile();
        }
    }
}
