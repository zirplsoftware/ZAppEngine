using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Zirpl.AppEngine.Model.Customization
{
    public interface ICustomFieldDefinitionType<TId, TEnum> : IStaticLookup<TId, TEnum>, ICustomFieldDefinitionType
        where TId : IEquatable<TId>
        where TEnum : struct
    {
    }

    public interface ICustomFieldDefinitionType :IStaticLookup
    {

    }
}
