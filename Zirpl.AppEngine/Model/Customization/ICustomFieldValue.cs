using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Zirpl.AppEngine.Model.Customization
{
    public interface ICustomFieldValue<TExtendedEntity, TId> : ICustomFieldValue, IPersistable<TId> where TId : IEquatable<TId>
    {
        TId ExtendedEntityId { get; set; }
        TExtendedEntity ExtendedEntity { get; set; }
    }

    public interface ICustomFieldValue : IPersistable
    {   
        String Value { get; set; }
        Object GetExtendedEntityId();
        Object GetExtendedEntity();
    }
}
