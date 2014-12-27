using System;
using System.Collections.Generic;
using System.Reflection;

namespace Zirpl.Reflection.Fluent
{
    public interface IMethodReturnTypeAssignabilityQuery :IAssignabilityQueryBase<MethodInfo, IMethodQuery>
    {
        //new IMethodQuery AssignableFrom(Type type);
        //new IMethodQuery AssignableFromAny(IEnumerable<Type> types);
        //new IMethodQuery AssignableFromAll(IEnumerable<Type> types);
        //new IMethodQuery AssignableTo(Type type);
        //new IMethodQuery AssignableToAny(IEnumerable<Type> types);
        //new IMethodQuery AssignableToAll(IEnumerable<Type> types);
        IMethodQuery Void();
    }
}