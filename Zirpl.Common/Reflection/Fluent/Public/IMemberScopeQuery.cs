using System.Collections.Generic;
using System.Reflection;

namespace Zirpl.Reflection.Fluent
{
    public interface IMemberScopeQuery<out TMemberInfo, out TReturnQuery> : IQueryResult<TMemberInfo>
        where TMemberInfo : MemberInfo
        where TReturnQuery : IQueryResult<TMemberInfo>
    {
        IMemberScopeQuery<TMemberInfo, TReturnQuery> Instance();
        IMemberScopeQuery<TMemberInfo, TReturnQuery> Static();
        IMemberScopeQuery<TMemberInfo, TReturnQuery> DeclaredOnThisType();
        IMemberScopeQuery<TMemberInfo, TReturnQuery> DeclaredOnBaseTypes();
        IMemberScopeQuery<TMemberInfo, TReturnQuery> DeclaredOnBaseTypes(int levelsDeep);
        TReturnQuery All();
        TReturnQuery All(int levelsDeep);
        TReturnQuery Default();
        TReturnQuery And();
    }
}