using System;
using System.Collections.Generic;
using Zirpl.AppEngine.Model;
using Zirpl.AppEngine.Model.Search;

namespace Zirpl.AppEngine.DataService.EntityFramework
{
    public abstract class ReadOnlyDbContextDataServiceBase<TContext, TEntity, TId> : DbContextDataServiceBase<TContext, TEntity, TId>, IReadOnlyDataService<TEntity, TId>
        where TEntity : class, IPersistable<TId>
        where TContext : DbContextBase
        where TId : IEquatable<TId>
    {
        public override TEntity Create()
        {
            throw new InvalidOperationException();
        }
        public override TDerivedEntity Create<TDerivedEntity>()
        {
            throw new InvalidOperationException();
        }
        public override void Insert(IEnumerable<TEntity> entities)
        {
            throw new InvalidOperationException();
        }
        public override void Insert(TEntity entity)
        {
            throw new InvalidOperationException();
        }
        public override void Delete(IEnumerable<TEntity> entities)
        {
            throw new InvalidOperationException();
        }
        public override void Delete(ISearchCriteria searchCriteria)
        {
            throw new InvalidOperationException();
        }
        public override void Delete(TEntity entity)
        {
            throw new InvalidOperationException();
        }
        public override void DeleteById(IEnumerable<TId> ids)
        {
            throw new InvalidOperationException();
        }
        public override void DeleteById(TId id)
        {
            throw new InvalidOperationException();
        }
        public override void Update(IEnumerable<TEntity> entities)
        {
            throw new InvalidOperationException();
        }
        public override void Update(TEntity entity)
        {
            throw new InvalidOperationException();
        }
        public override void Save(TEntity entity)
        {
            throw new InvalidOperationException();
        }
        public override void Save(IEnumerable<TEntity> entities)
        {
            throw new InvalidOperationException();
        }
    }
}
