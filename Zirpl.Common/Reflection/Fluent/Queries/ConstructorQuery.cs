using System;
using System.Reflection;

namespace Zirpl.Reflection.Fluent
{
    internal sealed class ConstructorQuery : TypeMemberQueryBase<ConstructorInfo, IConstructorQuery>, 
        IConstructorQuery
    {
        internal ConstructorQuery(Type type)
            :base(type)
        {
            _memberTypeEvaluator.Constructor = true;
        }
    }
}
