using System;
using System.Collections;
using System.Collections.Generic;
using System.Linq;
using Zirpl.AppEngine.Model;
using Zirpl.AppEngine.Model.Customization;

namespace Zirpl.Examples.CodeGeneration.VS2013.Commerce.Model.Subscriptions
{
    public partial class SubscriptionPeriod  : AuditableBase<int>
    {
		public virtual int ChargePeriod { get; set; }
		public virtual Zirpl.Examples.CodeGeneration.VS2013.Commerce.Model.Subscriptions.SubscriptionPeriodType ChargePeriodType { get; set; }
		public virtual byte ChargePeriodTypeId { get; set; }
		public virtual int ShipmentPeriod { get; set; }
		public virtual Zirpl.Examples.CodeGeneration.VS2013.Commerce.Model.Subscriptions.SubscriptionPeriodType ShipmentPeriodType { get; set; }
		public virtual byte ShipmentPeriodTypeId { get; set; }
    }
}

