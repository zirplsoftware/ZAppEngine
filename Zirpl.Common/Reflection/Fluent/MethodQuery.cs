﻿using System;
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
        private readonly AssignabilityEvaluator _returnTypeAssignabilityEvaluator;

        internal MethodQuery(Type type)
            :base(type)
        {
            _returnTypeAssignabilityEvaluator = new AssignabilityEvaluator();
        }

        protected override bool IsMatch(MemberInfo memberInfo)
        {
            return _returnTypeAssignabilityEvaluator.IsMatch(((MethodInfo)memberInfo).ReturnType);
        }

        protected override MemberTypeFlags MemberTypes
        {
            get { return MemberTypeFlags.Method; }
        }

        public IMethodReturnTypeAssignabilityQuery OfReturnType()
        {
            return this;
        }

        IMethodQuery IAssignabilityQueryBase<MethodInfo, IMethodQuery>.AssignableFrom(Type type)
        {
            _returnTypeAssignabilityEvaluator.AssignableFrom(type);
            return this;
        }

        IMethodQuery IAssignabilityQueryBase<MethodInfo, IMethodQuery>.AssignableFromAny(IEnumerable<Type> types)
        {
            _returnTypeAssignabilityEvaluator.AssignableFromAny(types);
            return this;
        }

        IMethodQuery IAssignabilityQueryBase<MethodInfo, IMethodQuery>.AssignableFromAll(IEnumerable<Type> types)
        {
            _returnTypeAssignabilityEvaluator.AssignableFromAll(types);
            return this;
        }

        IMethodQuery IAssignabilityQueryBase<MethodInfo, IMethodQuery>.AssignableTo(Type type)
        {
            _returnTypeAssignabilityEvaluator.AssignableTo(type);
            return this;
        }

        IMethodQuery IAssignabilityQueryBase<MethodInfo, IMethodQuery>.AssignableToAny(IEnumerable<Type> types)
        {
            _returnTypeAssignabilityEvaluator.AssignableToAny(types);
            return this;
        }

        IMethodQuery IAssignabilityQueryBase<MethodInfo, IMethodQuery>.AssignableToAll(IEnumerable<Type> types)
        {
            _returnTypeAssignabilityEvaluator.AssignableToAll(types);
            return this;
        }

        IMethodQuery IMethodReturnTypeAssignabilityQuery.Void()
        {
            _returnTypeAssignabilityEvaluator.Void();
            return this;
        }
    }
}