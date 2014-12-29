using System;
using System.Collections.Generic;
using System.Reflection;

namespace Zirpl.Reflection.Fluent
{
    public interface ITypeQuery<out TResult, out TReturnQuery> : IQueryResult<TResult>
        where TResult : MemberInfo
    {
        ITypeQuery<TResult, TReturnQuery> AssignableFrom(Type type);
        ITypeQuery<TResult, TReturnQuery> AssignableFrom<T>();
        ITypeQuery<TResult, TReturnQuery> AssignableFromAll(IEnumerable<Type> types);
        ITypeQuery<TResult, TReturnQuery> AssignableFromAny(IEnumerable<Type> types);
        ITypeQuery<TResult, TReturnQuery> AssignableTo(Type type);
        ITypeQuery<TResult, TReturnQuery> AssignableTo<T>();
        ITypeQuery<TResult, TReturnQuery> AssignableToAll(IEnumerable<Type> types);
        ITypeQuery<TResult, TReturnQuery> AssignableToAny(IEnumerable<Type> types);
        INameQuery<TResult, TReturnQuery> Named();
        INameQuery<TResult, TReturnQuery> FullNamed();
        TReturnQuery And();
    }
}