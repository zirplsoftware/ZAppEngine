using System.Reflection;

namespace Zirpl.Reflection.Fluent
{
    public interface IMemberQuery : INamedMemberQuery<MemberInfo, IMemberQuery>
    {
        IMemberTypeQuery OfMemberType();
    }
}