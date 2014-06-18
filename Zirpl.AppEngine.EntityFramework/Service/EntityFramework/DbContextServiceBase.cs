using System;
using System.Collections.Generic;
using System.Data.Entity;
using Zirpl.AppEngine.DataService.EntityFramework;
using Zirpl.AppEngine.Model;
using Zirpl.AppEngine.Validation;

namespace Zirpl.AppEngine.Service.EntityFramework
{
    public abstract class DbContextServiceBase<TContext, TEntity, TId> : AbstractedSupportsImplService<TEntity, TId>
        where TEntity : class, IPersistable<TId>
        where TId : IEquatable<TId>
        where TContext : DbContext
    {
        public TContext DataContext { get; set; }
        public ITransactionalUnitOfWorkFactory UnitOfWorkFactory { get; set; }
        public IDbContextCudHandler DataContextCudHandler { get; set; }

        protected override void Validate(ServiceAction serviceAction, TEntity entity)
        {
            this.AssertValidation();
        }

        private void AssertValidation()
        {
            List<ValidationError> newValidationResults = new List<ValidationError>();
            foreach (var validationResult in this.DataContext.GetValidationErrors())
            {
                foreach (var error in validationResult.ValidationErrors)
                {
                    if (validationResult.Entry.State == EntityState.Added
                        && (error.PropertyName == AuditableBase<int>.CREATED_DATE_UTC_PROPERTY_NAME
                        || error.PropertyName == AuditableBase<int>.CREATED_USER_ID_PROPERTY_NAME
                        || error.PropertyName == AuditableBase<int>.UPDATED_DATE_UTC_PROPERTY_NAME
                        || error.PropertyName == AuditableBase<int>.UPDATED_USER_ID_PROPERTY_NAME))
                    {
                        // ignore it- we don't care about these errors during service-level validation
                    }
                    else
                    {
                        newValidationResults.Add(new EntityValidationError(error.PropertyName, error.ErrorMessage, validationResult.Entry.Entity));
                    }
                }
            }

            if (newValidationResults.Count > 0)
            {
                throw new ValidationException("Could not process service action due to validation errors", newValidationResults);
            }
        }

        protected override void OnPostCreate(TEntity entity)
        {
            this.DataContextCudHandler.MarkInserted<TContext, TEntity, TId>(this.DataContext, entity);

            base.OnPostCreate(entity);
        }

        protected override void OnPreInsertPreValidate(TEntity entity)
        {
            // this is necessary because it is the only way to ensure this entity gets validated
            // when AssertValidation is called
            this.DataContextCudHandler.MarkInserted<TContext, TEntity, TId>(this.DataContext, entity);

            base.OnPreInsertPreValidate(entity);
        }

        protected override void OnPreDelete(TEntity entity)
        {
            // this is necessary because it is the only way to ensure this entity gets validated
            // when AssertValidation is called
            this.DataContextCudHandler.MarkDeleted<TContext, TEntity, TId>(this.DataContext, entity);

            base.OnPreDelete(entity);
        }

        protected override void OnPreUpdatePreValidate(TEntity entity)
        {
            // this is necessary because it is the only way to ensure this entity gets validated
            // when AssertValidation is called
            this.DataContextCudHandler.MarkUpdated<TContext, TEntity, TId>(this.DataContext, entity);

            base.OnPreUpdatePreValidate(entity);
        }
    }
}
