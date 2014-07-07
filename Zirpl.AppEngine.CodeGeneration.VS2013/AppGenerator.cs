using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using Microsoft.VisualStudio.TextTemplating;
using Zirpl.AppEngine.CodeGeneration;
using Zirpl.AppEngine.CodeGeneration.V1.Templates;

namespace Zirpl.AppEngine.CodeGeneration
{
    public static class AppGenerator
    {
        public static void GenerateApp(this TextTransformation callingTemplate)
        {
            using (var transformationHelper = new V1.TransformationHelper(callingTemplate))
            {
                new ModelTemplate(transformationHelper).TransformText();
                new ModelMetadataTemplate(transformationHelper).TransformText();
                new ModelEnumTemplate(transformationHelper).TransformText();
                new DataServiceInterfaceTemplate(transformationHelper).TransformText();
                new DataServiceTemplate(transformationHelper).TransformText();
                new DataContextTemplate(transformationHelper).TransformText();
                new EntityFrameworkMappingTemplate(transformationHelper).TransformText();
            }
        }
    }
}
