using System.Reflection;

namespace Zirpl.Reflection.Fluent
{
    public interface IMemberEvaluator
    {
        bool IsMatch(MemberInfo memberInfo);
    }
}