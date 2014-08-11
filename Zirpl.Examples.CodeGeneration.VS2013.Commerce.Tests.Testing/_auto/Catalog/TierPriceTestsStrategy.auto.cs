using System;
using Zirpl.AppEngine.Ioc;
using Zirpl.AppEngine.Service;
using Zirpl.AppEngine.Testing;
using Zirpl.AppEngine.Web.Mvc.Ioc.Autofac;
using Zirpl.Examples.CodeGeneration.VS2013.Commerce.Model.Catalog;

namespace Zirpl.Examples.CodeGeneration.VS2013.Commerce.Tests.Catalog
{
    public partial class TierPriceTestsStrategy
		 : IEntityLayerTestsStrategy<TierPrice, int, TierPriceEntityWrapper>
    {
        // NOTE: this auto-generated class will not build until the interface is fully implemented in a partial class file

        public IDependencyResolver DependencyResolver { get { return IocUtils.DependencyResolver; } }
        public ITransactionalUnitOfWorkFactory UnitOfWorkFactory { get { return this.DependencyResolver.Resolve<ITransactionalUnitOfWorkFactory>(); } }
        public DataProvider Data { get { return this.DependencyResolver.Resolve<DataProvider>(); } }
		
        // NOTE: this auto-generated class will not build until the interface is fully implemented in a partial class file
    }
}
