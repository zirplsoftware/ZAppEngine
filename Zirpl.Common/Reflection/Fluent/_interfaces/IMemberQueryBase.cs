using System;
using System.Collections.Generic;
using System.Reflection;

namespace Zirpl.Reflection.Fluent
{
    public interface IMemberQueryBase<out TMemberInfo, out TMemberQuery, out TAccessibilityQuery> 
        : IEnumerable<TMemberInfo>
    {
        TAccessibilityQuery WithAccessibility();
        TMemberQuery AreInstance();
        TMemberQuery AreStatic();
        TMemberQuery AreStaticInBaseTypes();
        TMemberQuery AreDeclaredOnlyOnThisType();
        TMemberQuery IgnoreCase();
        TMemberQuery ByName(String name);
    }
}