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
        private readonly PropertyTypeEvaluator _typeEvaluator;

        internal PropertyQuery(Type type)
            :base(type)
        {
            _readWriteEvaluator = new PropertyReadWriteEvaluator();
            _typeEvaluator = new PropertyTypeEvaluator();
            _memberTypesBuilder.Property = true;
            _memberEvaluators.Add(_readWriteEvaluator);
            _memberEvaluators.Add(_typeEvaluator);
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
            _typeEvaluator.AssignableFrom(type);
            return this;
        }

        public IPropertyQuery AssignableFromAny(IEnumerable<Type> types)
        {
            _typeEvaluator.AssignableFromAny(types);
            return this;
        }

        public IPropertyQuery AssignableFromAll(IEnumerable<Type> types)
        {
            _typeEvaluator.AssignableFromAll(types);
            return this;
        }

        public IPropertyQuery AssignableTo(Type type)
        {
            _typeEvaluator.AssignableTo(type);
            return this;
        }

        public IPropertyQuery AssignableToAny(IEnumerable<Type> types)
        {
            _typeEvaluator.AssignableToAny(types);
            return this;
        }

        public IPropertyQuery AssignableToAll(IEnumerable<Type> types)
        {
            _typeEvaluator.AssignableToAll(types);
            return this;
        }
    }
}
