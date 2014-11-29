using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using Zirpl.AppEngine.Model;

namespace Zirpl.AppEngine.DataService.EntityFramework.Mapping
{
    public class DictionaryEntityMapping<TEntity, TId, TEnum> : EntityMappingBase<TEntity, TId>
        where TEntity : StaticLookupEntityBase<TId, TEnum>
        where TEnum : struct
        where TId : IEquatable<TId>
    {
        protected override void MapProperties()
        {
            base.MapProperties();
        }
    }
}
