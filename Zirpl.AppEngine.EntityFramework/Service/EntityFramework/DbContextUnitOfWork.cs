using System;
using System.Collections.Generic;
using System.Data.Entity.Validation;
using System.Transactions;
using Zirpl.AppEngine.DataService.EntityFramework;
using Zirpl.AppEngine.Logging;
using Zirpl.AppEngine.Validation;

namespace Zirpl.AppEngine.Service.EntityFramework
{
    public class DbContextUnitOfWork :IUnitOfWork
    {
        private DbContextBase dataContext;
        private TransactionScope transactionScope;

        public DbContextUnitOfWork(DbContextBase dataContext, TransactionScopeOption transactionScopeOption, IsolationLevel? isolationLevel = IsolationLevel.ReadCommitted, TimeSpan? timeout = null)
        {
            if (dataContext == null)
            {
                throw new ArgumentNullException("dataContext");
            }

            this.dataContext = dataContext;

            // NOTE: would like to use SnapShot as default, but ASP.Net membership appears to cause MSDTC promotion
            // so we need to make the server default ReadCommitted to Read_Committed_SnapShot
            // NOTE: take a look at: http://social.msdn.microsoft.com/Forums/en-US/linqtosql/thread/6ea9fbed-43a3-4961-b5c6-312480e56d25/
            // NOTE: http://www.jimmcleod.net/blog/index.php/2009/08/27/the-potential-dangers-of-the-read-committed-snapshot-isolation-level/

            if (isolationLevel.HasValue)
            {
                if (timeout.HasValue)
                {
                    this.transactionScope = new TransactionScope(transactionScopeOption,
                                                                 new TransactionOptions()
                                                                     {
                                                                         IsolationLevel = isolationLevel.Value,
                                                                         Timeout = timeout.Value
                                                                     });
                }
                else
                {
                    this.transactionScope = new TransactionScope(transactionScopeOption,
                                                                 new TransactionOptions()
                                                                 {
                                                                     IsolationLevel = isolationLevel.Value
                                                                 });
                }
            }
            else if (timeout.HasValue)
            {
                this.transactionScope = new TransactionScope(transactionScopeOption, new TransactionOptions() { IsolationLevel = IsolationLevel.ReadCommitted, Timeout = timeout.Value });
            }
            else
            {
                this.transactionScope = new TransactionScope(transactionScopeOption, new TransactionOptions() { IsolationLevel = IsolationLevel.ReadCommitted });
            }


            // NOTE: cannot open it these ways as they still seem to have MSDTC problems due to ASP.Net providers
            // Option 1:
            //if (this.dataContext.GetObjectContext().Connection.State == ConnectionState.Closed)
            //{
            //    this.dataContext.GetObjectContext().Connection.Open();
            //}
            // Option 2:
            //this.dataContext.GetObjectContext().Connection.Open();
        }

        public void Flush()
        {
            try
            {
                this.dataContext.SaveChanges();
            }
            catch (DbEntityValidationException e)
            {
                List<ValidationError> newValidationResults = new List<ValidationError>();
                foreach (var validationResult in e.EntityValidationErrors)
                {
                    foreach (var error in validationResult.ValidationErrors)
                    {
                        newValidationResults.Add(new EntityValidationError(error.PropertyName, error.ErrorMessage, validationResult.Entry.Entity));
                    }
                }

                var exception = new ValidationException("Could not save changes due to validation errors", newValidationResults, e);
                this.GetLog().TryDebug(exception);
                throw exception;
            }
        }

        public void Commit()
        {
            try
            {
                this.dataContext.SaveChanges();
                this.transactionScope.Complete();
            }
            catch (DbEntityValidationException e)
            {
                List<ValidationError> newValidationResults = new List<ValidationError>();
                foreach (var validationResult in e.EntityValidationErrors)
                {
                    foreach (var error in validationResult.ValidationErrors)
                    {
                        newValidationResults.Add(new EntityValidationError(error.PropertyName, error.ErrorMessage, validationResult.Entry.Entity));
                    }
                }

                var exception = new ValidationException("Could not save changes due to validation errors", newValidationResults, e);
                this.GetLog().TryDebug(exception);
                throw exception;
            }
            //finally
            //{
            //    try
            //    {
            //        this.dataContext.Database.Connection.Close();
            //    }
            //    catch (Exception e)
            //    {
            //        this.GetLog().TryError(e, "Error trying to close database");
            //    }
            //}
        }

        public void Dispose()
        {
            Dispose(true);
            GC.SuppressFinalize(this);
        }

        protected virtual void Dispose(bool disposing)
        {
            if (disposing)
            {
                if (transactionScope != null)
                {
                    this.transactionScope.Dispose();
                }
            }
        }
    }
}
