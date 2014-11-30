
using Zirpl.AppEngine.CodeGeneration.TextTemplating;
using Zirpl.AppEngine.CodeGeneration.V1;


namespace Zirpl.AppEngine.CodeGeneration.V1.Templates.DataService
{
	public partial class DataServiceInterfaceTemplate
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
	public partial class DataContextTemplate
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
	public partial class DataServiceTemplate
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
	public partial class MappingTemplate
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
	public partial class CustomFieldValueTemplate
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
	public partial class EnumTemplate
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

namespace Zirpl.AppEngine.CodeGeneration.V1.Templates.Model.Metadata.Constants
{
	public partial class MetadataConstantsTemplate
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
	public partial class ModelTemplate
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
	public partial class ServiceTemplate
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
	public partial class ServiceInterfaceTemplate
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
	public partial class PeristableModelTestsStrategyTemplate
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
	public partial class PersistableModelTestsEntityWrapperTemplate
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
	public partial class DataServicesProviderTemplate
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
	public partial class DataServiceTestsTemplate
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
	public partial class ServiceTestsTemplate
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
	public partial class ServicesProviderTemplate
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
	public partial class ValidatorTemplate
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
