using System;
using System.Collections;
using System.Collections.Generic;
using System.Linq;
using Zirpl.AppEngine.Model;
using Zirpl.AppEngine.Model.Customization;

namespace Zirpl.Examples.CodeGeneration.VS2013.Commerce.Model.Subscriptions
{
    public partial class SubscriptionInstance  : AuditableBase<int>
    {
		public virtual DateTime StartDate { get; set; }
		public virtual int TotalShipments { get; set; }
		public virtual int ShipmentsRemaining { get; set; }
		public virtual Zirpl.Examples.CodeGeneration.VS2013.Commerce.Model.Subscriptions.Subscription Subscription { get; set; }
		public virtual int SubscriptionId { get; set; }
		public virtual Zirpl.Examples.CodeGeneration.VS2013.Commerce.Model.Orders.SubscriptionOrderItem CreatedByOrderItem { get; set; }
		public virtual Zirpl.Examples.CodeGeneration.VS2013.Commerce.Model.Subscriptions.PendingSubscriptionChange PendingSubscriptionChange { get; set; }
		public virtual int? PendingSubscriptionChangeId { get; set; }
    }
}

