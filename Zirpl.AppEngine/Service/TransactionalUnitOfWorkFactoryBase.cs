#if !SILVERLIGHT
using System.Transactions;

namespace Zirpl.AppEngine.Service
{
    public abstract class TransactionalUnitOfWorkFactoryBase : UnitOfWorkFactoryBase, ITransactionalUnitOfWorkFactory
    {
        public abstract IUnitOfWork Create(TransactionScopeOption transactionScopeOption);

        /// <summary>
        /// Creates a new IUnitOfWork with TransactionScopeOption.Required
        /// </summary>
        /// <returns></returns>
        public override IUnitOfWork Create()
        {
            return this.CreateRequired();
        }

        public virtual IUnitOfWork CreateRequired()
        {
            return this.Create(TransactionScopeOption.Required);
        }

        public virtual IUnitOfWork CreateRequiresNew()
        {
            return this.Create(TransactionScopeOption.RequiresNew);
        }

        public virtual IUnitOfWork CreateSuppress()
        {
            return this.Create(TransactionScopeOption.Suppress);
        }
    }
}
#endif