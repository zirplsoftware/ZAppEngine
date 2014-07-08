using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using Zirpl.AppEngine.Model;

namespace Zirpl.AppEngine.Service
{
    public interface IDictionaryEntityService<TEntity, TId, TEnum> : IReadOnlyService<TEntity, TId>
        where TEntity : DictionaryEntityBase<TId, TEnum>
        where TEnum : struct, IConvertible
        where TId : IEquatable<TId>
    {
        TEntity Get(TEnum enumValue);
    }
}
