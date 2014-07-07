using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Zirpl.AppEngine.Model
{
    public interface IDictionaryEntity : IPersistable, IAuditable
    {
        String Name { get; set; }
    }

    public interface IDictionaryEntity<TId, TEnum> : IDictionaryEntity
        where TEnum: struct
        where TId : IEquatable<TId>
    {

    }
}
