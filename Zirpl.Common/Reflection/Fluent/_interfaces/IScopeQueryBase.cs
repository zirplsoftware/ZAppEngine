using System.Collections.Generic;
using System.Reflection;

namespace Zirpl.Reflection.Fluent
{
    public interface IScopeQueryBase<out TMemberInfo, out TMemberQuery, out TScopeQuery> : IEnumerable<TMemberInfo>
    {
        TScopeQuery Instance();
        TScopeQuery Static();
        TScopeQuery DeclaredOnThisType();
        TScopeQuery DeclaredOnBaseTypes();
        TScopeQuery DeclaredOnBaseTypes(int levelsDeep);
        TMemberQuery All();
        TMemberQuery All(int levelsDeep);
        TMemberQuery And();
    }
}