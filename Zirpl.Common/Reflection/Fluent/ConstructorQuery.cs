using System;
using System.Reflection;

namespace Zirpl.Reflection.Fluent
{
    internal sealed class ConstructorQuery : MemberQueryBase<ConstructorInfo, IConstructorQuery, IConstructorAccessibilityQuery, IConstructorScopeQuery>, 
        IConstructorQuery,
        IConstructorAccessibilityQuery,
        IConstructorScopeQuery
    {
        internal ConstructorQuery(Type type)
            :base(type)
        {
            _memberTypesBuilder.Constructor = true;
        }
    }
}
