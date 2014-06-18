using System;
using System.Collections.Generic;
using System.Data.Entity;
using Zirpl.AppEngine.Model;

namespace Zirpl.AppEngine.Service.EntityFramework
{
    public abstract class ReadOnlyDbContextServiceBase<TContext, TEntity, TId> : DbContextServiceBase<TContext, TEntity, TId>, IReadOnlyService<TEntity, TId>
        where TEntity : class, IPersistable<TId>
        where TId : IEquatable<TId>
        where TContext : DbContext
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
    }
}
