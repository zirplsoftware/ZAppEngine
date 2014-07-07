using System;
using System.Collections.Generic;
using System.Linq;
using System.Reflection;
using System.Text;
using System.Threading.Tasks;
using Microsoft.VisualStudio.TextTemplating;
using Zirpl.AppEngine.CodeGeneration.Transformation;
using Zirpl.Reflection;

namespace Zirpl.AppEngine.CodeGeneration.Templates
{
    public partial class ModelTemplate : IPreprocessedTextTransformation
    {
        public ModelTemplate(TemplateHelper templateHelper)
        {
            this.TemplateHelper = templateHelper;
            this.Host = this.TemplateHelper.CallingTemplate.Host;
            this.GenerationEnvironment = this.TemplateHelper.CallingTemplate.GenerationEnvironment;
        }

        public TemplateHelper TemplateHelper { get; private set; }
    }
    public partial class ModelMetadataTemplate: IPreprocessedTextTransformation
    {
        public ModelMetadataTemplate(TemplateHelper templateHelper)
        {
            this.TemplateHelper = templateHelper;
            this.Host = this.TemplateHelper.CallingTemplate.Host;
            this.GenerationEnvironment = this.TemplateHelper.CallingTemplate.GenerationEnvironment;
        }

        public TemplateHelper TemplateHelper { get; private set; }
    }
    public partial class ModelEnumTemplate : IPreprocessedTextTransformation
    {
        public ModelEnumTemplate(TemplateHelper templateHelper)
        {
            this.TemplateHelper = templateHelper;
            this.Host = this.TemplateHelper.CallingTemplate.Host;
            this.GenerationEnvironment = this.TemplateHelper.CallingTemplate.GenerationEnvironment;
        }

        public TemplateHelper TemplateHelper { get; private set; }
    }
}
