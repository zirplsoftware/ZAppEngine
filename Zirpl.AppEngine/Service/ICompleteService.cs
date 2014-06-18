using System;
using Zirpl.AppEngine.Model;

namespace Zirpl.AppEngine.Service
{
    public interface ICompleteService<TEntity, TId> :
        IService<TEntity, TId>, 
        ISupportsDeleteById<TId>,
        ISupportsDeleteList<TEntity>,
        ISupportsDeleteListByIds<TId>,
        ISupportsGetById<TEntity, TId>,
        ISupportsGetAll<TEntity>,
        ISupportsInsertList<TEntity>,
        ISupportsUpdateList<TEntity>,
        ISupportsExists<TId>,
        ISupportsGetTotalCount,
        ISupportsSave<TEntity>,
        ISupportsSaveList<TEntity>,
        ISupportsSearch<TEntity>,
        ISupportsSearchUnique<TEntity>,
        ISupportsDeleteSearch,
        ISupportsRiaServiceActions<TEntity>,
        ISupportsCreate<TEntity>,
        ISupportsReload<TEntity>
        where TEntity : class, IPersistable<TId>
        where TId : IEquatable<TId>
    {
    }
}
