using System;
using System.Collections.Generic;
using System.Linq;
using Microsoft.VisualStudio.TextTemplating;
using Newtonsoft.Json;
using Zirpl.AppEngine.VisualStudioAutomation.AppGeneration.ConfigModel.FileGeneration;
using Zirpl.AppEngine.VisualStudioAutomation.AppGeneration.ConfigModel.Parsers;
using Zirpl.AppEngine.VisualStudioAutomation.TextTemplating;

namespace Zirpl.AppEngine.VisualStudioAutomation.AppGeneration
{
    public static class AppGenerator
    {
        public static void GenerateApp(
            this TextTransformation callingTemplate, 
            AppFileParser appFileParser = null, 
            DomainFileParser domainFileParser = null,
            OutputFileFactory factory = null,
            IDictionary<String, Object> additionalTemplateParameters = null)
        {
            using (var session = TransformSession.StartSession(callingTemplate))
            {
                appFileParser = appFileParser ?? new AppFileParser();
                domainFileParser = domainFileParser ?? new DomainFileParser();
                factory = factory ?? new OutputFileFactory();

                var domainFilePaths = new List<String>();
                String appFilePath = null;

                // get all ProjectItems for the project with the initial template
                //
                var projectItems = session.TemplateProjectItem.ContainingProject.ProjectItems.GetAllProjectItemsRecursive();
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
                app.FilesToGenerate.AddRange(factory.CreateList(app));

                session.CallingTemplate.WriteLine(JsonConvert.SerializeObject(app, Formatting.Indented, new JsonSerializerSettings { PreserveReferencesHandling = PreserveReferencesHandling.Objects }));

                foreach (var file in app.FilesToGenerate)
                {
                    var templateParameters = new Dictionary<string, object>();
                    if (additionalTemplateParameters != null
                        && additionalTemplateParameters.Any())
                    {
                        foreach (var additionalTemplateParameter in additionalTemplateParameters)
                        {
                            templateParameters.Add(additionalTemplateParameter.Key, additionalTemplateParameter.Value);
                        }
                    }
                    foreach (var parameter in file.TemplateParameters)
                    {
                        templateParameters.Add(parameter.Key, parameter.Value);
                    }
                    templateParameters.Add("App", app);
                    session.TransformToFile(file, templateParameters);
                }
            }
        }

        //public static void GenerateV1App(this TextTransformation callingTemplate, V1Helper helper = null)
        //{
        //    using (helper = helper ?? new V1Helper(callingTemplate))
        //    {
        //        new V1.Templates.Model.ModelTemplate(helper).TransformText();
        //        new V1.Templates.Model.Customization.CustomFieldValueTemplate(helper).TransformText();
        //        new V1.Templates.Model.Metadata.Constants.MetadataConstantsTemplate(helper).TransformText();
        //        new V1.Templates.Model.EnumTemplate(helper).TransformText();
        //        new V1.Templates.DataService.DataServiceInterfaceTemplate(helper).TransformText();
        //        new V1.Templates.DataService.EntityFramework.DataServiceTemplate(helper).TransformText();
        //        new V1.Templates.DataService.EntityFramework.DataContextTemplate(helper).TransformText();
        //        new V1.Templates.DataService.EntityFramework.Mapping.MappingTemplate(helper).TransformText();
        //        new V1.Templates.Service.ServiceInterfaceTemplate(helper).TransformText();
        //        new V1.Templates.Service.EntityFramework.ServiceTemplate(helper).TransformText();
        //        new V1.Templates.Validation.EntityFramework.FluentValidation.ValidatorTemplate(helper).TransformText();
        //        new V1.Templates.Tests.DataService.DataServicesProviderTemplate(helper).TransformText();
        //        new V1.Templates.Tests.Common.PersistableModelTestsEntityWrapperTemplate(helper).TransformText();
        //        new V1.Templates.Tests.Common.PeristableModelTestsStrategyTemplate(helper).TransformText();
        //        new V1.Templates.Tests.DataService.EntityFramework.DataServiceTestsTemplate(helper).TransformText();
        //        new V1.Templates.Tests.Service.ServicesProviderTemplate(helper).TransformText();
        //        new V1.Templates.Tests.Service.EntityFramework.ServiceTestsTemplate(helper).TransformText();
        //    }
        //}
    }
}
