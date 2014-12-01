using System;
using System.Data.Entity;
using System.Data.Entity.ModelConfiguration;
using System.Linq.Expressions;
using Zirpl.AppEngine.Model;

namespace Zirpl.AppEngine.DataService.EntityFramework.Mapping
{
    public abstract class EntityMappingBase<TEntity, TId> : EntityTypeConfiguration<TEntity>, IEntityMapping
        where TEntity : class, IPersistable<TId>
        where TId : IEquatable<TId>
    {
        protected EntityMappingBase()
        {
            this.ToTable(this.GetTableName());
            //this.HasKey(this.GetKeyExpression());

            this.MapProperties();

            // ignore IsPersisted
            this.Ignore(entity => entity.IsPersisted);
        }

        protected virtual void MapProperties()
        {
            if (this.MapEntityBaseProperties)
            {
                Type type = typeof(TEntity);
                if (typeof(IAuditable).IsAssignableFrom(type))
                {
                    this.Property(s => ((IAuditable)s).CreatedDate).IsRequired().IsDateTime();
                    this.Property(s => ((IAuditable)s).CreatedUserId).IsRequired();
                    this.Property(s => ((IAuditable)s).UpdatedDate).IsRequired().IsDateTime();
                    this.Property(s => ((IAuditable)s).UpdatedUserId).IsRequired();
                }
                if (typeof(IVersionable).IsAssignableFrom(type))
                {
                    this.Property(s => ((IVersionable)s).RowVersion).IsRequired().IsRowVersion();
                }
                // TODO: map IExtendable
            }
        }

        protected virtual String GetTableName()
        {
            return typeof(TEntity).Name;
        }

        protected virtual Expression<Func<TEntity, TId>> GetKeyExpression()
        {
            return o => o.Id;
        }

        public void OnModelCreating(DbModelBuilder modelBuilder)
        {
            modelBuilder.Configurations.Add(this);
        }

        protected virtual bool MapEntityBaseProperties
        {
            get { return true; }
        }
    }
}
