using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Zirpl.Reflection.Fluent
{
    public interface IQueryResult<out TResult>
    {
        IEnumerable<TResult> Execute();
        TResult ExecuteSingle();
        TResult ExecuteSingleOrDefault();
    }
}
