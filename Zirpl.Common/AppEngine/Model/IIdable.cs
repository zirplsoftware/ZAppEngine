
using System;

namespace Zirpl.AppEngine.Model
{
    public interface IIdable<TId> : IIdable
    {
         TId Id { get; }
    }

    public interface IIdable
    {
        Object GetId();
    }
}
