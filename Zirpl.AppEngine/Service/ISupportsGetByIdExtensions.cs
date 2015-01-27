using System;
using System.Linq;
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

            return (from object value in array 
                    where value.Equals(enumValue) 
                    select service.Get((TId) value)).FirstOrDefault();
        }
    }
}
