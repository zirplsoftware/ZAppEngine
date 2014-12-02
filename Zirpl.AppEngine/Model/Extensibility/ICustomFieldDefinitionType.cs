using System;

namespace Zirpl.AppEngine.Model.Extensibility
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
