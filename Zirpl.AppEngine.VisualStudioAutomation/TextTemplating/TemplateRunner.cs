using System;
using System.Collections.Generic;
using Zirpl.FluentReflection;
using Zirpl.Logging;
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
                if (sessionParameters != null)
                {
                    var session = childTransform.Session;
                    session.Clear();
                    foreach (var pair in sessionParameters)
                    {
                        session.Add(pair.Key, pair.Value);
                    }
                }
                childTransform.Initialize();

                if (!template.Property<bool>("ShouldTransform").Exists
                    || template.Property<bool>("ShouldTransform").Value)
                {
                    OutputInfo outputFile = null;
                    if (template.Property<OutputInfo>("OutputFile").Exists)
                    {
                        outputFile = template.Property<OutputInfo>("OutputFile").Value;
                    }
                    if (outputFile == null
                        && outputFileProvider != null)
                    {
                        outputFile = outputFileProvider.GetOutputInfo(childTransform);
                    }

                    this.LogTransforming(childTransform);
                    if (outputFile != null)
                    {
                        childTransform.FileManager.StartFile(childTransform, outputFile);
                        // run the template
                        childTransform.TransformText();
                        childTransform.FileManager.EndFile();
                        childTransform.GenerationEnvironment.Clear();
                    }
                    else
                    {
                        childTransform.FileManager.UseDefaultFile(childTransform);
                        childTransform.TransformText();
                        childTransform.GenerationEnvironment.Clear();
                    }
                }
                else
                {
                    this.LogShouldTransformAsFalse(childTransform);
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

        protected virtual void LogShouldTransformAsFalse(ITransform childTransform)
        {
            this.GetLog().Debug("   Not transforming");
        }

        protected virtual void LogTransforming(ITransform childTransform)
        {
            this.GetLog().Debug(String.Format("   Transforming"));
        }
    }
}
