
using System;

namespace Zirpl.AppEngine.Model
{
    public interface IKeyedEntity<TId> : IKeyedEntity
    {
         TId Id { get; }
    }

    public interface IKeyedEntity
    {
        Object GetId();
    }
}
