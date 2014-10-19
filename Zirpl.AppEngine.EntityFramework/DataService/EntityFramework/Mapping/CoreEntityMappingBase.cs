using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using Zirpl.AppEngine.Model;

namespace Zirpl.AppEngine.DataService.EntityFramework.Mapping
{
    public abstract partial class CoreEntityMappingBase<TEntity, TId> : EntityMappingBase<TEntity, TId>
        where TEntity : class, IPersistable<TId>
        where TId : IEquatable<TId>
    {
        protected virtual bool MapCoreEntityBaseProperties
        {
            get { return true; }
        }

        protected override void MapProperties()
        {
            if (this.MapCoreEntityBaseProperties)
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
            }

            base.MapProperties();
        }
    }
}
