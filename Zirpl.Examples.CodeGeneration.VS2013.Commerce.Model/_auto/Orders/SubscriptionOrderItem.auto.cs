using System;
using System.Collections;
using System.Collections.Generic;
using Zirpl.AppEngine.Model;

namespace Zirpl.Examples.CodeGeneration.VS2013.Commerce.Model.Orders
{
    public partial class SubscriptionOrderItem : Zirpl.Examples.CodeGeneration.VS2013.Commerce.Model.Orders.OrderItem
    {
		public virtual Zirpl.Examples.CodeGeneration.VS2013.Commerce.Model.Orders.SubscriptionOrderItemType SubscriptionOrderItemType { get; set; }
		public virtual byte SubscriptionOrderItemTypeId { get; set; }
		public virtual Zirpl.Examples.CodeGeneration.VS2013.Commerce.Model.Subscriptions.SubscriptionPeriod SubscriptionPeriod { get; set; }
		public virtual int? SubscriptionPeriodId { get; set; }
		public virtual Zirpl.Examples.CodeGeneration.VS2013.Commerce.Model.Subscriptions.SubscriptionInstance TriggeredBySubscriptionInstance { get; set; }
		public virtual int? TriggeredBySubscriptionInstanceId { get; set; }
		public virtual Zirpl.Examples.CodeGeneration.VS2013.Commerce.Model.Subscriptions.SubscriptionInstance ResultingSubscriptionInstance { get; set; }
    }
}

