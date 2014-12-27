using System;
using System.Collections.Generic;
using System.Reflection;

namespace Zirpl.Reflection.Fluent
{
    internal sealed class MethodQuery : MemberQueryBase<MethodInfo, IMethodQuery, IMethodAccessibilityQuery, IMethodScopeQuery>, 
        IMethodQuery,
        IMethodAccessibilityQuery,
        IMethodScopeQuery,
        IMethodReturnTypeAssignabilityQuery
    {
        private readonly MethodReturnTypeEvaluator _returnTypeEvaluator;

        internal MethodQuery(Type type)
            :base(type)
        {
            _returnTypeEvaluator = new MethodReturnTypeEvaluator();
            _memberTypesBuilder.Method = true;
            _memberEvaluators.Add(_returnTypeEvaluator);
        }

        public IMethodReturnTypeAssignabilityQuery OfReturnType()
        {
            return this;
        }

        IMethodQuery IAssignabilityQueryBase<MethodInfo, IMethodQuery>.AssignableFrom(Type type)
        {
            _returnTypeEvaluator.AssignableFrom(type);
            return this;
        }

        IMethodQuery IAssignabilityQueryBase<MethodInfo, IMethodQuery>.AssignableFromAny(IEnumerable<Type> types)
        {
            _returnTypeEvaluator.AssignableFromAny(types);
            return this;
        }

        IMethodQuery IAssignabilityQueryBase<MethodInfo, IMethodQuery>.AssignableFromAll(IEnumerable<Type> types)
        {
            _returnTypeEvaluator.AssignableFromAll(types);
            return this;
        }

        IMethodQuery IAssignabilityQueryBase<MethodInfo, IMethodQuery>.AssignableTo(Type type)
        {
            _returnTypeEvaluator.AssignableTo(type);
            return this;
        }

        IMethodQuery IAssignabilityQueryBase<MethodInfo, IMethodQuery>.AssignableToAny(IEnumerable<Type> types)
        {
            _returnTypeEvaluator.AssignableToAny(types);
            return this;
        }

        IMethodQuery IAssignabilityQueryBase<MethodInfo, IMethodQuery>.AssignableToAll(IEnumerable<Type> types)
        {
            _returnTypeEvaluator.AssignableToAll(types);
            return this;
        }

        IMethodQuery IMethodReturnTypeAssignabilityQuery.Void()
        {
            _returnTypeEvaluator.Void();
            return this;
        }
    }
}
