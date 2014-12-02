using System;
using System.Collections.Generic;

namespace Zirpl.AppEngine.Model.Extensibility
{
    public interface IExtensible<TExtendedEntity, TExtendedEntityFieldValue, TId> : IPersistable<TId>, IExtensible
        where TId : IEquatable<TId>
        where TExtendedEntityFieldValue : IExtendedEntityFieldValue<TExtendedEntity, TId>
        where TExtendedEntity: IPersistable<TId>
    {
        IList<TExtendedEntityFieldValue> ExtendedFieldValues { get; set; }
    }

    public interface IExtensible : IPersistable
    {
        IList<IExtendedEntityFieldValue> GetExtendedFieldValues();
        void SetExtendedFieldValues(IList<IExtendedEntityFieldValue> list);
    }
}
