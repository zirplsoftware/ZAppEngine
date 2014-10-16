using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Zirpl.AppEngine.Model
{
    public abstract class DictionaryEntityBase<TId, TEnum> : AuditableBase<TId>, IDictionaryEntity<TId, TEnum>
        where TEnum : struct
        where TId : IEquatable<TId>
    {
        public virtual String Name { get; set; }
    }

    public abstract class DictionaryEntityBaseMetadataConstantsBase : MetadataConstantsBase
    {
    }
}
