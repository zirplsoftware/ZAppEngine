using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Zirpl.AppEngine.Model.Customization
{
    public interface ICustomizable<TExtendedEntity, TCustomFieldValue, TId> : IPersistable<TId>, ICustomizable
        where TId : IEquatable<TId>
        where TCustomFieldValue : ICustomFieldValue<TExtendedEntity, TId>
        where TExtendedEntity: IPersistable<TId>
    {
        IList<TCustomFieldValue> CustomFieldValues { get; set; }
    }

    public interface ICustomizable : IPersistable
    {
        IList<ICustomFieldValue> GetCustomFieldValues();
        void SetCustomFieldValues(IList<ICustomFieldValue> list);
    }
}
