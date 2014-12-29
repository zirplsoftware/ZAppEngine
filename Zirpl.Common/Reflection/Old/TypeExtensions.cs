using System;
using System.ComponentModel;
using System.Reflection;

namespace Zirpl.Reflection
{
    public static class TypeExtensions
    {



        public static ITypeAccessor GetTypeAccessor(this Type type)
        {
            if (type == null) throw new ArgumentNullException("type");

            return TypeAccessorFactory.GetTypeAccessor(type);
        }        
        
    }
}
