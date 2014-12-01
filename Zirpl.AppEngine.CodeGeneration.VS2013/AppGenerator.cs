using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using EnvDTE;
using Microsoft.VisualStudio.TextTemplating;
using Newtonsoft.Json;
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
            AppFileParser appFileParser = null, 
            DomainFileParser domainFileParser = null,
            IDictionary<String, Object> additionalTemplateParameters = null)
        {
            using (var session = TextTransformationSession.StartSession(callingTemplate))
            {
                appFileParser = appFileParser ?? new AppFileParser();
                domainFileParser = domainFileParser ?? new DomainFileParser();

                var domainFilePaths = new List<String>();
                String appFilePath = null;

                // get all ProjectItems for the project with the initial template
                //
                var projectItems = session.FileManager.TemplateProjectItem.ContainingProject.ProjectItems.GetAllProjectItemsRecursive();
                foreach (var configProjectItem in projectItems)
                {
                    var path = configProjectItem.GetFullPath();
                    if (path.EndsWith(".domain.zae"))
                    {
                        domainFilePaths.Add(path);
                        session.LogLineToBuildPane("Domain file: " + path);
                    }
                    else if (path.EndsWith(".app.zae"))
                    {
                        appFilePath = path;
                        session.LogLineToBuildPane("App file: " + path);
                    }
                }

                if (appFilePath == null)
                {
                    throw new Exception("An App config file with extension .app.zae must be present");
                }

                var app = appFileParser.Parse(appFilePath);
                app.DomainTypes.AddRange(domainFileParser.Parse(app, domainFilePaths));

                session.CallingTemplate.WriteLine(JsonConvert.SerializeObject(app, Formatting.Indented, new JsonSerializerSettings { PreserveReferencesHandling = PreserveReferencesHandling.Objects }));

                additionalTemplateParameters = additionalTemplateParameters ?? new Dictionary<string, object>();
                additionalTemplateParameters.Add("AppInfo", app);
                foreach (var file in app.FilesToGenerate)
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
