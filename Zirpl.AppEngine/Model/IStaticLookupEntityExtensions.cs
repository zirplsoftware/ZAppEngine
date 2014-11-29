using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Zirpl.AppEngine.Model
{
    public static class IStaticLookupEntityExtensions
    {
        public static TEnum? EvaluateEnumValue<TId, TEnum>(this IStaticLookup<TId, TEnum> staticLookup)
            where TEnum : struct
            where TId : IEquatable<TId>
        {
            TEnum? value = null;
            if (staticLookup != null
                && staticLookup.IsPersisted)
            {
                value = (TEnum?)Enum.Parse(typeof(TEnum), Enum.GetName(typeof(TEnum), staticLookup.GetId()), true);
            }
            return value;
        }
    }
}
