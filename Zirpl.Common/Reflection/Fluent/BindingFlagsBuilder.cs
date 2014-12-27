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
        private readonly ScopeEvaluator _scopeEvaluator;
        private readonly AccessibilityEvaluator _accessibilityEvaluator;
        private readonly NameEvaluator _nameEvaluator;

        internal BindingFlagsBuilder(AccessibilityEvaluator accessibilityEvaluator, ScopeEvaluator scopeEvaluator, NameEvaluator nameEvaluator)
        {
            _scopeEvaluator = scopeEvaluator;
            _accessibilityEvaluator = accessibilityEvaluator;
            _nameEvaluator = nameEvaluator;
        }

        internal BindingFlags BindingFlags
        {
            get
            {
                var bindings = default(BindingFlags);
                bindings = _accessibilityEvaluator.Public ? bindings | BindingFlags.Public : bindings;
                bindings = _accessibilityEvaluator.Private || _accessibilityEvaluator.Protected || _accessibilityEvaluator.ProtectedInternal || _accessibilityEvaluator.Internal
                            ? bindings | BindingFlags.NonPublic : bindings;
                bindings = _scopeEvaluator.Instance ? bindings | BindingFlags.Instance : bindings;
                bindings = _scopeEvaluator.Static ? bindings | BindingFlags.Static : bindings;
                bindings = _scopeEvaluator.Static && _scopeEvaluator.DeclaredOnBaseTypes ? bindings | BindingFlags.FlattenHierarchy : bindings;
                bindings = _nameEvaluator.IgnoreCase ? bindings | BindingFlags.IgnoreCase : bindings;
                bindings = _scopeEvaluator.DeclaredOnThisType && !_scopeEvaluator.DeclaredOnBaseTypes ? bindings | BindingFlags.DeclaredOnly : bindings;
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
