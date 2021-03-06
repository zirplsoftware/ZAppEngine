﻿using System;
using System.Collections.Generic;
using System.Data.Entity;
using System.Data.Entity.Core.Objects;
using System.Data.Entity.Infrastructure;
using System.Data.Entity.Validation;
using Zirpl.AppEngine.Model;
using Zirpl.AppEngine.Session;
using Zirpl.AppEngine.Validation;

namespace Zirpl.AppEngine.DataService.EntityFramework
{
    public abstract class DbContextBase :DbContext
    {
        public IValidationHelper ValidationHelper { get; set; }
        public ICurrentUserKeyProvider CurrentUserKeyProvider { get; set; }
        public IRetryPolicyFactory RetryPolicyFactory { get; set; }
        
        //protected override void OnModelCreating(DbModelBuilder modelBuilder)
        //{
        //    base.OnModelCreating(modelBuilder);

        //    this.GetObjectContext().Connection.Open();
        //}

        protected abstract bool IsModifiable(Object obj);
    
        public override int SaveChanges()
        {
            // make sure we aren't trying to work with any objects that shouldn't be persisted through EF
            //
            var objStateEntries = this.GetObjectContext().ObjectStateManager.GetObjectStateEntries(
                EntityState.Added | EntityState.Modified | EntityState.Deleted);
            foreach (ObjectStateEntry entry in objStateEntries)
            {
                if (!this.IsModifiable(entry.Entity))
                {
                    throw new Exception(String.Format("Cannot persist entity type directly through DbContext: {0}", entry.Entity.GetType()));
                }
            }

            // call OnSaveChanges to set the automatic properties
            //
            objStateEntries = this.GetObjectContext().ObjectStateManager.GetObjectStateEntries(
               EntityState.Added | EntityState.Modified);

            foreach (ObjectStateEntry entry in objStateEntries)
            {
                this.OnSaveChanges(entry);
            }

            // Retry Save if specified
            //
            return this.RetryPolicyFactory != null
                ? this.RetryPolicyFactory.CreateRetryPolicy().ExecuteAction<int>(base.SaveChanges)
                : base.SaveChanges();
        }

        protected virtual void OnSaveChanges(ObjectStateEntry entry)
        {
            DateTime now = DateTime.Now;
            var auditable = entry.Entity as IAuditable;
            if (auditable != null)
            {
                auditable.UpdatedDate = now;
                var id = this.CurrentUserKeyProvider.GetCurrentUserKey();
                String idAsString = id == null ? null : id.ToString();
                auditable.UpdatedUserId = idAsString;
                if (entry.State == EntityState.Added)
                {
                    auditable.CreatedDate = now;
                    auditable.CreatedUserId = idAsString;
                }
            }

            // this block rejects ALL changes to properties where the old and new values are the same
            // as otherwise SQL could be run that attempts to update a column to the same value.
            // This will fail in cases where Update has been denied on that column
            //
            if (entry.State == EntityState.Modified)
            {
                foreach (var propertyName in entry.GetModifiedProperties())
                {
                    if (Object.Equals(entry.OriginalValues[propertyName], entry.CurrentValues[propertyName]))
                    {
                        entry.RejectPropertyChanges(propertyName);
                    }
                }
            }
        }


        protected override DbEntityValidationResult ValidateEntity(DbEntityEntry entityEntry, IDictionary<object, object> items)
        {
            List<DbValidationError> errors = new List<DbValidationError>();

            //if (entityEntry.State == EntityState.Added
            //    || entityEntry.State == EntityState.Modified)
            //{
                if (this.ValidationHelper != null)
                {
                    if (this.ValidationHelper.IsValidatable(entityEntry))
                    {
                        foreach (var error in this.ValidationHelper.Validate(entityEntry))
                        {
                            errors.Add(new DbValidationError(error.PropertyName, error.ErrorMessage));
                        }
                    }
                }
            //}
            errors.AddRange(base.ValidateEntity(entityEntry, items).ValidationErrors);

            return new DbEntityValidationResult(entityEntry, errors);
        }

        public ObjectContext GetObjectContext()
        {
            return ((IObjectContextAdapter) this).ObjectContext;
        }
    }
}
