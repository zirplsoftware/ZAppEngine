using System;
using System.Reflection;

namespace Zirpl.Reflection.Fluent
{
    internal sealed class MethodQuery : MemberQueryBase<MethodInfo, IMethodQuery, IMethodAccessibilityQuery, IMethodScopeQuery>, 
        IMethodQuery,
        IMethodAccessibilityQuery,
        IMethodScopeQuery
    {
        internal MethodQuery(Type type)
            :base(type)
        {
        }

        protected override bool IsMatch(MemberInfo memberInfo)
        {
            return true;
        }

        protected override MemberTypeFlags MemberTypes
        {
            get { return MemberTypeFlags.Method; }
        }
    }
}
