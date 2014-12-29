using System.Reflection;

namespace Zirpl.Reflection.Fluent
{
    public interface INamedMemberQuery<out TMemberInfo, out TMemberQuery> : IMemberQuery<TMemberInfo, TMemberQuery>
        where TMemberInfo : MemberInfo
        where TMemberQuery : INamedMemberQuery<TMemberInfo, TMemberQuery>
    {
        INameQuery<TMemberInfo, INamedMemberQuery<TMemberInfo, TMemberQuery>> Named();
    }
}