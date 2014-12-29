using System;
using System.Collections.Generic;

namespace Zirpl.Reflection.Fluent
{
    public interface ITypeQuery : IQueryResult<Type>
    {
        ITypeQuery AssignableFrom(Type type);
        ITypeQuery AssignableFrom<T>();
        ITypeQuery AssignableFromAll(IEnumerable<Type> types);
        ITypeQuery AssignableFromAny(IEnumerable<Type> types);
        ITypeQuery AssignableTo(Type type);
        ITypeQuery AssignableTo<T>();
        ITypeQuery AssignableToAll(IEnumerable<Type> types);
        ITypeQuery AssignableToAny(IEnumerable<Type> types);
        INameQuery<Type, ITypeQuery> Named();
        INameQuery<Type, ITypeQuery> FullNamed();
    }
}