using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Zirpl.Reflection.Fluent
{
    public interface IMemberQueryResult<out TMemberInfo>
    {
        IEnumerable<TMemberInfo> Execute();
        TMemberInfo ExecuteSingle();
        TMemberInfo ExecuteSingleOrDefault();
    }
}
