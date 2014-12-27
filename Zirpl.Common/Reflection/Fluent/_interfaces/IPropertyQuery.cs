using System.Reflection;

namespace Zirpl.Reflection.Fluent
{
    public interface IPropertyQuery : IMemberQueryBase<PropertyInfo, IPropertyQuery, IPropertyAccessibilityQuery, IPropertyScopeQuery>
    {
        IPropertyQuery WithGetters();
        IPropertyQuery WithSetters();
        IPropertyQuery WithGettersAndSetters();
        IPropertyAssignabilityQuery OfType();
    }
}