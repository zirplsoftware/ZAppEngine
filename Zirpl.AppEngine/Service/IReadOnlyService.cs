using System;
using Zirpl.AppEngine.Model;

namespace Zirpl.AppEngine.Service
{
    public interface IReadOnlyService<TEntity, TId> :
        IService,
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
