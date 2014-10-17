using System;
using System.Collections;
using System.Collections.Generic;
using System.Linq;
using Zirpl.AppEngine.Model;
using Zirpl.AppEngine.Model.Customization;
using Zirpl.Examples.CodeGeneration.VS2013.Commerce.Model.Customization.Subscriptions;

namespace Zirpl.Examples.CodeGeneration.VS2013.Commerce.Model.Subscriptions
{
    public partial class Subscription  : AuditableBase<int>, ICustomizable<Subscription, SubscriptionCustomFieldValue, int>
    {
		public Subscription()
		{
			this.CustomFieldValues = this.CustomFieldValues ?? new List<SubscriptionCustomFieldValue>();
		}
		
		public virtual DateTime StartDate { get; set; }
		public virtual Zirpl.Examples.CodeGeneration.VS2013.Commerce.Model.Subscriptions.SubscriptionStatusType StatusType { get; set; }
		public virtual byte StatusTypeId { get; set; }
		public virtual DateTime NextShipmentDate { get; set; }
		public virtual DateTime NextChargeDate { get; set; }
		public virtual bool AutoRenew { get; set; }
		public virtual Zirpl.Examples.CodeGeneration.VS2013.Commerce.Model.Customers.Customer Customer { get; set; }
		public virtual int CustomerId { get; set; }
		public virtual Zirpl.Examples.CodeGeneration.VS2013.Commerce.Model.Address ShippingAddress { get; set; }
		public virtual int ShippingAddressId { get; set; }
		public virtual Zirpl.Examples.CodeGeneration.VS2013.Commerce.Model.Customers.CustomerChargeOption CustomerChargeOption { get; set; }
		public virtual int CustomerChargeOptionId { get; set; }
		public virtual Zirpl.Examples.CodeGeneration.VS2013.Commerce.Model.Subscriptions.SubscriptionInstance CurrentSubscriptionInstance { get; set; }
		public virtual int? CurrentSubscriptionInstanceId { get; set; }


		public virtual IList<SubscriptionCustomFieldValue> CustomFieldValues { get; set; }
        public virtual IList<ICustomFieldValue> GetCustomFieldValues()
        {
            return this.CustomFieldValues.OfType<ICustomFieldValue>().ToList();
        }	
		public virtual void SetCustomFieldValues(IList<ICustomFieldValue> list)
		{
		    this.CustomFieldValues = list.OfType<SubscriptionCustomFieldValue>().ToList();
		}
    }
}

