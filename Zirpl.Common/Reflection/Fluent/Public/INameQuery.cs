using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Zirpl.Reflection.Fluent
{
    public interface INameQuery<out TResult, out TReturnQuery> : IQueryResult<TResult>
    {
        TReturnQuery Exactly(String name);
        TReturnQuery Any(IEnumerable<String> names);
        TReturnQuery ExactlyIgnoreCase(String name);
        TReturnQuery AnyIgnoreCase(IEnumerable<String> names);
        TReturnQuery StartingWith(String name);
        TReturnQuery StartingWithAny(IEnumerable<String> names);
        TReturnQuery StartingWithIgnoreCase(String name);
        TReturnQuery StartingWithAnyIgnoreCase(IEnumerable<String> names);
        TReturnQuery Containing(String name);
        TReturnQuery ContainingAny(IEnumerable<String> names);
        TReturnQuery ContainingIgnoreCase(String name);
        TReturnQuery ContainingAnyIgnoreCase(IEnumerable<String> names);
        TReturnQuery EndingWith(String name);
        TReturnQuery EndingWithAny(IEnumerable<String> names);
        TReturnQuery EndingWithIgnoreCase(String name);
        TReturnQuery EndingWithAnyIgnoreCase(IEnumerable<String> names);
    }
}
