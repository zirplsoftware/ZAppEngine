using System;
using System.Collections.Generic;

namespace Zirpl.Reflection.Fluent
{
    public interface IAssignabilityQueryBase<out TMemberInfo, out TMemberQuery> : IEnumerable<TMemberInfo>
    {
        TMemberQuery AssignableFrom(Type type);
        TMemberQuery AssignableFromAny(IEnumerable<Type> types);
        TMemberQuery AssignableFromAll(IEnumerable<Type> types);
        TMemberQuery AssignableTo(Type type);
        TMemberQuery AssignableToAny(IEnumerable<Type> types);
        TMemberQuery AssignableToAll(IEnumerable<Type> types);
    }
}