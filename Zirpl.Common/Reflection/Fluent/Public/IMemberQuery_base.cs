using System;
using System.Collections.Generic;
using System.Reflection;

namespace Zirpl.Reflection.Fluent
{
    public interface IMemberQuery<out TMemberInfo, out TMemberQuery> : IQueryResult<TMemberInfo>
        where TMemberInfo : MemberInfo
        where TMemberQuery : IMemberQuery<TMemberInfo, TMemberQuery>
    {
        IMemberAccessibilityQuery<TMemberInfo, TMemberQuery> OfAccessibility();
        IMemberScopeQuery<TMemberInfo, TMemberQuery> OfScope();
    }
}