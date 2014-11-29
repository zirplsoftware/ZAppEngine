using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using Microsoft.VisualStudio.TextTemplating;
using Zirpl.AppEngine.CodeGeneration;
using Zirpl.AppEngine.CodeGeneration.V1;
using Zirpl.AppEngine.CodeGeneration.V2;

namespace Zirpl.AppEngine.CodeGeneration
{
    public static class AppGenerator
    {
        public static void GenerateApp(this TextTransformation callingTemplate, V2Helper helper = null)
        {
            using (helper = helper ?? new V2Helper(callingTemplate))
            {
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
