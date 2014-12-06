using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Zirpl.AppEngine.VisualStudioAutomation.AppGeneration.TextTemplating
{
    public interface IOutputFileBuilderFactory
    {
        IEnumerable<IOutputFileBuilder> GetAllBuilders();
        T GetBuilderByKey<T>(String key) where T : IOutputFileBuilder;
    }
}
