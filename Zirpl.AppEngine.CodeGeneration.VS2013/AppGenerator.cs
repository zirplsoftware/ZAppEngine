using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using Microsoft.VisualStudio.TextTemplating;
using Zirpl.AppEngine.CodeGeneration.Templates;

namespace Zirpl.AppEngine.CodeGeneration
{
    public static class AppGenerator
    {
        public static void GenerateApp(TextTransformation callingTemplate)
        {
            var templateHelper = new TemplateHelper(callingTemplate);
            new ModelTemplate(templateHelper).TransformText();
            new ModelMetadataTemplate(templateHelper).TransformText();
            new ModelEnumTemplate(templateHelper).TransformText();
            templateHelper.End();
        }
    }
}
