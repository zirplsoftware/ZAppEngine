using System;

namespace Zirpl.AppEngine.Model.Extensibility
{
    public interface ICustomFieldDefinitionType<TId> : IPersistable<TId>, ICustomFieldDefinitionType
        where TId : IEquatable<TId>
    {
    }

    public interface ICustomFieldDefinitionType :IStaticLookup
    {

    }
}
