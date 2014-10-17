
using Zirpl.AppEngine.CodeGeneration.TextTemplating;
using Zirpl.AppEngine.CodeGeneration.V1;


namespace Zirpl.AppEngine.CodeGeneration.V1.Templates.DataService
{
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
}

namespace Zirpl.AppEngine.CodeGeneration.V1.Templates.DataService.EntityFramework
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
}

namespace Zirpl.AppEngine.CodeGeneration.V1.Templates.DataService.EntityFramework
{
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
}

namespace Zirpl.AppEngine.CodeGeneration.V1.Templates.DataService.EntityFramework.Mapping
{
	public partial class MappingTemplate: IPreprocessedTextTransformation
    {
        public MappingTemplate(V1Helper helper)
        {
			this.Helper = helper;
            this.Host = this.Helper.CallingTemplate.Host;
            this.GenerationEnvironment = this.Helper.CallingTemplate.GenerationEnvironment;
        }

        public V1Helper Helper { get; private set; }
    }
}

namespace Zirpl.AppEngine.CodeGeneration.V1.Templates.Model.Customization
{
	public partial class CustomFieldValueTemplate: IPreprocessedTextTransformation
    {
        public CustomFieldValueTemplate(V1Helper helper)
        {
			this.Helper = helper;
            this.Host = this.Helper.CallingTemplate.Host;
            this.GenerationEnvironment = this.Helper.CallingTemplate.GenerationEnvironment;
        }

        public V1Helper Helper { get; private set; }
    }
}

namespace Zirpl.AppEngine.CodeGeneration.V1.Templates.Model
{
	public partial class EnumTemplate: IPreprocessedTextTransformation
    {
        public EnumTemplate(V1Helper helper)
        {
			this.Helper = helper;
            this.Host = this.Helper.CallingTemplate.Host;
            this.GenerationEnvironment = this.Helper.CallingTemplate.GenerationEnvironment;
        }

        public V1Helper Helper { get; private set; }
    }
}

namespace Zirpl.AppEngine.CodeGeneration.V1.Templates.Model
{
	public partial class MetadataConstantsTemplate: IPreprocessedTextTransformation
    {
        public MetadataConstantsTemplate(V1Helper helper)
        {
			this.Helper = helper;
            this.Host = this.Helper.CallingTemplate.Host;
            this.GenerationEnvironment = this.Helper.CallingTemplate.GenerationEnvironment;
        }

        public V1Helper Helper { get; private set; }
    }
}

namespace Zirpl.AppEngine.CodeGeneration.V1.Templates.Model
{
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

namespace Zirpl.AppEngine.CodeGeneration.V1.Templates.Service.EntityFramework
{
	public partial class ServiceTemplate: IPreprocessedTextTransformation
    {
        public ServiceTemplate(V1Helper helper)
        {
			this.Helper = helper;
            this.Host = this.Helper.CallingTemplate.Host;
            this.GenerationEnvironment = this.Helper.CallingTemplate.GenerationEnvironment;
        }

        public V1Helper Helper { get; private set; }
    }
}

namespace Zirpl.AppEngine.CodeGeneration.V1.Templates.Service
{
	public partial class ServiceInterfaceTemplate: IPreprocessedTextTransformation
    {
        public ServiceInterfaceTemplate(V1Helper helper)
        {
			this.Helper = helper;
            this.Host = this.Helper.CallingTemplate.Host;
            this.GenerationEnvironment = this.Helper.CallingTemplate.GenerationEnvironment;
        }

        public V1Helper Helper { get; private set; }
    }
}

namespace Zirpl.AppEngine.CodeGeneration.V1.Templates.Tests.Common
{
	public partial class PeristableModelTestsStrategyTemplate: IPreprocessedTextTransformation
    {
        public PeristableModelTestsStrategyTemplate(V1Helper helper)
        {
			this.Helper = helper;
            this.Host = this.Helper.CallingTemplate.Host;
            this.GenerationEnvironment = this.Helper.CallingTemplate.GenerationEnvironment;
        }

        public V1Helper Helper { get; private set; }
    }
}

namespace Zirpl.AppEngine.CodeGeneration.V1.Templates.Tests.Common
{
	public partial class PersistableModelTestsEntityWrapperTemplate: IPreprocessedTextTransformation
    {
        public PersistableModelTestsEntityWrapperTemplate(V1Helper helper)
        {
			this.Helper = helper;
            this.Host = this.Helper.CallingTemplate.Host;
            this.GenerationEnvironment = this.Helper.CallingTemplate.GenerationEnvironment;
        }

        public V1Helper Helper { get; private set; }
    }
}

namespace Zirpl.AppEngine.CodeGeneration.V1.Templates.Tests.DataService
{
	public partial class DataServicesProviderTemplate: IPreprocessedTextTransformation
    {
        public DataServicesProviderTemplate(V1Helper helper)
        {
			this.Helper = helper;
            this.Host = this.Helper.CallingTemplate.Host;
            this.GenerationEnvironment = this.Helper.CallingTemplate.GenerationEnvironment;
        }

        public V1Helper Helper { get; private set; }
    }
}

namespace Zirpl.AppEngine.CodeGeneration.V1.Templates.Tests.DataService.EntityFramework
{
	public partial class DataServiceTestsTemplate: IPreprocessedTextTransformation
    {
        public DataServiceTestsTemplate(V1Helper helper)
        {
			this.Helper = helper;
            this.Host = this.Helper.CallingTemplate.Host;
            this.GenerationEnvironment = this.Helper.CallingTemplate.GenerationEnvironment;
        }

        public V1Helper Helper { get; private set; }
    }
}

namespace Zirpl.AppEngine.CodeGeneration.V1.Templates.Tests.Service.EntityFramework
{
	public partial class ServiceTestsTemplate: IPreprocessedTextTransformation
    {
        public ServiceTestsTemplate(V1Helper helper)
        {
			this.Helper = helper;
            this.Host = this.Helper.CallingTemplate.Host;
            this.GenerationEnvironment = this.Helper.CallingTemplate.GenerationEnvironment;
        }

        public V1Helper Helper { get; private set; }
    }
}

namespace Zirpl.AppEngine.CodeGeneration.V1.Templates.Tests.Service
{
	public partial class ServicesProviderTemplate: IPreprocessedTextTransformation
    {
        public ServicesProviderTemplate(V1Helper helper)
        {
			this.Helper = helper;
            this.Host = this.Helper.CallingTemplate.Host;
            this.GenerationEnvironment = this.Helper.CallingTemplate.GenerationEnvironment;
        }

        public V1Helper Helper { get; private set; }
    }
}

namespace Zirpl.AppEngine.CodeGeneration.V1.Templates.Validation.EntityFramework.FluentValidation
{
	public partial class ValidatorTemplate: IPreprocessedTextTransformation
    {
        public ValidatorTemplate(V1Helper helper)
        {
			this.Helper = helper;
            this.Host = this.Helper.CallingTemplate.Host;
            this.GenerationEnvironment = this.Helper.CallingTemplate.GenerationEnvironment;
        }

        public V1Helper Helper { get; private set; }
    }
}
