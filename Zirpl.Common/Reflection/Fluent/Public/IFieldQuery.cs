using System.Reflection;

namespace Zirpl.Reflection.Fluent
{
    public interface IFieldQuery : INamedMemberQuery<FieldInfo, IFieldQuery>
    {
        ITypeQuery<FieldInfo, IFieldQuery> OfFieldType();
    }
}