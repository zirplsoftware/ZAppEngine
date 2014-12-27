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
        private readonly FieldTypeAssignabilityEvaluator _assignabilityEvaluator;

        internal FieldQuery(Type type)
            :base(type)
        {
            _memberTypesBuilder.Field = true;
            _assignabilityEvaluator = new FieldTypeAssignabilityEvaluator();
            _memberEvaluators.Add(_assignabilityEvaluator);
        }

        public IFieldAssignabilityQuery OfType()
        {
            return this;
        }

        public IFieldQuery AssignableFrom(Type type)
        {
            _assignabilityEvaluator.AssignableFrom(type);
            return this;
        }

        public IFieldQuery AssignableFromAny(IEnumerable<Type> types)
        {
            _assignabilityEvaluator.AssignableFromAny(types);
            return this;
        }

        public IFieldQuery AssignableFromAll(IEnumerable<Type> types)
        {
            _assignabilityEvaluator.AssignableFromAll(types);
            return this;
        }

        public IFieldQuery AssignableTo(Type type)
        {
            _assignabilityEvaluator.AssignableTo(type);
            return this;
        }

        public IFieldQuery AssignableToAny(IEnumerable<Type> types)
        {
            _assignabilityEvaluator.AssignableToAny(types);
            return this;
        }

        public IFieldQuery AssignableToAll(IEnumerable<Type> types)
        {
            _assignabilityEvaluator.AssignableToAll(types);
            return this;
        }
    }
}
