using System;
using Zirpl.AppEngine.Service;
using Zirpl.Examples.CodeGeneration.VS2013.Commerce.Model.Subscriptions;

namespace Zirpl.Examples.CodeGeneration.VS2013.Commerce.Service.Subscriptions
{
    public partial interface ISubscriptionPeriodTypeService  : IDictionaryEntityService<SubscriptionPeriodType, byte, SubscriptionPeriodTypeEnum>
    {
    }
}
