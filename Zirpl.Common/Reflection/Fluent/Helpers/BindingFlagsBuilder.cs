using System;
using System.Collections.Generic;
using System.Linq;
using System.Reflection;
using System.Text;
using System.Threading.Tasks;

namespace Zirpl.Reflection.Fluent
{
    internal sealed class BindingFlagsBuilder
    {
        private readonly MemberScopeEvaluator _memberScopeEvaluator;
        private readonly AccessibilityEvaluator _memberAccessibilityEvaluator;
        private readonly NameEvaluator _memberNameEvalulator;

        internal BindingFlagsBuilder(AccessibilityEvaluator memberAccessibilityEvaluator, MemberScopeEvaluator memberScopeEvaluator, NameEvaluator memberNameEvalulator)
        {
            _memberScopeEvaluator = memberScopeEvaluator;
            _memberAccessibilityEvaluator = memberAccessibilityEvaluator;
            _memberNameEvalulator = memberNameEvalulator;
        }

        internal BindingFlags BindingFlags
        {
            get
            {
                var bindings = default(BindingFlags);
                bindings = _memberAccessibilityEvaluator.Public ? bindings | BindingFlags.Public : bindings;
                bindings = _memberAccessibilityEvaluator.Private || _memberAccessibilityEvaluator.Protected || _memberAccessibilityEvaluator.ProtectedInternal || _memberAccessibilityEvaluator.Internal
                            ? bindings | BindingFlags.NonPublic : bindings;
                bindings = _memberScopeEvaluator.Instance ? bindings | BindingFlags.Instance : bindings;
                bindings = _memberScopeEvaluator.Static ? bindings | BindingFlags.Static : bindings;
                bindings = _memberScopeEvaluator.Static && _memberScopeEvaluator.DeclaredOnBaseTypes ? bindings | BindingFlags.FlattenHierarchy : bindings;
                bindings = _memberNameEvalulator.IgnoreCase ? bindings | BindingFlags.IgnoreCase : bindings;
                bindings = _memberScopeEvaluator.DeclaredOnThisType && !_memberScopeEvaluator.DeclaredOnBaseTypes ? bindings | BindingFlags.DeclaredOnly : bindings;
                if (!bindings.HasFlag(BindingFlags.Instance)
                    && !bindings.HasFlag(BindingFlags.Static))
                {
                    bindings = bindings | BindingFlags.Instance;
                }
                if (!bindings.HasFlag(BindingFlags.Public)
                    && !bindings.HasFlag(BindingFlags.NonPublic))
                {
                    bindings = bindings | BindingFlags.Public;
                }
                return bindings;
            }
        }
    }
}
