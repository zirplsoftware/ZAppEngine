using System;using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using Zirpl.AppEngine.VisualStudioAutomation.AppGeneration.TextTemplating.V1.Templates.Model;
using Zirpl.Collections;

namespace Zirpl.AppEngine.VisualStudioAutomation.AppGeneration.TextTemplating
{
    public abstract class TemplateOutputFileBuilderStrategyFactoryBase : ITemplateOutputFileBuilderStrategyFactory
    {
        protected IList<ITemplateOutputFileBuilderStrategy> AllStrategies { get; private set; }
        protected IList<ITemplateOutputFileBuilderStrategy> DefaultStrategies { get; private set; }

        protected TemplateOutputFileBuilderStrategyFactoryBase()
        {
            this.AllStrategies = new List<ITemplateOutputFileBuilderStrategy>();
            this.DefaultStrategies = new List<ITemplateOutputFileBuilderStrategy>();

            this.DefaultStrategies.AddRange(this.CreateDefaultStrategies());
            this.AllStrategies.AddRange(this.DefaultStrategies);
        }

        protected abstract IEnumerable<ITemplateOutputFileBuilderStrategy> CreateDefaultStrategies();

        public IEnumerable<ITemplateOutputFileBuilderStrategy> GetAllStrategies()
        {
            return this.AllStrategies.ToList();
        }

        public virtual void AddStrategy(ITemplateOutputFileBuilderStrategy strategy)
        {
            this.AllStrategies.Add(strategy);
        }

        public IEnumerable<ITemplateOutputFileBuilderStrategy> GetStrategiesByTemplateCategory(string categoryName)
        {
            return this.AllStrategies.Where(o => o.TemplateCategory == categoryName).ToList();
        }
    }
}
