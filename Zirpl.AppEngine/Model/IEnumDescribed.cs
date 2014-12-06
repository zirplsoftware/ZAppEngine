using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

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
