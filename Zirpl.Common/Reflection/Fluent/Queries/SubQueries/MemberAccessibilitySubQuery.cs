using System;
using System.Collections.Generic;
using System.Linq;
using System.Reflection;
using System.Text;
using System.Threading.Tasks;

namespace Zirpl.Reflection.Fluent
{
    internal sealed class MemberAccessibilitySubQuery<TMemberInfo, TReturnQuery> : SubQueryBase<TMemberInfo, TReturnQuery>,
        IMemberAccessibilityQuery<TMemberInfo, TReturnQuery>
        where TMemberInfo : MemberInfo
        where TReturnQuery : IQueryResult<TMemberInfo>
    {
        private readonly AccessibilityEvaluator _accessibilityEvaluator;

        internal MemberAccessibilitySubQuery(TReturnQuery returnQuery, AccessibilityEvaluator accessibilityEvaluator)
            :base(returnQuery)
        {
            _accessibilityEvaluator = accessibilityEvaluator;
        }

        IMemberAccessibilityQuery<TMemberInfo, TReturnQuery> IMemberAccessibilityQuery<TMemberInfo, TReturnQuery>.Public()
        {
            _accessibilityEvaluator.Public = true;
            return this;
        }

        IMemberAccessibilityQuery<TMemberInfo, TReturnQuery> IMemberAccessibilityQuery<TMemberInfo, TReturnQuery>.Private()
        {
            _accessibilityEvaluator.Private = true;
            return this;
        }

        IMemberAccessibilityQuery<TMemberInfo, TReturnQuery> IMemberAccessibilityQuery<TMemberInfo, TReturnQuery>.Protected()
        {
            _accessibilityEvaluator.Protected = true;
            return this;
        }

        IMemberAccessibilityQuery<TMemberInfo, TReturnQuery> IMemberAccessibilityQuery<TMemberInfo, TReturnQuery>.Internal()
        {
            _accessibilityEvaluator.Internal = true;
            return this;
        }

        IMemberAccessibilityQuery<TMemberInfo, TReturnQuery> IMemberAccessibilityQuery<TMemberInfo, TReturnQuery>.ProtectedInternal()
        {
            _accessibilityEvaluator.ProtectedInternal = true;
            return this;
        }

        TReturnQuery IMemberAccessibilityQuery<TMemberInfo, TReturnQuery>.NotPrivate()
        {
            _accessibilityEvaluator.Public = true;
            _accessibilityEvaluator.Protected = true;
            _accessibilityEvaluator.Internal = true;
            _accessibilityEvaluator.ProtectedInternal = true;
            return (TReturnQuery)(Object)this;
        }

        TReturnQuery IMemberAccessibilityQuery<TMemberInfo, TReturnQuery>.NotPublic()
        {
            _accessibilityEvaluator.Private = true;
            _accessibilityEvaluator.Protected = true;
            _accessibilityEvaluator.Internal = true;
            _accessibilityEvaluator.ProtectedInternal = true;
            return (TReturnQuery)(Object)this;
        }

        TReturnQuery IMemberAccessibilityQuery<TMemberInfo, TReturnQuery>.All()
        {
            _accessibilityEvaluator.Public = true;
            _accessibilityEvaluator.Private = true;
            _accessibilityEvaluator.Protected = true;
            _accessibilityEvaluator.Internal = true;
            _accessibilityEvaluator.ProtectedInternal = true;
            return (TReturnQuery)(Object)this;
        }

        TReturnQuery IMemberAccessibilityQuery<TMemberInfo, TReturnQuery>.And()
        {
            return (TReturnQuery)(Object)this;
        }
    }
}
