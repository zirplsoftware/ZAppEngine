using System;
using System.Collections;
using System.Collections.Generic;
using System.Linq;
using System.Reflection;
using Zirpl.Collections;

namespace Zirpl.Reflection.Fluent
{
    internal abstract class TypeMemberQueryBase<TMemberInfo, TMemberQuery> : 
        IMemberQuery<TMemberInfo, TMemberQuery>
        where TMemberInfo : MemberInfo 
        where TMemberQuery : IMemberQuery<TMemberInfo, TMemberQuery>
    {
        private readonly Type _type;
        private readonly BindingFlagsBuilder _bindingFlagsBuilder;
        private readonly AccessibilityEvaluator _memberAccessibilityEvaluator;
        private readonly MemberScopeEvaluator _memberScopeEvaluator;
        private readonly MemberTypeFlagsBuilder _memberTypeFlagsBuilder;
        protected readonly MemberTypeEvaluator _memberTypeEvaluator;
        protected readonly IList<IMatchEvaluator> _matchEvaluators;
        protected readonly NameEvaluator _memberNameEvaluator;

        internal TypeMemberQueryBase(Type type)
        {
            _type = type;
            _memberScopeEvaluator = new MemberScopeEvaluator(type);
            _memberAccessibilityEvaluator = new AccessibilityEvaluator();
            _memberNameEvaluator = new NameEvaluator();
            _memberTypeEvaluator = new MemberTypeEvaluator();
            _bindingFlagsBuilder = new BindingFlagsBuilder(_memberAccessibilityEvaluator, _memberScopeEvaluator, _memberNameEvaluator);
            _memberTypeFlagsBuilder = new MemberTypeFlagsBuilder(_memberTypeEvaluator);
            _matchEvaluators = new List<IMatchEvaluator>();
            _matchEvaluators.Add(_memberNameEvaluator);
            _matchEvaluators.Add(_memberAccessibilityEvaluator);
            _matchEvaluators.Add(_memberScopeEvaluator);
            _matchEvaluators.Add(_memberTypeEvaluator);
        }

        #region IQueryResult implementation
        IEnumerable<TMemberInfo> IQueryResult<TMemberInfo>.Execute()
        {
            var list = new List<TMemberInfo>();
            var memberQueryService = new MemberQueryService(_type);
            var matches = memberQueryService.FindMembers(_memberTypeFlagsBuilder.MemberTypeFlags, _bindingFlagsBuilder.BindingFlags);
            if (_memberScopeEvaluator.DeclaredOnBaseTypes && _memberAccessibilityEvaluator.Private)
            {
                list.AddRange(memberQueryService.FindPrivateMembersOnBaseTypes(_memberTypeFlagsBuilder.MemberTypeFlags, _bindingFlagsBuilder.BindingFlags, _memberScopeEvaluator.LevelsDeep.GetValueOrDefault()));
            }
            return from memberInfo in matches
                    where _matchEvaluators.All(eval => eval.IsMatch(memberInfo))
                    select (TMemberInfo)memberInfo;
        }

        TMemberInfo IQueryResult<TMemberInfo>.ExecuteSingle()
        {
            var result = ((IQueryResult<TMemberInfo>)this).Execute();
            if (result.Count() > 1) throw new AmbiguousMatchException("Found more than 1 member matching the criteria");

            return result.Single();
        }

        TMemberInfo IQueryResult<TMemberInfo>.ExecuteSingleOrDefault()
        {
            var result = ((IQueryResult<TMemberInfo>)this).Execute();
            if (result.Count() > 1) throw new AmbiguousMatchException("Found more than 1 member matching the criteria");

            return result.SingleOrDefault();
        }
        #endregion


        IMemberAccessibilityQuery<TMemberInfo, TMemberQuery> IMemberQuery<TMemberInfo, TMemberQuery>.OfAccessibility()
        {
            return new MemberAccessibilitySubQuery<TMemberInfo, TMemberQuery>((TMemberQuery)(Object)this, _memberAccessibilityEvaluator);
        }

        IMemberScopeQuery<TMemberInfo, TMemberQuery> IMemberQuery<TMemberInfo, TMemberQuery>.OfScope()
        {
            return new MemberScopeSubQuery<TMemberInfo, TMemberQuery>((TMemberQuery)(Object)this, _memberScopeEvaluator);
        }
    }
}
