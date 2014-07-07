using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using Zirpl.AppEngine.Model;

namespace Zirpl.AppEngine.DataService.EntityFramework.Mapping
{
    public abstract partial class CoreEntityMappingBase<TEntity, TId> : EntityMappingBase<TEntity, TId>
        where TEntity : AuditableBase<TId>
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
                bool continueLoop = true;
                do
                {
                    if (type.Equals(typeof(AuditableBase<TId>)))
                    {
                        this.Property(s => s.CreatedDate).IsRequired().IsDateTime();
                        this.Property(s => s.CreatedUserId).IsRequired();
                        this.Property(s => s.UpdatedDate).IsRequired().IsDateTime();
                        this.Property(s => s.UpdatedUserId).IsRequired();
                        this.Property(s => s.RowVersion).IsRequired().IsRowVersion();
                        continueLoop = false;
                    }
                    else
                    {
                        type = type.BaseType;
                    }
                } while (continueLoop);
            }


            base.MapProperties();
        }
    }
}
