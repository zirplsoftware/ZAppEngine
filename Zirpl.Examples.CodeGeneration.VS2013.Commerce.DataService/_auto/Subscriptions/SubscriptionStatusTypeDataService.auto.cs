using System;
using System.Linq;
using Zirpl.AppEngine.DataService;
using Zirpl.AppEngine.DataService.EntityFramework;
using Zirpl.Examples.CodeGeneration.VS2013.Commerce.Model.Subscriptions;

namespace Zirpl.Examples.CodeGeneration.VS2013.Commerce.DataService.Subscriptions
{
    public partial class SubscriptionStatusTypeDataService : ReadOnlyDbContextDataServiceBase<CommerceDataContext, SubscriptionStatusType, byte>, ISubscriptionStatusTypeDataService
    {
    }
}
