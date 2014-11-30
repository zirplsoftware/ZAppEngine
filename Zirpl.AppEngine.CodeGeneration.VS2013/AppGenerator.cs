using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using EnvDTE;
using Microsoft.VisualStudio.TextTemplating;
using Zirpl.AppEngine.CodeGeneration;
using Zirpl.AppEngine.CodeGeneration.TextTemplating;
using Zirpl.AppEngine.CodeGeneration.V1;
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
            using (var session = TextTransformationSession.StartSession(callingTemplate))
            {
                appConfigParser = appConfigParser ?? new AppConfigParser();
                domainClassConfigParser = domainClassConfigParser ?? new DomainClassConfigParser();

                var domainClassConfigProjectItems = new List<ProjectItem>();
                ProjectItem appConfigProjectItem = null;

                // get all ProjectItems for the project with the initial template
                //
                var projectItems = session.FileManager.TemplateProjectItem.ContainingProject.ProjectItems.GetAllProjectItemsRecursive();
                foreach (var configProjectItem in projectItems)
                {
                    var fullPath = configProjectItem.GetFullPath();
                    if (fullPath.EndsWith(".domain.zae"))
                    {
                        domainClassConfigProjectItems.Add(configProjectItem);
                        session.LogLineToBuildPane(fullPath);
                    }
                    else if (fullPath.EndsWith(".app.zae"))
                    {
                        appConfigProjectItem = configProjectItem;
                        session.LogLineToBuildPane("App config file: " + fullPath);
                    }
                }

                var appConfig = appConfigParser.Parse(appConfigProjectItem);
                appConfig.DomainTypes.AddRange(domainClassConfigParser.Parse(appConfig, domainClassConfigProjectItems));

                additionalTemplateParameters = additionalTemplateParameters ?? new Dictionary<string, object>();
                additionalTemplateParameters.Add("AppConfig", appConfig);
                foreach (var file in appConfig.FilesToGenerate)
                {
                    session.CreateFile(file, additionalTemplateParameters);
                }
            }
        }

        public static void GenerateV1App(this TextTransformation callingTemplate, V1Helper helper = null)
        {
            using (helper = helper ?? new V1Helper(callingTemplate))
            {
                new V1.Templates.Model.ModelTemplate(helper).TransformText();
                new V1.Templates.Model.Customization.CustomFieldValueTemplate(helper).TransformText();
                new V1.Templates.Model.Metadata.Constants.MetadataConstantsTemplate(helper).TransformText();
                new V1.Templates.Model.EnumTemplate(helper).TransformText();
                new V1.Templates.DataService.DataServiceInterfaceTemplate(helper).TransformText();
                new V1.Templates.DataService.EntityFramework.DataServiceTemplate(helper).TransformText();
                new V1.Templates.DataService.EntityFramework.DataContextTemplate(helper).TransformText();
                new V1.Templates.DataService.EntityFramework.Mapping.MappingTemplate(helper).TransformText();
                new V1.Templates.Service.ServiceInterfaceTemplate(helper).TransformText();
                new V1.Templates.Service.EntityFramework.ServiceTemplate(helper).TransformText();
                new V1.Templates.Validation.EntityFramework.FluentValidation.ValidatorTemplate(helper).TransformText();
                new V1.Templates.Tests.DataService.DataServicesProviderTemplate(helper).TransformText();
                new V1.Templates.Tests.Common.PersistableModelTestsEntityWrapperTemplate(helper).TransformText();
                new V1.Templates.Tests.Common.PeristableModelTestsStrategyTemplate(helper).TransformText();
                new V1.Templates.Tests.DataService.EntityFramework.DataServiceTestsTemplate(helper).TransformText();
                new V1.Templates.Tests.Service.ServicesProviderTemplate(helper).TransformText();
                new V1.Templates.Tests.Service.EntityFramework.ServiceTestsTemplate(helper).TransformText();
            }
        }
    }
}
