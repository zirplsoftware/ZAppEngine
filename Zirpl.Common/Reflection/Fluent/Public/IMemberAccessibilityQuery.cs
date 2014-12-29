using System.Collections.Generic;
using System.Reflection;

namespace Zirpl.Reflection.Fluent
{
    public interface IMemberAccessibilityQuery<out TMemberInfo, out TReturnQuery> : IQueryResult<TMemberInfo>
        where TMemberInfo : MemberInfo
        where TReturnQuery : IQueryResult<TMemberInfo>
    {
        IMemberAccessibilityQuery<TMemberInfo, TReturnQuery> Public();
        IMemberAccessibilityQuery<TMemberInfo, TReturnQuery> Private();
        //IMemberAccessibilityQuery<TMemberInfo, TReturnQuery> PrivateIncludingOnBaseTypes();
        IMemberAccessibilityQuery<TMemberInfo, TReturnQuery> Protected();
        IMemberAccessibilityQuery<TMemberInfo, TReturnQuery> Internal();
        IMemberAccessibilityQuery<TMemberInfo, TReturnQuery> ProtectedInternal();
        TReturnQuery NotPrivate();
        TReturnQuery NotPublic();
        TReturnQuery All();
        TReturnQuery And();
    }
}