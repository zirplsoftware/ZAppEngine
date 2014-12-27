using System;
using System.Collections.Generic;
using System.Linq;
using System.Reflection;

namespace Zirpl.Reflection.Fluent
{
    internal abstract class TypeEvaluatorBase : IMemberEvaluator
    {
        // TODO: how can these be used? Type.FindInterfaces, Type.IsIstanceOf, Type.IsSubClassOf

        private Type _assignableFrom;
        private IEnumerable<Type> _assignableFromAny;
        private IEnumerable<Type> _assignableFromAll;
        private Type _assignableTo;
        private IEnumerable<Type> _assignableToAny;
        private IEnumerable<Type> _assignableToAll;
        private bool _isVoid;

        // TODO: implement all these
        //private bool _isValueType;
        //private bool _isNullableValueType;
        //private bool _isValueTypeOrNullableValueType;
        //private bool _isEnum;
        //private bool _isNullableEnum;
        //private bool _isEnumOrIsNullableEnum;
        //private bool _isClass;
        //private bool _isInterface;
        //private bool _isClassOrInterface;
        //private bool _isPrimtive;
        //private Type _implementingInterface;
        //private IEnumerable<Type> _implementingAllInterfaces;
        //private IEnumerable<Type> _implementingAnyInterfaces;
        //private Type _exact;
        //private IEnumerable<Type> _exactAny;

        public abstract bool IsMatch(MemberInfo memberInfo);

        protected bool IsTypeMatch(Type variableType)
        {
            if (_isVoid
                || _assignableFrom != null
                || _assignableFromAny != null
                || _assignableFromAll != null
                || _assignableTo != null
                || _assignableToAny != null
                || _assignableToAll != null)
            {
                if (variableType == null)
                {
                    if (_assignableFrom != null
                        || _assignableFromAny != null
                        || _assignableFromAll != null
                        || _assignableTo != null
                        || _assignableToAny != null
                        || _assignableToAll != null)
                    {
                        return false;
                    }
                }
                else
                {
                    if (_isVoid) return false;
                    if (_assignableFrom != null && !variableType.IsAssignableFrom(_assignableFrom)) return false;
                    if (_assignableTo != null && !_assignableTo.IsAssignableFrom(variableType)) return false;
                    if (_assignableFromAll != null && !_assignableFromAll.All(variableType.IsAssignableFrom)) return false;
                    if (_assignableToAll != null && !_assignableToAll.All(type => type.IsAssignableFrom(variableType))) return false;
                    if (_assignableFromAny != null && !_assignableFromAny.Any(variableType.IsAssignableFrom)) return false;
                    if (_assignableToAny != null && !_assignableToAny.All(type => type.IsAssignableFrom(variableType))) return false;
                }
            }
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

            _isVoid = true;
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
