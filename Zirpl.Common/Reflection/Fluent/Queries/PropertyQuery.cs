using System;
using System.Collections.Generic;
using System.Reflection;

namespace Zirpl.Reflection.Fluent
{
    internal sealed class PropertyQuery : NamedTypeMemberQueryBase<PropertyInfo, IPropertyQuery>,
        IPropertyQuery
    {
        private readonly PropertyReadWriteEvaluator _readWriteEvaluator;
        private readonly PropertyTypeEvaluator _propertyTypeEvaluator;

        internal PropertyQuery(Type type)
            :base(type)
        {
            _readWriteEvaluator = new PropertyReadWriteEvaluator();
            _propertyTypeEvaluator = new PropertyTypeEvaluator();
            _memberTypeEvaluator.Property = true;
            _matchEvaluators.Add(_readWriteEvaluator);
            _matchEvaluators.Add(_propertyTypeEvaluator);
        }

        ITypeQuery<PropertyInfo, IPropertyQuery> IPropertyQuery.OfPropertyType()
        {
            return new TypeSubQuery<PropertyInfo, IPropertyQuery>(this, _propertyTypeEvaluator);
        }

        IPropertyQuery IPropertyQuery.WithGetter()
        {
            _readWriteEvaluator.CanRead = true;
            return this;
        }

        IPropertyQuery IPropertyQuery.WithSetter()
        {
            _readWriteEvaluator.CanWrite = true;
            return this;
        }

        IPropertyQuery IPropertyQuery.WithGetterAndSetter()
        {
            _readWriteEvaluator.CanRead = true;
            _readWriteEvaluator.CanWrite = true;
            return this;
        }
    }
}
