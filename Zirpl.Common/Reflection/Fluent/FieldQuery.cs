using System;
using System.Reflection;

namespace Zirpl.Reflection.Fluent
{
    internal sealed class FieldQuery : MemberQueryBase<FieldInfo, IFieldQuery, IFieldAccessibilityQuery, IFieldScopeQuery>, 
        IFieldQuery,
        IFieldAccessibilityQuery,
        IFieldScopeQuery
    {
        internal FieldQuery(Type type)
            :base(type)
        {
        }

        protected override bool IsMatch(MemberInfo memberInfo)
        {
            return true;
        }

        protected override MemberTypeFlags MemberTypes
        {
            get { return MemberTypeFlags.Field; }
        }
    }
}
