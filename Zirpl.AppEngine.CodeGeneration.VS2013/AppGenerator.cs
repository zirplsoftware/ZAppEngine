using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using Microsoft.VisualStudio.TextTemplating;
using Zirpl.AppEngine.CodeGeneration;
using Zirpl.AppEngine.CodeGeneration.V1;
using Zirpl.AppEngine.CodeGeneration.V1.Templates;

namespace Zirpl.AppEngine.CodeGeneration
{
    public static class AppGenerator
    {
        public static void GenerateApp(this TextTransformation callingTemplate, V1Helper helper = null)
        {
            using (helper = helper ?? new V1Helper(callingTemplate))
            {
                new ModelTemplate(helper).TransformText();
                new ModelMetadataTemplate(helper).TransformText();
                new ModelEnumTemplate(helper).TransformText();
                new DataServiceInterfaceTemplate(helper).TransformText();
                new DataServiceTemplate(helper).TransformText();
                new DataContextTemplate(helper).TransformText();
                new EntityFrameworkMappingTemplate(helper).TransformText();
            }
        }
    }
}
