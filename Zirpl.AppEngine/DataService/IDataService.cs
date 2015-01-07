using System;
using System.Runtime.InteropServices;
using Zirpl.AppEngine.Model;

namespace Zirpl.AppEngine.DataService
{
    public interface IDataService<TEntity, TId> : IDataService
        where TId : IEquatable<TId>
        where TEntity : IPersistable<TId>
    {
    }
    public interface IDataService
    {
    }
}
