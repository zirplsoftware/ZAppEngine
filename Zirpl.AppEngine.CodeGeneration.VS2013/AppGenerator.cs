using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using EnvDTE;
using Microsoft.VisualStudio.TextTemplating;
using Zirpl.AppEngine.CodeGeneration;
using Zirpl.AppEngine.CodeGeneration.TextTemplating;
using Zirpl.AppEngine.CodeGeneration.V2;
using Zirpl.AppEngine.CodeGeneration.V2.ConfigModel;
using Zirpl.AppEngine.CodeGeneration.V2.Parsers;
using Zirpl.AppEngine.CodeGeneration.V2.Templates.Model;
using Zirpl.Reflection;

namespace Zirpl.AppEngine.CodeGeneration
{
    public static class AppGenerator
    {
        public static void GenerateApp(
            this TextTransformation callingTemplate, 
            AppConfigParser appConfigParser = null, 
            DomainClassConfigParser domainClassConfigParser = null,
            IDictionary<String, Object> additionalTemplateParameters = null)
        {
            using (var session = V2.TextTransformationSession.StartSession(callingTemplate))
            {
                session.Initialize(
                    appConfigParser ?? new AppConfigParser(),
                    domainClassConfigParser ?? new DomainClassConfigParser());

                foreach (var file in session.AppConfig.FilesToGenerate)
                {
                    session.CreateFile(session.AppConfig, file, additionalTemplateParameters);
                }
            }
        }
    }
}
