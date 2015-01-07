using System;

namespace Zirpl.AppEngine.Model
{
    public interface IEnumDescribed<TId, TEnum> : IPersistable<TId>, IEnumDescribed
        where TEnum : struct
        where TId : IEquatable<TId>
    {
    }

    public interface IEnumDescribed : IPersistable
    {

    }
}
