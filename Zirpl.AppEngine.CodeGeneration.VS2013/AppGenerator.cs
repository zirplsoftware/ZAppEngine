using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using Microsoft.VisualStudio.TextTemplating;
using Zirpl.AppEngine.CodeGeneration;
using Zirpl.AppEngine.CodeGeneration.V1;

namespace Zirpl.AppEngine.CodeGeneration
{
    public static class AppGenerator
    {
        public static void GenerateApp(this TextTransformation callingTemplate, V1Helper helper = null)
        {
            using (helper = helper ?? new V1Helper(callingTemplate))
            {
                new Zirpl.AppEngine.CodeGeneration.V1.Templates.Model.ModelTemplate(helper).TransformText();
                new Zirpl.AppEngine.CodeGeneration.V1.Templates.Model.MetadataTemplate(helper).TransformText();
                new Zirpl.AppEngine.CodeGeneration.V1.Templates.Model.EnumTemplate(helper).TransformText();
                new Zirpl.AppEngine.CodeGeneration.V1.Templates.DataService.DataServiceInterfaceTemplate(helper).TransformText();
                new Zirpl.AppEngine.CodeGeneration.V1.Templates.DataService.EntityFramework.DataServiceTemplate(helper).TransformText();
                new Zirpl.AppEngine.CodeGeneration.V1.Templates.DataService.EntityFramework.DataContextTemplate(helper).TransformText();
                new Zirpl.AppEngine.CodeGeneration.V1.Templates.DataService.EntityFramework.Mapping.MappingTemplate(helper).TransformText();
                new Zirpl.AppEngine.CodeGeneration.V1.Templates.Service.ServiceInterfaceTemplate(helper).TransformText();
                new Zirpl.AppEngine.CodeGeneration.V1.Templates.Service.EntityFramework.ServiceTemplate(helper).TransformText();
                new Zirpl.AppEngine.CodeGeneration.V1.Templates.Validation.EntityFramework.FluentValidation.ValidatorTemplate(helper).TransformText();
                new Zirpl.AppEngine.CodeGeneration.V1.Templates.Tests.DataService.DataServicesProviderTemplate(helper).TransformText();
            }
        }
    }
}
