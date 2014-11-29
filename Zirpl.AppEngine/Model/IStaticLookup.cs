using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Zirpl.AppEngine.Model
{
    public interface IStaticLookup<TId, TEnum> : IStaticLookup, IPersistable<TId>
        where TEnum : struct
        where TId : IEquatable<TId>
    {
        TEnum? EnumValue { get; }
    }
    public interface IStaticLookup : IPersistable
    {
        String Name { get; set; }
    }
}
