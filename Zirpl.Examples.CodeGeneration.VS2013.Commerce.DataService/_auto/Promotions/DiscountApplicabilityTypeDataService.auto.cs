using System;
using System.Linq;
using Zirpl.AppEngine.DataService;
using Zirpl.AppEngine.DataService.EntityFramework;
using Zirpl.Examples.CodeGeneration.VS2013.Commerce.Model.Promotions;

namespace Zirpl.Examples.CodeGeneration.VS2013.Commerce.DataService.Promotions
{
    public partial class DiscountApplicabilityTypeDataService : ReadOnlyDbContextDataServiceBase<CommerceDataContext, DiscountApplicabilityType, byte>, IDiscountApplicabilityTypeDataService
    {
    }
}
