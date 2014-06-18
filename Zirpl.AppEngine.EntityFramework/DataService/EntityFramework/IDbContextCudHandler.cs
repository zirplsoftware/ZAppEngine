using System;
using System.Data.Entity;
using Zirpl.AppEngine.Model;

namespace Zirpl.AppEngine.DataService.EntityFramework
{
    public interface IDbContextCudHandler
    {
        void MarkInserted<TContext, TEntity, TId>(TContext context, TEntity entity)
            where TEntity : class, IPersistable<TId>
            where TId : IEquatable<TId>
            where TContext : DbContext;
        void MarkUpdated<TContext, TEntity, TId>(TContext context, TEntity entity)
            where TEntity : class, IPersistable<TId>
            where TId : IEquatable<TId>
            where TContext : DbContext;
        void MarkDeleted<TContext, TEntity, TId>(TContext context, TEntity entity)
            where TEntity : class, IPersistable<TId>
            where TId : IEquatable<TId>
            where TContext : DbContext;
    }
}
