using System;
using Zirpl.AppEngine.Ioc;
using Zirpl.AppEngine.Service;
using Zirpl.AppEngine.Testing;
using Zirpl.AppEngine.Web.Mvc.Ioc.Autofac;
using Zirpl.Examples.CodeGeneration.VS2013.Commerce.Model.Customers;

namespace Zirpl.Examples.CodeGeneration.VS2013.Commerce.Tests.Customers
{
    public partial class CustomerTagTestsStrategy
		 : IEntityLayerTestsStrategy<CustomerTag, int, CustomerTagEntityWrapper>
    {
        // NOTE: this auto-generated class will not build until the interface is fully implemented in a partial class file

        public IDependencyResolver DependencyResolver { get { return IocUtils.DependencyResolver; } }
        public ITransactionalUnitOfWorkFactory UnitOfWorkFactory { get { return this.DependencyResolver.Resolve<ITransactionalUnitOfWorkFactory>(); } }
        public DataProvider Data { get { return this.DependencyResolver.Resolve<DataProvider>(); } }
		
        // NOTE: this auto-generated class will not build until the interface is fully implemented in a partial class file
    }
}
