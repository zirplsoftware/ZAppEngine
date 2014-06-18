using System;
using System.Collections.Generic;
using System.Data.Entity;
using System.Linq;
using System.Transactions;

namespace Zirpl.AppEngine.DataService.EntityFramework
{
    public abstract class TableBasedSequenceValueProviderBase :ISequenceValueProvider
    {
        public DbContext DataContext { get; set; }

        //protected abstract String SchemaName { get; }
        protected abstract String SequenceName { get; }

        public long GetNextValue()
        {
            long nextValue = 0;
            using (TransactionScope transaction = new TransactionScope(TransactionScopeOption.Suppress))
            {
                IEnumerable<long> returnValues = this.DataContext.Database.SqlQuery<long>(
                    String.Format("EXEC dbo.usp_GetNext{0}", this.SequenceName));
                nextValue = returnValues.First();

                transaction.Complete();
            }
            return nextValue;
        }

        public long GetCurrentValue()
        {
            long currentValue = 0;
            using (TransactionScope transaction = new TransactionScope(TransactionScopeOption.Suppress))
            {
                IEnumerable<long> returnValues = this.DataContext.Database.SqlQuery<long>(
                    String.Format("EXEC dbo.usp_GetCurrent{0}", this.SequenceName));
                currentValue = returnValues.First();

                transaction.Complete();
            }
            return currentValue;
        }
    }
}
