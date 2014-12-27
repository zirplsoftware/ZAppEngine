using System;
using System.Collections.Generic;
using System.Reflection;

namespace Zirpl.Reflection.Fluent
{
    internal sealed class PropertyQuery : MemberQueryBase<PropertyInfo, IPropertyQuery, IPropertyAccessibilityQuery, IPropertyScopeQuery>, 
        IPropertyQuery,
        IPropertyAccessibilityQuery,
        IPropertyScopeQuery,
        IPropertyAssignabilityQuery
    {
        private readonly PropertyReadWriteEvaluator _readWriteEvaluator;
        private readonly PropertyTypeAssignabilityEvaluator _assignabilityEvaluator;

        internal PropertyQuery(Type type)
            :base(type)
        {
            _readWriteEvaluator = new PropertyReadWriteEvaluator();
            _assignabilityEvaluator = new PropertyTypeAssignabilityEvaluator();
            _memberTypesBuilder.Property = true;
            _memberEvaluators.Add(_readWriteEvaluator);
            _memberEvaluators.Add(_assignabilityEvaluator);
        }

        public IPropertyQuery WithGetters()
        {
            _readWriteEvaluator.CanRead = true;
            return this;
        }

        public IPropertyQuery WithSetters()
        {
            _readWriteEvaluator.CanWrite = true;
            return this;
        }

        public IPropertyQuery WithGettersAndSetters()
        {
            _readWriteEvaluator.CanRead = true;
            _readWriteEvaluator.CanWrite = true;
            return this;
        }

        public IPropertyAssignabilityQuery OfType()
        {
            return this;
        }

        public IPropertyQuery AssignableFrom(Type type)
        {
            _assignabilityEvaluator.AssignableFrom(type);
            return this;
        }

        public IPropertyQuery AssignableFromAny(IEnumerable<Type> types)
        {
            _assignabilityEvaluator.AssignableFromAny(types);
            return this;
        }

        public IPropertyQuery AssignableFromAll(IEnumerable<Type> types)
        {
            _assignabilityEvaluator.AssignableFromAll(types);
            return this;
        }

        public IPropertyQuery AssignableTo(Type type)
        {
            _assignabilityEvaluator.AssignableTo(type);
            return this;
        }

        public IPropertyQuery AssignableToAny(IEnumerable<Type> types)
        {
            _assignabilityEvaluator.AssignableToAny(types);
            return this;
        }

        public IPropertyQuery AssignableToAll(IEnumerable<Type> types)
        {
            _assignabilityEvaluator.AssignableToAll(types);
            return this;
        }
    }
}
