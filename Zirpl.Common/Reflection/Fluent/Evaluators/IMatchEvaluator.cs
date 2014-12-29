using System.Reflection;

namespace Zirpl.Reflection.Fluent
{
    public interface IMatchEvaluator
    {
        bool IsMatch(MemberInfo memberInfo);
    }
}