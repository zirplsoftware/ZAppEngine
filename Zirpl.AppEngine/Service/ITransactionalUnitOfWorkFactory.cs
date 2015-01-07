#if !SILVERLIGHT && !PORTABLE
using System.Transactions;

namespace Zirpl.AppEngine.Service
{
    public interface ITransactionalUnitOfWorkFactory :IUnitOfWorkFactory
    {
        IUnitOfWork Create(TransactionScopeOption transactionScopeOption);
        IUnitOfWork CreateRequired();
        IUnitOfWork CreateRequiresNew();
        IUnitOfWork CreateSuppress();
    }
}
#endif
