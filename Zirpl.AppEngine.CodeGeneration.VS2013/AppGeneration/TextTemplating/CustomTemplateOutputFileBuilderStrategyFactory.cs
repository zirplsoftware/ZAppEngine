using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using Zirpl.AppEngine.VisualStudioAutomation.AppGeneration.TextTemplating.V1.Templates.Model;
using Zirpl.Collections;

namespace Zirpl.AppEngine.VisualStudioAutomation.AppGeneration.TextTemplating
{
    public class CustomTemplateOutputFileBuilderStrategyFactory : TemplateOutputFileBuilderStrategyFactoryBase
    {
        public override void AddStrategy(ITemplateOutputFileBuilderStrategy strategy)
        {
            this.AllStrategies.Add(strategy);
        }

        protected override IEnumerable<ITemplateOutputFileBuilderStrategy> CreateDefaultStrategies()
        {
            return new ITemplateOutputFileBuilderStrategy[]
            {
            };
        }
    }
}
