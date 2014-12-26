using System.Collections.Generic;
using System.Reflection;

namespace Zirpl.Reflection.Fluent
{
    public interface IAccessibilityQueryBase<out TMemberInfo, out TMemberQuery, out TAccessibilityQuery> : IEnumerable<TMemberInfo>
    {
        TAccessibilityQuery Public();
        TAccessibilityQuery NotPublic();
        TAccessibilityQuery Private();
        TAccessibilityQuery Protected();
        TAccessibilityQuery Internal();
        TAccessibilityQuery ProtectedInternal();
        TMemberQuery All();
    }
}