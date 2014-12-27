using System.Reflection;

namespace Zirpl.Reflection.Fluent
{
    public interface IPropertyQuery : IMemberQueryBase<PropertyInfo, IPropertyQuery, IPropertyAccessibilityQuery, IPropertyScopeQuery>
    {
        IPropertyQuery WithGetter();
        IPropertyQuery WithSetter();
        IPropertyQuery WithGetterAndSetter();
    }
}