using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Zirpl.AppEngine.Testing
{
    public class UpdatableProperty<T>
    {
        public T Original { get; set; }
        public T Updated { get; set; }
    }
}
