using System;using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using Zirpl.AppEngine.VisualStudioAutomation.AppGeneration.TextTemplating.V1.Templates.Model;
using Zirpl.Collections;

namespace Zirpl.AppEngine.VisualStudioAutomation.AppGeneration.TextTemplating
{
    public class DefaultOutputFileBuilderFactory : IOutputFileBuilderFactory
    {
        private IList<IOutputFileBuilder> Builders { get; set; }

        public DefaultOutputFileBuilderFactory()
        {
            this.Builders = new List<IOutputFileBuilder>();
        }

        public IEnumerable<IOutputFileBuilder> GetAllBuilders()
        {
            return this.Builders.ToList();
        }

        public virtual void AddBuilders(IEnumerable<IOutputFileBuilder> builders)
        {
            if (builders != null)
            {
                this.Builders.AddRange(builders);
            }
        }

        public T GetBuilderByKey<T>(string key) where T : IOutputFileBuilder
        {
            return this.Builders.Cast<T>().Where(o => o.Key == key).SingleOrDefault();
        }
    }
}
