using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using Zirpl.AppEngine.Model;

namespace Zirpl.AppEngine.Service
{
    public static class ISupportsGetByIdExtensions
    {
        public static TEntity GetByEnum<TEntity, TId, TEnum>(this ISupportsGetById<TEntity, TId> service, TEnum enumValue)
            where TEntity : class, IPersistable<TId>
            where TEnum : struct
        {
            var array = Enum.GetValues(typeof(TEnum));

            TEntity entity = null;
            foreach (var value in array)
            {
                if (value.Equals(enumValue))
                {
                    entity = service.Get((TId)value);
                    break;
                }
            }
            return entity;
        }
    }
}
