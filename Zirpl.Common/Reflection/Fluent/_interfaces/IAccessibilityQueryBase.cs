using System.Collections.Generic;

namespace Zirpl.Reflection.Fluent
{
    public interface IAccessibilityQueryBase<out TMemberInfo, out TMemberQuery, out TAccessibilityQuery> :IMemberQueryResult<TMemberInfo>
    {
        TAccessibilityQuery Public();
        TAccessibilityQuery Private();
        TAccessibilityQuery Protected();
        TAccessibilityQuery Internal();
        TAccessibilityQuery ProtectedInternal();
        TMemberQuery NotPrivate();
        TMemberQuery NotPublic();
        TMemberQuery All();
        TMemberQuery And();
    }
}