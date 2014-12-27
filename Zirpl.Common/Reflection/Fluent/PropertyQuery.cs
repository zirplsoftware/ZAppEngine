using System;
using System.Reflection;

namespace Zirpl.Reflection.Fluent
{
    internal sealed class PropertyQuery : MemberQueryBase<PropertyInfo, IPropertyQuery, IPropertyAccessibilityQuery, IPropertyScopeQuery>, 
        IPropertyQuery,
        IPropertyAccessibilityQuery,
        IPropertyScopeQuery
    {
        private readonly GetSetMatcher _getSetMatcher;

        internal PropertyQuery(Type type)
            :base(type)
        {
            _getSetMatcher = new GetSetMatcher();
        }

        protected override bool IsMatch(MemberInfo memberInfo)
        {
            return _getSetMatcher.IsMatch((PropertyInfo)memberInfo);
        }

        protected override MemberTypeFlags MemberTypes
        {
            get { return MemberTypeFlags.Property; }
        }

        public IPropertyQuery WithGetter()
        {
            _getSetMatcher.WithGetter = true;
            return this;
        }

        public IPropertyQuery WithSetter()
        {
            _getSetMatcher.WithSetter = true;
            return this;
        }

        public IPropertyQuery WithGetterAndSetter()
        {
            _getSetMatcher.WithGetter = true;
            _getSetMatcher.WithSetter = true;
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
