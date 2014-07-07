using System;
using System.Collections;
using System.Collections.Generic;
using Zirpl.AppEngine.Model;

namespace Zirpl.Examples.CodeGeneration.VS2013.Commerce.Model.Catalog
{
    public partial class SubscriptionChoice : AuditableBase<int>
    {
		public virtual Zirpl.Examples.CodeGeneration.VS2013.Commerce.Model.Catalog.DisplayProduct DisplayProduct { get; set; }
		public virtual int DisplayProductId { get; set; }
		public virtual Zirpl.Examples.CodeGeneration.VS2013.Commerce.Model.Subscriptions.SubscriptionPeriod SubscriptionPeriod { get; set; }
		public virtual int SubscriptionPeriodId { get; set; }
		public virtual decimal PriceEach { get; set; }
    }
}

