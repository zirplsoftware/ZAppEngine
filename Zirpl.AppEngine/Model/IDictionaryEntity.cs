using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Zirpl.AppEngine.Model
{
    public interface IDictionaryEntity<TId, TEnum> : IDictionaryEntity, IPersistable<TId>
        where TEnum : struct
        where TId : IEquatable<TId>
    {

    }
    public interface IDictionaryEntity : IPersistable
    {
        String Name { get; set; }
    }
}
