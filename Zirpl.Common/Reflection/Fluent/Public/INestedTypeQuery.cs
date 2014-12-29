using System;
using System.Reflection;

namespace Zirpl.Reflection.Fluent
{
    public interface INestedTypeQuery : INamedMemberQuery<Type, INestedTypeQuery>
    {
        ITypeQuery<Type, INestedTypeQuery> OfType();
    }
}