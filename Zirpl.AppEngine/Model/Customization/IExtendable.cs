using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Zirpl.AppEngine.Model.Customization
{
    public interface IExtendable<TExtendedEntity, TExtendedEntityFieldValue, TId> : IPersistable<TId>, IExtendable
        where TId : IEquatable<TId>
        where TExtendedEntityFieldValue : IExtendedEntityFieldValue<TExtendedEntity, TId>
        where TExtendedEntity: IPersistable<TId>
    {
        IList<TExtendedEntityFieldValue> ExtendedFieldValues { get; set; }
    }

    public interface IExtendable : IPersistable
    {
        IList<IExtendedEntityFieldValue> GetExtendedFieldValues();
        void SetExtendedFieldValues(IList<IExtendedEntityFieldValue> list);
    }
}
