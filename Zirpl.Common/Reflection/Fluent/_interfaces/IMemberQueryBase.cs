using System;
using System.Collections.Generic;
using System.Reflection;

namespace Zirpl.Reflection.Fluent
{
    public interface IMemberQueryBase<out TMemberInfo, out TMemberQuery, out TAccessibilityQuery, out TScopeQuery> 
        : IEnumerable<TMemberInfo>
    {
        TAccessibilityQuery WithAccessibility();
        TScopeQuery WithScope();
        TMemberQuery IgnoreCase();
        TMemberQuery ByName(String name);
    }
}