using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Zirpl.AppEngine.Testing
{
    public class EntityWrapper<TEntity>
    {
        public TEntity Entity { get; set; }
        public bool IsUpdated { get; set; }
        public bool IsUsed { get; set; }
    }
}
