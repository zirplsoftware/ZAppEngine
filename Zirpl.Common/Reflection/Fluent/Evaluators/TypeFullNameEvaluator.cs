using System;
using System.Collections.Generic;
using System.Linq;
using System.Reflection;

namespace Zirpl.Reflection.Fluent
{
    internal sealed class TypeFullNameEvaluator : NameEvaluator
    {
        protected override string GetNameToCheck(MemberInfo memberInfo)
        {
            var type = (Type) memberInfo;
            return type.FullName;
        }
    }
}