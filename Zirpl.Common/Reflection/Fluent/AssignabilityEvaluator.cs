﻿using System;
using System.Collections.Generic;
using System.Linq;
using System.Reflection;
using System.Text;
using System.Threading.Tasks;

namespace Zirpl.Reflection.Fluent
{
    internal sealed class AssignabilityEvaluator
    {
        private Type _assignableFrom;
        private IEnumerable<Type> _assignableFromAny;
        private IEnumerable<Type> _assignableFromAll;
        private Type _assignableTo;
        private IEnumerable<Type> _assignableToAny;
        private IEnumerable<Type> _assignableToAll;
        private bool _void;

        internal bool IsMatch(Type variableType)
        {
            if (_void && variableType == null) return true;
            if (_assignableFrom != null && !variableType.IsAssignableFrom(_assignableFrom)) return false;
            if (_assignableTo != null && !_assignableTo.IsAssignableFrom(variableType)) return false;
            if (_assignableFromAll != null && !_assignableFromAll.All(variableType.IsAssignableFrom)) return false;
            if (_assignableToAll != null && !_assignableToAll.All(type => type.IsAssignableFrom(variableType))) return false;
            if (_assignableFromAny != null && !_assignableFromAny.Any(variableType.IsAssignableFrom)) return false;
            if (_assignableToAny != null && !_assignableToAny.All(type => type.IsAssignableFrom(variableType))) return false;
            return true;
        }

        internal void Void()
        {
            if (_assignableFrom != null) throw new InvalidOperationException("Cannot call both Void and AssignableFrom. Use 2 queries if needed.");
            if (_assignableFromAll != null) throw new InvalidOperationException("Cannot call both Void and AssignableFromAll. Use 2 queries if needed.");
            if (_assignableFromAny != null) throw new InvalidOperationException("Cannot call both Void and AssignableFromAny. Use 2 queries if needed.");
            if (_assignableTo != null) throw new InvalidOperationException("Cannot call both Void and AssignableTo. Use 2 queries if needed.");
            if (_assignableToAll != null) throw new InvalidOperationException("Cannot call both Void and AssignableToAll. Use 2 queries if needed.");
            if (_assignableToAny != null) throw new InvalidOperationException("Cannot call both Void and AssignableToAny. Use 2 queries if needed.");

            _void = true;
        }

        internal void AssignableFrom(Type type)
        {
            if (type == null) throw new ArgumentNullException("type");
            if (_assignableFrom != null) throw new InvalidOperationException("Cannot call AssignableFrom twice. Use 2 queries if needed.");
            if (_assignableFromAll != null) throw new InvalidOperationException("Cannot call both AssignableFromAll and AssignableFrom. Use 2 queries if needed.");
            if (_assignableFromAny != null) throw new InvalidOperationException("Cannot call both AssignableFromAny and AssignableFrom. Use 2 queries if needed.");

            _assignableFrom = type;
        }

        internal void AssignableFromAny(IEnumerable<Type> types)
        {
            if (types == null) throw new ArgumentNullException("types");
            if (_assignableFromAny != null) throw new InvalidOperationException("Cannot call AssignableFromAny twice. Use 2 queries if needed.");
            if (_assignableFrom != null) throw new InvalidOperationException("Cannot call both AssignableFromAny and AssignableFrom. Use 2 queries if needed.");

            _assignableFromAny = types;
        }

        internal void AssignableFromAll(IEnumerable<Type> types)
        {
            if (types == null) throw new ArgumentNullException("types");
            if (_assignableFromAll != null) throw new InvalidOperationException("Cannot call AssignableFromAll twice. Use 2 queries if needed.");
            if (_assignableFrom != null) throw new InvalidOperationException("Cannot call both AssignableFromAll and AssignableFrom. Use 2 queries if needed.");

            _assignableFromAll = types;
        }

        internal void AssignableTo(Type type)
        {
            if (type == null) throw new ArgumentNullException("type");
            if (_assignableTo != null) throw new InvalidOperationException("Cannot call AssignableTo twice. Use 2 queries if needed.");
            if (_assignableToAny != null) throw new InvalidOperationException("Cannot call both AssignableToAny and AssignableTo. Use 2 queries if needed.");
            if (_assignableToAll != null) throw new InvalidOperationException("Cannot call both AssignableToAll and AssignableTo. Use 2 queries if needed.");

            _assignableTo = type;
        }

        internal void AssignableToAny(IEnumerable<Type> types)
        {
            if (types == null) throw new ArgumentNullException("types");
            if (_assignableToAny != null) throw new InvalidOperationException("Cannot call AssignableToAny twice. Use 2 queries if needed.");
            if (_assignableTo != null) throw new InvalidOperationException("Cannot call both AssignableToAny and AssignableTo. Use 2 queries if needed.");

            _assignableToAny = types;
        }

        internal void AssignableToAll(IEnumerable<Type> types)
        {
            if (types == null) throw new ArgumentNullException("types");
            if (_assignableToAll != null) throw new InvalidOperationException("Cannot call AssignableToAll twice. Use 2 queries if needed.");
            if (_assignableTo != null) throw new InvalidOperationException("Cannot call both AssignableToAll and AssignableTo. Use 2 queries if needed.");

            _assignableToAll = types;
        }
    }
}