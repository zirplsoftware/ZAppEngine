using Zirpl.AppEngine.CodeGeneration.TextTemplating;

namespace Zirpl.AppEngine.CodeGeneration.V1.Templates
{
	public partial class DataContextTemplate: IPreprocessedTextTransformation
    {
        public DataContextTemplate(V1Helper helper)
        {
			this.Helper = helper;
            this.Host = this.Helper.CallingTemplate.Host;
            this.GenerationEnvironment = this.Helper.CallingTemplate.GenerationEnvironment;
        }

        public V1Helper Helper { get; private set; }
    }
	public partial class DataServiceInterfaceTemplate: IPreprocessedTextTransformation
    {
        public DataServiceInterfaceTemplate(V1Helper helper)
        {
			this.Helper = helper;
            this.Host = this.Helper.CallingTemplate.Host;
            this.GenerationEnvironment = this.Helper.CallingTemplate.GenerationEnvironment;
        }

        public V1Helper Helper { get; private set; }
    }
	public partial class DataServiceTemplate: IPreprocessedTextTransformation
    {
        public DataServiceTemplate(V1Helper helper)
        {
			this.Helper = helper;
            this.Host = this.Helper.CallingTemplate.Host;
            this.GenerationEnvironment = this.Helper.CallingTemplate.GenerationEnvironment;
        }

        public V1Helper Helper { get; private set; }
    }
	public partial class EntityFrameworkMappingTemplate: IPreprocessedTextTransformation
    {
        public EntityFrameworkMappingTemplate(V1Helper helper)
        {
			this.Helper = helper;
            this.Host = this.Helper.CallingTemplate.Host;
            this.GenerationEnvironment = this.Helper.CallingTemplate.GenerationEnvironment;
        }

        public V1Helper Helper { get; private set; }
    }
	public partial class ModelEnumTemplate: IPreprocessedTextTransformation
    {
        public ModelEnumTemplate(V1Helper helper)
        {
			this.Helper = helper;
            this.Host = this.Helper.CallingTemplate.Host;
            this.GenerationEnvironment = this.Helper.CallingTemplate.GenerationEnvironment;
        }

        public V1Helper Helper { get; private set; }
    }
	public partial class ModelMetadataTemplate: IPreprocessedTextTransformation
    {
        public ModelMetadataTemplate(V1Helper helper)
        {
			this.Helper = helper;
            this.Host = this.Helper.CallingTemplate.Host;
            this.GenerationEnvironment = this.Helper.CallingTemplate.GenerationEnvironment;
        }

        public V1Helper Helper { get; private set; }
    }
	public partial class ModelTemplate: IPreprocessedTextTransformation
    {
        public ModelTemplate(V1Helper helper)
        {
			this.Helper = helper;
            this.Host = this.Helper.CallingTemplate.Host;
            this.GenerationEnvironment = this.Helper.CallingTemplate.GenerationEnvironment;
        }

        public V1Helper Helper { get; private set; }
    }
}
