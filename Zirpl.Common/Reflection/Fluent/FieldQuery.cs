using System;
using System.Collections.Generic;
using System.Linq;
using System.Reflection;

namespace Zirpl.Reflection.Fluent
{
    internal sealed class FieldQuery : MemberQueryBase<FieldInfo, IFieldQuery, IFieldAccessibilityQuery, IFieldScopeQuery>, 
        IFieldQuery,
        IFieldAccessibilityQuery,
        IFieldScopeQuery,
        IFieldAssignabilityQuery
    {
        private readonly AssignabilityEvaluator _assignabilityEvaluator;

        internal FieldQuery(Type type)
            :base(type)
        {
            _assignabilityEvaluator = new AssignabilityEvaluator();
        }

        protected override bool IsMatch(MemberInfo memberInfo)
        {
            return _assignabilityEvaluator.IsMatch(((FieldInfo)memberInfo).FieldType);
        }

        protected override MemberTypeFlags MemberTypes
        {
            get { return MemberTypeFlags.Field; }
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
