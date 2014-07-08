using System;
using Zirpl.AppEngine.Service;
using Zirpl.AppEngine.Service.EntityFramework;
using Zirpl.Examples.CodeGeneration.VS2013.Commerce.DataService.Subscriptions;
using Zirpl.Examples.CodeGeneration.VS2013.Commerce.DataService;
using Zirpl.Examples.CodeGeneration.VS2013.Commerce.Model.Subscriptions;

namespace Zirpl.Examples.CodeGeneration.VS2013.Commerce.Service.Subscriptions
{
    public partial class SubscriptionInstanceService  : DbContextServiceBase<CommerceDataContext, SubscriptionInstance, int>, ISubscriptionInstanceService
    {
    }
}
