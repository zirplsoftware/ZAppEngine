using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using Microsoft.VisualStudio.TextTemplating;
using Zirpl.AppEngine.Logging;
using Zirpl.AppEngine.VisualStudioAutomation.AppGeneration.Config;
using Zirpl.Reflection;

namespace Zirpl.AppEngine.VisualStudioAutomation.TextTemplating
{
    public class TemplateRunner : ITemplateRunner
    {
        public virtual void RunTemplate(TextTransformation textTransformation, Object template, IDictionary<string, object> sessionParameters = null, IOutputFileProvider outputFileProvider = null)
        {
            try
            {
                if (textTransformation == null) throw new ArgumentNullException("textTransformation");
                if (template == null) throw new ArgumentNullException("template");

                var templateWrapper = new TextTransformationWrapper(template);
                var session = templateWrapper.Session ?? new TextTemplatingSession();
                if (textTransformation.Session != null)
                {
                    foreach (var pair in textTransformation.Session)
                    {
                        session.Add("ParentTemplate." + pair.Key, pair.Value);
                    }
                }
                if (sessionParameters != null)
                {
                    foreach (var pair in sessionParameters)
                    {
                        session.Add(pair.Key, pair.Value);
                    }
                }
                session.Add("ParentTemplate", textTransformation);
                session.Add("FileManager", textTransformation.Wrap().FileManager);
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
                    if (outputFile == null
                        && outputFileProvider != null)
                    {
                        outputFile = outputFileProvider.GetOutputFile(textTransformation, template);
                    }

                    this.GetLog().Debug(String.Format("      Transforming template: " + template.GetType().FullName));
                    if (outputFile != null)
                    {
                        textTransformation.Wrap().FileManager.StartFile(template, outputFile);
                        // run the template
                        templateWrapper.TransformText();
                        textTransformation.Wrap().FileManager.EndFile();
                    }
                    else
                    {
                        textTransformation.Wrap().FileManager.UseDefaultFile(template);
                        templateWrapper.TransformText();
                    }
                }
                else
                {
                    this.GetLog().Debug(String.Format("      ShouldTransform == false. Not transforming"));
                }

            }
            catch (Exception e)
            {
                try
                {
                    LogManager.GetLog().Debug(e.ToString());
                    textTransformation.WriteLine(e.ToString());
                }
                catch (Exception)
                {
                }
                throw;
            }
        }
    }
}
