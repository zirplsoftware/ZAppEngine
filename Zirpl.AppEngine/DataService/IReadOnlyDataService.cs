using System;
using Zirpl.AppEngine.Model;
using Zirpl.AppEngine.Service;

namespace Zirpl.AppEngine.DataService
{
    public interface IReadOnlyDataService<TEntity, TId> :
        IDataService<TEntity, TId>,
        ISupports,
        ISupportsGetById<TEntity, TId>,
        ISupportsGetAll<TEntity>,
        ISupportsExists<TId>,
        ISupportsGetTotalCount,
        ISupportsSearch<TEntity>,
        ISupportsSearchUnique<TEntity>,
        ISupportsQueryable<TEntity>
        where TEntity : class, IPersistable<TId>
        where TId : IEquatable<TId>
    {
    }
}
