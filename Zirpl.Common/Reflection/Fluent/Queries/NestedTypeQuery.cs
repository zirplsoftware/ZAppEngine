using System;
using System.Reflection;

namespace Zirpl.Reflection.Fluent
{
    internal sealed class NestedTypeQuery : NamedTypeMemberQueryBase<Type, INestedTypeQuery>, 
        INestedTypeQuery
    {
        private readonly TypeEvaluator _typeEvaluator;
        internal NestedTypeQuery(Type type)
            :base(type)
        {
            _memberTypeEvaluator.NestedType = true;
            _typeEvaluator = new TypeEvaluator();
            _matchEvaluators.Add(_typeEvaluator);
        }

        ITypeQuery<Type, INestedTypeQuery> INestedTypeQuery.OfType()
        {
            return new TypeSubQuery<Type, INestedTypeQuery>(this, _typeEvaluator);
        }
    }
}
