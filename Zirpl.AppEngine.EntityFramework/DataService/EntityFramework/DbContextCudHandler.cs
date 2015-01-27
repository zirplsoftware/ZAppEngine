using System;
using System.Data.Entity;
using System.Linq;
using Zirpl.AppEngine.Model;

namespace Zirpl.AppEngine.DataService.EntityFramework
{
    public class DbContextCudHandler : IDbContextCudHandler
    {
        public void MarkInserted<TContext, TEntity, TId>(TContext context, TEntity entity)
            where TEntity : class, IPersistable<TId>
            where TId : IEquatable<TId>
            where TContext : DbContext
        {
            var entry = context.Entry(entity);
            if (entry.State == EntityState.Detached)
            {
                context.Set<TEntity>().Attach(entity);
            }
            entry.State = EntityState.Added;
        }

        public void MarkUpdated<TContext, TEntity, TId>(TContext context, TEntity entity)
            where TEntity : class, IPersistable<TId>
            where TId : IEquatable<TId>
            where TContext : DbContext
        {
            if (!context.Set<TEntity>().Local.Any(o => o.Id.Equals(entity.Id)))
            {
                // is already attached
                //
                var entry = context.Entry(entity);
                if (entry.State == EntityState.Detached)
                {
                    context.Set<TEntity>().Attach(entity);
                    entry.State = EntityState.Modified;
                }
            }
        }

        public void MarkDeleted<TContext, TEntity, TId>(TContext context, TEntity entity)
            where TEntity : class, IPersistable<TId>
            where TId : IEquatable<TId>
            where TContext : DbContext
        {
            var entityDeletable = entity as IMarkDeletable;
            if (entityDeletable != null)
            {
                entityDeletable.IsMarkedDeleted = true;
                this.MarkUpdated<TContext, TEntity, TId>(context, entity);
            }
            else
            {
                var entry = context.Entry(entity);
                if (entry.State == EntityState.Detached)
                {
                    context.Set<TEntity>().Attach(entity);
                }
                entry.State = EntityState.Deleted;
            }
        }
    }
}
