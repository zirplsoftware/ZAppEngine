using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Zirpl.AppEngine.Model.Extensibility
{
    public interface IExtendedEntityFieldValue<TExtendedEntityFieldValue, TExtendedEntity, TId> : 
        IExtendedEntityFieldValue,
        IPersistable<TId>
        where TExtendedEntity : IExtensible<TExtendedEntity, TExtendedEntityFieldValue, TId>
        where TExtendedEntityFieldValue : IExtendedEntityFieldValue<TExtendedEntityFieldValue, TExtendedEntity, TId>
        where TId : IEquatable<TId>
    {
        TId ExtendedEntityId { get; set; }
        TExtendedEntity ExtendedEntity { get; set; }
    }

    public interface IExtendedEntityFieldValue : IPersistable
    {
        String Value { get; set; }
        Object GetExtendedEntityId();
        IExtensible GetExtendedEntity();
        void SetExtendedEntityId(Object id);
        void SetExtendedEntity(IExtensible entity);
    }
}
