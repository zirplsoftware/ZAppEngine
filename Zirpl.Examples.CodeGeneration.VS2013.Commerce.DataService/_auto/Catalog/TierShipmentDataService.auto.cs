using System;
using System.Linq;
using Zirpl.AppEngine.DataService;
using Zirpl.AppEngine.DataService.EntityFramework;
using Zirpl.Examples.CodeGeneration.VS2013.Commerce.Model.Catalog;

namespace Zirpl.Examples.CodeGeneration.VS2013.Commerce.DataService.Catalog
{
    public partial class TierShipmentDataService : DbContextDataServiceBase<CommerceDataContext, TierShipment, int>, ITierShipmentDataService
    {
		protected override IQueryable<TierShipment> ApplyDefaultSort(IQueryable<TierShipment> query)
        {
            return from o in query
                   orderby o.Quantity
                   select o;
        }
    }
}
