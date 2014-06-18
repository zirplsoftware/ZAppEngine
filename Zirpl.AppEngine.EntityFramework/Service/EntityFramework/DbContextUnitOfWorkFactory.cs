using System.Transactions;
using Zirpl.AppEngine.DataService.EntityFramework;

namespace Zirpl.AppEngine.Service.EntityFramework
{
    public class DbContextUnitOfWorkFactory :TransactionalUnitOfWorkFactoryBase
    {
        public DbContextBase DataContext { get; set; }
        
        public override IUnitOfWork Create(TransactionScopeOption transactionScopeOption)
        {
            return new DbContextUnitOfWork(this.DataContext, transactionScopeOption);
        }
    }
}
