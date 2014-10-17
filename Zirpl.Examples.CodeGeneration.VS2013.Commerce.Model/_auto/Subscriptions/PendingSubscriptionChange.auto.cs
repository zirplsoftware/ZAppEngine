using System;
using System.Collections;
using System.Collections.Generic;
using System.Linq;
using Zirpl.AppEngine.Model;
using Zirpl.AppEngine.Model.Customization;

namespace Zirpl.Examples.CodeGeneration.VS2013.Commerce.Model.Subscriptions
{
    public partial class PendingSubscriptionChange  : AuditableBase<int>
    {
		public virtual int Quantity { get; set; }
		public virtual Zirpl.Examples.CodeGeneration.VS2013.Commerce.Model.Catalog.SubscriptionChoice SubscriptionChoice { get; set; }
		public virtual int SubscriptionChoiceId { get; set; }
    }
}

