using System;
using System.Reflection;

namespace Zirpl.Reflection.Fluent
{
    internal sealed class PropertyQuery : MemberQueryBase<PropertyInfo, IPropertyQuery, IPropertyAccessibilityQuery, IPropertyScopeQuery>, 
        IPropertyQuery,
        IPropertyAccessibilityQuery,
        IPropertyScopeQuery
    {
        internal PropertyQuery(Type type)
            :base(type)
        {
        }

        protected override bool IsMatch(MemberInfo memberInfo)
        {
            return true;
        }

        protected override MemberTypeFlags MemberTypes
        {
            get { return MemberTypeFlags.Property; }
        }
    }
}
