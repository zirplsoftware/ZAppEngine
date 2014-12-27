using System;
using System.Collections.Generic;
using System.Reflection;

namespace Zirpl.Reflection.Fluent
{
    public interface IFieldQuery : IMemberQueryBase<FieldInfo, IFieldQuery, IFieldAccessibilityQuery, IFieldScopeQuery>
    {
        IFieldAssignabilityQuery OfType();
    }
}