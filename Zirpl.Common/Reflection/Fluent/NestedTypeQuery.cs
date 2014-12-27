using System;
using System.Reflection;

namespace Zirpl.Reflection.Fluent
{
    internal sealed class NestedTypeQuery : MemberQueryBase<Type, INestedTypeQuery, INestedTypeAccessibilityQuery, INestedTypeScopeQuery>, 
        INestedTypeQuery,
        INestedTypeAccessibilityQuery,
        INestedTypeScopeQuery
    {
        internal NestedTypeQuery(Type type)
            :base(type)
        {
            _memberTypesBuilder.NestedType = true;
        }
    }
}
