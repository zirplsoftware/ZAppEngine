using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using Zirpl.AppEngine.Model;

namespace Zirpl.AppEngine.DataService
{
    public interface IDictionaryEntityDataService<TEntity, TId, TEnum> : IReadOnlyDataService<TEntity, TId>
        where TEntity : class, IStaticLookup<TId, TEnum> 
        where TEnum : struct
        where TId : IEquatable<TId>
    {
    }
}
