using System;
using System.Collections.Generic;
using System.Linq;
using System.Reflection;

namespace Zirpl.Reflection.Fluent
{
    internal sealed class PropertyQuery : MemberQueryBase<PropertyInfo, IPropertyQuery, IPropertyAccessibilityQuery, IPropertyScopeQuery>, 
        IPropertyQuery,
        IPropertyAccessibilityQuery,
        IPropertyScopeQuery,
        IPropertyAssignabilityQuery
    {
        private readonly GetSetMatcher _getSetMatcher;
        private readonly AssignabilityEvaluator _assignabilityEvaluator;

        internal PropertyQuery(Type type)
            :base(type)
        {
            _getSetMatcher = new GetSetMatcher();
            _assignabilityEvaluator = new AssignabilityEvaluator();
        }

        protected override bool IsMatch(MemberInfo memberInfo)
        {
            return _getSetMatcher.IsMatch((PropertyInfo)memberInfo)
                && _assignabilityEvaluator.IsMatch(((PropertyInfo)memberInfo).PropertyType);
        }

        protected override MemberTypeFlags MemberTypes
        {
            get { return MemberTypeFlags.Property; }
        }

        public IPropertyQuery WithGetters()
        {
            _getSetMatcher.WithGetter = true;
            return this;
        }

        public IPropertyQuery WithSetters()
        {
            _getSetMatcher.WithSetter = true;
            return this;
        }

        public IPropertyQuery WithGettersAndSetters()
        {
            _getSetMatcher.WithGetter = true;
            _getSetMatcher.WithSetter = true;
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

        private class GetSetMatcher
        {
            internal bool WithGetter { get; set; }
            internal bool WithSetter { get; set; }

            internal bool IsMatch(PropertyInfo property)
            {
                if (WithGetter && !property.CanRead) return false;
                if (WithSetter && !property.CanWrite) return false;
                return true;
            }
        }
    }
}
