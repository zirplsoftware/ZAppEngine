using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using EnvDTE;
using Microsoft.VisualStudio.TextTemplating;
using Zirpl.AppEngine.CodeGeneration;
using Zirpl.AppEngine.CodeGeneration.V2;
using Zirpl.AppEngine.CodeGeneration.V2.ConfigModel.Parsers;
using Zirpl.Reflection;

namespace Zirpl.AppEngine.CodeGeneration
{
    public static class AppGenerator
    {
        public static void GenerateApp(
            this TextTransformation callingTemplate, 
            AppConfigParser appConfigParser = null, 
            DomainClassConfigParser domainClassConfigParser = null)
        {
            using (var session = V2.TextTransformationSession.StartSession(callingTemplate))
            {
                session.LoadConfiguration(
                    appConfigParser ?? new AppConfigParser(),
                    domainClassConfigParser ?? new DomainClassConfigParser());

                var template = new V2.Templates.Model.PersistableDomainClassTemplate();
                template.Host = TextTransformationSession.Instance.CallingTemplate.Host;
                template.GetDynamicAccessorForDeclaredType().SetPropertyValue(template, "GenerationEnvironment", TextTransformationSession.Instance.CallingTemplate.GenerationEnvironment);


                template.Session = new Microsoft.VisualStudio.TextTemplating.TextTemplatingSession();
                template.Session["AppConfig"] = session.AppConfig;
                // Add other parameter values to t.Session here.
                template.Initialize(); // Must call this to transfer values.

                template.TransformText();
            }
        }
    }
}
