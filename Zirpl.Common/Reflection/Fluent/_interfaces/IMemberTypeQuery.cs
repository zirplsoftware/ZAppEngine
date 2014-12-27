using System.Collections.Generic;
using System.Reflection;

namespace Zirpl.Reflection.Fluent
{
    public interface IMemberTypeQuery : IEnumerable<MemberInfo>
    {
        IMemberTypeQuery Constructor();
        IMemberTypeQuery Event();
        IMemberTypeQuery Field();
        IMemberTypeQuery Method();
        IMemberTypeQuery NestedType();
        IMemberTypeQuery Property();
        IMemberQuery All();
        IMemberQuery And();
    }
}