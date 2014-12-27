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
        }

        protected override bool IsMatch(MemberInfo memberInfo)
        {
            return true;
        }

        protected override MemberTypeFlags MemberTypes
        {
            get { return MemberTypeFlags.NestedType; }
        }
    }
}
