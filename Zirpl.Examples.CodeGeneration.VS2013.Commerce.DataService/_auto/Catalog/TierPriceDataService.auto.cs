using System;
using System.Linq;
using Zirpl.AppEngine.DataService;
using Zirpl.AppEngine.DataService.EntityFramework;
using Zirpl.Examples.CodeGeneration.VS2013.Commerce.Model.Catalog;

namespace Zirpl.Examples.CodeGeneration.VS2013.Commerce.DataService.Catalog
{
    public partial class TierPriceDataService : DbContextDataServiceBase<CommerceDataContext, TierPrice, int>, ITierPriceDataService
    {
		protected override IQueryable<TierPrice> ApplyDefaultSort(IQueryable<TierPrice> query)
        {
            return from o in query
                   orderby o.Quantity
                   select o;
        }
    }
}
