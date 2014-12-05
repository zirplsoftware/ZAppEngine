using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Zirpl.AppEngine.VisualStudioAutomation.AppGeneration.TextTemplating
{
    public interface ITemplateOutputFileBuilderStrategyFactory
    {
        IEnumerable<ITemplateOutputFileBuilderStrategy> GetAllStrategies();
        IEnumerable<ITemplateOutputFileBuilderStrategy> GetStrategiesByTemplateCategory(String categoryName);
        void AddStrategy(ITemplateOutputFileBuilderStrategy strategy);
    }
}
