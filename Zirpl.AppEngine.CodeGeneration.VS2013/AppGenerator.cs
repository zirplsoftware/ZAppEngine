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
            DomainClassConfigParser domainClassConfigParser = null)
        {
            using (var session = V2.TextTransformationSession.StartSession(callingTemplate))
            {
                session.LoadConfiguration(
                    appConfigParser ?? new AppConfigParser(),
                    domainClassConfigParser ?? new DomainClassConfigParser());

                var file = new FileToGenerate()
                {
                    BuildAction = OutputFileBuildActionType.Compile,
                    DestinationProject = session.AppConfig.ModelProject,
                    FileExtension = ".auto.cs",
                    FileNameWithoutExtension = "Testing123",
                    FolderPath = "_auto/",
                    TemplateType = typeof (PersistableDomainClassTemplate)

                };

                session.CreateFile(file);
            }
        }
    }
}
