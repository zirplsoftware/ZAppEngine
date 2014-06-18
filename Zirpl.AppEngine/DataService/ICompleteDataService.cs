using System;
using Zirpl.AppEngine.Model;
using Zirpl.AppEngine.Service;

namespace Zirpl.AppEngine.DataService
{
    public interface ICompleteDataService<TEntity, TId> :
        IDataService<TEntity, TId>,
        ISupports,
        ISupportsDelete<TEntity>, 
        ISupportsDeleteById<TId>,
        ISupportsDeleteList<TEntity>,
        ISupportsDeleteListByIds<TId>,
        ISupportsGetById<TEntity, TId>,
        ISupportsGetAll<TEntity>,
        ISupportsInsert<TEntity>,
        ISupportsInsertList<TEntity>,
        ISupportsUpdate<TEntity>,
        ISupportsUpdateList<TEntity>,
        ISupportsExists<TId>,
        ISupportsGetTotalCount,
        ISupportsSave<TEntity>,
        ISupportsSaveList<TEntity>,
        ISupportsSearch<TEntity>,
        ISupportsSearchUnique<TEntity>,
        ISupportsDeleteSearch,
        ISupportsQueryable<TEntity>,
        ISupportsReload<TEntity>,
        ISupportsCreate<TEntity> 
        where TEntity : class, IPersistable<TId>
        where TId : IEquatable<TId>
    {
    }
}
