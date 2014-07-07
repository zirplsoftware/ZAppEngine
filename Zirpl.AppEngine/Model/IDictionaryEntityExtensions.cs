using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Zirpl.AppEngine.Model
{
    public static class IDictionaryEntityExtensions
    {
        public static TEnum? GetEnumValue<TId, TEnum>(this IDictionaryEntity<TId, TEnum> dictionaryEntity)
            where TEnum : struct
            where TId : IEquatable<TId>
        {
            TEnum? value = null;
            if (dictionaryEntity != null
                && dictionaryEntity.IsPersisted)
            {
                value = (TEnum?)Enum.Parse(typeof(TEnum), Enum.GetName(typeof(TEnum), dictionaryEntity.GetId()));
            }
            return value;
        }
    }
}
