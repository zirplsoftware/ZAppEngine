using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Zirpl.Examples.CodeGeneration.VS2013.Commerce.Model.Subscriptions
{
    public interface ISubscriptionPeriodFields
    {
        int ChargePeriod { get; set; }
        SubscriptionPeriodType ChargePeriodType { get; set; }

        int ShipmentPeriod { get; set; }
        SubscriptionPeriodType ShipmentPeriodType { get; set; }
    }
}
