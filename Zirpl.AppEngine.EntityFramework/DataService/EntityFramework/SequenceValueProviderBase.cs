using System;
using System.Collections.Generic;
using System.Data.Entity;
using System.Linq;
using System.Transactions;

namespace Zirpl.AppEngine.DataService.EntityFramework
{
    public abstract class SequenceValueProviderBase :ISequenceValueProvider
    {
        public DbContext DataContext { get; set; }

        protected abstract String SchemaName { get; }
        protected abstract String SequenceName { get; }

        public long GetNextValue()
        {
            long nextValue = 0;
            using (TransactionScope transaction = new TransactionScope(TransactionScopeOption.Suppress))
            {
                IEnumerable<long> returnValues = this.DataContext.Database.SqlQuery<long>(
                    String.Format("SELECT NEXT VALUE FOR {0}.{1}", this.SchemaName, this.SequenceName));
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
                    String.Format("SELECT seq.CURRENT_VALUE FROM sys.sequences seq INNER JOIN sys.schemas sch on sch.schema_id = seq.schema_id WHERE seq.name = '{1}' AND sch.name = '{0}'", this.SchemaName, this.SequenceName));
                currentValue = returnValues.First();

                transaction.Complete();
            }
            return currentValue;
        }
    }
}
