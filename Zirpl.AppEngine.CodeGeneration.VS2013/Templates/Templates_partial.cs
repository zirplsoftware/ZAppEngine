using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using Microsoft.VisualStudio.TextTemplating;
using Zirpl.AppEngine.CodeGeneration.Transformation;
using Zirpl.Reflection;

namespace Zirpl.AppEngine.CodeGeneration.Templates
{
    public partial class ModelTemplate : IPreprocessedTextTransformation
    {
        public ModelTemplate(TextTransformation callingTemplate, TemplateHelper templateHelper)
        {
            this.Host = callingTemplate.GetDynamicAccessorWrapper().GetPropertyValue<ITextTemplatingEngineHost>("Host");
            this.CallingTemplate = callingTemplate;
            this.TemplateHelper = templateHelper;
        }

        public TextTransformation CallingTemplate { get; private set; }
        public TemplateHelper TemplateHelper { get; private set; }
    }
    public partial class ModelMetadataTemplate: IPreprocessedTextTransformation
    {
        public ModelMetadataTemplate(TextTransformation callingTemplate, TemplateHelper templateHelper)
        {
            this.Host = callingTemplate.GetDynamicAccessorWrapper().GetPropertyValue<ITextTemplatingEngineHost>("Host");
            this.CallingTemplate = callingTemplate;
            this.TemplateHelper = templateHelper;
        }

        public TextTransformation CallingTemplate { get; private set; }
        public TemplateHelper TemplateHelper { get; private set; }
    }
    public partial class ModelEnumTemplate : IPreprocessedTextTransformation
    {
        public ModelEnumTemplate(TextTransformation callingTemplate, TemplateHelper templateHelper)
        {
            this.Host = callingTemplate.GetDynamicAccessorWrapper().GetPropertyValue<ITextTemplatingEngineHost>("Host");
            this.CallingTemplate = callingTemplate;
            this.TemplateHelper = templateHelper;
        }

        public TextTransformation CallingTemplate { get; private set; }
        public TemplateHelper TemplateHelper { get; private set; }
    }
}
