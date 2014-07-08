using System;
using System.Collections.Generic;
using System.Data.Entity;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using Zirpl.AppEngine.DataService.EntityFramework;
using Zirpl.AppEngine.Model;

namespace Zirpl.AppEngine.Service.EntityFramework
{
    public abstract class DictionaryEntityService<TDataContext, TEntity, TId, TEnum> :
           ReadOnlyDbContextServiceBase<TDataContext, TEntity, TId>,
           IDictionaryEntityService<TEntity, TId, TEnum>
        where TDataContext : DbContext
        where TEntity : DictionaryEntityBase<TId, TEnum>
        where TEnum : struct, IConvertible
        where TId : IEquatable<TId>
    {
        public TEntity Get(TEnum enumValue)
        {
            var array = Enum.GetValues(typeof(TEnum));

            TEntity entity = null;
            foreach (var value in array)
            {
                if (value.Equals(enumValue))
                {
                    entity = this.Get((TId)value);
                    break;
                }
            }
            return entity;
        }
    }
}
