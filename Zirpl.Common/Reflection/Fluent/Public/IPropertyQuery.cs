using System;
using System.Reflection;

namespace Zirpl.Reflection.Fluent
{
    public interface IPropertyQuery : INamedMemberQuery<PropertyInfo, IPropertyQuery>
    {
        ITypeQuery<PropertyInfo, IPropertyQuery> OfPropertyType();
        IPropertyQuery WithGetter();
        IPropertyQuery WithSetter();
        IPropertyQuery WithGetterAndSetter();
    }
}