using System;
using System.Collections.Generic;
using System.Reflection;

namespace Zirpl.Reflection.Fluent
{
    internal sealed class FieldQuery : MemberQueryBase<FieldInfo, IFieldQuery, IFieldAccessibilityQuery, IFieldScopeQuery>, 
        IFieldQuery,
        IFieldAccessibilityQuery,
        IFieldScopeQuery,
        IFieldAssignabilityQuery
    {
        private readonly FieldTypeEvaluator _typeEvaluator;

        internal FieldQuery(Type type)
            :base(type)
        {
            _memberTypesBuilder.Field = true;
            _typeEvaluator = new FieldTypeEvaluator();
            _memberEvaluators.Add(_typeEvaluator);
        }

        public IFieldAssignabilityQuery OfType()
        {
            return this;
        }

        public IFieldQuery AssignableFrom(Type type)
        {
            _typeEvaluator.AssignableFrom(type);
            return this;
        }

        public IFieldQuery AssignableFromAny(IEnumerable<Type> types)
        {
            _typeEvaluator.AssignableFromAny(types);
            return this;
        }

        public IFieldQuery AssignableFromAll(IEnumerable<Type> types)
        {
            _typeEvaluator.AssignableFromAll(types);
            return this;
        }

        public IFieldQuery AssignableTo(Type type)
        {
            _typeEvaluator.AssignableTo(type);
            return this;
        }

        public IFieldQuery AssignableToAny(IEnumerable<Type> types)
        {
            _typeEvaluator.AssignableToAny(types);
            return this;
        }

        public IFieldQuery AssignableToAll(IEnumerable<Type> types)
        {
            _typeEvaluator.AssignableToAll(types);
            return this;
        }
    }
}
