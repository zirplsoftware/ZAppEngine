using Zirpl.AppEngine.CodeGeneration.TextTemplating;

namespace Zirpl.AppEngine.CodeGeneration.V2.Templates.Model
{
	public partial class ModelTemplate: IPreprocessedTextTransformation
    {
        public ModelTemplate()
        {
            this.Host = TextTransformationSession.Instance.CallingTemplate.Host;
            this.GenerationEnvironment = TextTransformationSession.Instance.CallingTemplate.GenerationEnvironment;
        }
    }
}
