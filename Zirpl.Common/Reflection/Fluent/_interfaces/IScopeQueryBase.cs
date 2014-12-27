using System.Collections.Generic;

namespace Zirpl.Reflection.Fluent
{
    public interface IScopeQueryBase<out TMemberInfo, out TMemberQuery, out TScopeQuery> : IEnumerable<TMemberInfo>
    {
        TScopeQuery Instance();
        TScopeQuery Static();
        TScopeQuery DeclaredOnThisType();
        TScopeQuery DeclaredOnThisTypeAndBaseTypes();
        TScopeQuery DeclaredOnThisTypeAndBaseTypes(int levelsDeep);
        TMemberQuery All();
        TMemberQuery All(int levelsDeep);
        TMemberQuery And();
    }
}