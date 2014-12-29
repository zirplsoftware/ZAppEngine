using System.Reflection;

namespace Zirpl.Reflection.Fluent
{
    public interface IMethodQuery : INamedMemberQuery<MethodInfo, IMethodQuery>
    {
        ITypeQuery<MethodInfo, IMethodQuery> OfReturnType();
    }
}