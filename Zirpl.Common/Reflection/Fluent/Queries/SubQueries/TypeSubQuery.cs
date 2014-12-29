using System;
using System.Collections.Generic;
using System.Linq;
using System.Reflection;
using System.Text;
using System.Threading.Tasks;

namespace Zirpl.Reflection.Fluent
{
    internal sealed class TypeSubQuery<TResult, TReturnQuery> : SubQueryBase<TResult, TReturnQuery>, 
        ITypeQuery<TResult, TReturnQuery>
        where TResult : MemberInfo
        where TReturnQuery : IQueryResult<TResult>
    {
        private readonly TypeEvaluator _typeEvaluator;

        internal TypeSubQuery(TReturnQuery returnQuery, TypeEvaluator typeEvaluator)
            : base(returnQuery)
        {
            _typeEvaluator = typeEvaluator;
        }

        ITypeQuery<TResult, TReturnQuery> ITypeQuery<TResult, TReturnQuery>.AssignableFrom(Type type)
        {
            _typeEvaluator.AssignableFrom = type;
            return this;
        }

        ITypeQuery<TResult, TReturnQuery> ITypeQuery<TResult, TReturnQuery>.AssignableFrom<T>()
        {
            _typeEvaluator.AssignableFrom = typeof (T);
            return this;
        }

        ITypeQuery<TResult, TReturnQuery> ITypeQuery<TResult, TReturnQuery>.AssignableFromAll(IEnumerable<Type> types)
        {
            _typeEvaluator.AssignableFroms = types;
            return this;
        }

        ITypeQuery<TResult, TReturnQuery> ITypeQuery<TResult, TReturnQuery>.AssignableFromAny(IEnumerable<Type> types)
        {
            _typeEvaluator.AssignableFroms = types;
            _typeEvaluator.Any = true;
            return this;
        }

        ITypeQuery<TResult, TReturnQuery> ITypeQuery<TResult, TReturnQuery>.AssignableTo(Type type)
        {
            _typeEvaluator.AssignableTo = type;
            return this;
        }

        ITypeQuery<TResult, TReturnQuery> ITypeQuery<TResult, TReturnQuery>.AssignableTo<T>()
        {
            _typeEvaluator.AssignableTo = typeof(T);
            return this;
        }

        ITypeQuery<TResult, TReturnQuery> ITypeQuery<TResult, TReturnQuery>.AssignableToAll(IEnumerable<Type> types)
        {
            _typeEvaluator.AssignableTos = types;
            return this;
        }

        ITypeQuery<TResult, TReturnQuery> ITypeQuery<TResult, TReturnQuery>.AssignableToAny(IEnumerable<Type> types)
        {
            _typeEvaluator.AssignableTos = types;
            _typeEvaluator.Any = true;
            return this;
        }

        INameQuery<TResult, TReturnQuery> ITypeQuery<TResult, TReturnQuery>.Named()
        {
            return new NameSubQuery<TResult, TReturnQuery>(_returnQuery, _typeEvaluator.NameEvaluator);
        }

        INameQuery<TResult, TReturnQuery> ITypeQuery<TResult, TReturnQuery>.FullNamed()
        {
            return new NameSubQuery<TResult, TReturnQuery>(_returnQuery, _typeEvaluator.FullNameEvaluator);
        }

        TReturnQuery ITypeQuery<TResult, TReturnQuery>.And()
        {
            return (TReturnQuery)(Object)this;
        }
    }
}
