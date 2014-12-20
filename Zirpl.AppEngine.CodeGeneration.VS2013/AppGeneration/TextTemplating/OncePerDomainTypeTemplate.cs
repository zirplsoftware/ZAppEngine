using Zirpl.AppEngine.VisualStudioAutomation.AppGeneration.Config;

namespace Zirpl.AppEngine.VisualStudioAutomation.AppGeneration.TextTemplating
{
    public abstract class OncePerDomainTypeTemplate : TemplateBase
    {
        public DomainType DomainType
        {
            get
            {
                return (DomainType)this.Session["DomainType"];
            }
        }
    }
}
