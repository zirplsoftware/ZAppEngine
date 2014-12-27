using System;
using System.Collections;
using System.Collections.Generic;
using System.Linq;
using System.Reflection;
using Zirpl.Collections;

namespace Zirpl.Reflection.Fluent
{
    internal abstract class MemberQueryBase<TMemberInfo, TMemberQuery, TAccessibilityQuery, TScopeQuery> : 
        IMemberQueryBase<TMemberInfo, TMemberQuery, TAccessibilityQuery, TScopeQuery>,
        IAccessibilityQueryBase<TMemberInfo, TMemberQuery, TAccessibilityQuery>,
        IScopeQueryBase<TMemberInfo, TMemberQuery, TScopeQuery>
        where TMemberInfo : MemberInfo
    {
        private readonly Type _type;
        private readonly BindingFlagsBuilder _bindingFlagsBuilder;
        private readonly AccessibilityEvaluator _accessibilityEvaluator;
        private readonly NameEvaluator _nameEvaluator;
        private readonly ScopeEvaluator _scopeEvaluator;
        protected readonly MemberTypesBuilder _memberTypesBuilder;
        protected readonly IList<IMemberEvaluator> _memberEvaluators;

        internal MemberQueryBase(Type type)
        {
            _type = type;
            _scopeEvaluator = new ScopeEvaluator(type);
            _accessibilityEvaluator = new AccessibilityEvaluator();
            _nameEvaluator = new NameEvaluator();
            _bindingFlagsBuilder = new BindingFlagsBuilder(_accessibilityEvaluator, _scopeEvaluator, _nameEvaluator);
            _memberTypesBuilder = new MemberTypesBuilder();
            _memberEvaluators = new List<IMemberEvaluator>();
            _memberEvaluators.Add(_nameEvaluator);
            _memberEvaluators.Add(_accessibilityEvaluator);
            _memberEvaluators.Add(_scopeEvaluator);
        }

        #region IMemberQuery implementation
        public TAccessibilityQuery OfAccessibility()
        {
            return (TAccessibilityQuery)(Object)this;
        }
        public TScopeQuery OfScope()
        {
            return (TScopeQuery)(Object)this;
        }
        #endregion

        #region IAccessibilityQuery implementation
        TAccessibilityQuery IAccessibilityQueryBase<TMemberInfo, TMemberQuery, TAccessibilityQuery>.Public()
        {
            _accessibilityEvaluator.Public = true;
            return (TAccessibilityQuery)(Object)this;
        }
        TMemberQuery IAccessibilityQueryBase<TMemberInfo, TMemberQuery, TAccessibilityQuery>.NotPublic()
        {
            _accessibilityEvaluator.Private = true;
            _accessibilityEvaluator.Protected = true;
            _accessibilityEvaluator.Internal = true;
            _accessibilityEvaluator.ProtectedInternal = true;
            return (TMemberQuery)(Object)this;
        }
        TMemberQuery IAccessibilityQueryBase<TMemberInfo, TMemberQuery, TAccessibilityQuery>.NotPrivate()
        {
            _accessibilityEvaluator.Public = true;
            _accessibilityEvaluator.Protected = true;
            _accessibilityEvaluator.Internal = true;
            _accessibilityEvaluator.ProtectedInternal = true;
            return (TMemberQuery)(Object)this;
        }
        TAccessibilityQuery IAccessibilityQueryBase<TMemberInfo, TMemberQuery, TAccessibilityQuery>.Private()
        {
            _accessibilityEvaluator.Private = true;
            return (TAccessibilityQuery)(Object)this;
        }
        TAccessibilityQuery IAccessibilityQueryBase<TMemberInfo, TMemberQuery, TAccessibilityQuery>.Protected()
        {
            _accessibilityEvaluator.Protected = true;
            return (TAccessibilityQuery)(Object)this;
        }
        TAccessibilityQuery IAccessibilityQueryBase<TMemberInfo, TMemberQuery, TAccessibilityQuery>.Internal()
        {
            _accessibilityEvaluator.Internal = true;
            return (TAccessibilityQuery)(Object)this;
        }
        TAccessibilityQuery IAccessibilityQueryBase<TMemberInfo, TMemberQuery, TAccessibilityQuery>.ProtectedInternal()
        {
            _accessibilityEvaluator.ProtectedInternal = true;
            return (TAccessibilityQuery)(Object)this;
        }
        TMemberQuery IAccessibilityQueryBase<TMemberInfo, TMemberQuery, TAccessibilityQuery>.All()
        {
            _accessibilityEvaluator.Public = true;
            _accessibilityEvaluator.Private = true;
            _accessibilityEvaluator.Protected = true;
            _accessibilityEvaluator.Internal = true;
            _accessibilityEvaluator.ProtectedInternal = true;
            return (TMemberQuery)(Object)this;
        }
        TMemberQuery IAccessibilityQueryBase<TMemberInfo, TMemberQuery, TAccessibilityQuery>.And()
        {
            return (TMemberQuery)(Object)this;
        }
        #endregion
      
        #region IScopeQuery implementation
        TScopeQuery IScopeQueryBase<TMemberInfo, TMemberQuery, TScopeQuery>.Instance()
        {
            _scopeEvaluator.Instance = true;
            return (TScopeQuery)(Object)this;
        }

        TScopeQuery IScopeQueryBase<TMemberInfo, TMemberQuery, TScopeQuery>.Static()
        {
            _scopeEvaluator.Static = true;
            return (TScopeQuery)(Object)this;
        }

        TScopeQuery IScopeQueryBase<TMemberInfo, TMemberQuery, TScopeQuery>.DeclaredOnThisType()
        {
            _scopeEvaluator.DeclaredOnThisType = true;
            return (TScopeQuery)(Object)this;
        }

        TScopeQuery IScopeQueryBase<TMemberInfo, TMemberQuery, TScopeQuery>.DeclaredOnThisTypeAndBaseTypes(int levelsDeep)
        {
            _scopeEvaluator.DeclaredOnBaseTypes = true;
            _scopeEvaluator.DeclaredOnThisType = true;
            _scopeEvaluator.LevelsDeep = levelsDeep;
            return (TScopeQuery)(Object)this;
        }

        TScopeQuery IScopeQueryBase<TMemberInfo, TMemberQuery, TScopeQuery>.DeclaredOnThisTypeAndBaseTypes()
        {
            _scopeEvaluator.DeclaredOnThisType = true;
            _scopeEvaluator.DeclaredOnBaseTypes = true;
            return (TScopeQuery)(Object)this;
        }

        TMemberQuery IScopeQueryBase<TMemberInfo, TMemberQuery, TScopeQuery>.All(int levelsDeep)
        {
            _scopeEvaluator.Instance = true;
            _scopeEvaluator.Static = true;
            _scopeEvaluator.DeclaredOnThisType = true;
            _scopeEvaluator.DeclaredOnBaseTypes = true;
            _scopeEvaluator.LevelsDeep = levelsDeep;
            return (TMemberQuery)(Object)this;
        }

        TMemberQuery IScopeQueryBase<TMemberInfo, TMemberQuery, TScopeQuery>.All()
        {
            _scopeEvaluator.Instance = true;
            _scopeEvaluator.Static = true;
            _scopeEvaluator.DeclaredOnThisType = true;
            _scopeEvaluator.DeclaredOnBaseTypes = true;
            return (TMemberQuery)(Object)this;
        }

        TMemberQuery IScopeQueryBase<TMemberInfo, TMemberQuery, TScopeQuery>.And()
        {
            return (TMemberQuery)(Object)this;
        }
        #endregion
        
        public TMemberQuery IgnoreCase()
        {
            _nameEvaluator.IgnoreCase = true;
            return (TMemberQuery)(Object)this;
        }

        public TMemberQuery Named(String name)
        {
            _nameEvaluator.Named = name;
            return (TMemberQuery)(Object)this;
        }

        public TMemberQuery NamedAny(IEnumerable<String> names)
        {
            _nameEvaluator.NamedAny = names;
            return (TMemberQuery)(Object)this;
        }

        #region IEnumerable implementation
        public IEnumerator<TMemberInfo> GetEnumerator()
        {
            var matches = new MemberQueryService(_type).FindMembers(
                (MemberTypes) _memberTypesBuilder.MemberTypes,
                _bindingFlagsBuilder.BindingFlags,
                _nameEvaluator.Named,
                _scopeEvaluator.DeclaredOnBaseTypes && _accessibilityEvaluator.Private);
            return (from memberInfo in matches
                   where _memberEvaluators.All(eval => eval.IsMatch(memberInfo))
                   select (TMemberInfo)memberInfo).GetEnumerator();
        }

        IEnumerator IEnumerable.GetEnumerator()
        {
            return this.GetEnumerator();
        }
        #endregion
    }
}
