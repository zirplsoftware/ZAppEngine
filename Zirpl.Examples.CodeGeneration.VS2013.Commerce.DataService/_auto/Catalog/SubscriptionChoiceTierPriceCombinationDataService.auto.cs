using System;
using System.Linq;
using Zirpl.AppEngine.DataService;
using Zirpl.AppEngine.DataService.EntityFramework;
using Zirpl.Examples.CodeGeneration.VS2013.Commerce.Model.Catalog;

namespace Zirpl.Examples.CodeGeneration.VS2013.Commerce.DataService.Catalog
{
    public partial class SubscriptionChoiceTierPriceCombinationDataService : DbContextDataServiceBase<CommerceDataContext, SubscriptionChoiceTierPriceCombination, int>, ISubscriptionChoiceTierPriceCombinationDataService
    {
    }
}
