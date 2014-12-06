using System;
using System.Collections.Generic;
using System.Linq;
using Zirpl.Collections;

namespace Zirpl.AppEngine.Model.Extensibility
{
    public interface IExtensible<TExtendedEntity, TExtendedEntityFieldValue, TId> : IPersistable<TId>, IExtensible
        where TId : IEquatable<TId>
        where TExtendedEntityFieldValue : IExtendedEntityFieldValue<TExtendedEntityFieldValue, TExtendedEntity, TId>
        where TExtendedEntity: IExtensible<TExtendedEntity, TExtendedEntityFieldValue, TId>
    {
        IList<TExtendedEntityFieldValue> ExtendedFieldValues { get; set; }
    }

    public interface IExtensible : IPersistable
    {
        IList<IExtendedEntityFieldValue> GetExtendedFieldValues();
        void SetExtendedFieldValues(IList<IExtendedEntityFieldValue> list);
    }
}
