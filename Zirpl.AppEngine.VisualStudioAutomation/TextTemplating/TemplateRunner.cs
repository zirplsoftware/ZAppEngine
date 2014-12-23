using System;
using System.Collections.Generic;
using Microsoft.VisualStudio.TextTemplating;
using Zirpl.AppEngine.Logging;
using Zirpl.Reflection;

namespace Zirpl.AppEngine.VisualStudioAutomation.TextTemplating
{
    public class TemplateRunner : ITemplateRunner
    {
        public virtual void RunTemplate(ITransform transform, Object template, IDictionary<string, object> sessionParameters = null, IOutputInfoProvider outputFileProvider = null)
        {
            if (transform == null) throw new ArgumentNullException("transform");
            if (template == null) throw new ArgumentNullException("template");

            try
            {
                var childTransform = transform.GetChild(template);
                var session = childTransform.Session;
                if (sessionParameters != null)
                {
                    foreach (var pair in sessionParameters)
                    {
                        session.Add(pair.Key, pair.Value);
                    }
                }
                childTransform.Initialize();

                if (!template.Access().HasGet<bool>("ShouldTransform")
                    || template.Access().Property<bool>("ShouldTransform"))
                {
                    OutputInfo outputFile = null;
                    if (template.Access().HasGet<OutputInfo>("OutputFile"))
                    {
                        outputFile = template.Access().Property<OutputInfo>("OutputFile");
                    }
                    if (outputFile == null
                        && outputFileProvider != null)
                    {
                        outputFile = outputFileProvider.GetOutputInfo(childTransform);
                    }

                    this.GetLog().Debug(String.Format("      Transforming template: " + template.GetType().FullName));
                    if (outputFile != null)
                    {
                        childTransform.FileManager.StartFile(childTransform, outputFile);
                        // run the template
                        childTransform.TransformText();
                        childTransform.FileManager.EndFile();
                    }
                    else
                    {
                        childTransform.FileManager.UseDefaultFile(childTransform);
                        childTransform.TransformText();
                    }
                }
                else
                {
                    this.GetLog().Debug("      ShouldTransform == false. Not transforming");
                }

            }
            catch (Exception e)
            {
                try
                {
                    LogManager.GetLog().Debug(e.ToString());
                    transform.Host.HostTransform.GenerationEnvironment.Append(e);
                }
                catch (Exception)
                {
                }
                throw;
            }
        }
    }
}
