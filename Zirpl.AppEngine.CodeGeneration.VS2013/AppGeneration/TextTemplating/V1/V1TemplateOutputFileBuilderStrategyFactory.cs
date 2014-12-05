using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using Zirpl.AppEngine.VisualStudioAutomation.AppGeneration.TextTemplating.V1.Templates.Model;
using Zirpl.Collections;

namespace Zirpl.AppEngine.VisualStudioAutomation.AppGeneration.TextTemplating.V1
{
    public class V1TemplateOutputFileBuilderStrategyFactory : TemplateOutputFileBuilderStrategyFactoryBase
    {
        public override void AddStrategy(ITemplateOutputFileBuilderStrategy strategy)
        {
            var matchesInDefault = this.DefaultStrategies.Where(o => o.TemplateCategory == strategy.TemplateCategory).ToList();
            var matchesInAll = this.AllStrategies.Where(o => o.TemplateCategory == strategy.TemplateCategory).ToList();

            if (matchesInDefault.Any())
            {
                // this means it is a REPLACEMENT
                foreach (var match in matchesInDefault)
                {
                    this.DefaultStrategies.Remove(match);
                    this.AllStrategies.Remove(match);
                }
            }
            this.AllStrategies.Add(strategy);
        }

        protected override IEnumerable<ITemplateOutputFileBuilderStrategy> CreateDefaultStrategies()
        {
            return new ITemplateOutputFileBuilderStrategy[]
            {
                new PersistableDomainClassStrategy()
            };
        }
    }
}
