using System;
using System.Collections.Generic;

namespace Zirpl.Reflection.Fluent
{
    public interface IMemberQueryBase<out TMemberInfo, out TMemberQuery, out TAccessibilityQuery, out TScopeQuery> 
        : IEnumerable<TMemberInfo>
    {
        TAccessibilityQuery OfAccessibility();
        TScopeQuery OfScope();
        TMemberQuery IgnoreCase();
        TMemberQuery Named(String name);
        TMemberQuery NamedAny(IEnumerable<String> names);
    }
}