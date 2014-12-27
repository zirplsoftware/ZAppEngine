using System;
using System.Collections.Generic;
using System.Reflection;

namespace Zirpl.Reflection.Fluent
{
    public interface IMethodQuery : IMemberQueryBase<MethodInfo, IMethodQuery, IMethodAccessibilityQuery, IMethodScopeQuery>
    {
        IMethodReturnTypeAssignabilityQuery OfReturnType();
    }
}